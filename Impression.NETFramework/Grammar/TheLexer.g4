lexer grammar TheLexer
	;

ESC_QUOTE:  '\'';

EMPTY_LITERAL: '\'' '\'';

LITERAL: '\'' (ESCAPED_SEQ|.)*? '\'' ;

ESCAPED_SEQ: '\\\'' | '\\\\' ; // 2-char sequences \" and \\

COMMENT : '/*' .*? '*/' -> skip ; 

WS: (' '|'\t'|'\f') -> channel(HIDDEN);