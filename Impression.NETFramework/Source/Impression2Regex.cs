using Antlr4.Runtime;
using Impression.NETFramework.Grammar;

namespace ES.ON.Impression {
	public class ImpressionToRegex {
		public static string Convert(string input) {
			AntlrInputStream inputStream = new AntlrInputStream(input);
			TheLexer lexer = new TheLexer(inputStream);
			CommonTokenStream commonTokenStream = new CommonTokenStream(lexer);
			TheParser parser = new TheParser(commonTokenStream);

			TheParser.StartContext expressionContext = parser.start();
			Visitor visitor = new Visitor();

			return visitor.Visit(expressionContext);
		}
	}
}
