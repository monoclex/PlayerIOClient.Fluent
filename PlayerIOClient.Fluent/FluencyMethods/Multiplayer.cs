using System.Collections.Generic;

namespace PlayerIOClient.Fluent {
	public static class FluentMultiplayerFluency {
		public static FluentConnectionWrapper CreateJoinRoom(this FluentMultiplayerWrapper m, string roomId, string roomType, bool visible, Dictionary<string, string> roomData, Dictionary<string, string> joinData)
			=> new FluentConnectionWrapper(m, m.Multiplayer.CreateJoinRoom(roomId, roomType, visible, roomData, joinData), new ConnectionArguments(roomId, roomType, visible, roomData, joinData));

		public static FluentConnectionWrapper JoinRoom(this FluentMultiplayerWrapper m, string roomId, Dictionary<string, string> joinData)
			=> new FluentConnectionWrapper(m, m.Multiplayer.JoinRoom(roomId, joinData), new ConnectionArguments(roomId, joinData));

		public static FluentMultiplayerWrapper ListRooms(this FluentMultiplayerWrapper m, string roomType, Dictionary<string, string> searchCriteria, int resultLimit, int resultOffset, out RoomInfo[] roomInfo) {
			roomInfo = m.Multiplayer.ListRooms(roomType, searchCriteria, resultLimit, resultOffset); return m;
		}
	}
}
