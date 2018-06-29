using System;

namespace PlayerIOClient.Fluent.Testing {
	class Program {
		static void Main(string[] args) {
			var bot = PlayerIO.QuickConnect.SimpleConnect("everybody-edits-su9rn58o40itdbnw69plyw", "email", "password", null)
				.ApplyFluency() //make the client fluent
				
				.BigDB
					.Load("config", "config", out var dbo)
					.Previous()

				.ConnectUserId(out var connectUserId)

				.Multiplayer
					.CreateJoinRoom(connectUserId + "_aAaA", "Lobby" + dbo["version"], false, null, null)
						.OnDisconnect((c, i, e) => {
							if (i) {
								Console.WriteLine($"Disconnected for no reason from the lobby: {e}");
								c.Reconnect();
							}
						})

						.On("groupdisallowedjoin", (c, e) => {
							Console.WriteLine("Can't join!");
							c.Disconnect();
						})

						.On("connectioncomplete", (c, e) => {
							Console.WriteLine("Connected to the lobby!");
							c.Disconnect();
						})

						.On("*", (c, e) => {
							Console.WriteLine("Recieved message: " + e.ToString());
						})

						.Send("connectioncomplete")
						.Previous()

					.CreateJoinRoom("PW", "Everybodyedits" + dbo["version"], false, null, null)
						.OnDisconnect((c, i, e) => {
							if (!i) {
								Console.WriteLine($"Disconnected for no reason: {e}");
								c.Reconnect();
							}
						})

						.On("init2", (c, e) => {
							c.Send("say", "hellolo!");
						})

						.On("init", (c, e) => {
							c.Send("init2");
						})

						.On("say", (c, e) => {
							e
								.Get(0, out int id)
								.Get(1, out string msg);

							c
								.Send("say", $"Hey {id}! You said {msg}.");
						})

						.Send("init");

			Console.ReadLine();
		}
	}
}