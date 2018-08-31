using Antlr4.Runtime;
using Impression.NETFramework.Grammar;

namespace ES.ON.Impression {
	public class ImpressionToRegex {
		public static string Convert(string input) {
			var inputStream = new AntlrInputStream(input);
			var lexer = new TheLexer(inputStream);
			var commonTokenStream = new CommonTokenStream(lexer);
			var parser = new TheParser(commonTokenStream);

			var context = parser.start();
			var visitor = new Visitor();
			var result = visitor.TryVisit(context);

			if(result == null) System.Console.WriteLine(visitor.semanticErrorListener.lastError.message);

			return result;
		}
	}
}
