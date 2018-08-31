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

WS: (' ' | '\t' | '\f') -> channel(HIDDEN);

CHAR_TYPE: 'type';

CHAR_TYPE_ID: [a-zA-Z-]+;

CHAR: .;


