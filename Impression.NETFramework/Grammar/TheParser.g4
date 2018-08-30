parser grammar TheParser
	;

options
{
	tokenVocab = TheLexer;
}

start: expr+;

expr: EMPTY_LITERAL | LITERAL; 




