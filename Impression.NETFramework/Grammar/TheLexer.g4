lexer grammar TheLexer
	;

EMPTY_LITERAL: '\'' '\'';

LITERAL: '\'' LITERAL_CONTENT '\'';

fragment LITERAL_CONTENT: (ESCAPED_SEQ|.)*? ;

fragment ESCAPED_SEQ: '\\\'' | '\\\\' ; // 2-char sequences \" and \\

COMMENT : '/*' .*? '*/' -> skip ; 

WS: (' '|'\t'|'\f') -> channel(HIDDEN);