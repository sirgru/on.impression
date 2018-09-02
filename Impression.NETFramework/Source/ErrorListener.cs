using Antlr4.Runtime;
using System.Collections.Generic;

namespace ES.ON.Impression {
	public class ErrorListener : BaseErrorListener {

		public class ErrorData {
			public IToken token { get; private set; }
			public string message { get; private set; }
			public ErrorType type { get; private set; }

			public ErrorData (IToken token, string message, ErrorType type) {
				this.token = token;
				this.message = message;
				this.type = type;
			}

			public string Print() {
				return type.ToString() + " Error at line: " + token.Line + " position: " + token.StartIndex + "\nMessage: " + message + "\n";
			}
		}
		public List<ErrorData> errors { get; private set; } = new List<ErrorData>();

		public ErrorData lastError {
			get { return errors.Count > 0 ? errors[errors.Count - 1] : null; }
		}

		public bool hadErrors {
			get {
				return errors.Count > 0;
			}
		}

		public void Reset() {
			errors.Clear();
		}

		public string PrintErrors() {
			string result = "";
			if(errors.Count > 1) {
				int i=0;
				foreach(var error in errors) {
					result += "Error [" + i + "]:\n";
					result += error.Print();
					result += "-------";
					i++;
				}
			} else result = errors[0].Print();
			return result;
		}

		public override void SyntaxError(IRecognizer recognizer, IToken offendingToken, int line, int charPositionInLine, string message, RecognitionException e) {
			errors.Add(new ErrorData(offendingToken, message, ErrorType.Parsing));
		}

		public void AddLexicalError(IToken token, string message) {
			errors.Add(new ErrorData(token, message, ErrorType.Lexical));
		}

		public void AddSemanticError(IToken token, string message) {
			errors.Add(new ErrorData(token, message, ErrorType.Semantic));
		}
	}

	public enum ErrorType {
		Lexical, Parsing, Semantic
	}
}
