using NUnit.Framework;
using ES.ON.Impression;

namespace Impression.Test {
	[TestFixture]
	public class MainTest {
		[Test]
		public void Test() {
			Assert.AreEqual("", ImpressionToRegex.Convert("a"));
		}
	}
}
