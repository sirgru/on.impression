using NUnit.Framework;
using ES.ON.Impression;

namespace Impression.Test {

	[TestFixture]
	public class ErrorTestsSemantic {
		[Test]
		public void EmptyLiteral() {
			var ps = new ParserState(" '' ");
			var context = ps.parser.expressionSeq();
			var visitor = new Visitor(ps.errorListener);
			visitor.Visit(context);

			var error = visitor.errorListener.lastError;
			Assert.AreEqual("''", error.tokenStart.Text);
			Assert.AreEqual(1, error.tokenStart.StartIndex);
		}

		[TestCase("  [] ", ExpectedResult = 2)]
		[TestCase(" ! [] ", ExpectedResult = 3)]
		public int EmptySet(string input) {
			var ps = new ParserState(input);
			var context = ps.parser.expressionSeq();
			var visitor = new Visitor(ps.errorListener);
			visitor.Visit(context);

			var error = visitor.errorListener.lastError;
			Assert.AreEqual("[]", error.tokenStart.Text);
			return error.tokenStart.StartIndex;
		}
	}
}
