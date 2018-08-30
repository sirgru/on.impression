using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using Impression.NETFramework.Grammar;

namespace ES.ON.Impression {
	public class Visitor : TheParserBaseVisitor<string> {
		public SemanticErrorListener semanticErrorListener { get; private set; } = new SemanticErrorListener();

		string SequentiallyAggregateFromChildren(IParseTree context, string currentResult = "") {
			for(int i = 0; i < context.ChildCount; i++) {
				currentResult += Visit(context.GetChild(i));
			}
			return currentResult;
		}

		public override string VisitStart([NotNull] TheParser.StartContext context) {
			return SequentiallyAggregateFromChildren(context);
		}

		public override string VisitLiteralWithContent([NotNull] TheParser.LiteralWithContentContext context) {
			var quotedContent = context.GetText();
			var content = quotedContent.Substring(1, quotedContent.Length - 2);

			content = content.Replace(@"\b", @"\u0008");
			content = content.Replace(@"\\", @"\u005C");
			content = content.Replace(@".", @"\u002E");
			content = content.Replace(@"$", @"\u0024");
			content = content.Replace(@"^", @"\u005E");
			content = content.Replace(@"{", @"\u007B");
			content = content.Replace(@"}", @"\u007D");
			content = content.Replace(@"[", @"\u005B");
			content = content.Replace(@"]", @"\u005D");
			content = content.Replace(@"(", @"\u0028");
			content = content.Replace(@")", @"\u0029");
			content = content.Replace(@"|", @"\u007C");
			content = content.Replace(@"*", @"\u002A");
			content = content.Replace(@"+", @"\u002B");
			content = content.Replace(@"?", @"\u003F");
			content = content.Replace(@"-", @"\u002D");

			return content;
		}

		public override string VisitEmptyLiteral([NotNull] TheParser.EmptyLiteralContext context) {
			var token = context.EMPTY_LITERAL().Symbol;
			semanticErrorListener.AddSemanticError(new SemanticErrorListener.ErrorData(token, "Empty literals aren't allowed."));
			return "";
		}
	}
}
