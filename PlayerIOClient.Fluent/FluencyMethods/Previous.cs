namespace PlayerIOClient.Fluent {
	public static class PreviousFluency {
		public static T Previous<T>(this IChild<T> p)
			=> p.Parent;
	}
}
