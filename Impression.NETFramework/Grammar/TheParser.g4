parser grammar TheParser
	;

options
{
	tokenVocab = TheLexer;
}

start: (literal | set | not_set | sub_set)+;

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



