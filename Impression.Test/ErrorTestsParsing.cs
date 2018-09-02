using NUnit.Framework;

namespace Impression.Test {
	[TestFixture]
	public class ErrorTestsParsing {
		
		[TestCase("not: not: [abc]")]
		public void ParsingError(string input) {
			var ps = new ParserState(input);
			var context = ps.parser.expressionSeq();

			Assert.AreNotEqual(null, ps.errorListener.lastError);	
		}
	}
}
