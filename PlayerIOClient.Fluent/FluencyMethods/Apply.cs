namespace PlayerIOClient.Fluent {
	public static class FluentApplyFluency {
		public static FluentClientWrapper ApplyFluency(this Client client)
			=> new FluentClientWrapper(client);

		public static FluentBigDBWrapper ApplyFluency(this BigDB db)
			=> new FluentBigDBWrapper(db);

		public static FluentMultiplayerWrapper ApplyFluency(this Multiplayer m)
			=> new FluentMultiplayerWrapper(m);

		public static FluentConnectionWrapper ApplyFluency(this Connection m, ConnectionArguments cargs)
			=> new FluentConnectionWrapper(m, cargs);
	}
}
