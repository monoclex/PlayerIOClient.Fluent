using System.Collections.Generic;

namespace PlayerIOClient.Fluent {
	public class ConnectionArguments {
		internal ConnectionArguments() { }

		public ConnectionArguments(string roomId, string roomType, bool visible, Dictionary<string, string> roomData, Dictionary<string, string> joinData) {
			this.Type = ConnectType.CreateJoinRoom;

			this.RoomId = roomId;
			this.RoomType = roomType;
			this.Visible = visible;
			this.RoomData = roomData;
			this.JoinData = joinData;
		}

		public ConnectionArguments(string roomId, Dictionary<string, string> joinData) {
			this.Type = ConnectType.JoinRoom;

			this.RoomId = roomId;
			this.JoinData = joinData;
		}

		public ConnectType Type { get; internal set; }

		public string RoomId { get; internal set; }
		public string RoomType { get; internal set; }
		public bool Visible { get; internal set; }
		public Dictionary<string, string> RoomData { get; internal set; }
		public Dictionary<string, string> JoinData { get; internal set; }

		public Connection Join(Multiplayer m)
			=> (this.Type == ConnectType.CreateJoinRoom ?
				m.CreateJoinRoom(this.RoomId, this.RoomType, this.Visible, this.RoomData, this.JoinData)
				: m.JoinRoom(this.RoomId, this.JoinData)
			);
	}

	public enum ConnectType {
		CreateJoinRoom,
		JoinRoom
	}
}
