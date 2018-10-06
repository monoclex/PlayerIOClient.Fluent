using System;

namespace PlayerIOClient.Fluent {
	public class FluentBigDBWrapper : IChild<FluentClientWrapper> {
		public FluentBigDBWrapper(BigDB db) => this._db = db;
		public FluentBigDBWrapper(FluentClientWrapper fcw) {
			this._fcw = fcw ?? throw new ArgumentNullException(nameof(fcw));
			this._db = this._fcw.Client.BigDB;
		}

		private FluentClientWrapper _fcw;
		private BigDB _db;

		public FluentClientWrapper Parent => this._fcw ?? throw new Exception("No parent found! Did you try to apply fluency to a bigdb?");
		public BigDB BigDB => this._db;
	}
}
