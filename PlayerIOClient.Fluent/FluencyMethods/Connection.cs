using System;

namespace PlayerIOClient.Fluent {
	public static class FluentConnectionFluency {
		public static FluentConnectionWrapper On(this FluentConnectionWrapper m, string type, Action<FluentConnectionWrapper, Message> callback) {
			m.AttatchOnMessage(type, callback); return m;
		}

		public static FluentConnectionWrapper OnDisconnect(this FluentConnectionWrapper m, DisconnectCallback callback) {
			m.AttatchOnDisconnect(callback); return m;
		}

		public static FluentConnectionWrapper Send(this FluentConnectionWrapper m, string type, params object[] args) {
			m.SendMessage(type, args); return m;
		}

		public static FluentConnectionWrapper Send(this FluentConnectionWrapper m, Message msg) {
			m.SendMessage(msg); return m;
		}

		public static FluentConnectionWrapper Disconnect(this FluentConnectionWrapper m) {
			m.EndConnection(); return m;
		}

		public static FluentConnectionWrapper Reconnect(this FluentConnectionWrapper m) {
			m.ResetConnection(m.ConnectionArguments); return m;
		}

		public static FluentConnectionWrapper Reconnect(this FluentConnectionWrapper m, ConnectionArguments cargs) {
			m.ResetConnection(cargs); return m;
		}

		public static FluentConnectionWrapper BeforeSend(this FluentConnectionWrapper m, BeforeSend callback) {
			m.AttatchBeforeSend(callback); return m;
		}

		public static FluentConnectionWrapper AfterSend(this FluentConnectionWrapper m, AfterSend callback) {
			m.AttatchAfterSend(callback); return m;
		}
	}
}
