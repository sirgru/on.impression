using NUnit.Framework;
using ES.ON.Impression;

namespace Impression.Test {

	[TestFixture]
	public class ErrorTestsSemantic {
		[Test]
		public void EmptyLiteral() {
			var ps = new ParserState(" '' ");
			var context = ps.parser.expressionSeq();
			var visitor = new Visitor();
			visitor.TryVisit(context);

			var error = visitor.nonParsingErrorListener.lastError;
			Assert.AreEqual("''", error.text);
			Assert.AreEqual(1, error.charPositionInLine);
		}

		[TestCase("  [] ", ExpectedResult = 2)]
		[TestCase(" not: [] ", ExpectedResult = 6)]
		public int EmptySet(string input) {
			var ps = new ParserState(input);
			var context = ps.parser.expressionSeq();
			var visitor = new Visitor();
			visitor.TryVisit(context);

			var error = visitor.nonParsingErrorListener.lastError;
			Assert.AreEqual("[]", error.text);
			return error.charPositionInLine;
		}
	}
}
