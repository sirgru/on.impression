using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using Impression.NETFramework.Grammar;

namespace ES.ON.Impression {
	public class Visitor : TheParserBaseVisitor<string> {
		public ErrorListener errorListener { get; private set; }

		public Visitor(ErrorListener errorListener) {
			this.errorListener = errorListener;
		}

		public override string VisitExpressionSeq([NotNull] TheParser.ExpressionSeqContext context) {
			string result = "";
			for(int i = 0; i < context.ChildCount; i++) {
				result += Visit(context.GetChild(i));
			}
			return result;
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
			errorListener.AddSemanticError(token, token, "Empty literals aren't allowed.");
			return "";
		}

		public override string VisitEmptySet([NotNull] TheParser.EmptySetContext context) {
			var token = context.EMPTY_SET().Symbol;
			errorListener.AddSemanticError(token, token, "Empty sets aren't allowed.");
			return "[]";
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
			var content = context.GetText().Split('.');

			var first = ConvertSpecialCharactersForRange(content[0]);
			var second = ConvertSpecialCharactersForRange(content[content.Length-1]);

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

		public override string VisitCharType([NotNull] TheParser.CharTypeContext context) {
			return @"\p{" + GetContentFromCharType(context) + "}";
		}

		public override string VisitNotCharType([NotNull] TheParser.NotCharTypeContext context) {
			return @"\P{" + GetContentFromCharType(context) + "}";
		}
		string GetContentFromCharType(ParserRuleContext context) {
			var text = context.GetText();
			var split = text.Split(' ', '\t', ':');
			return split[split.Length - 1];
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
		public override string VisitStartString([NotNull] TheParser.StartStringContext context) {
			return @"\A";
		}
		public override string VisitEndStringBeforeWS([NotNull] TheParser.EndStringBeforeWSContext context) {
			return @"(?=\s*\z)";
		}
		public override string VisitEndString([NotNull] TheParser.EndStringContext context) {
			return @"\z";
		}
		public override string VisitLastMatchEnd([NotNull] TheParser.LastMatchEndContext context) {
			return @"\G";
		}

		public override string VisitParenExpr([NotNull] TheParser.ParenExprContext context) {
			return Visit(context.expressionSeq());
		}
		public override string VisitNaming([NotNull] TheParser.NamingContext context) {
			var content = GetContentFromCharType(context);
			return "(?<" + content + ">" + Visit(context.expression()) + ")";
		}
		public override string VisitRenaming([NotNull] TheParser.RenamingContext context) {
			var content = GetRenameTypes(context);
			var names = content.Split(':');
			var name1 = names[0];
			var name2 = names[1];
			return "(?<" + name1 + "-" + name2 + ">" + Visit(context.expression()) + ")";
		}
		string GetRenameTypes(ParserRuleContext context) {
			var text = context.GetText();
			var split = text.Split(' ', '\t');
			return split[split.Length - 1];
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
			if(n.number1 == n.number2) {
				return "(" + Visit(context.expression()) + "){" + n.number1 + "}";
			}
			if(n.number1 == int.MaxValue) {
				return "(" + Visit(context.expression()) + "){0," + n.number2 + "}";
			}
			if(n.number2 == int.MaxValue) {
				return "(" + Visit(context.expression()) + "){" + n.number1 + ",}";
			}

			return "(" + Visit(context.expression()) + "){" + n.number1 + "," + n.number2 + "}";
		}

		public override string VisitLazyQuantifier([NotNull] TheParser.LazyQuantifierContext context) {
			var inputRaw = context.LAZY_QUANTIFIER().GetText().Substring(1).ToCharArray();
			var n = VisitorHelper.ExtractTwoNumbers(inputRaw);

			if(n.number1 == 0 && n.number2 == int.MaxValue) {
				return "(" + Visit(context.expression()) + ")*?";
			}
			if(n.number1 == 1 && n.number2 == int.MaxValue) {
				return "(" + Visit(context.expression()) + ")+?";
			}
			if(n.number1 == 0 && n.number2 == 1) {
				return "(" + Visit(context.expression()) + ")??";
			}
			if(n.number1 == n.number2) {
				return "(" + Visit(context.expression()) + "){" + n.number1 + "}?";
			}
			if(n.number1 == int.MaxValue) {
				return "(" + Visit(context.expression()) + "){0," + n.number2 + "}?";
			}
			if(n.number2 == int.MaxValue) {
				return "(" + Visit(context.expression()) + "){" + n.number1 + ",}?";
			}

			return "(" + Visit(context.expression()) + "){" + n.number1 + "," + n.number2 + "}?";
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
		public override string VisitAnyNotNL([NotNull] TheParser.AnyNotNLContext context) {
			return @"[^\r\n]";
		}
		public override string VisitAnyChar([NotNull] TheParser.AnyCharContext context) {
			return @".";
		}
		public override string VisitWordBegin([NotNull] TheParser.WordBeginContext context) {
			return @"((?<=\W)(?=\w)|^(?=\w))";
		}
		public override string VisitWordEnd([NotNull] TheParser.WordEndContext context) {
			return @"((?<=\w)(?=\W)|(?=\w)$)";
		}

		public override string VisitAnyGreedy([NotNull] TheParser.AnyGreedyContext context) {
			return @"(" + Visit(context.expression()) + ")*";
		}
		public override string VisitAnyLazy([NotNull] TheParser.AnyLazyContext context) {
			return @"(" + Visit(context.expression()) + ")*?";
		}
		public override string VisitAllGreedy([NotNull] TheParser.AllGreedyContext context) {
			return @"(" + Visit(context.expression()) + ")+";
		}
		public override string VisitAllLazy([NotNull] TheParser.AllLazyContext context) {
			return @"(" + Visit(context.expression()) + ")+?";
		}
		public override string VisitMaybeGreedy([NotNull] TheParser.MaybeGreedyContext context) {
			return @"(" + Visit(context.expression()) + ")?";
		}
		public override string VisitMaybeLazy([NotNull] TheParser.MaybeLazyContext context) {
			return @"(" + Visit(context.expression()) + ")??";
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

		public override string VisitLexicalError([NotNull] TheParser.LexicalErrorContext context) {
			errorListener.AddLexicalError(context.start, context.stop, "Unrecognized character sequence.");
			return "";
		}

		public override string VisitNamedBackreference([NotNull] TheParser.NamedBackreferenceContext context) {
			var varName = context.GetText().Substring(1);
			return @"\k<" + varName + ">";
		}

		public override string VisitEnclosedAlternation([NotNull] TheParser.EnclosedAlternationContext context) {
			var subExprLeft = context.expressionSeq(0);
			string left;
			if(subExprLeft.GetChild(0) is TheParser.AlternationContext el) left = VisitSubAlternation(el);
			else left = Visit(subExprLeft);

			var subExprRight = context.expressionSeq(1);
			string right;
			if(subExprRight.GetChild(0) is TheParser.AlternationContext er) right = VisitSubAlternation(er);
			else right = Visit(subExprRight);

			return "(" + left + "|" + right + ")";
		}
	}
}
