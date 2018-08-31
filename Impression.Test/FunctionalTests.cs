using NUnit.Framework;
using ES.ON.Impression;

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

		[Test]
		public void BasicSets() {
			Assert.AreEqual(@"[ ab*]", ImpressionToRegex.Convert(@" [ ab*]"));
		}

		[Test]
		public void BasicNegativeSets() {
			Assert.AreEqual(@"[^ab*]", ImpressionToRegex.Convert(@" not [ab*]"));
		}
	}
}
