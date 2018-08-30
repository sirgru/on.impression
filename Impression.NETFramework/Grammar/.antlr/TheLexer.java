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
		EMPTY_LITERAL=1, LITERAL=2, COMMENT=3, WS=4;
	public static String[] channelNames = {
		"DEFAULT_TOKEN_CHANNEL", "HIDDEN"
	};

	public static String[] modeNames = {
		"DEFAULT_MODE"
	};

	public static final String[] ruleNames = {
		"EMPTY_LITERAL", "LITERAL", "LITERAL_CONTENT", "ESCAPED_SEQ", "COMMENT", 
		"WS"
	};

	private static final String[] _LITERAL_NAMES = {
	};
	private static final String[] _SYMBOLIC_NAMES = {
		null, "EMPTY_LITERAL", "LITERAL", "COMMENT", "WS"
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
		"\3\u608b\ua72a\u8133\ub9ed\u417c\u3be7\u7786\u5964\2\6\65\b\1\4\2\t\2"+
		"\4\3\t\3\4\4\t\4\4\5\t\5\4\6\t\6\4\7\t\7\3\2\3\2\3\2\3\3\3\3\3\3\3\3\3"+
		"\4\3\4\7\4\31\n\4\f\4\16\4\34\13\4\3\5\3\5\3\5\3\5\5\5\"\n\5\3\6\3\6\3"+
		"\6\3\6\7\6(\n\6\f\6\16\6+\13\6\3\6\3\6\3\6\3\6\3\6\3\7\3\7\3\7\3\7\4\32"+
		")\2\b\3\3\5\4\7\2\t\2\13\5\r\6\3\2\3\5\2\13\13\16\16\"\"\2\66\2\3\3\2"+
		"\2\2\2\5\3\2\2\2\2\13\3\2\2\2\2\r\3\2\2\2\3\17\3\2\2\2\5\22\3\2\2\2\7"+
		"\32\3\2\2\2\t!\3\2\2\2\13#\3\2\2\2\r\61\3\2\2\2\17\20\7)\2\2\20\21\7)"+
		"\2\2\21\4\3\2\2\2\22\23\7)\2\2\23\24\5\7\4\2\24\25\7)\2\2\25\6\3\2\2\2"+
		"\26\31\5\t\5\2\27\31\13\2\2\2\30\26\3\2\2\2\30\27\3\2\2\2\31\34\3\2\2"+
		"\2\32\33\3\2\2\2\32\30\3\2\2\2\33\b\3\2\2\2\34\32\3\2\2\2\35\36\7^\2\2"+
		"\36\"\7)\2\2\37 \7^\2\2 \"\7^\2\2!\35\3\2\2\2!\37\3\2\2\2\"\n\3\2\2\2"+
		"#$\7\61\2\2$%\7,\2\2%)\3\2\2\2&(\13\2\2\2\'&\3\2\2\2(+\3\2\2\2)*\3\2\2"+
		"\2)\'\3\2\2\2*,\3\2\2\2+)\3\2\2\2,-\7,\2\2-.\7\61\2\2./\3\2\2\2/\60\b"+
		"\6\2\2\60\f\3\2\2\2\61\62\t\2\2\2\62\63\3\2\2\2\63\64\b\7\3\2\64\16\3"+
		"\2\2\2\7\2\30\32!)\4\b\2\2\2\3\2";
	public static final ATN _ATN =
		new ATNDeserializer().deserialize(_serializedATN.toCharArray());
	static {
		_decisionToDFA = new DFA[_ATN.getNumberOfDecisions()];
		for (int i = 0; i < _ATN.getNumberOfDecisions(); i++) {
			_decisionToDFA[i] = new DFA(_ATN.getDecisionState(i), i);
		}
	}
}