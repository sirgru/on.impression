using NUnit.Framework;

namespace Impression.Test {
	[TestFixture]
	public class ParserTests {
		[TestCase(" [abc")]
		[TestCase(" 'abc")]
		public void InvalidSet(string input) {
			var ps = new ParserState(input);
			var context = ps.parser.start();

			Assert.AreNotEqual(null, ps.errorListener.lastError);	
		}
	}
}
