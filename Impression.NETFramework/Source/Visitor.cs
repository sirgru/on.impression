using Antlr4.Runtime.Misc;
using Impression.NETFramework.Grammar;

namespace ES.ON.Impression {
	class Visitor : TheParserBaseVisitor<string> {
		public override string VisitStart([NotNull] TheParser.StartContext context) {
			return "";
		}
	}
}
