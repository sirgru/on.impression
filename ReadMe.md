# ON.Impression

![ON.Impression](./Logo/Logo.png)

ON.Impression is a programming language for recognizing string patterns with regular grammar. It is effectively an alternative syntax to regular expressions.


## Versions

v2 - Upgraded grammar, reduced verbosity and improved consistency
v1 - Original version 


## Installation & Usage

Via Package Manager:  
`Install-Package On.Impression`  
Via dotnet CLI:  
`dotnet add package On.Impression`  

Usage:  
`using ES.ON.Impression;`  
`string regex = ImpressionToRegex.Convert(impressionQueryString);`  


## Why should you know about this language?

While the ability to search for a string pattern inside another string is very useful, the currently accepted syntax for doing so is quite problematic. 

**Regular expressions are hard to read.**

The problem is the syntax for regular expressions is it mixes literals and special characters in complex and context dependant ways. Sometimes a character is a literal and sometimes it is special, but it is special only in certain contexts (for example `-` character is different in and out of class, and if it's first in class, but it could also be an exclusion). For this reason, the reader must mentally parse the regular expression just like a computer would, keeping mental track of the translated expressions to get the general meaning of the expression. Regex does not convey intent clearly because the concepts of a literal and a "keyword" are mixed inside the language. This manifests with a lot of escape characters that must be understood, and in some languages the backslash has to be doubly escaped leading to increased visual clutter. Further, the terseness of the language is a problem - every "keyword" must be represented by a special set of symbols, which must be un-intuitively translated. Most programmers have trouble memorizing the unintuitive conventions of Regex, and even then the resulting regex is hard to read and parse mentally.

Some programmers decorate their regex with detailed comments and this alleviates some of the problem, but most don't. Comments should never be required for basic understanding of the program and this is a symptom of the obviously bad language design.

Sometimes the regex is so complex that it is easier to write it from scratch than try to understand it and debug it. 

> Programs must be written for people to read, and only incidentally for machines to execute. --Harold Abelson, 1984


**Regular expressions are hard to write correctly**

Some of the problem is the inherent complexity of working with text, other is accidental complexity arising from the idioms of the regex language. 

Some of the complexity is related to effective un-debug-ability of regex without the presence of specialized tools. 


**It is easy to go wild with complexity in regular expressions**

> Some people, when confronted with a problem, think "I know, I'll use regular expressions." Now they have two problems. -- Jamie Zawinski

Some programmers will simply overuse regex. "When you have a hammer, everything looks like a nail." The nature of the thing is that a regex is assembled part by part, and the final solution is usually much longer than the initial solution and much less intuitive. It is easy to create a "monster regex". Programmers are people, and people will do things that make them feel smart, so the programmers that have a firm grasp on regex and not shy about growing them out of control. The monster regex quickly becomes unmaintainable.

Most users practically copy the regex from the internet if they are needing it for a well-known purpose. This subtly negates the purpose of existence of the regex language - they might have used a library and it would have executed faster (no regex compile) and it would have been standardized, easier to test and debug. 


**Regular expressions are not standardized**

The expressions are not standardized, the need for escaping can be different when embedded in different languages, the engines can have their own quirks or bugs. The engines can be of different types and have different performance characteristics.

In practice, the problem is that a regex from one language cannot be used as-is in another language. 


**Tools are band-aids, not solutions**

When writing a complex regex from scratch, the workflow usually includes an external tool, paid or free, or one of the numerous online helper tools. Tools can help in initially writing the Regex, but there are problems: the created regex exhibits all the mentioned negative characteristics when maintenance is needed. It quickly becomes a piece of illegible text, sometimes dependant on that external tool for change. This may seem like it comes from inherent complexity, but other programming languages don't require special tools in order to write code.


**Shortness is not a benefit**

Zipped text files are also short, but they are also unreadable by humans. In programming language design there is always this tension between explicitness (verbosity) and terseness (compactness). The correct answer depends on use case statistics - most commonly used operations should be terse, rarely used things should be explicit. A programming language is a convention, and it should be a useful convention.

# Spec

## Literals & Comments:

Single quotes are used to delimit string literals. This allows us to embed the imp expression inside a verbatim literal in most languages (like C#) and don't have to do escaping. If the single quote must be used as data, it must be escaped, e.g. `'q\'q'`. If the string literal must contain a backslash, it must be escaped with another backslash.

Comments are allowed outside literals and classes.

Empty literals (`''`) aren't allowed.

| Imp:          | Regex:        |
| ------------- | ------------- |
| 'abc'         | abc           |
| /* comment */ | (?# comment ) |


## Escapes:

Escapes appear inside literals and sets. They are exactly the same as in Regex.

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


## Character Sets (classes):

Character sets (classes) are denoted with `[]` and define a set of single character literals. If a closing bracket must be used as part of data in the class, it must be escaped, e.g. `[(){}[\]]`. If the set must contain a backslash, it must be escaped with another backslash, just like in literals. 

Empty sets (`[]`) aren't allowed.

| Imp:                | Regex:         | Explanation:                                                    |
| ------------------- | -------------- | --------------------------------------------------------------- |
| [cg]                | [cg]           | Matches any single character in character set (class)           |
| ![cg]               | [^cg]          | Negation: Matches any single character that is not in set.      |
| a..z                | [a-z]          | Range                                                           |
| a..z u A..Z u [123] | [a-zA-Z123]    | Example of 2 ranges combined with a set.                        |
| a..z u 0..9 - [6]   | [a-z0-9-[6]]   | Character Class Subtraction. [1]                                |
| type: IsCyrillic    | \p{IsCyrillic} | Matches any 1 character in the Unicode category or named block. |
| !type: Lu           | \P{Lu}         | Not \p                                                          |
| w                   | \w             | Matches any character that can be part of a "word".             |
| !w                  | \W             | Not \w                                                          |
| ws                  | \s             | Matches any white-space character.                              |
| !ws                 | \S             | Not \s                                                          |
| d                   | \d             | Matches a digit.                                                |
| !d                  | \D             | Not \d                                                          |

[1] Subtraction has higher priority than addition, thus any additions after the subtraction are treated as belonging to the subtraction.


## Anchors:

| Imp:           | Regex:    | Explanation:                                                        |
| -------------- | --------- | ------------------------------------------------------------------- |
| <              | ^         | Start of Line                                                       |
| >              | $         | End of Line                                                         |
| <<             | \A        | Start of String                                                     |
| >>_            | (?=\s*\z) | Match at the end of the string before optional trailing whitespace. |
| >>             | \z        | Match at the end of the string, including trailing whitespace.      |
| last-match-end | \G        | Match at the point where the previous match ended.                  |
| ,              | \b        | Match on a word boundary.                                           |
| !,             | \B        | Match must not occur on a word boundary.                            |


## Grouping:

Variables can contain letters, numbers and underscores, and can be just a number.

| Imp:                   | Regex:                   | Explanation:                                                   |
| ---------------------- | ------------------------ | -------------------------------------------------------------- |
| -                      | ( subExpr )              | Captures the matched subExpr and assigns it an index variable. |
| subExpr as name        | (?<name> subExpr )       | Captures the matched subExpr into a named group.               |
| subExpr as name1:name2 | (?<name1-name2> subExpr) | Defines a balancing group definition.                          |
| (subExpr)              | (?: subExpr )            | Defines a non-capturing  group.                                |
| i: subExpr             | (?i: subExpr )           | Applies case invariance within subExpr.                        |
| before: subExpr        | (?= subExpr )            | Positive lookahead.                                            |
| !before: subExpr       | (?! subExpr )            | Negative lookahead.                                            |
| after: subExpr         | (?<= subExpr )           | Positive lookbehind.                                           |
| !after: subExpr        | (?<! subExpr )           | Negative lookbehind.                                           |
| atomic: subExpr        | (?> subExpr )            | Atomic (non-backtracking) subExpr.                             |


## Quantifiers:

The 3 most popular symbols in Regex are the same.

| Imp: | Regex: | Explanation:                               |
| ---- | ------ | ------------------------------------------ |
| *    | *      | Matches the expression zero or more times. |
| +    | +      | Matches the expression one or more times.  |
| ?    | ?      | Matches the expression zero or one time.   |

With explicit number of matches, the syntax is like "expression times n".

| Imp:           | Regex:    | Explanation:                                                       |
| -------------- | --------- | ------------------------------------------------------------------ |
| x n            | { n }     | Matches the expression exactly n times.                            |
| x n..          | { n ,}    | Matches the expression at least n times.                           |
| x n..m (m > n) | { n , m } | Matches the expression at least n times, but no more than m times. |

The non-greedy (lazy) variations are prefixed with `.`.

| Imp:            | Regex:     | Explanation:                                                                |
| --------------- | ---------- | --------------------------------------------------------------------------- |
| .*              | *?         | Matches the expression zero or more times, but as few times as possible.    |
| .+              | +?         | Matches the expression one or more times, but as few times as possible.     |
| .?              | ??         | Matches the expression zero or one time, but as few times as possible.      |
| .x n..          | { n ,}?    | Matches the expression at least n times, but as few times as possible.      |
| .x n..m (m > n) | { n , m }? | Matches the expression between n and m times, but as few times as possible. |

Quantifiers can also have full, read-friendly names:

| Imp:             | Regex:     |
| ---------------- | ---------- |
| expr :any        | `(expr)*`  |
| expr :any-lazy   | `(expr)*?` |
| expr :all        | `(expr)+`  |
| expr :all-lazy   | `(expr)+?` |
| expr :maybe      | `(expr)?`  |
| expr :maybe-lazy | `(expr)??` |


## Back-references:

| Imp:  | Regex:     | Explanation:                                                   |
| ----- | ---------- | -------------------------------------------------------------- |
| $name | \k\<name\> | Named back-reference. Matches the value of a named expression. |


## Alternation:

| Imp:                          | Regex:                       | Explanation:         |
| ----------------------------- | ---------------------------- | -------------------- |
| `'th'('e'\|'is'\|'at')`       | `th(e\|is\|at)`              | Any one alternative. |
| `if (expression) yes else no` | `(?(expression) yes \| no )` | [1]                  |
| `if $name yes else no`        | `(?(name) yes \| no )`       | [1]                  |

[1] Matches yes if the regular expression pattern or name designated by expression matches; otherwise, matches the optional no part. Expression is interpreted as a zero-width assertion.


## Substitutions:

| Imp:         | Regex:  | Explanation:                                               |
| ------------ | ------- | ---------------------------------------------------------- |
| ${name}      | ${name} | Named substitution                                         |
| match        | $&      | Substitutes a copy of the whole match.                     |
| before-match | $`      | Substitutes all text of the input string before the match. |
| after-match  | $'      | Substitutes all text of the input string after the match.  |
| input        | $_      | Substitutes the entire input string.                       |


## Additions:

When translating into Regex, we always use `m, n, s` options and never use `x` option in the generated Regex. We explicitly specify `i` option. 

| Imp:  | Regex:                     | Explanation:                           |
| ----- | -------------------------- | -------------------------------------- |
| nl    | \r?\n                      | OS-independent newline                 |
| word  | \w+                        | Word                                   |
| int   | \d+                        | Integral number                        |
| space | \s+                        | At least 1 white space character       |
| wb    | `((?<=\W)(?=\w)\|^(?=\w))` | Word Begin                             |
| we    | `((?<=\w)(?=\W)\|(?=\w)$)` | Word End                               |
| c     | `[^\r\n]`                  | Match 1 character, except `\r` or `\n` |
| a     | `.` single line            | Match any character                    |

Which Means:

| Imp:          | Regex:              |
| ------------- | ------------------- |
| c :any        | `([^\r\n])*`        |
| c :any-lazy   | `([^\r\n])*?`       |
| a :any        | `(.)*` single line  |
| a :any-lazy   | `(.)*?` single line |
| c :all        | `([^\r\n])+`        |
| c :all-lazy   | `([^\r\n])+?`       |
| a :all        | `(.)+` single line  |
| a :all-lazy   | `(.)+?` single line |
| c :maybe      | `([^\r\n])?`        |
| c :maybe-lazy | `([^\r\n])??`       |
| a :maybe      | `(.)?`              |
| a :maybe-lazy | `(.)??`             |

> Mnemonics: the shorthand keywords (c, a, w...) represent 1 length selections. Complete words (word, space) represent a complete selection counterpart, a series of characters.


# Grammar:

An expression consists of a series of sub-expressions. Sub-expressions may be enclosed in parenthesis, which do not influence capturing. Sub-expressions may be prefixed with a positioning keyword, or a case-insensitivity modifier keyword, or may be suffixed with a quantifier. The types of expressions that associate right to left (quantification and naming) have higher precedence than regular rules, for example `'ab' | 'cd' x 1..3` evaluates as `'ab' | ('cd' x 1..3)`.

Spaces, tabs and newlines have no meaning.

A semicolon character (`;`) can be used to arbitrarily separate elements in the expression, it will have no meaning and will be discarded like a space.


## Author:

Grammar and implementation in C# created by Gojko Radonjić "Gru".

