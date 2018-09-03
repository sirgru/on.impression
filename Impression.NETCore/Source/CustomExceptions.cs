using System;
using System.Runtime.Serialization;

namespace ES.ON.Impression {
	public class SemanticErrorException : Exception {
		public SemanticErrorException() : base() { }

		public SemanticErrorException(string message) : base(message) { }

		public SemanticErrorException(String message, Exception innerException) : base(message, innerException) { }

		protected SemanticErrorException(SerializationInfo info, StreamingContext context) : base(info, context) { }
	}

	public class LexerErrorException : Exception {
		public LexerErrorException() : base() { }

		public LexerErrorException(string message) : base(message) { }

		public LexerErrorException(String message, Exception innerException) : base(message, innerException) { }

		protected LexerErrorException(SerializationInfo info, StreamingContext context) : base(info, context) { }
	}
}
