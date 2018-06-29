using System;

namespace PlayerIOClient.Fluent {
	public class FluentMultiplayerWrapper : IChild<FluentClientWrapper> {
		public FluentMultiplayerWrapper(Multiplayer m) => this._m = m;
		public FluentMultiplayerWrapper(FluentClientWrapper fcw) {
			this._fcw = fcw ?? throw new ArgumentNullException(nameof(fcw));
			this._m = this._fcw.Client.Multiplayer;
		}

		private FluentClientWrapper _fcw;
		private Multiplayer _m;

		public FluentClientWrapper Parent => this._fcw ?? throw new Exception("No parent found! Did you try to apply fluency to a multiplayer?");
		public Multiplayer Multiplayer => this._m;
	}
}
