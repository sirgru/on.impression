parser grammar TheParser
	;

options
{
	tokenVocab = TheLexer;
}

start: literal+;

literal:  EMPTY_LITERAL #EmptyLiteral | LITERAL #LiteralWithContent;



