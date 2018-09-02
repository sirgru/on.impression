using NUnit.Framework;
using ES.ON.Impression;

namespace Impression.Test {
	[TestFixture]
	public class FunctionalTests {
		[Test]
		public void BasicLiteralsAndComments() {
			Assert.AreEqual(@"abc ", ImpressionToRegex.ConvertNoOptions(@"'abc' /* comment */ ' ' "));
		}

		[Test]
		public void EscapedLiteral() {
			Assert.AreEqual(@"ab\u0027c\u005C", ImpressionToRegex.ConvertNoOptions(@"'ab\'c\\'"));
		}

		[Test]
		public void AdvancedLiteralsAndComments() {
			Assert.AreEqual(@"abc /\u002A /\u002A \u002A/\u0007", ImpressionToRegex.ConvertNoOptions(@"'abc' ' /*' /* comment */ ' /* */' '\u0007'"));
		}

		[Test]
		public void BasicSets() {
			Assert.AreEqual(@"[ ab*]", ImpressionToRegex.ConvertNoOptions(@" [ ab*]"));
		}

		[Test]
		public void BasicNegativeSets() {
			Assert.AreEqual(@"[^ab*]", ImpressionToRegex.ConvertNoOptions(@" not: [ab*]"));
		}

		[Test]
		public void BasicRangeSet() {
			Assert.AreEqual(@"[a-z]", ImpressionToRegex.ConvertNoOptions(@"a..z"));
		}

		[Test]
		public void BasicCombinationSet() {
			Assert.AreEqual(@"[az]", ImpressionToRegex.ConvertNoOptions(@"[a] + [z]"));
		}

		[Test]
		public void BasicSubtractionSet() {
			Assert.AreEqual(@"[a-z-[p]]", ImpressionToRegex.ConvertNoOptions(@"a..z - [p]"));
		}

		[Test]
		public void AdvancedSet() {
			Assert.AreEqual(@"[a-zA-Z123]", ImpressionToRegex.ConvertNoOptions(@"a..z + A..Z + [123]"));
		}

		[Test]
		public void AdvancedSubtraction() {
			Assert.AreEqual(@"[a-z0-9-[p23]]", ImpressionToRegex.ConvertNoOptions(@"a..z + 0..9 - [p] + [23]"));
		}

		[Test]
		public void BasicType() {
			Assert.AreEqual(@"\p{Lu}", ImpressionToRegex.ConvertNoOptions(@"type: Lu"));
		}

		[Test]
		public void BasicNotType() {
			Assert.AreEqual(@"\P{Lu}", ImpressionToRegex.ConvertNoOptions(@"not-type:		  Lu"));
		}

		[Test]
		public void ShortsTests() {
			Assert.AreEqual(@"\w\s\d\b", ImpressionToRegex.ConvertNoOptions(@"w ws d wb"));
		}

		[Test]
		public void NotShortsTests() {
			Assert.AreEqual(@"\W\S\D\B", ImpressionToRegex.ConvertNoOptions(@"not: w not: ws not: d not: wb"));
		}

		[Test]
		public void AnchorsTests() {
			Assert.AreEqual(@"^$\A(?=\s*\z)\z\G", ImpressionToRegex.ConvertNoOptions(@"start end head tail-after-ws tail last-match"));
		}

		[Test]
		public void BasicParenExpr() {
			Assert.AreEqual(@"[a-z]", ImpressionToRegex.ConvertNoOptions(@"(a..z)"));
		}

		[Test]
		public void BasicNaming() {
			Assert.AreEqual(@"(?<var>[a-z])", ImpressionToRegex.ConvertNoOptions(@"a..z as var"));
		}

		[Test]
		public void BasicParenNaming() {
			Assert.AreEqual(@"(?<var>[a-z])", ImpressionToRegex.ConvertNoOptions(@"(a..z) as var"));
		}

		[Test]
		public void BasicReNaming() {
			Assert.AreEqual(@"(?<var1-var2>[a-z])", ImpressionToRegex.ConvertNoOptions(@"a..z as var1:var2"));
		}

		[Test]
		public void BasicParenReNaming() {
			Assert.AreEqual(@"(?<var1-var2>[a-z])", ImpressionToRegex.ConvertNoOptions(@"(a..z) as var1:var2"));
		}

		[Test]
		public void BasicGrouping() {
			Assert.AreEqual(@"(?i:a)", ImpressionToRegex.ConvertNoOptions(@"i: 'a'"));
			Assert.AreEqual(@"(?=a)", ImpressionToRegex.ConvertNoOptions(@"before: 'a'"));
			Assert.AreEqual(@"(?!a)", ImpressionToRegex.ConvertNoOptions(@"not-before: 'a'"));
			Assert.AreEqual(@"(?<=a)", ImpressionToRegex.ConvertNoOptions(@"after: 'a'"));
			Assert.AreEqual(@"(?<!a)", ImpressionToRegex.ConvertNoOptions(@"not-after: 'a'"));
			Assert.AreEqual(@"(?>a)", ImpressionToRegex.ConvertNoOptions(@"atomic: 'a'"));
		}

		[Test]
		public void BasicQuantifiers() {
			Assert.AreEqual(@"(ab)*", ImpressionToRegex.ConvertNoOptions(@"'ab' x 0.."));
			Assert.AreEqual(@"(ab)+", ImpressionToRegex.ConvertNoOptions(@"'ab' x 1.."));
			Assert.AreEqual(@"(ab)?", ImpressionToRegex.ConvertNoOptions(@"'ab' x 0..1"));
			Assert.AreEqual(@"(ab){3}", ImpressionToRegex.ConvertNoOptions(@"'ab' x 3"));
			Assert.AreEqual(@"(ab){3,}", ImpressionToRegex.ConvertNoOptions(@"'ab' x 3.."));
			Assert.AreEqual(@"(ab)*?", ImpressionToRegex.ConvertNoOptions(@"'ab' x ..0"));
			Assert.AreEqual(@"(ab)+?", ImpressionToRegex.ConvertNoOptions(@"'ab' x ..1"));
			Assert.AreEqual(@"(ab)??", ImpressionToRegex.ConvertNoOptions(@"'ab' x 1..0"));
			Assert.AreEqual(@"(ab){3,}?", ImpressionToRegex.ConvertNoOptions(@"'ab' x ..3"));
			Assert.AreEqual(@"(ab){3,5}", ImpressionToRegex.ConvertNoOptions(@"'ab' x 3..5"));
			Assert.AreEqual(@"(ab){3,5}?", ImpressionToRegex.ConvertNoOptions(@"'ab' x 5..3"));
		}

		[Test]
		public void AlternationTest() {
			Assert.AreEqual(@"th(e|is|at)", ImpressionToRegex.ConvertNoOptions(@"'th''e'|'is'|'at' "));
		}
		[Test]
		public void EnclosedAlternationTest() {
			Assert.AreEqual(@"th(e|is|at)", ImpressionToRegex.ConvertNoOptions(@"'th'('e'|'is'|'at')"));
		}

		[Test]
		public void ConditionTest1() {
			Assert.AreEqual(@"(?(a)ab|bc)", ImpressionToRegex.ConvertNoOptions(@"if ('a') 'ab' else 'bc'"));
		}
		[Test]
		public void ConditionTest2() {
			Assert.AreEqual(@"(?(var)ab|bc)", ImpressionToRegex.ConvertNoOptions(@"if $var 'ab' else 'bc'"));
		}

		[Test]
		public void Additions1Tests() {
			Assert.AreEqual(@"\r?\n", ImpressionToRegex.ConvertNoOptions(@"nl"));
			Assert.AreEqual(@"\w+", ImpressionToRegex.ConvertNoOptions(@"word"));
			Assert.AreEqual(@"\d+", ImpressionToRegex.ConvertNoOptions(@"int"));
			Assert.AreEqual(@"\s+", ImpressionToRegex.ConvertNoOptions(@"whitespace"));
			Assert.AreEqual(@"[^\r\n]", ImpressionToRegex.ConvertNoOptions(@"c"));
			Assert.AreEqual(@".", ImpressionToRegex.ConvertNoOptions(@"a"));
			Assert.AreEqual(@"((?<=\W)(?=\w)|^(?=\w))", ImpressionToRegex.ConvertNoOptions(@"bw"));
			Assert.AreEqual(@"((?<=\w)(?=\W)|(?=\w)$)", ImpressionToRegex.ConvertNoOptions(@"ew"));
		}

		[Test]
		public void Additions2Tests() {
			Assert.AreEqual(@"(a)*", ImpressionToRegex.ConvertNoOptions(@"'a' :any"));
			Assert.AreEqual(@"(a)*?", ImpressionToRegex.ConvertNoOptions(@"'a' :any-lazy"));
			Assert.AreEqual(@"(a)+", ImpressionToRegex.ConvertNoOptions(@"'a' :all"));
			Assert.AreEqual(@"(a)+?", ImpressionToRegex.ConvertNoOptions(@"'a' :all-lazy"));
			Assert.AreEqual(@"(a)?", ImpressionToRegex.ConvertNoOptions(@"'a' :maybe"));
			Assert.AreEqual(@"(a)??", ImpressionToRegex.ConvertNoOptions(@"'a' :maybe-lazy"));
		}

		[Test]
		public void BasicSubstitutionKeywordText() {
			Assert.AreEqual(@"${var}", ImpressionToRegex.ConvertNoOptions(@"${var}"));
			Assert.AreEqual(@"$&", ImpressionToRegex.ConvertNoOptions(@"match"));
			Assert.AreEqual(@"$`", ImpressionToRegex.ConvertNoOptions(@"before-match"));
			Assert.AreEqual(@"$'", ImpressionToRegex.ConvertNoOptions(@"after-match"));
			Assert.AreEqual(@"$_", ImpressionToRegex.ConvertNoOptions(@"input"));
		}

		[Test]
		public void BasicNamedBackreference() {
			Assert.AreEqual(@"(?<a>.)\k<a>", ImpressionToRegex.ConvertNoOptions(@"a as a $a"));
		}

		[Test]
		public void SeparationByComma() {
			Assert.AreEqual(@"ab", ImpressionToRegex.ConvertNoOptions(@"'a' , 'b'"));
		}
	}
}
