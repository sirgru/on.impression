using NUnit.Framework;
using ES.ON.Impression;

namespace Impression.Test {

	[TestFixture]
	public class SemanticTests {
		[Test]
		public void EmptyLiteral() {
			var ps = new ParserState(" '' ");
			var context = ps.parser.start();
			var visitor = new Visitor();
			try {
				visitor.Visit(context);
			} catch {}

			var error = visitor.semanticErrorListener.lastError;
			Assert.AreEqual("''", error.text);
			Assert.AreEqual(1, error.charPositionInLine);
		}

		[TestCase("  [] ", ExpectedResult = 2)]
		[TestCase(" not [] ", ExpectedResult = 5)]
		public int EmptySet(string input) {
			var ps = new ParserState(input);
			var context = ps.parser.start();
			var visitor = new Visitor();
			try {
				visitor.Visit(context);
			} catch { }

			var error = visitor.semanticErrorListener.lastError;
			Assert.AreEqual("[]", error.text);
			return error.charPositionInLine;
		}
	}
}
