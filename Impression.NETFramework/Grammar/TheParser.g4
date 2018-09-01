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
    | expression QUANTIFIER # Quantifier
    | expression '|' expression                         # Alternation
    | IF expression THEN expression ELSE expression     # ConditionExpression
    | IF VAR_USE THEN expression ELSE expression        # ConditionVariable
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
	: 'w'  # Word
	| 'ws'  # WhiteSpace
	| 'd'   # Digit
	| 'wb'  # WordBoundary
	;

not_short
	: NOT 'w'   # NotWord
	| NOT 'ws'  # NotWhiteSpace
	| NOT 'd'   # NotDigit
	| NOT 'wb'  # NotWordBoundary
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

