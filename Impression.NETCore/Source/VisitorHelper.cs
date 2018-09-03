namespace ES.ON.Impression {
	public class VisitorHelper {
		// Avoiding tuples
		public struct TwoNumbers {
			public int number1;
			public int number2;
		}
		public struct ResultAndIndex {
			public int result;
			public int index;
		}
		public static TwoNumbers ExtractTwoNumbers(char[] charArray) {
			// Avoiding LINQ
			int index = 0;
			int number1 = int.MaxValue, number2 = int.MaxValue;
			while(charArray[index] == 'x' || charArray[index] == ' ' || charArray[index] == '\t') index++;
			// 12 or 12.. or 12..34
			if(char.IsDigit(charArray[index])) {
				var result1 = GetNumber(charArray, index);
				number1 = result1.result;
				index = result1.index;
				// 12
				if(index >= charArray.Length) return new TwoNumbers() { number1 = number1, number2 = number1 };
				while(charArray[index] != '.') index++;
				// 12.. or 12 ..
				index += 2;
				do if(index >= charArray.Length) return new TwoNumbers() { number1 = number1, number2 = int.MaxValue };
				while(!char.IsDigit(charArray[index++]));

				number2 = GetNumber(charArray, index - 1).result;
			}
			// ..34
			else {
				number1 = int.MaxValue;
				while(!char.IsDigit(charArray[index])) index++;
				number2 = GetNumber(charArray, index).result;
			}
			return new TwoNumbers() { number1 = number1, number2 = number2 };
		}
		static ResultAndIndex GetNumber(char[] charArray, int index) {
			string resultStr = "";
			while(index < charArray.Length && char.IsDigit(charArray[index])) resultStr += charArray[index++];
			int result = int.Parse(resultStr);
			return new ResultAndIndex() { result = result, index = index };
		}
	}
}
