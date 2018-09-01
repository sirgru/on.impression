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

		[Test]
		public void AnchorsTests() {
			Assert.AreEqual(@"^$\A(?=\s*\z)\z\G", ImpressionToRegex.Convert(@"start end head tail-not-ws tail last-match"));
		}

		[Test]
		public void BasicParenExpr() {
			Assert.AreEqual(@"[a-z]", ImpressionToRegex.Convert(@"(a..z)"));
		}

		[Test]
		public void BasicNaming() {
			Assert.AreEqual(@"(?<var>[a-z])", ImpressionToRegex.Convert(@"a..z as var"));
		}

		[Test]
		public void BasicParenNaming() {
			Assert.AreEqual(@"(?<var>[a-z])", ImpressionToRegex.Convert(@"(a..z) as var"));
		}

		[Test]
		public void BasicReNaming() {
			Assert.AreEqual(@"(?<var1-var2>[a-z])", ImpressionToRegex.Convert(@"a..z as var1:var2"));
		}

		[Test]
		public void BasicParenReNaming() {
			Assert.AreEqual(@"(?<var1-var2>[a-z])", ImpressionToRegex.Convert(@"(a..z) as var1:var2"));
		}

		[Test]
		public void BasicGrouping() {
			Assert.AreEqual(@"(?i:a)", ImpressionToRegex.Convert(@"i 'a'"));
			Assert.AreEqual(@"(?=a)", ImpressionToRegex.Convert(@"before 'a'"));
			Assert.AreEqual(@"(?!a)", ImpressionToRegex.Convert(@"not-before 'a'"));
			Assert.AreEqual(@"(?<=a)", ImpressionToRegex.Convert(@"after 'a'"));
			Assert.AreEqual(@"(?<!a)", ImpressionToRegex.Convert(@"not-after 'a'"));
			Assert.AreEqual(@"(?>a)", ImpressionToRegex.Convert(@"atomic 'a'"));
		}

		[Test]
		public void BasicQuantifiers() {
			Assert.AreEqual(@"(ab)*", ImpressionToRegex.Convert(@"'ab' x 0.."));
			Assert.AreEqual(@"(ab)+", ImpressionToRegex.Convert(@"'ab' x 1.."));
			Assert.AreEqual(@"(ab)?", ImpressionToRegex.Convert(@"'ab' x 0..1"));
			Assert.AreEqual(@"(ab){3}", ImpressionToRegex.Convert(@"'ab' x 3"));
			Assert.AreEqual(@"(ab){3,}", ImpressionToRegex.Convert(@"'ab' x 3.."));
			Assert.AreEqual(@"(ab)*?", ImpressionToRegex.Convert(@"'ab' x ..0"));
			Assert.AreEqual(@"(ab)+?", ImpressionToRegex.Convert(@"'ab' x ..1"));
			Assert.AreEqual(@"(ab)??", ImpressionToRegex.Convert(@"'ab' x 1..0"));
			Assert.AreEqual(@"(ab){3,}?", ImpressionToRegex.Convert(@"'ab' x ..3"));
			Assert.AreEqual(@"(ab){3,5}", ImpressionToRegex.Convert(@"'ab' x 3..5"));
			Assert.AreEqual(@"(ab){3,5}?", ImpressionToRegex.Convert(@"'ab' x 5..3"));
		}

		[Test]
		public void AlternationTest() {
			Assert.AreEqual(@"th(e|is|at)", ImpressionToRegex.Convert(@"'th'('e'|'is'|'at')"));
		}

		[Test]
		public void ConditionTest1() {
			Assert.AreEqual(@"(?(a)ab|bc)", ImpressionToRegex.Convert(@"if 'a' then 'ab' else 'bc'"));
		}
		[Test]
		public void ConditionTest2() {
			Assert.AreEqual(@"(?(var)ab|bc)", ImpressionToRegex.Convert(@"if $var then 'ab' else 'bc'"));
		}

		[Test]
		public void Additions1Tests() {
			Assert.AreEqual(@"\r?\n", ImpressionToRegex.Convert(@"nl"));
			Assert.AreEqual(@"\w+", ImpressionToRegex.Convert(@"word"));
			Assert.AreEqual(@"\d+", ImpressionToRegex.Convert(@"int"));
			Assert.AreEqual(@"\s+", ImpressionToRegex.Convert(@"whitespace"));
			Assert.AreEqual(@"[^\r\n]", ImpressionToRegex.Convert(@"c"));
			Assert.AreEqual(@".", ImpressionToRegex.Convert(@"."));
			Assert.AreEqual(@"((?<=\W)(?=\w)|^(?=\w))", ImpressionToRegex.Convert(@"bw"));
			Assert.AreEqual(@"((?<=\w)(?=\W)|(?=\w)$)", ImpressionToRegex.Convert(@"ew"));
		}

		[Test]
		public void Additions2Tests() {
			Assert.AreEqual(@"(a)*", ImpressionToRegex.Convert(@"'a' any-greedy"));
			Assert.AreEqual(@"(a)*?", ImpressionToRegex.Convert(@"'a' any"));
			Assert.AreEqual(@"(a)+", ImpressionToRegex.Convert(@"'a' all-greedy"));
			Assert.AreEqual(@"(a)+?", ImpressionToRegex.Convert(@"'a' all"));
			Assert.AreEqual(@"(a)?", ImpressionToRegex.Convert(@"'a' maybe"));
		}

		[Test]
		public void BasicSubstitutionKeywordText() {
			Assert.AreEqual(@"${var}", ImpressionToRegex.Convert(@"${var}"));
			Assert.AreEqual(@"$&", ImpressionToRegex.Convert(@"match"));
			Assert.AreEqual(@"$`", ImpressionToRegex.Convert(@"before-match"));
			Assert.AreEqual(@"$'", ImpressionToRegex.Convert(@"after-match"));
			Assert.AreEqual(@"$_", ImpressionToRegex.Convert(@"input"));
		}
	}
}
