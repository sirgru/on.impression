using Antlr4.Runtime;
using Impression.NETFramework.Grammar;
using System;
using System.IO;

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

			//StringWriter writer = new StringWriter();
			//var errorListener = new ParserErrorListener(writer);
			//lexer.RemoveErrorListeners();
			//parser.RemoveErrorListeners();
			//parser.AddErrorListener(errorListener);

			var context = parser.expressionSeq();
			var visitor = new Visitor();
			var result = visitor.TryVisit(context);

			//if(errorListener.lastError != null) throw new InvalidOperationException("Parsing Error: " + errorListener.lastError.message);
			if(visitor.nonParsingErrorListener.lastError != null) throw new InvalidOperationException("Semantic Error: " + visitor.nonParsingErrorListener.lastError.message);

			return result;
		}
	}
}
