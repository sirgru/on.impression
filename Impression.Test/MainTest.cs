using NUnit.Framework;
using ES.ON.Impression;
using Impression.NETFramework.Grammar;
using Antlr4.Runtime;
using System.IO;

namespace Impression.Test {
	[TestFixture]
	public class FunctionalTests {
		[Test]
		public void BasicLiteralsAndComments() {
			Assert.AreEqual(@"abc ", ImpressionToRegex.Convert(@"'abc' /* comment */ ' ' "));
		}

		[Test]
		public void AdvancedLiteralsAndComments() {
			Assert.AreEqual(@"abc /\u002A /\u002A \u002A/\u0007", ImpressionToRegex.Convert(@"'abc' ' /*' /* comment */ ' /* */' '\u0007'"));
		}
	}

	[TestFixture]
	public class ParserTests {
		struct ParserState {
			public TheParser   parser;
			public TheLexer    lexer;
			public ErrorListener    errorListener;

			public ParserState(string input) {
				var inputStream = new AntlrInputStream(input);
				lexer = new TheLexer(inputStream);
				var commonTokenStream = new CommonTokenStream(lexer);
				parser = new TheParser(commonTokenStream);

				StringWriter writer = new StringWriter();
				errorListener = new ErrorListener(writer);
				lexer.RemoveErrorListeners();
				parser.RemoveErrorListeners();
				parser.AddErrorListener(errorListener);
			}
		}
		
	}
}
