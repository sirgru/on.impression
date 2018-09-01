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

			content = content.Replace(@"\'", @"\u0027");
			content = ConvertSpecialCharacters(content);

			content = content.Replace(@".", @"\u002E");
			content = content.Replace(@"$", @"\u0024");
			content = content.Replace(@"^", @"\u005E");
			content = content.Replace(@"{", @"\u007B");
			content = content.Replace(@"}", @"\u007D");
			content = content.Replace(@"(", @"\u0028");
			content = content.Replace(@")", @"\u0029");
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
			content = content.Replace(@"[", @"\u005B");
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

			content = content.Replace(@"\]", @"\u005D");
			content = ConvertSpecialCharacters(content);
			return "[" + content + "]";
		}

		public override string VisitSetNegative([NotNull] TheParser.SetNegativeContext context) {
			var positivePart = Visit(context.set());

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
			content = content.Replace(@"[", @"\u005B");
			content = content.Replace(@"]", @"\u005D");
			return content;
		}

		public override string VisitCombinationSet([NotNull] TheParser.CombinationSetContext context) {
			var first = Visit(context.set(0));
			var second = Visit(context.set(1));
			return first.Substring(0, first.Length - 1) + second.Substring(1, second.Length - 1);
		}

		string GetContentFromKeywordToken(ParserRuleContext context) {
			var text = context.GetText();
			var split = text.Split(' ', '\t');
			return split[split.Length - 1];
		}

		public override string VisitCharType([NotNull] TheParser.CharTypeContext context) {
			return @"\p{" + GetContentFromKeywordToken(context) + "}";
		}

		public override string VisitNotCharType([NotNull] TheParser.NotCharTypeContext context) {
			return @"\P{" + GetContentFromKeywordToken(context) + "}";
		}

		public override string VisitSubtractionSet([NotNull] TheParser.SubtractionSetContext context) {
			var first = Visit(context.set(0));
			var second = Visit(context.set(1));

			return first.Substring(0, first.Length - 1) + "-" + second + "]";
		}

		public override string VisitWordChar([NotNull] TheParser.WordCharContext context) {
			return @"\w";
		}
		public override string VisitWhiteSpace([NotNull] TheParser.WhiteSpaceContext context) {
			return @"\s";
		}
		public override string VisitDigit([NotNull] TheParser.DigitContext context) {
			return @"\d";
		}
		public override string VisitWordBoundary([NotNull] TheParser.WordBoundaryContext context) {
			return @"\b";
		}

		public override string VisitNotWord([NotNull] TheParser.NotWordContext context) {
			return @"\W";
		}
		public override string VisitNotWhiteSpace([NotNull] TheParser.NotWhiteSpaceContext context) {
			return @"\S";
		}
		public override string VisitNotDigit([NotNull] TheParser.NotDigitContext context) {
			return @"\D";
		}
		public override string VisitNotWordBoundary([NotNull] TheParser.NotWordBoundaryContext context) {
			return @"\B";
		}

		public override string VisitStartLine([NotNull] TheParser.StartLineContext context) {
			return @"^";
		}
		public override string VisitEndLine([NotNull] TheParser.EndLineContext context) {
			return @"$";
		}
		public override string VisitHead([NotNull] TheParser.HeadContext context) {
			return @"\A";
		}
		public override string VisitTailNotWS([NotNull] TheParser.TailNotWSContext context) {
			return @"(?=\s*\z)";
		}
		public override string VisitTail([NotNull] TheParser.TailContext context) {
			return @"\z";
		}
		public override string VisitLastMatch([NotNull] TheParser.LastMatchContext context) {
			return @"\G";
		}

		public override string VisitParenExpr([NotNull] TheParser.ParenExprContext context) {
			return Visit(context.expression(0));
		}
		public override string VisitNaming([NotNull] TheParser.NamingContext context) {
			var content = GetContentFromKeywordToken(context);
			return "(?<" + content + ">" + Visit(context.expression()) + ")";
		}
		public override string VisitRenaming([NotNull] TheParser.RenamingContext context) {
			var content = GetContentFromKeywordToken(context);
			var names = content.Split(':');
			var name1 = names[0];
			var name2 = names[1];
			return "(?<" + name1 + "-" + name2 + ">" + Visit(context.expression()) + ")";
		}

		public override string VisitCaseInsensitive([NotNull] TheParser.CaseInsensitiveContext context) {
			return "(?i:" + Visit(context.expression()) + ")";
		}
		public override string VisitBefore([NotNull] TheParser.BeforeContext context) {
			return "(?=" + Visit(context.expression()) + ")";
		}
		public override string VisitNotBefore([NotNull] TheParser.NotBeforeContext context) {
			return "(?!" + Visit(context.expression()) + ")";
		}
		public override string VisitAfter([NotNull] TheParser.AfterContext context) {
			return "(?<=" + Visit(context.expression()) + ")";
		}
		public override string VisitNotAfter([NotNull] TheParser.NotAfterContext context) {
			return "(?<!" + Visit(context.expression()) + ")";
		}
		public override string VisitAtomic([NotNull] TheParser.AtomicContext context) {
			return "(?>" + Visit(context.expression()) + ")";
		}

		public override string VisitQuantifier([NotNull] TheParser.QuantifierContext context) {
			var inputRaw = context.QUANTIFIER().GetText().ToCharArray();
			var n = VisitorHelper.ExtractTwoNumbers(inputRaw);

			if(n.number1 == 0 && n.number2 == int.MaxValue) {
				return "(" + Visit(context.expression()) + ")*";
			}
			if(n.number1 == 1 && n.number2 == int.MaxValue) {
				return "(" + Visit(context.expression()) + ")+";
			}
			if(n.number1 == 0 && n.number2 == 1) {
				return "(" + Visit(context.expression()) + ")?";
			}
			if(n.number1 == n.number2 ) {
				return "(" + Visit(context.expression()) + "){" + n.number1 + "}";
			}
			if(n.number2 == int.MaxValue) {
				return "(" + Visit(context.expression()) + "){" + n.number1 + ",}";
			}
			if(n.number1 == int.MaxValue && n.number2 == 0) {
				return "(" + Visit(context.expression()) + ")*?";
			}
			if(n.number1 == int.MaxValue && n.number2 == 1) {
				return "(" + Visit(context.expression()) + ")+?";
			}
			if(n.number1 == 1 && n.number2 == 0) {
				return "(" + Visit(context.expression()) + ")??";
			}
			if(n.number1 == int.MaxValue) {
				return "(" + Visit(context.expression()) + "){" + n.number2 + ",}?";
			}

			if(n.number2 > n.number1) {
				return "(" + Visit(context.expression()) + "){" + n.number1 + "," + n.number2 + "}";
			} else {
				return "(" + Visit(context.expression()) + "){" + n.number2 + "," + n.number1 + "}?";
			}
		}

		public override string VisitAlternation([NotNull] TheParser.AlternationContext context) {
			return "(" + VisitSubAlternation(context) + ")";
		}
		string VisitSubAlternation(TheParser.AlternationContext context) {
			var exprLeft = context.expression(0);
			string left;
			if(exprLeft is TheParser.AlternationContext el) left = VisitSubAlternation(el);
			else left = Visit(exprLeft);

			var exprRight = context.expression(1);
			string right;
			if(exprRight is TheParser.AlternationContext er) right = VisitSubAlternation(er);
			else right = Visit(exprRight);

			return left + "|" + right;
		}

		public override string VisitConditionExpression([NotNull] TheParser.ConditionExpressionContext context) {
			return "(?(" + Visit(context.expression(0)) + ")" + Visit(context.expression(1)) + "|" + Visit(context.expression(2)) + ")";
		}
		public override string VisitConditionVariable([NotNull] TheParser.ConditionVariableContext context) {
			var varName = context.VAR_USE().GetText().Substring(1);
			return "(?(" + varName + ")" + Visit(context.expression(0)) + "|" + Visit(context.expression(1)) + ")";
		}

		public override string VisitNewLine([NotNull] TheParser.NewLineContext context) {
			return @"\r?\n";
		}
		public override string VisitWord([NotNull] TheParser.WordContext context) {
			return @"\w+";

		}
		public override string VisitInt([NotNull] TheParser.IntContext context) {
			return @"\d+";

		}
		public override string VisitWhitespace([NotNull] TheParser.WhitespaceContext context) {
			return @"\s+";

		}
		public override string VisitC([NotNull] TheParser.CContext context) {
			return @"[^\r\n]";

		}
		public override string VisitDot([NotNull] TheParser.DotContext context) {
			return @".";

		}
		public override string VisitBeginWord([NotNull] TheParser.BeginWordContext context) {
			return @"((?<=\W)(?=\w)|^(?=\w))";

		}
		public override string VisitEndWord([NotNull] TheParser.EndWordContext context) {
			return @"((?<=\w)(?=\W)|(?=\w)$)";
		}

		public override string VisitAnyGreedy([NotNull] TheParser.AnyGreedyContext context) {
			return @"(" + Visit(context.expression()) + ")*";
		}
		public override string VisitAny([NotNull] TheParser.AnyContext context) {
			return @"(" + Visit(context.expression()) + ")*?";
		}
		public override string VisitAllGreedy([NotNull] TheParser.AllGreedyContext context) {
			return @"(" + Visit(context.expression()) + ")+";
		}
		public override string VisitAll([NotNull] TheParser.AllContext context) {
			return @"(" + Visit(context.expression()) + ")+?";
		}
		public override string VisitMaybe([NotNull] TheParser.MaybeContext context) {
			return @"(" + Visit(context.expression()) + ")?";
		}

		public override string VisitNamedSubst([NotNull] TheParser.NamedSubstContext context) {
			return context.GetText();
		}
		public override string VisitMatchKeyword([NotNull] TheParser.MatchKeywordContext context) {
			return @"$&";
		}
		public override string VisitBeforeMatchKeyword([NotNull] TheParser.BeforeMatchKeywordContext context) {
			return @"$`";
		}
		public override string VisitAfterMatchKeyword([NotNull] TheParser.AfterMatchKeywordContext context) {
			return @"$'";
		}
		public override string VisitInputKeyword([NotNull] TheParser.InputKeywordContext context) {
			return @"$_";
		}
	}
}
