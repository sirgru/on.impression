// Generated from d:\Codeworks\Projects\ImpressiON\Impression.NETFramework\Grammar\TheLexer.g4 by ANTLR 4.7.1
import org.antlr.v4.runtime.Lexer;
import org.antlr.v4.runtime.CharStream;
import org.antlr.v4.runtime.Token;
import org.antlr.v4.runtime.TokenStream;
import org.antlr.v4.runtime.*;
import org.antlr.v4.runtime.atn.*;
import org.antlr.v4.runtime.dfa.DFA;
import org.antlr.v4.runtime.misc.*;

@SuppressWarnings({"all", "warnings", "unchecked", "unused", "cast"})
public class TheLexer extends Lexer {
	static { RuntimeMetaData.checkVersion("4.7.1", RuntimeMetaData.VERSION); }

	protected static final DFA[] _decisionToDFA;
	protected static final PredictionContextCache _sharedContextCache =
		new PredictionContextCache();
	public static final int
		QUOTE=1, ESCAPED_QUOTE=2, ESCAPED_DBL_BACKSLASH=3, ESCAPED_CLOSE_BRACKET=4, 
		EMPTY_LITERAL=5, LITERAL=6, EMPTY_SET=7, SET=8, COMMENT=9, NOT=10, RANGE_SEPARATOR=11, 
		RANGE_JOIN=12, WS=13, ANY=14;
	public static String[] channelNames = {
		"DEFAULT_TOKEN_CHANNEL", "HIDDEN"
	};

	public static String[] modeNames = {
		"DEFAULT_MODE"
	};

	public static final String[] ruleNames = {
		"QUOTE", "ESCAPED_QUOTE", "ESCAPED_DBL_BACKSLASH", "ESCAPED_CLOSE_BRACKET", 
		"EMPTY_LITERAL", "LITERAL", "EMPTY_SET", "SET", "COMMENT", "NOT", "RANGE_SEPARATOR", 
		"RANGE_JOIN", "WS", "ANY"
	};

	private static final String[] _LITERAL_NAMES = {
		null, "'''", "'\\''", "'\\\\'", "'\\]'", null, null, null, null, null, 
		"'not'", "'..'", "'+'"
	};
	private static final String[] _SYMBOLIC_NAMES = {
		null, "QUOTE", "ESCAPED_QUOTE", "ESCAPED_DBL_BACKSLASH", "ESCAPED_CLOSE_BRACKET", 
		"EMPTY_LITERAL", "LITERAL", "EMPTY_SET", "SET", "COMMENT", "NOT", "RANGE_SEPARATOR", 
		"RANGE_JOIN", "WS", "ANY"
	};
	public static final Vocabulary VOCABULARY = new VocabularyImpl(_LITERAL_NAMES, _SYMBOLIC_NAMES);

	/**
	 * @deprecated Use {@link #VOCABULARY} instead.
	 */
	@Deprecated
	public static final String[] tokenNames;
	static {
		tokenNames = new String[_SYMBOLIC_NAMES.length];
		for (int i = 0; i < tokenNames.length; i++) {
			tokenNames[i] = VOCABULARY.getLiteralName(i);
			if (tokenNames[i] == null) {
				tokenNames[i] = VOCABULARY.getSymbolicName(i);
			}

			if (tokenNames[i] == null) {
				tokenNames[i] = "<INVALID>";
			}
		}
	}

	@Override
	@Deprecated
	public String[] getTokenNames() {
		return tokenNames;
	}

	@Override

	public Vocabulary getVocabulary() {
		return VOCABULARY;
	}


	public TheLexer(CharStream input) {
		super(input);
		_interp = new LexerATNSimulator(this,_ATN,_decisionToDFA,_sharedContextCache);
	}

	@Override
	public String getGrammarFileName() { return "TheLexer.g4"; }

	@Override
	public String[] getRuleNames() { return ruleNames; }

	@Override
	public String getSerializedATN() { return _serializedATN; }

	@Override
	public String[] getChannelNames() { return channelNames; }

	@Override
	public String[] getModeNames() { return modeNames; }

	@Override
	public ATN getATN() { return _ATN; }

	public static final String _serializedATN =
		"\3\u608b\ua72a\u8133\ub9ed\u417c\u3be7\u7786\u5964\2\20c\b\1\4\2\t\2\4"+
		"\3\t\3\4\4\t\4\4\5\t\5\4\6\t\6\4\7\t\7\4\b\t\b\4\t\t\t\4\n\t\n\4\13\t"+
		"\13\4\f\t\f\4\r\t\r\4\16\t\16\4\17\t\17\3\2\3\2\3\3\3\3\3\3\3\4\3\4\3"+
		"\4\3\5\3\5\3\5\3\6\3\6\3\6\3\7\3\7\3\7\3\7\7\7\62\n\7\f\7\16\7\65\13\7"+
		"\3\7\3\7\3\b\3\b\3\b\3\t\3\t\3\t\3\t\7\t@\n\t\f\t\16\tC\13\t\3\t\3\t\3"+
		"\n\3\n\3\n\3\n\7\nK\n\n\f\n\16\nN\13\n\3\n\3\n\3\n\3\n\3\n\3\13\3\13\3"+
		"\13\3\13\3\f\3\f\3\f\3\r\3\r\3\16\3\16\3\16\3\16\3\17\3\17\5\63AL\2\20"+
		"\3\3\5\4\7\5\t\6\13\7\r\b\17\t\21\n\23\13\25\f\27\r\31\16\33\17\35\20"+
		"\3\2\3\5\2\13\13\16\16\"\"\2i\2\3\3\2\2\2\2\5\3\2\2\2\2\7\3\2\2\2\2\t"+
		"\3\2\2\2\2\13\3\2\2\2\2\r\3\2\2\2\2\17\3\2\2\2\2\21\3\2\2\2\2\23\3\2\2"+
		"\2\2\25\3\2\2\2\2\27\3\2\2\2\2\31\3\2\2\2\2\33\3\2\2\2\2\35\3\2\2\2\3"+
		"\37\3\2\2\2\5!\3\2\2\2\7$\3\2\2\2\t\'\3\2\2\2\13*\3\2\2\2\r-\3\2\2\2\17"+
		"8\3\2\2\2\21;\3\2\2\2\23F\3\2\2\2\25T\3\2\2\2\27X\3\2\2\2\31[\3\2\2\2"+
		"\33]\3\2\2\2\35a\3\2\2\2\37 \7)\2\2 \4\3\2\2\2!\"\7^\2\2\"#\7)\2\2#\6"+
		"\3\2\2\2$%\7^\2\2%&\7^\2\2&\b\3\2\2\2\'(\7^\2\2()\7_\2\2)\n\3\2\2\2*+"+
		"\7)\2\2+,\7)\2\2,\f\3\2\2\2-\63\7)\2\2.\62\5\5\3\2/\62\5\7\4\2\60\62\13"+
		"\2\2\2\61.\3\2\2\2\61/\3\2\2\2\61\60\3\2\2\2\62\65\3\2\2\2\63\64\3\2\2"+
		"\2\63\61\3\2\2\2\64\66\3\2\2\2\65\63\3\2\2\2\66\67\7)\2\2\67\16\3\2\2"+
		"\289\7]\2\29:\7_\2\2:\20\3\2\2\2;A\7]\2\2<@\5\t\5\2=@\5\7\4\2>@\13\2\2"+
		"\2?<\3\2\2\2?=\3\2\2\2?>\3\2\2\2@C\3\2\2\2AB\3\2\2\2A?\3\2\2\2BD\3\2\2"+
		"\2CA\3\2\2\2DE\7_\2\2E\22\3\2\2\2FG\7\61\2\2GH\7,\2\2HL\3\2\2\2IK\13\2"+
		"\2\2JI\3\2\2\2KN\3\2\2\2LM\3\2\2\2LJ\3\2\2\2MO\3\2\2\2NL\3\2\2\2OP\7,"+
		"\2\2PQ\7\61\2\2QR\3\2\2\2RS\b\n\2\2S\24\3\2\2\2TU\7p\2\2UV\7q\2\2VW\7"+
		"v\2\2W\26\3\2\2\2XY\7\60\2\2YZ\7\60\2\2Z\30\3\2\2\2[\\\7-\2\2\\\32\3\2"+
		"\2\2]^\t\2\2\2^_\3\2\2\2_`\b\16\3\2`\34\3\2\2\2ab\13\2\2\2b\36\3\2\2\2"+
		"\b\2\61\63?AL\4\b\2\2\2\3\2";
	public static final ATN _ATN =
		new ATNDeserializer().deserialize(_serializedATN.toCharArray());
	static {
		_decisionToDFA = new DFA[_ATN.getNumberOfDecisions()];
		for (int i = 0; i < _ATN.getNumberOfDecisions(); i++) {
			_decisionToDFA[i] = new DFA(_ATN.getDecisionState(i), i);
		}
	}
}