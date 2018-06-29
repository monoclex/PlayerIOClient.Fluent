namespace PlayerIOClient.Fluent {
	public static class FluentClientFluency {
		public static FluentClientWrapper ConnectUserId(this FluentClientWrapper c, out string connectUserId) {
			connectUserId = c.ConnectUserId; return c;
		}

		public static FluentClientWrapper Set(this FluentClientWrapper c, string index, object value) {
			c[index] = value; return c;
		}

		public static FluentClientWrapper Get(this FluentClientWrapper c, string index, out object value) {
			value = c[index]; return c;
		}
	}
}
