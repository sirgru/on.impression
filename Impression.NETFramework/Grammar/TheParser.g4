parser grammar TheParser
	;

options
{
	tokenVocab = TheLexer;
}

start: (expression)+;

expression
	: paren_expression      # n__ParenExpr
	| literal               # n__Literal
	| set                   # n__Set
	| not_set               # n__NotSet
	| sub_set               # n__SubSet
	| type                  # n__Type
	| shorts                # n__Shorts
	| not_short             # n__NotShort
	| anchors               # n__Anchors
	| expression NAME       # Naming
    | expression RENAME     # Renaming
    | grouping              # n__Grouping
    | expression QUANTIFIER     # Quantifier
    | expression ANY_GREEDY     # AnyGreedy
    | expression ANY            # Any
    | expression ALL_GREEDY     # AllGreedy
    | expression ALL            # All
    | expression MAYBE          # Maybe
    | expression '|' expression                         # Alternation
    | IF expression THEN expression ELSE expression     # ConditionExpression
    | IF VAR_USE THEN expression ELSE expression        # ConditionVariable
    | additions             # n__Additions
    | subst_special         # n__SubstSpecial
    ;

paren_expression: '(' + expression + ')' #ParenExpr;

literal
	: EMPTY_LITERAL	# EmptyLiteral
	| LITERAL		# LiteralWithContent
	;

set
	: EMPTY_SET			# EmptySet
	| SET				# SetWithContent
	| CHAR '..' CHAR	# RangeSet
	| set '+' set		# CombinationSet
	;

not_set: NOT set # SetNegative;

sub_set: (set | not_set) '-' set       # SubtractionSet;

type: CHAR_TYPE         # CharType
    | NOT_CHAR_TYPE     # NotCharType
    ;

shorts
	: C_WORD         # WordChar
	| C_WHITE_SPACE  # WhiteSpace
	| C_DIGIT        # Digit
	| WORD_BOUNDARY  # WordBoundary
	;

not_short
	: NOT C_WORD         # NotWord
	| NOT C_WHITE_SPACE  # NotWhiteSpace
	| NOT C_DIGIT        # NotDigit
	| NOT WORD_BOUNDARY  # NotWordBoundary
	;

anchors
	: START         # StartLine
	| END           # EndLine
	| HEAD          # Head
	| TAIL_NOT_WS   # TailNotWS
	| TAIL          # Tail
	| LAST_MATCH    # LastMatch
	;

grouping
	: I expression          # CaseInsensitive
	| BEFORE expression     # Before
	| NOT_BEFORE expression # NotBefore
	| AFTER expression      # After
	| NOT_AFTER expression  # NotAfter
	| ATOMIC expression     # Atomic
	;

additions
	: NL            # NewLine
	| WORD          # Word
	| INT           # Int
	| WHITESPACE    # Whitespace
	| C             # C
	| DOT           # Dot
	| BEGIN_WORD    # BeginWord
	| END_WORD      # EndWord
	;

subst_special
	: NAMED_SUBST   # NamedSubst
    | MATCH         # MatchKeyword
    | BEFORE_MATCH  # BeforeMatchKeyword
    | AFTER_MATCH   # AfterMatchKeyword
    | INPUT         # InputKeyword
	;