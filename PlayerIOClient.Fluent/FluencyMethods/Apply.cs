namespace PlayerIOClient.Fluent {
	public static class FluentApplyFluency {
		public static FluentClientWrapper ApplyFluency(this Client client)
			=> new FluentClientWrapper(client);
	}
}
