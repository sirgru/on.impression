using NUnit.Framework;

namespace Impression.Test {
	[TestFixture]
	public class ParserTests {
		[TestCase(" [abc")]
		[TestCase(" 'abc")]
		[TestCase("not not [abc]")]
		[TestCase("a..")]
		[TestCase("[ab] as ")]
		public void InvalidSet(string input) {
			var ps = new ParserState(input);
			var context = ps.parser.expressionSeq();

			Assert.AreNotEqual(null, ps.errorListener.lastError);	
		}
	}
}
