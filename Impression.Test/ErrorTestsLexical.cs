using ES.ON.Impression;
using NUnit.Framework;

namespace Impression.Test {
	[TestFixture]
	public class ErrorTestsLexical {
		[TestCase(" [abc")]
		[TestCase(" 'abc")]
		[TestCase("a..")]
		[TestCase("[ab] as ")]
		public void LexicalError(string input) {
			var ps = new ParserState(input);
			var context = ps.parser.expressionSeq();
			var visitor = new Visitor();
			visitor.TryVisit(context);

			Assert.AreNotEqual(null, visitor.errorListener.lastError);
		}
	}
}
