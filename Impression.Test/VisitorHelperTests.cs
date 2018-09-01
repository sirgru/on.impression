using ES.ON.Impression;
using NUnit.Framework;

namespace Impression.Test {
	[TestFixture]
	public class VisitorHelperTests {
		[TestCase("x 12", 12, 12)]
		[TestCase("x12..34", 12, 34)]
		[TestCase("x 12	.. 34", 12, 34)]
		[TestCase("x 12	..	", 12, int.MaxValue)]
		[TestCase("x		..		34", int.MaxValue, 34)]
		public void ValidateNumbersExtractionFromQuantifiers(string input, int n1, int n2) {
			var result = VisitorHelper.ExtractTwoNumbers(input.ToCharArray());
			Assert.AreEqual(n1, result.number1);
			Assert.AreEqual(n2, result.number2);
		}
	}
}
