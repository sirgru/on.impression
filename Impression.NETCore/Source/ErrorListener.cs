using Antlr4.Runtime;
using System.Collections.Generic;

namespace ES.ON.Impression {
	public class ErrorListener : BaseErrorListener {

		private CommonTokenStream _commonTokenStream;
		private string[] _lines;

		public ErrorListener(CommonTokenStream commonTokenStream) {
			_commonTokenStream = commonTokenStream;
		}

		public class ErrorData {
			public IToken tokenStart { get; private set; }
			public IToken tokenStop { get; private set; }
			public string message { get; private set; }
			public ErrorType type { get; private set; }

			public ErrorData(IToken tokenStart, IToken tokenStop, string message, ErrorType type) {
				this.tokenStart = tokenStart;
				this.tokenStop = tokenStop;
				this.message = message;
				this.type = type;
			}

			public string Print(string[] lines) {
				if(tokenStart.Line != tokenStop.Line) return "Error in multiple lines: from " + tokenStart.Line + " to " + tokenStop.Line + ".";
				var cleanedLine = lines[tokenStart.Line - 1].Replace('\t', ' ');
				var result = "";
				var length = (tokenStop.StopIndex - tokenStart.StartIndex + 1).ToString();
				result += type.ToString() + " Error at line: " + tokenStart.Line + " position: " + tokenStart.StartIndex + " length: " + length + "\n";
				if(tokenStart.StartIndex >= 0 && tokenStop.StopIndex >= 0) {
					result += cleanedLine + '\n';
					for(int i = 0; i < tokenStart.StartIndex; i++) result += ' ';
					for(int i = tokenStart.StartIndex; i < tokenStop.StopIndex + 1; i++) result += '^';
					result += '\n';
				}
				result += "Message: " + message + "\n";
				return result;
			}
		}
		public List<ErrorData> errors { get; private set; } = new List<ErrorData>();

		public ErrorData lastError {
			get { return errors.Count > 0 ? errors[errors.Count - 1] : null; }
		}

		public bool hadErrors => errors.Count > 0;

		public void Reset() {
			errors.Clear();
		}

		public string PrintErrors() {
			string result = "";
			if(_lines == null) GenerateLines();
			if(_lines.Length == 0) return "No input.";

			if(errors.Count > 1) {
				int i=0;
				foreach(var error in errors) {
					result += "Error [" + i + "]:\n";
					result += error.Print(_lines);
					result += "-------\n";
					i++;
				}
			} else result = errors[0].Print(_lines);
			return result;
		}
		public void GenerateLines() {
			string fileText = _commonTokenStream.TokenSource.InputStream.ToString();
			_lines = fileText.Split('\n');
		}

		public override void SyntaxError(IRecognizer recognizer, IToken offendingToken, int line, int charPositionInLine, string message, RecognitionException e) {
			errors.Add(new ErrorData(offendingToken, offendingToken, message, ErrorType.Parsing));
		}

		public void AddLexicalError(IToken tokenStart, IToken tokenStop, string message) {
			errors.Add(new ErrorData(tokenStart, tokenStop, message, ErrorType.Lexical));
		}

		public void AddSemanticError(IToken tokenStart, IToken tokenStop, string message) {
			errors.Add(new ErrorData(tokenStart, tokenStop, message, ErrorType.Semantic));
		}
	}

	public enum ErrorType {
		Lexical, Parsing, Semantic
	}
}
