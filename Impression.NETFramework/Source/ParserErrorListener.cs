using Antlr4.Runtime;
using System.Collections.Generic;
using System.IO;

namespace ES.ON.Impression {
	public class ParserErrorListener : BaseErrorListener {
		public StringWriter writer { get; private set; }

		public ParserErrorListener(StringWriter writer) {
			this.writer = writer;
		}

		// TODO: Consider Restructuring
		public class ErrorData {
			public IToken token { get; private set; }
			public int line { get; private set; }
			public int charPositionInLine { get; private set; }
			public string message { get; private set; }

			public ErrorData (IToken symbol, int line, int charPositionInLine, string message) {
				this.token = symbol;
			}
		}
		public List<ErrorData> errors { get; private set; } = new List<ErrorData>();

		public ErrorData lastError {
			get { return errors.Count > 0 ? errors[errors.Count - 1] : null; }
		}

		public void Reset() {
			errors.Clear();
		}

		public override void SyntaxError(IRecognizer recognizer, IToken offendingToken, int line, int charPositionInLine, string message, RecognitionException e) {
			errors.Add(new ErrorData(offendingToken, line, charPositionInLine, message));
			writer.WriteLine(message);
		}
	}
}
