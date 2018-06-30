﻿using System;
using System.Collections.Generic;

namespace PlayerIOClient.Fluent {
	public class FluentConnectionWrapper : IChild<FluentMultiplayerWrapper> {
		public FluentConnectionWrapper(FluentMultiplayerWrapper fmw, Connection con, ConnectionArguments cargs) {
			this._fmw = fmw;
			Constructor(con, cargs);
		}

		public FluentConnectionWrapper(Connection con, ConnectionArguments cargs) {
			this._fmw = null;
			Constructor(con, cargs);
		}

		private void Constructor(Connection con, ConnectionArguments cargs) {
			this._messageCallbacks = new Dictionary<string, List<Action<FluentConnectionWrapper, Message>>>();
			this._disconnectCallbacks = new List<Action<FluentConnectionWrapper, DisconnectionType, string>>();
			this._beforeSend = new List<Func<FluentConnectionWrapper, Message, Message>>();
			this._afterSend = new List<Action<FluentConnectionWrapper, Message>>();

			this._discon = DisconnectionType.Unexplained;

			this._cargs = cargs;
			this._con = con;
			AddHooks();
		}

		private Dictionary<string, List<Action<FluentConnectionWrapper, Message>>> _messageCallbacks;
		private List<Action<FluentConnectionWrapper, DisconnectionType, string>> _disconnectCallbacks;
		private List<Func<FluentConnectionWrapper, Message, Message>> _beforeSend;
		private List<Action<FluentConnectionWrapper, Message>> _afterSend;
		private ConnectionArguments _cargs;
		private Connection _con;
		private FluentMultiplayerWrapper _fmw;
		private DisconnectionType _discon; // if we're disconnecting on purpose

		public FluentMultiplayerWrapper Parent => this._fmw ?? throw new Exception("No parent found! Did you try to apply fluency to a connection?");
		public ConnectionArguments ConnectionArguments => this._cargs;
		public Connection Connection => this._con;
		
		public bool Connected => this._con.Connected;
		public string ConnectUserId => this.Parent.Parent.ConnectUserId;

		public void AttatchOnMessage(string type, Action<FluentConnectionWrapper, Message> callback) {
			if (this._messageCallbacks.TryGetValue(type, out var callbacks))
				callbacks.Add(callback ?? throw new ArgumentNullException(nameof(callback)));
			else this._messageCallbacks[type] = new List<Action<FluentConnectionWrapper, Message>> { callback ?? throw new ArgumentNullException(nameof(callback)) };
		}

		public void AttatchOnDisconnect(Action<FluentConnectionWrapper, DisconnectionType, string> callback)
			=> this._disconnectCallbacks.Add(callback ?? throw new ArgumentNullException(nameof(callback)));

		public void AttatchBeforeSend(Func<FluentConnectionWrapper, Message, Message> callback)
			=> this._beforeSend.Add(callback ?? throw new ArgumentNullException(nameof(callback)));

		public void AttatchAfterSend(Action<FluentConnectionWrapper, Message> callback)
			=> this._afterSend.Add(callback ?? throw new ArgumentNullException(nameof(callback)));

		public void SendMessage(string type, params object[] args)
			=> this.SendMessage(Message.Create(type ?? throw new ArgumentNullException(nameof(type)), args ?? throw new ArgumentNullException(nameof(args))));

		public void SendMessage(Message msg) {
			if (this._beforeSend.Count > 0)
				foreach (var i in this._beforeSend)
					msg = i(this, msg ?? throw new ArgumentException(nameof(msg)));

			this._con.Send(msg ?? throw new ArgumentException(nameof(msg)));

			if (this._afterSend.Count > 0)
				foreach (var i in this._afterSend)
					i(this, msg);
		}

		public void EndConnection() {
			if (this._discon == DisconnectionType.Unexplained) //if it's unexplained, set it to 'disconnect'
				this._discon = DisconnectionType.Disconnect;

			if (this._con != null)
				this._con.Disconnect();

			this._discon = DisconnectionType.Unexplained; //set it back to 'unexplained'
		}

		public void ResetConnection(ConnectionArguments cargs) {
			this._discon = DisconnectionType.Reconnect; //set the disconnection type
			this.EndConnection();
			
			this._con = cargs.Join(this.Parent.Multiplayer);
			AddHooks();
		}

		internal void AddHooks() {
			this._con.OnMessage += (sender, e) => {
				if (this._messageCallbacks.TryGetValue("*", out var allCallbacks))
					foreach (var i in allCallbacks)
						i(this, e);

				bool foundCallback = this._messageCallbacks.TryGetValue(e.Type, out var msgCallbacks);

				if (foundCallback)
					foreach (var i in msgCallbacks)
						i(this, e);
				else if (this._messageCallbacks.TryGetValue("#", out var unhandledCallbacks))
					foreach (var i in unhandledCallbacks)
						i(this, e);
			};

			this._con.OnDisconnect += (sender, e) => {
				foreach (var i in this._disconnectCallbacks)
					i(this, this._discon, e);
			};
		}
	}

	public enum DisconnectionType {
		/// <summary>The connection was disconnected on purpose - the .Disconnect() function was called</summary>
		Disconnect,

		/// <summary>The connection was disconnected on purpose - the .Reconnect() function was called</summary
		Reconnect,

		/// <summary>PlayerIO must've kicked us from the server, as we do not know why.</summary>
		Unexplained
	}
}
