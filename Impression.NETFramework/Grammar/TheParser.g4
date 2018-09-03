parser grammar TheParser
	;

options
{
	tokenVocab = TheLexer;
}

expressionSeq: (expression)+;

expression
	: '(' expressionSeq	'|'	expressionSeq ')'		# EnclosedAlternation
	| '(' expressionSeq	')'							# ParenExpr
	| literal				# n__Literal
	| set					# n__Set
	| not_set				# n__NotSet
	| subtr_set				# n__SubSet
	| type					# n__Type
	| shorts				# n__Shorts
	| not_short				# n__NotShort
	| anchors				# n__Anchors
	| expression NAME		# Naming
	| expression RENAME		# Renaming
	| grouping				# n__Grouping
	| expression QUANTIFIER		# Quantifier
	| expression ANY_GREEDY		# AnyGreedy
	| expression ANY_LAZY		# AnyLazy
	| expression ALL_GREEDY		# AllGreedy
	| expression ALL_LAZY		# AllLazy
	| expression MAYBE_GREEDY	# MaybeGreedy
	| expression MAYBE_LAZY		# MaybeLazy
	| expression '|' expression							# Alternation
	| IF '(' expression	')'	expression ELSE	expression	# ConditionExpression
	| IF VAR_USE expression	ELSE expression				# ConditionVariable
	| additions				# n__Additions
	| subst_special			# n__SubstSpecial
	| VAR_USE				# NamedBackreference
	| lex_error				# LexicalError
	;

literal
	: EMPTY_LITERAL	# EmptyLiteral
	| LITERAL		# LiteralWithContent
	;

set
	: EMPTY_SET			# EmptySet
	| SET				# SetWithContent
	| RANGE_SET			# RangeSet
	| set '+' set		# CombinationSet
	;

not_set: NOT set # SetNegative;

subtr_set: (set	| not_set) '-' set		 # SubtractionSet;

type: CHAR_TYPE			# CharType
	| NOT_CHAR_TYPE		# NotCharType
	;

shorts
	: C_WORD		 # WordChar
	| C_WHITE_SPACE	 # WhiteSpace
	| C_DIGIT		 # Digit
	| WORD_BOUNDARY	 # WordBoundary
	;

not_short
	: NOT C_WORD		 # NotWord
	| NOT C_WHITE_SPACE	 # NotWhiteSpace
	| NOT C_DIGIT		 # NotDigit
	| NOT WORD_BOUNDARY	 # NotWordBoundary
	;

anchors
	: START				# StartLine
	| END				# EndLine
	| HEAD				# Head
	| TAIL_AFTER_WS		# TailAfterWS
	| TAIL				# Tail
	| LAST_MATCH		# LastMatch
	;

grouping
	: I	expression			# CaseInsensitive
	| BEFORE expression		# Before
	| NOT_BEFORE expression	# NotBefore
	| AFTER	expression		# After
	| NOT_AFTER	expression	# NotAfter
	| ATOMIC expression		# Atomic
	;

additions
	: NL			# NewLine
	| WORD			# Word
	| INT			# Int
	| WHITESPACE	# Whitespace
	| C_ANY_NOT_NL	# AnyNotNL
	| C_ANY			# AnyChar
	| BEGIN_WORD	# BeginWord
	| END_WORD		# EndWord
	;

subst_special
	: NAMED_SUBST	# NamedSubst
	| MATCH			# MatchKeyword
	| BEFORE_MATCH	# BeforeMatchKeyword
	| AFTER_MATCH	# AfterMatchKeyword
	| INPUT			# InputKeyword
	;

lex_error
	: NEVER+
	;


