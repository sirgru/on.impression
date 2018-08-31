lexer grammar TheLexer
	;

QUOTE: '\'';

ESCAPED_QUOTE: '\\\'';
ESCAPED_DBL_BACKSLASH: '\\\\';
ESCAPED_CLOSE_BRACKET: '\\]';

EMPTY_LITERAL: '\'' '\'';
LITERAL: '\'' (ESCAPED_QUOTE | ESCAPED_DBL_BACKSLASH | .)*? '\'';

EMPTY_SET: '[' ']';
SET: '[' (ESCAPED_CLOSE_BRACKET | ESCAPED_DBL_BACKSLASH | .)*? ']';

COMMENT: '/*' .*? '*/' -> skip;

NOT: 'not';

RANGE_SEPARATOR: '..';
RANGE_JOIN: '+';
RANGE_SUBTRACT: '-';

CHAR_TYPE: 'type' ( ' ' | '\t')+ [a-zA-Z-]+;
NOT_CHAR_TYPE: 'not-type' ( ' ' | '\t')+ [a-zA-Z-]+;

C_WORD: 'w';
C_WHITE_SPACE: 'ws';
C_DIGIT: 'd';
WORD_BOUNDARY: 'wb';

WS: (' ' | '\t' | '\f' | '\r' | '\n') -> channel(HIDDEN);

CHAR: .;

