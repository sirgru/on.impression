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

	struct ParserState {
		public TheParser   parser;
		public TheLexer    lexer;
		public ParserErrorListener    errorListener;

		public ParserState(string input) {
			var inputStream = new AntlrInputStream(input);
			lexer = new TheLexer(inputStream);
			var commonTokenStream = new CommonTokenStream(lexer);
			parser = new TheParser(commonTokenStream);

			StringWriter writer = new StringWriter();
			errorListener = new ParserErrorListener(writer);
			lexer.RemoveErrorListeners();
			parser.RemoveErrorListeners();
			parser.AddErrorListener(errorListener);
		}
	}

	[TestFixture]
	public class ParserTests {
		[Test]
		public void InvalidLiteral() {
			var ps = new ParserState(" 'abc");
			var context = ps.parser.start();

			var offendingSymbol = ps.errorListener.lastError.token;
			Assert.AreEqual(1, offendingSymbol.Column);
			Assert.AreEqual("'", offendingSymbol.Text);
		}
	}

	[TestFixture]
	public class SemanticTests {
		[Test]
		public void EmptyLiteral() {
			var ps = new ParserState(" '' ");
			var context = ps.parser.start();
			var visitor = new Visitor();
			visitor.Visit(context);

			var error = visitor.semanticErrorListener.lastError;
			Assert.AreEqual("''", error.text);
			Assert.AreEqual(1, error.charPositionInLine);
		}
	}
}
