using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using Impression.NETFramework.Grammar;

namespace ES.ON.Impression {
	public class Visitor : TheParserBaseVisitor<string> {
		public SemanticErrorListener semanticErrorListener { get; private set; } = new SemanticErrorListener();

		public string TryVisit([NotNull] IParseTree tree) {
			try {
				return Visit(tree);
			} catch(SemanticErrorException) {
				return null;
			}
		}

		public override string VisitStart([NotNull] TheParser.StartContext context) {
			return SequentiallyAggregateFromChildren(context);
		}

		string SequentiallyAggregateFromChildren(IParseTree context, string currentResult = "") {
			for(int i = 0; i < context.ChildCount; i++) {
				currentResult += Visit(context.GetChild(i));
			}
			return currentResult;
		}

		public override string VisitLiteralWithContent([NotNull] TheParser.LiteralWithContentContext context) {
			var enclosedContent = context.GetText();
			var content = enclosedContent.Substring(1, enclosedContent.Length - 2);

			content = ConvertSpecialCharacters(content);

			content = content.Replace(@".", @"\u002E");
			content = content.Replace(@"$", @"\u0024");
			content = content.Replace(@"^", @"\u005E");
			content = content.Replace(@"{", @"\u007B");
			content = content.Replace(@"}", @"\u007D");
			content = content.Replace(@"(", @"\u0028");
			content = content.Replace(@")", @"\u0029");
			content = content.Replace(@"[", @"\u005B");
			content = content.Replace(@"]", @"\u005D");
			content = content.Replace(@"|", @"\u007C");
			content = content.Replace(@"*", @"\u002A");
			content = content.Replace(@"?", @"\u003F");
			content = content.Replace(@"+", @"\u002B");
			return content;
		}

		string ConvertSpecialCharacters(string content) {
			content = content.Replace(@"\b", @"\u0008");
			content = content.Replace(@"\\", @"\u005C");
			content = content.Replace(@"-", @"\u002D");
			return content;
		}

		public override string VisitEmptyLiteral([NotNull] TheParser.EmptyLiteralContext context) {
			var token = context.EMPTY_LITERAL().Symbol;
			semanticErrorListener.AddSemanticError(new SemanticErrorListener.ErrorData(token, "Empty literals aren't allowed."));
			throw new SemanticErrorException();
		}

		public override string VisitEmptySet([NotNull] TheParser.EmptySetContext context) {
			var token = context.EMPTY_SET().Symbol;
			semanticErrorListener.AddSemanticError(new SemanticErrorListener.ErrorData(token, "Empty sets aren't allowed."));
			throw new SemanticErrorException();
		}

		public override string VisitSetWithContent([NotNull] TheParser.SetWithContentContext context) {
			var enclosedContent = context.GetText();
			var content = enclosedContent.Substring(1, enclosedContent.Length - 2);

			content = ConvertSpecialCharacters(content);
			content = content.Replace(@"\]", @"\u005D");
			return "[" + content + "]";
		}

		public override string VisitSetNegative([NotNull] TheParser.SetNegativeContext context) {
			var positivePart = VisitSet(context.set());

			return "[^" + positivePart.Substring(1, positivePart.Length - 1);
		}

		public override string VisitRangeSet([NotNull] TheParser.RangeSetContext context) {
			var first = ConvertSpecialCharactersForRange(context.CHAR(0).GetText());
			var second = ConvertSpecialCharactersForRange(context.CHAR(1).GetText());

			return "[" + first + "-" + second + "]";
		}

		string ConvertSpecialCharactersForRange(string content) {
			content = content.Replace(@"\b", @"\u0008");
			content = content.Replace(@"-", @"\u002D");
			content = content.Replace(@"]", @"\u005D");
			return content;
		}

		public override string VisitCombinationSet([NotNull] TheParser.CombinationSetContext context) {
			var first = VisitSet(context.set(0));
			var second = VisitSet(context.set(1));
			return first.Substring(0, first.Length - 1) + second.Substring(1, second.Length - 1);
		}
		
		public override string VisitSet([NotNull] TheParser.SetContext context) {
			switch(context) {
				case TheParser.SetWithContentContext swcc: return VisitSetWithContent(swcc);
				case TheParser.EmptySetContext esc: return VisitEmptySet(esc);
				case TheParser.RangeSetContext rsc: return VisitRangeSet(rsc);
				case TheParser.CombinationSetContext csc: return VisitCombinationSet(csc);
				default: throw new System.InvalidOperationException("Unhandled set type.");
			}
		}
	}
}
