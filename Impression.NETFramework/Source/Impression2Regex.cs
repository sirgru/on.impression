using Antlr4.Runtime;
using Impression.NETFramework.Grammar;
using System;

namespace ES.ON.Impression {
	public class ImpressionToRegex {

		/// <summary>
		/// Converts the supplied string to a Regular Expression String, enclosed in required modifiers.
		/// </summary>
		/// <param name="input">A string in Impression format.</param>
		/// <returns></returns>
		public static string Convert(string input) {
			return @"(?mns:" + ConvertNoOptions(input) +")";
		}

		/// <summary>
		/// Will convert the input to a Regular Expression String, but will not enclose it with reuired options. Used for testing purposes.
		/// </summary>
		/// <param name="input">A string in Impression format.</param>
		/// <returns></returns>
		public static string ConvertNoOptions(string input) {
			var inputStream = new AntlrInputStream(input);
			var lexer = new TheLexer(inputStream);
			var commonTokenStream = new CommonTokenStream(lexer);
			var parser = new TheParser(commonTokenStream);

			var errorListener = new ErrorListener();
			lexer.RemoveErrorListeners();
			parser.RemoveErrorListeners();
			parser.AddErrorListener(errorListener);

			var context = parser.expressionSeq();
			var visitor = new Visitor(errorListener);
			var result = visitor.TryVisit(context);

			if(visitor.errorListener.hadErrors) throw new InvalidOperationException(visitor.errorListener.PrintErrors());

			return result;
		}
	}
}
