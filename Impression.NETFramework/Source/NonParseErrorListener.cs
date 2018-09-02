using Antlr4.Runtime;
using System.Collections.Generic;

namespace ES.ON.Impression {
	// TODO: Consider restructuring
	public class NonParseErrorListener {
		public class ErrorData {
			public IToken token { get; private set; }

			public int		line				{ get; private set; }
			public int		charPositionInLine	{ get; private set; }
			public string	text				{ get; private set; }
			public string	message				{ get; private set; }
			public bool		isSemantic			{ get; private set; }

			public ErrorData(IToken token, string message, bool isSemantic) {
				this.token = token;
				this.line = token.Line;
				this.charPositionInLine = token.StartIndex;
				this.text = token.Text;
				this.message = message;
				this.isSemantic = isSemantic;
			}

			public ErrorData(int line, int charPositionInLine, string text, string message) {
				this.line = line;
				this.charPositionInLine = charPositionInLine;
				this.text = text;
				this.message = message;
			}
		}
		public List<ErrorData> errors { get; private set; } = new List<ErrorData>();
		public ErrorData lastError {
			get { return errors.Count > 0 ? errors[errors.Count - 1] : null; }
		}

		public void Clear() {
			errors.Clear();
		}

		public void AddError(ErrorData errorData) {
			errors.Add(errorData);
		}
	}

}
