lexer grammar TheLexer
	;

QUOTE: '\'';
PIPE: '|';

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

START: 'start';
END: 'end';
HEAD: 'head';
TAIL_NOT_WS: 'tail-not-ws';
TAIL: 'tail';
LAST_MATCH: 'last-match';

fragment ID: [a-zA-Z_] [a-zA-Z0-9_]*;
fragment SEP: ( ' ' | '\t')+;
NAME: 'as' SEP ID;
RENAME: 'as' SEP ID ':' ID;

PAREN_OPEN: '(';
PAREN_CLOSE: ')';

I: 'i';
BEFORE: 'before';
NOT_BEFORE: 'not-before';
AFTER: 'after';
NOT_AFTER: 'not-after';
ATOMIC: 'atomic';

fragment DIGIT: [0-9];
fragment OPT_SEP: ( ' ' | '\t')*;
QUANTIFIER
	: 'x' OPT_SEP DIGIT
	| 'x' OPT_SEP DIGIT OPT_SEP '..' OPT_SEP (DIGIT)?
	| 'x' OPT_SEP (DIGIT)? OPT_SEP '..' OPT_SEP DIGIT
	;

WS: (' ' | '\t' | '\f' | '\r' | '\n') -> channel(HIDDEN);

CHAR: .;

