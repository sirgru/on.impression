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
		public void EscapedLiteral() {
			Assert.AreEqual(@"ab\u0027c\u005C", ImpressionToRegex.Convert(@"'ab\'c\\'"));
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
		public void BasicSubtractionSet() {
			Assert.AreEqual(@"[a-z-[p]]", ImpressionToRegex.Convert(@"a..z - [p]"));
		}

		[Test]
		public void AdvancedSet() {
			Assert.AreEqual(@"[a-zA-Z123]", ImpressionToRegex.Convert(@"a..z + A..Z + [123]"));
		}

		[Test]
		public void AdvancedSubtraction() {
			Assert.AreEqual(@"[a-z0-9-[p23]]", ImpressionToRegex.Convert(@"a..z + 0..9 - [p] + [23]"));
		}

		[Test]
		public void BasicType() {
			Assert.AreEqual(@"\p{Lu}", ImpressionToRegex.Convert(@"type Lu"));
		}

		[Test]
		public void BasicNotType() {
			Assert.AreEqual(@"\P{Lu}", ImpressionToRegex.Convert(@"not-type		  Lu"));
		}

		[Test]
		public void ShortsTests() {
			Assert.AreEqual(@"\w\s\d\b", ImpressionToRegex.Convert(@"w ws d wb"));
		}

		[Test]
		public void NotShortsTests() {
			Assert.AreEqual(@"\W\S\D\B", ImpressionToRegex.Convert(@"not w not ws not d not wb"));
		}
	}
}
