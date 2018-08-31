parser grammar TheParser
	;

options
{
	tokenVocab = TheLexer;
}

start: (literal | set | not_set)+;

literal
	: EMPTY_LITERAL	# EmptyLiteral
	| LITERAL		# LiteralWithContent
	;

set
	: EMPTY_SET		# EmptySet
	| SET			# SetWithContent
	| ANY '..' ANY	# RangeSet
	| set '+' set	# CombinationSet
	;

not_set: NOT set # SetNegative;

