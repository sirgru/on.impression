# Impression

Impression is a programming language for recognizing string patterns with regular grammar. It is effectively an alternative syntax to regular expressions.


## Why should you know about this language?

We don't disagree that searching for a pattern of text inside another pattern of text is a useful idea, it's just that the currently accepted syntax is quite problematic. 

**Regular expressions are hard to read.**

The problem is the syntax for regular expressions mixes literals and special characters in complex and context dependant rules. Sometimes a character is a literal and sometimes it is special, but it is special only in certain contexts (for example `-` character is different in and out of class, and if it's first in class, but it could also be an exclusion). For this reason, the reader must mentally parse the regular expression just like a computer would, keeping mental track of the translated expressions to get the general meaning of the expression. Regex does not convey intent clearly because the concepts of a literal and a "keyword" are mixed inside the language. This manifests with a lot of escape characters that must be understood, and in some languages the backslash has to be doubly escaped leading to incredible visual clutter. Further, the terseness of the language is a problem - every "keyword" must be represented by a special set of symbols, which must be "un-intuitively translated". How many programmers can immediately tell what these mean: `(?<= sub expression )`, `(?<v> sub expression)`? Most programmers don't work with text processing often enough and with enough volume to have enough experience to mentally parse the regex quickly. 

Some programmers decorate their regex with detailed comments and this alleviates most of the problem, but most programmers don't. Comments should never be required for basic understanding of the program and this is a symptom of the obvious bad language design.

Sometimes the regex is so complex that it is easier to write it from scratch than try to understand it and debug it. 

> Programs must be written for people to read, and only incidentally for machines to execute. --Harold Abelson, 1984


**Regular expressions are hard to write correctly**

Some of the problem is the inherent complexity of working with text, other is accidental complexity arising from the idioms of the regex language. 

Some of the complexity is related to effective undebuggability of regex without the presence of specialized tools. 


**It is easy to get out of hand with regular expressions**

> Some people, when confronted with a problem, think "I know, I'll use regular expressions." Now they have two problems. -- Jamie Zawinski

One class of programmers will simply overuse regex. "When you have a hammer, everything looks like a nail" is the saying that applies strongly in this case. Programmers are people, and people will do things that make them feel smart, so will the programmers that have a firm grasp on regex. The problem is that making a complicated regex is not a smart thing to do, because other programmers will not dare touch it.

Most users practically copy the regex from some of the regex sharing sites if they are needing it for a well-known purpose. This subtly negates the purpose of existence of the regex language - they might have used a library and it would have been quicker (no regex compile) and it would have been standardized, easier to test and debug. 

The nature of the thing is that a regex is assembled part by part, and the final solution is usually much longer than the initial solution and much less intuitive. It is easy to create a "monster regex".


**Regular expressions are not standardized**

The expressions are not standardized, the need for escaping can be different when embedded in different languages, the engines can have their own quirks or bugs. The engines can be of different types and have different performance characteristics.

In practice, the problem is that a regex from one language cannot be used as-is in another language. 


**Tools are band-aids, not solutions**

When writing a regex from scratch, the workflow usually includes an external tool, paid or free, or one of the numerous online helper tools. Tools can generate regex much easier than writing the syntax, but there are problems: the created regex exhibits all the negative characteristics when maintenance is needed. It quickly becomes a piece of text dependant on that external tool for change.


## Solution

There exists a large amount of knowledge and techniques around implementing regular expressions efficiently. The experience and skills around working with regular expression expressions are also significant parts of a programmer's skillset. We have no intention of throwing those away, instead we feature the good parts and work on the missing parts. The solution is a better formulated language that is trans-compiled to a well-known regular expressions language. 


### Literals & Comments:

Single quotes are used to delimit string literals. This allows us to embed the imp expression inside a verbatim literal in most languages (like C#) and don't have to do escaping. If the single quote must be used as data, it must be escaped, e.g. `'q\'q'`. If the string literal must contain a backslash, it must be escaped with another backslash.

Comments are allowed outside literals and classes.

Empty literals ('') aren't allowed.

| Imp:          | Regex:        |
| ------------- | ------------- |
| 'abc'         | abc           |
| ------------  | ------------- |
| /* comment */ | (?# comment ) |


### Escapes:

Escapes appear inside literals and sets.

| Imp:   | Regex: | Explanation:                                                                             |
| ------ | ------ | ---------------------------------------------------------------------------------------- |
| \a     | \a     | Matches a bell character \u0007                                                          |
| \b     | \b     | matches a backspace \u0008                                                               |
| \t     | \t     | Matches a tab \u0009                                                                     |
| \r     | \r     | Matches a carriage return \u000D                                                         |
| \v     | \v     | Matches a vertical tab \u000B                                                            |
| \f     | \f     | Matches a form feed \u000C                                                               |
| \n     | \n     | Matches a new line character \u000A (not a Windows NewLine Sequence)                     |
| \e     | \e     | Matches an escape character \u001B                                                       |
| \nnn   | \nnn   | Uses octal representation to specify a character (nnn consists of two or three digits).  |
| \xnn   | \xnn   | Uses hexadecimal representation to specify a character (nn consists of two digits).      |
| \cX    | \cX    | Matches the ASCII control char specified by X or x, the letter of the control character. |
| \unnnn | \unnnn | Matches a Unicode character by using hexadecimal representation (four digits).           |



### Character Sets (classes):

Character sets (classes) are denoted with `[]`. If a closing bracket must be used as part of data in the class, it must be escaped, e.g. `[(){}[\]]`. If the set must contain a backslash, it must be escaped with another backslash. 

Elements in character classes are not translated (`w` literally means 'w'). Alternation or direct representation should be used instead.

Empty sets ([]) aren't allowed.

| Imp:                    | Regex:         | Explanation:                                                    |
| ----------------------- | -------------- | --------------------------------------------------------------- |
| [cg]                    | [cg]           | Matches any single character in character set (class) cg        |
| not: [cg]               | [^cg]          | Negation: Matches any single character that is not in set.      |
| [a..z] + [A..Z] + [123] | [a-zA-Z123]    | Example of 2 ranges combined with a set.                        |
| [a..z] + [0..9] - [6]   | [a-z0-9-[6]]   | Character Class Subtraction. [1]                                |
| type: IsCyrillic        | \p{IsCyrillic} | Matches any 1 character in the Unicode category or named block. |
| not-type: Lu            | \P{Lu}         | Not \p                                                          |
| w                       | \w             | Matches any character that can be part of a "word".             |
| not: w                  | \W             | Not \w                                                          |
| ws                      | \s             | Matches any white-space character.                              |
| not: ws                 | \S             | Not \s                                                          |
| d                       | \d             | Matches a digit.                                                |
| not: d                  | \D             | Not \d                                                          |

[1] Subtraction has higher priority than addition, thus any additions after the subtraction are treated as belonging to the subtraction.


### Anchors:

| Imp:          | Regex:    | Explanation:                                                        |
| ------------- | --------- | ------------------------------------------------------------------- |
| start         | ^         | Start of Line                                                       |
| end           | $         | End of Line                                                         |
| head          | \A        | Start of String                                                     |
| tail-after-ws | (?=\s*\z) | Match at the end of the string before optional trailing whitespace. |
| tail          | \z        | Match at the end of the string, including trailing whitespace.      |
| last-match    | \G        | Match at the point where the previous match ended.                  |
| wb            | \b        | Match on a word boundary.                                           |
| not: wb       | \B        | Match not occur on a word boundary.                                 |


### Grouping:

Variables can contain letter, numbers and underscore, but cannot begin with a number.

| Imp:                   | Regex:                   | Explanation:                                                  |
| ---------------------- | ------------------------ | ------------------------------------------------------------- |
| -                      | ( subexp )               | Captures the matched subexp and assigns it an index variable. |
| subexpr as name        | (?<name> subexp )        | Captures the matched subexp into a named group.               |
| subexpr as name1:name2 | (?<name1-name2> subexpr) | Defines a balancing group definition.                         |
| (subexpr)              | (?: subexpr )            | Defines a noncapturing  group.                                |
| i: subexpr             | (?i: subexpr )           | Applies case invariance within subexpr.                       |
| before: subexpr        | (?= subexpr )            | Positive lookahead.                                           |
| not-before: subexpr    | (?! subexpr )            | Negative lookahead.                                           |
| after: subexpr         | (?<= subexpr )           | Positive lookbehind.                                          |
| not-after: subexpr     | (?<! subexpression )     | Negative lookbehind.                                          |
| atomic: subexpr        | (?> subexpr )            | Atomic (non-backtracking) subexpr.                            |


### Quantifiers:

Quantifiers are specified in range syntax (closed and open ranges), with preferred number of matches second. If the preferred number of matches cannot be satisfied, it will try until and including the first number in range.

Quantifiers thus use the same syntax for greedy and non-greedy matching.

| Imp:           | Regex:     | Explanation:                                                                |
| -------------- | ---------- | --------------------------------------------------------------------------- |
| x 0..          | *          | Matches the expression zero or more times.                                  |
| x 1..          | +          | Matches the expression one or more times.                                   |
| x 0..1         | ?          | Matches the expression zero or one time.                                    |
| x n            | { n }      | Matches the expression exactly n times.                                     |
| x n..          | { n ,}     | Matches the expression at least n times.                                    |
| x n..m (m > n) | { n , m }  | Matches the expression at least n times, but no more than m times.          |
| x ..0          | *?         | Matches the expression zero or more times, but as few times as possible.    |
| x ..1          | +?         | Matches the expression one or more times, but as few times as possible.     |
| x 1..0         | ??         | Matches the expression zero or one time, but as few times as possible.      |
| x ..n          | { n ,}?    | Matches the expression at least n times, but as few times as possible.      |
| x n..m (m < n) | { n , m }? | Matches the expression between n and m times, but as few times as possible. |


### Backreferences:

| Imp:  | Regex:   | Explanation:                                                  |
| ----- | -------- | ------------------------------------------------------------- |
| $name | \k<name> | Named backreference. Matches the value of a named expression. |


### Alternation:

| Imp:                             | Regex:                      | Explanation:         |
| -------------------------------- | --------------------------- | -------------------- |
| `'th'('e'|'is'|'at')`            | `th(e|is|at)`               | Any one alternative. |
| `if expression then yes else no` | `(?(expression) yes | no )` | [1]                  |
| `if $name then yes else no`      | `(?(name) yes | no )`       | [1]                  |

[1] Matches yes if the regular expression pattern or name designated by expression matches; otherwise, matches the optional no part. expression is interpreted as a zero-width assertion.


### Substitutions:

| Imp:         | Regex:  | Explanation:                                               |
| ------------ | ------- | ---------------------------------------------------------- |
| ${name}      | ${name} | Named substitution                                         |
| match        | $&      | Substitutes a copy of the whole match.                     |
| before-match | $`      | Substitutes all text of the input string before the match. |
| after-match  | $'      | Substitutes all text of the input string after the match.  |
| input        | $_      | Substitutes the entire input string.                       |


### Additions:

We always use m, n and s options and never use x option in the generated Regex. We explicitly specify i option. 

| Imp:       | Regex:                    | Explanation:                        |
| ---------- | ------------------------- | ----------------------------------- |
| nl         | \r?\n                     | OS-independent newline.             |
| word       | \w+                       | Word.                               |
| int        | \d+                       | Integral number.                    |
| whitespace | \s+                       | White space.                        |
| bw         | `((?<=\W)(?=\w)|^(?=\w))` | Begin word.                         |
| ew         | `((?<=\w)(?=\W)|(?=\w)$)` | End word.                           |
| c          | `[^\r\n]`                 | Match 1 character, except \r or \n. |
| a          | `.` singleline            | Match any character.                |

Quantifiers can also have full, read-friendly names:

| Imp:             | Regex:     |
| ---------------- | ---------- |
| expr :any        | `(expr)*`  |
| expr :any-lazy   | `(expr)*?` |
| expr :all        | `(expr)+`  |
| expr :all-lazy   | `(expr)+?` |
| expr :maybe      | `(expr)?`  |
| expr :maybe-lazy | `(expr)??` |

Which Means:

| Imp:          | Regex:             |
| ------------- | ------------------ |
| c :any        | `([^\r\n])*`       |
| c :any-lazy   | `([^\r\n])*?`      |
| a :any        | `(.)*` singleline  |
| a :any-lazy   | `(.)*?` singleline |
| c :all        | `([^\r\n])+`       |
| c :all-lazy   | `([^\r\n])+?`      |
| a :all        | `(.)+` singleline  |
| a :all-lazy   | `(.)+?` singleline |
| c :maybe      | `([^\r\n])?`       |
| c :maybe-lazy | `([^\r\n])??`      |
| a :maybe      | `(.)?`             |
| a :maybe-lazy | `(.)??`            |

> Mnemonics: the shorthand keywords (c, a, w...) represent 1 length selections. Complete words (word, whitespace) represent a complete selection counterpart, a series of characters.


### Grammar:

Ax expression consists of a series of sub-expressions. Subexpressions may be enclosed in parenthesis, which do not influence capturing. Subexpressions may be prefixed with a positioning keyword, or a case-insensitive modifier keyword, or may be suffixed with a quantifier. The types of expressions that associate right to left (quantification and naming) have higher precedence than regular rules, for example `'ab' | 'cd' x 1..3` evaluates as `'ab' | ('cd' x 1..3)`.


