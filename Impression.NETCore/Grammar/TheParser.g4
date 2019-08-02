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
	| expression QUANTIFIER			# Quantifier
	| expression LAZY_QUANTIFIER	# LazyQuantifier
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
	| set SET_UNION set		# CombinationSet
	;

not_set: NOT set # SetNegative;

subtr_set: (set	| not_set) SET_DIFF set		 # SubtractionSet;

type: CHAR_TYPE			# CharType
	| NOT CHAR_TYPE		# NotCharType
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
	: START_LINE			# StartLine
	| END_LINE				# EndLine
	| START_STRING			# StartString
	| END_STRING_BEFORE_WS	# EndStringBeforeWS
	| END_STRING			# EndString
	| LAST_MATCH_END		# LastMatchEnd
	;

grouping
	: I	expression			# CaseInsensitive
	| BEFORE expression		# Before
	| NOT BEFORE expression	# NotBefore
	| AFTER	expression		# After
	| NOT AFTER	expression	# NotAfter
	| ATOMIC expression		# Atomic
	;

additions
	: NL			# NewLine
	| WORD			# Word
	| INT			# Int
	| WHITESPACE	# Whitespace
	| C_ANY_NOT_NL	# AnyNotNL
	| C_ANY			# AnyChar
	| WORD_BEGIN	# WordBegin
	| WORD_END		# WordEnd
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


