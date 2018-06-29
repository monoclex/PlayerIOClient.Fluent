using System;
using System.Collections.Generic;

namespace PlayerIOClient.Fluent {
	public class FluentClientWrapper {
		public FluentClientWrapper(Client cli) => this._client = cli ?? throw new ArgumentNullException(nameof(cli));

		private Dictionary<string, object> _internalStorage;
		private Client _client;

		public Client Client => this._client;
		public FluentBigDBWrapper BigDB => new FluentBigDBWrapper(this);
		public FluentMultiplayerWrapper Multiplayer => new FluentMultiplayerWrapper(this);

		public string ConnectUserId => Client.ConnectUserId;

		public object this[string key] {
			get => this._internalStorage.TryGetValue(key, out var res) ? res : null;
			set => this._internalStorage[key] = value;
		}
	}
}
