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

			var error = visitor.errorListener.lastError;
			Assert.AreEqual("''", error.token.Text);
			Assert.AreEqual(1, error.token.StartIndex);
		}

		[TestCase("  [] ", ExpectedResult = 2)]
		[TestCase(" not: [] ", ExpectedResult = 6)]
		public int EmptySet(string input) {
			var ps = new ParserState(input);
			var context = ps.parser.expressionSeq();
			var visitor = new Visitor();
			visitor.TryVisit(context);

			var error = visitor.errorListener.lastError;
			Assert.AreEqual("[]", error.token.Text);
			return error.token.StartIndex;
		}
	}
}
