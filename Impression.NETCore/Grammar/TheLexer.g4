lexer grammar TheLexer
	;

QUOTE: '\'';
PIPE: '|';

ESCAPED_QUOTE: '\\\'';
ESCAPED_DBL_BACKSLASH: '\\\\';
ESCAPED_CLOSE_BRACKET: '\\]';

EMPTY_LITERAL: '\''	'\'';
LITERAL: '\'' (ESCAPED_QUOTE | ESCAPED_DBL_BACKSLASH | .)*?	'\'';

EMPTY_SET: '[' ']';
SET: '[' (ESCAPED_CLOSE_BRACKET	| ESCAPED_DBL_BACKSLASH	| .)*? ']';

COMMENT: '/*' .*? '*/' -> skip;

NOT: 'not:';

SET_UNION: '+';
SET_DIFF: '-';

fragment SEP: (	' '	| '\t')+;
fragment OPT_SEP: (	' '	| '\t')*;
CHAR_TYPE: 'type:' OPT_SEP [a-zA-Z-]+;
NOT_CHAR_TYPE: 'not-type:' OPT_SEP [a-zA-Z-]+;

C_WORD:	'w';
C_WHITE_SPACE: 'ws';
C_DIGIT: 'd';
WORD_BOUNDARY: 'wb';

START: 'start';
END: 'end';
HEAD: 'head';
TAIL_AFTER_WS: 'tail-after-ws';
TAIL: 'tail';
LAST_MATCH:	'last-match';

fragment ID: [a-zA-Z0-9_]+;
NAME: 'as' SEP ID;
RENAME:	'as' SEP ID	':'	ID;

PAREN_OPEN:	'(';
PAREN_CLOSE: ')';

I: 'i:';
BEFORE:	'before:';
NOT_BEFORE:	'not-before:';
AFTER: 'after:';
NOT_AFTER: 'not-after:';
ATOMIC:	'atomic:';

fragment DIGIT:	[0-9];
QUANTIFIER
	: 'x' OPT_SEP DIGIT
	| 'x' OPT_SEP DIGIT	OPT_SEP	'..' OPT_SEP (DIGIT)?
	| 'x' OPT_SEP (DIGIT)? OPT_SEP '..'	OPT_SEP	DIGIT
	;

IF:	'if';
ELSE: 'else';
VAR_USE: '$' ID;

NL:	'nl';
WORD: 'word';
INT: 'int';
WHITESPACE:	'whitespace';
C_ANY_NOT_NL: 'c';
C_ANY: 'a';
BEGIN_WORD:	'bw';
END_WORD: 'ew';

ANY_GREEDY:	':any';
ANY_LAZY: ':any-lazy';
ALL_GREEDY:	':all';
ALL_LAZY: ':all-lazy';
MAYBE_GREEDY: ':maybe';
MAYBE_LAZY:	':maybe-lazy';

NAMED_SUBST: '${' ID '}';
MATCH: 'match';
BEFORE_MATCH: 'before-match';
AFTER_MATCH: 'after-match';
INPUT: 'input';

RANGE_SET: . '..' .;

WS:	(' ' | '\t'	| '\f' | '\r' |	'\n' | ',') -> channel(HIDDEN);

NEVER: .;
