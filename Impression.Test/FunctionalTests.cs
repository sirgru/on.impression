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

		[Test]
		public void BasicRangeSet() {
			Assert.AreEqual(@"[a-z]", ImpressionToRegex.Convert(@"a..z"));
		}

		[Test]
		public void BasicCombinationSet() {
			Assert.AreEqual(@"[az]", ImpressionToRegex.Convert(@"[a] + [z]"));
		}

		[Test]
		public void AdvancedSet() {
			Assert.AreEqual(@"[a-zA-Z123]", ImpressionToRegex.Convert(@"a..z + A..Z + [123]"));
		}

		[Test]
		public void BasicType() {
			Assert.AreEqual(@"\p{Lu}", ImpressionToRegex.Convert(@"type Lu"));
		}

		[Test]
		public void BasicNotType() {
			Assert.AreEqual(@"\P{Lu}", ImpressionToRegex.Convert(@"not-type		  Lu"));
		}
	}
}
