using ES.ON.Impression;
using Impression.NETFramework.Grammar;
using Antlr4.Runtime;

namespace Impression.Test {
	struct ParserState {
		public TheParser		parser;
		public TheLexer			lexer;
		public ErrorListener    errorListener;

		public ParserState(string input) {
			var inputStream = new AntlrInputStream(input);
			lexer = new TheLexer(inputStream);
			var commonTokenStream = new CommonTokenStream(lexer);
			parser = new TheParser(commonTokenStream);

			errorListener = new ErrorListener(commonTokenStream);
			lexer.RemoveErrorListeners();
			parser.RemoveErrorListeners();
			parser.AddErrorListener(errorListener);
		}
	}
}
