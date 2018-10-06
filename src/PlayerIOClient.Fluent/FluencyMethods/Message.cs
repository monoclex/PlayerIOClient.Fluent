namespace PlayerIOClient.Fluent {
	public static class FluentMessageFluency {
		/// <summary>Adds a single, or multiple arguments to the message</summary>
		/// <param name="msg">The message</param>
		/// <param name="args">The parameter(s)</param>
		/// <returns>Message</returns>
		public static Message AddArg(this Message msg, params object[] args) {
			msg.Add(args); return msg;
		}

		/// <summary>Returns the type of the message</summary>
		/// <param name="type">Set to the type of the message.</param>
		/// <returns>Message</returns>
		public static Message Type(this Message msg, out string type) {
			type = msg.Type; return msg;
		}

		/// <summary>Returns the count of the message</summary>
		/// <param name="count">Set to the count of the message.</param>
		/// <returns>Message</returns>
		public static Message Count(this Message msg, out uint count) {
			count = msg.Count; return msg;
		}

		/// <summary>Sets the value to whatever boolean is stored at the index of the message.</summary>
		/// <param name="index">The position the value is at</param>
		/// <param name="value">Set to the whatever value is at the position requested</param>
		/// <returns>Message</returns>
		public static Message Get(this Message msg, uint index, out bool value) {
			value = msg.GetBoolean(index); return msg;
		}

		/// <summary>Sets the value to whatever byte array is stored at the index of the message.</summary>
		/// <param name="index">The position the value is at</param>
		/// <param name="value">Set to the whatever value is at the position requested</param>
		/// <returns>Message</returns>
		public static Message Get(this Message msg, uint index, out byte[] value) {
			value = msg.GetByteArray(index); return msg;
		}

		/// <summary>Sets the value to whatever double is stored at the index of the message.</summary>
		/// <param name="index">The position the value is at</param>
		/// <param name="value">Set to the whatever value is at the position requested</param>
		/// <returns>Message</returns>
		public static Message Get(this Message msg, uint index, out double value) {
			value = msg.GetDouble(index); return msg;
		}

		/// <summary>Sets the value to whatever float is stored at the index of the message.</summary>
		/// <param name="index">The position the value is at</param>
		/// <param name="value">Set to the whatever value is at the position requested</param>
		/// <returns>Message</returns>
		public static Message Get(this Message msg, uint index, out float value) {
			value = msg.GetFloat(index); return msg;
		}

		/// <summary>Sets the value to whatever int is stored at the index of the message.</summary>
		/// <param name="index">The position the value is at</param>
		/// <param name="value">Set to the whatever value is at the position requested</param>
		/// <returns>Message</returns>
		public static Message Get(this Message msg, uint index, out int value) {
			value = msg.GetInt(index); return msg;
		}

		/// <summary>Sets the value to whatever long is stored at the index of the message.</summary>
		/// <param name="index">The position the value is at</param>
		/// <param name="value">Set to the whatever value is at the position requested</param>
		/// <returns>Message</returns>
		public static Message Get(this Message msg, uint index, out long value) {
			value = msg.GetLong(index); return msg;
		}

		/// <summary>Sets the value to whatever string is stored at the index of the message.</summary>
		/// <param name="index">The position the value is at</param>
		/// <param name="value">Set to the whatever value is at the position requested</param>
		/// <returns>Message</returns>
		public static Message Get(this Message msg, uint index, out string value) {
			value = msg.GetString(index); return msg;
		}

		/// <summary>Sets the value to whatever uint is stored at the index of the message.</summary>
		/// <param name="index">The position the value is at</param>
		/// <param name="value">Set to the whatever value is at the position requested</param>
		/// <returns>Message</returns>
		public static Message Get(this Message msg, uint index, out uint value) {
			value = msg.GetUInt(index); return msg;
		}

		/// <summary>Sets the value to whatever ulong is stored at the index of the message.</summary>
		/// <param name="index">The position the value is at</param>
		/// <param name="value">Set to the whatever value is at the position requested</param>
		/// <returns>Message</returns>
		public static Message Get(this Message msg, uint index, out ulong value) {
			value = msg.GetULong(index); return msg;
		}

		public static Message Modify(this Message msg, uint index, object value) {
			var msgCopy = Message.Create(msg.Type);

			for (var i = 0u; i < msg.Count; i++)
				if (i == index)
					msgCopy.AddArg(value);
				else msgCopy.AddArg(msg[i]);

			msg = msgCopy;

			return msgCopy;
		}
	}
}
