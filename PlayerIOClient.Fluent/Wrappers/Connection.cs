using System;
using System.Collections.Generic;

namespace PlayerIOClient.Fluent {
	public class FluentConnectionWrapper : IChild<FluentMultiplayerWrapper> {
		public FluentConnectionWrapper(FluentMultiplayerWrapper fmw, Connection con, ConnectionArguments cargs) {
			this._messageCallbacks = new Dictionary<string, List<Action<FluentConnectionWrapper, Message>>>();
			this._disconnectCallbacks = new List<Action<FluentConnectionWrapper, bool, string>>();

			this._discon = false;

			this._cargs = cargs;
			this._fmw = fmw;
			this._con = con;
			AddHooks();
		}

		private Dictionary<string, List<Action<FluentConnectionWrapper, Message>>> _messageCallbacks;
		private List<Action<FluentConnectionWrapper, bool, string>> _disconnectCallbacks;
		private ConnectionArguments _cargs;
		private Connection _con;
		private FluentMultiplayerWrapper _fmw;
		private bool _discon; // if we're disconnecting on purpose

		public FluentMultiplayerWrapper Parent => this._fmw ?? throw new Exception("No parent found! Did you try to apply fluency to a connection?");
		public ConnectionArguments ConnectionArguments => this._cargs;
		public Connection Connection => this._con;
		
		public bool Connected => this._con.Connected;
		public string ConnectUserId => this.Parent.Parent.ConnectUserId;

		public void AttatchOnMessage(string type, Action<FluentConnectionWrapper, Message> callback) {
			if (this._messageCallbacks.TryGetValue(type, out var callbacks))
				callbacks.Add(callback);
			else this._messageCallbacks[type] = new List<Action<FluentConnectionWrapper, Message>> { callback };
		}

		public void AttatchOnDisconnect(Action<FluentConnectionWrapper, bool, string> callback)
			=> this._disconnectCallbacks.Add(callback);

		public void SendMessage(string type, params object[] args)
			=> this._con.Send(type ?? throw new ArgumentNullException(nameof(type)), args ?? throw new ArgumentNullException(nameof(args)));

		public void SendMessage(Message msg)
			=> this._con.Send(msg ?? throw new ArgumentException(nameof(msg)));

		public void EndConnection() {
			this._discon = true;
			if(this._con != null)
				this._con.Disconnect();
			this._discon = false;
		}

		public void ResetConnection(ConnectionArguments cargs) {
			this.EndConnection();
			
			this._con = cargs.Join(this.Parent.Multiplayer);
			AddHooks();
		}

		internal void AddHooks() {
			this._con.OnMessage += (sender, e) => {
				if (this._messageCallbacks.TryGetValue("*", out var allCallbacks))
					foreach (var i in allCallbacks)
						i(this, e);

				if (this._messageCallbacks.TryGetValue(e.Type, out var msgCallbacks))
					foreach (var i in msgCallbacks)
						i(this, e);
			};

			this._con.OnDisconnect += (sender, e) => {
				foreach (var i in this._disconnectCallbacks)
					i(this, !this._discon, e);
			};
		}
	}
}
