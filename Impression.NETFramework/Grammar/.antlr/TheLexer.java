// Generated from d:\Codeworks\Projects\ImpressiON\Impression.NETFramework\Grammar/TheLexer.g4 by ANTLR 4.7.1
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
		RANGE_JOIN=12, WS=13, CHAR_TYPE=14, CHAR_TYPE_ID=15, CHAR=16;
	public static String[] channelNames = {
		"DEFAULT_TOKEN_CHANNEL", "HIDDEN"
	};

	public static String[] modeNames = {
		"DEFAULT_MODE"
	};

	public static final String[] ruleNames = {
		"QUOTE", "ESCAPED_QUOTE", "ESCAPED_DBL_BACKSLASH", "ESCAPED_CLOSE_BRACKET", 
		"EMPTY_LITERAL", "LITERAL", "EMPTY_SET", "SET", "COMMENT", "NOT", "RANGE_SEPARATOR", 
		"RANGE_JOIN", "WS", "CHAR_TYPE", "CHAR_TYPE_ID", "CHAR"
	};

	private static final String[] _LITERAL_NAMES = {
		null, "'''", "'\\''", "'\\\\'", "'\\]'", null, null, null, null, null, 
		"'not'", "'..'", "'+'", null, "'type'"
	};
	private static final String[] _SYMBOLIC_NAMES = {
		null, "QUOTE", "ESCAPED_QUOTE", "ESCAPED_DBL_BACKSLASH", "ESCAPED_CLOSE_BRACKET", 
		"EMPTY_LITERAL", "LITERAL", "EMPTY_SET", "SET", "COMMENT", "NOT", "RANGE_SEPARATOR", 
		"RANGE_JOIN", "WS", "CHAR_TYPE", "CHAR_TYPE_ID", "CHAR"
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
		"\3\u608b\ua72a\u8133\ub9ed\u417c\u3be7\u7786\u5964\2\22q\b\1\4\2\t\2\4"+
		"\3\t\3\4\4\t\4\4\5\t\5\4\6\t\6\4\7\t\7\4\b\t\b\4\t\t\t\4\n\t\n\4\13\t"+
		"\13\4\f\t\f\4\r\t\r\4\16\t\16\4\17\t\17\4\20\t\20\4\21\t\21\3\2\3\2\3"+
		"\3\3\3\3\3\3\4\3\4\3\4\3\5\3\5\3\5\3\6\3\6\3\6\3\7\3\7\3\7\3\7\7\7\66"+
		"\n\7\f\7\16\79\13\7\3\7\3\7\3\b\3\b\3\b\3\t\3\t\3\t\3\t\7\tD\n\t\f\t\16"+
		"\tG\13\t\3\t\3\t\3\n\3\n\3\n\3\n\7\nO\n\n\f\n\16\nR\13\n\3\n\3\n\3\n\3"+
		"\n\3\n\3\13\3\13\3\13\3\13\3\f\3\f\3\f\3\r\3\r\3\16\3\16\3\16\3\16\3\17"+
		"\3\17\3\17\3\17\3\17\3\20\6\20l\n\20\r\20\16\20m\3\21\3\21\5\67EP\2\22"+
		"\3\3\5\4\7\5\t\6\13\7\r\b\17\t\21\n\23\13\25\f\27\r\31\16\33\17\35\20"+
		"\37\21!\22\3\2\4\5\2\13\13\16\16\"\"\5\2//C\\c|\2x\2\3\3\2\2\2\2\5\3\2"+
		"\2\2\2\7\3\2\2\2\2\t\3\2\2\2\2\13\3\2\2\2\2\r\3\2\2\2\2\17\3\2\2\2\2\21"+
		"\3\2\2\2\2\23\3\2\2\2\2\25\3\2\2\2\2\27\3\2\2\2\2\31\3\2\2\2\2\33\3\2"+
		"\2\2\2\35\3\2\2\2\2\37\3\2\2\2\2!\3\2\2\2\3#\3\2\2\2\5%\3\2\2\2\7(\3\2"+
		"\2\2\t+\3\2\2\2\13.\3\2\2\2\r\61\3\2\2\2\17<\3\2\2\2\21?\3\2\2\2\23J\3"+
		"\2\2\2\25X\3\2\2\2\27\\\3\2\2\2\31_\3\2\2\2\33a\3\2\2\2\35e\3\2\2\2\37"+
		"k\3\2\2\2!o\3\2\2\2#$\7)\2\2$\4\3\2\2\2%&\7^\2\2&\'\7)\2\2\'\6\3\2\2\2"+
		"()\7^\2\2)*\7^\2\2*\b\3\2\2\2+,\7^\2\2,-\7_\2\2-\n\3\2\2\2./\7)\2\2/\60"+
		"\7)\2\2\60\f\3\2\2\2\61\67\7)\2\2\62\66\5\5\3\2\63\66\5\7\4\2\64\66\13"+
		"\2\2\2\65\62\3\2\2\2\65\63\3\2\2\2\65\64\3\2\2\2\669\3\2\2\2\678\3\2\2"+
		"\2\67\65\3\2\2\28:\3\2\2\29\67\3\2\2\2:;\7)\2\2;\16\3\2\2\2<=\7]\2\2="+
		">\7_\2\2>\20\3\2\2\2?E\7]\2\2@D\5\t\5\2AD\5\7\4\2BD\13\2\2\2C@\3\2\2\2"+
		"CA\3\2\2\2CB\3\2\2\2DG\3\2\2\2EF\3\2\2\2EC\3\2\2\2FH\3\2\2\2GE\3\2\2\2"+
		"HI\7_\2\2I\22\3\2\2\2JK\7\61\2\2KL\7,\2\2LP\3\2\2\2MO\13\2\2\2NM\3\2\2"+
		"\2OR\3\2\2\2PQ\3\2\2\2PN\3\2\2\2QS\3\2\2\2RP\3\2\2\2ST\7,\2\2TU\7\61\2"+
		"\2UV\3\2\2\2VW\b\n\2\2W\24\3\2\2\2XY\7p\2\2YZ\7q\2\2Z[\7v\2\2[\26\3\2"+
		"\2\2\\]\7\60\2\2]^\7\60\2\2^\30\3\2\2\2_`\7-\2\2`\32\3\2\2\2ab\t\2\2\2"+
		"bc\3\2\2\2cd\b\16\3\2d\34\3\2\2\2ef\7v\2\2fg\7{\2\2gh\7r\2\2hi\7g\2\2"+
		"i\36\3\2\2\2jl\t\3\2\2kj\3\2\2\2lm\3\2\2\2mk\3\2\2\2mn\3\2\2\2n \3\2\2"+
		"\2op\13\2\2\2p\"\3\2\2\2\t\2\65\67CEPm\4\b\2\2\2\3\2";
	public static final ATN _ATN =
		new ATNDeserializer().deserialize(_serializedATN.toCharArray());
	static {
		_decisionToDFA = new DFA[_ATN.getNumberOfDecisions()];
		for (int i = 0; i < _ATN.getNumberOfDecisions(); i++) {
			_decisionToDFA[i] = new DFA(_ATN.getDecisionState(i), i);
		}
	}
}