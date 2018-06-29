using System.Collections.Generic;

namespace PlayerIOClient.Fluent {
	public static class FluentMultiplayerFluency {
		public static FluentMultiplayerWrapper ApplyFluency(this Multiplayer m)
			=> new FluentMultiplayerWrapper(m);

		public static FluentConnectionWrapper CreateJoinRoom(this FluentMultiplayerWrapper m, string roomId, string roomType, bool visible, Dictionary<string, string> roomData, Dictionary<string, string> joinData)
			=> new FluentConnectionWrapper(m, m.Multiplayer.CreateJoinRoom(roomId, roomType, visible, roomData, joinData), new ConnectionArguments(roomId, roomType, visible, roomData, joinData));

		public static FluentConnectionWrapper JoinRoom(this FluentMultiplayerWrapper m, string roomId, Dictionary<string, string> joinData)
			=> new FluentConnectionWrapper(m, m.Multiplayer.JoinRoom(roomId, joinData), new ConnectionArguments(roomId, joinData));
	}
}
