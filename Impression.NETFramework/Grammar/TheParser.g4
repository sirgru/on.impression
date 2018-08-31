parser grammar TheParser
	;

options
{
	tokenVocab = TheLexer;
}

start: (literal | set | not_set | sub_set | short | not_short)+;

literal
	: EMPTY_LITERAL	# EmptyLiteral
	| LITERAL		# LiteralWithContent
	;

set
	: EMPTY_SET			# EmptySet
	| SET				# SetWithContent
	| CHAR '..' CHAR	# RangeSet
	| set '+' set		# CombinationSet
	| CHAR_TYPE         # CharType
	| NOT_CHAR_TYPE     # NotCharType
	;

not_set: NOT set # SetNegative;

sub_set: (set | not_set) '-' set       # SubtractionSet;

short: 'w'  # Word
    | 'ws'  # WhiteSpace
    | 'd'   # Digit
    | 'wb'  # WordBoundary
    ;

not_short:
      NOT 'w'   # NotWord
    | NOT 'ws'  # NotWhiteSpace
    | NOT 'd'   # NotDigit
    | NOT 'wb'  # NotWordBoundary
    ;
