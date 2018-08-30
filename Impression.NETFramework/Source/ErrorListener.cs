using Antlr4.Runtime;
using System.Collections.Generic;
using System.IO;

namespace ES.ON.Impression {
	public class ErrorListener : BaseErrorListener {
		public StringWriter writer { get; private set; }

		public ErrorListener(StringWriter writer) {
			this.writer = writer;
		}

		public class ErrorData {
			public IToken symbol { get; private set; }
			public int line { get; private set; }
			public int charPositionInLine { get; private set; }
			public string message { get; private set; }

			public ErrorData (IToken symbol, int line, int charPositionInLine, string message) {
				this.symbol = symbol;
			}
		}
		public List<ErrorData> errors { get; private set; } = new List<ErrorData>();

		public ErrorData lastError {
			get {
				return errors[errors.Count - 1];
			}
		}

		public void Reset() {
			errors.Clear();
		}

		public override void SyntaxError(IRecognizer recognizer, IToken offendingToken, int line, int charPositionInLine, string message, RecognitionException e) {
			errors.Add(new ErrorData(offendingToken, line, charPositionInLine, message));
			writer.WriteLine(message);
		}

		public void SemanticError(IToken offendingSymbol, int line, int charPositionInLine, string message) {
			errors.Add(new ErrorData(offendingSymbol, line, charPositionInLine, message));
			writer.WriteLine(message);
		}
	}
}
