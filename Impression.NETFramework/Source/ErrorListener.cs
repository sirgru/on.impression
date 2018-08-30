using Antlr4.Runtime;
using System;
using System.IO;

namespace ES.ON.Impression {
	public class ErrorListener : BaseErrorListener {
		public String symbol { get; private set; }
		public StringWriter writer { get; private set; }

		public ErrorListener(StringWriter writer) {
			this.writer = writer;
		}

		public override void SyntaxError(IRecognizer recognizer, IToken offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e) {
			writer.WriteLine(msg);
			symbol = offendingSymbol.Text;
		}
	}
}
