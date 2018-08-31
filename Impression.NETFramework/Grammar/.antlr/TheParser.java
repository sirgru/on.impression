// Generated from d:\Codeworks\Projects\ImpressiON\Impression.NETFramework\Grammar\TheParser.g4 by ANTLR 4.7.1
import org.antlr.v4.runtime.atn.*;
import org.antlr.v4.runtime.dfa.DFA;
import org.antlr.v4.runtime.*;
import org.antlr.v4.runtime.misc.*;
import org.antlr.v4.runtime.tree.*;
import java.util.List;
import java.util.Iterator;
import java.util.ArrayList;

@SuppressWarnings({"all", "warnings", "unchecked", "unused", "cast"})
public class TheParser extends Parser {
	static { RuntimeMetaData.checkVersion("4.7.1", RuntimeMetaData.VERSION); }

	protected static final DFA[] _decisionToDFA;
	protected static final PredictionContextCache _sharedContextCache =
		new PredictionContextCache();
	public static final int
		QUOTE=1, ESCAPED_QUOTE=2, ESCAPED_DBL_BACKSLASH=3, ESCAPED_CLOSE_BRACKET=4, 
		EMPTY_LITERAL=5, LITERAL=6, EMPTY_SET=7, SET=8, COMMENT=9, NOT=10, RANGE_SEPARATOR=11, 
		RANGE_JOIN=12, WS=13, ANY=14;
	public static final int
		RULE_start = 0, RULE_literal = 1, RULE_set = 2, RULE_not_set = 3;
	public static final String[] ruleNames = {
		"start", "literal", "set", "not_set"
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

	@Override
	public String getGrammarFileName() { return "TheParser.g4"; }

	@Override
	public String[] getRuleNames() { return ruleNames; }

	@Override
	public String getSerializedATN() { return _serializedATN; }

	@Override
	public ATN getATN() { return _ATN; }

	public TheParser(TokenStream input) {
		super(input);
		_interp = new ParserATNSimulator(this,_ATN,_decisionToDFA,_sharedContextCache);
	}
	public static class StartContext extends ParserRuleContext {
		public List<LiteralContext> literal() {
			return getRuleContexts(LiteralContext.class);
		}
		public LiteralContext literal(int i) {
			return getRuleContext(LiteralContext.class,i);
		}
		public List<SetContext> set() {
			return getRuleContexts(SetContext.class);
		}
		public SetContext set(int i) {
			return getRuleContext(SetContext.class,i);
		}
		public List<Not_setContext> not_set() {
			return getRuleContexts(Not_setContext.class);
		}
		public Not_setContext not_set(int i) {
			return getRuleContext(Not_setContext.class,i);
		}
		public StartContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_start; }
	}

	public final StartContext start() throws RecognitionException {
		StartContext _localctx = new StartContext(_ctx, getState());
		enterRule(_localctx, 0, RULE_start);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(11); 
			_errHandler.sync(this);
			_la = _input.LA(1);
			do {
				{
				setState(11);
				_errHandler.sync(this);
				switch (_input.LA(1)) {
				case EMPTY_LITERAL:
				case LITERAL:
					{
					setState(8);
					literal();
					}
					break;
				case EMPTY_SET:
				case SET:
				case ANY:
					{
					setState(9);
					set(0);
					}
					break;
				case NOT:
					{
					setState(10);
					not_set();
					}
					break;
				default:
					throw new NoViableAltException(this);
				}
				}
				setState(13); 
				_errHandler.sync(this);
				_la = _input.LA(1);
			} while ( (((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << EMPTY_LITERAL) | (1L << LITERAL) | (1L << EMPTY_SET) | (1L << SET) | (1L << NOT) | (1L << ANY))) != 0) );
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class LiteralContext extends ParserRuleContext {
		public LiteralContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_literal; }
	 
		public LiteralContext() { }
		public void copyFrom(LiteralContext ctx) {
			super.copyFrom(ctx);
		}
	}
	public static class LiteralWithContentContext extends LiteralContext {
		public TerminalNode LITERAL() { return getToken(TheParser.LITERAL, 0); }
		public LiteralWithContentContext(LiteralContext ctx) { copyFrom(ctx); }
	}
	public static class EmptyLiteralContext extends LiteralContext {
		public TerminalNode EMPTY_LITERAL() { return getToken(TheParser.EMPTY_LITERAL, 0); }
		public EmptyLiteralContext(LiteralContext ctx) { copyFrom(ctx); }
	}

	public final LiteralContext literal() throws RecognitionException {
		LiteralContext _localctx = new LiteralContext(_ctx, getState());
		enterRule(_localctx, 2, RULE_literal);
		try {
			setState(17);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case EMPTY_LITERAL:
				_localctx = new EmptyLiteralContext(_localctx);
				enterOuterAlt(_localctx, 1);
				{
				setState(15);
				match(EMPTY_LITERAL);
				}
				break;
			case LITERAL:
				_localctx = new LiteralWithContentContext(_localctx);
				enterOuterAlt(_localctx, 2);
				{
				setState(16);
				match(LITERAL);
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class SetContext extends ParserRuleContext {
		public SetContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_set; }
	 
		public SetContext() { }
		public void copyFrom(SetContext ctx) {
			super.copyFrom(ctx);
		}
	}
	public static class CombinationSetContext extends SetContext {
		public List<SetContext> set() {
			return getRuleContexts(SetContext.class);
		}
		public SetContext set(int i) {
			return getRuleContext(SetContext.class,i);
		}
		public CombinationSetContext(SetContext ctx) { copyFrom(ctx); }
	}
	public static class EmptySetContext extends SetContext {
		public TerminalNode EMPTY_SET() { return getToken(TheParser.EMPTY_SET, 0); }
		public EmptySetContext(SetContext ctx) { copyFrom(ctx); }
	}
	public static class RangeSetContext extends SetContext {
		public List<TerminalNode> ANY() { return getTokens(TheParser.ANY); }
		public TerminalNode ANY(int i) {
			return getToken(TheParser.ANY, i);
		}
		public RangeSetContext(SetContext ctx) { copyFrom(ctx); }
	}
	public static class SetWithContentContext extends SetContext {
		public TerminalNode SET() { return getToken(TheParser.SET, 0); }
		public SetWithContentContext(SetContext ctx) { copyFrom(ctx); }
	}

	public final SetContext set() throws RecognitionException {
		return set(0);
	}

	private SetContext set(int _p) throws RecognitionException {
		ParserRuleContext _parentctx = _ctx;
		int _parentState = getState();
		SetContext _localctx = new SetContext(_ctx, _parentState);
		SetContext _prevctx = _localctx;
		int _startState = 4;
		enterRecursionRule(_localctx, 4, RULE_set, _p);
		try {
			int _alt;
			enterOuterAlt(_localctx, 1);
			{
			setState(25);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case EMPTY_SET:
				{
				_localctx = new EmptySetContext(_localctx);
				_ctx = _localctx;
				_prevctx = _localctx;

				setState(20);
				match(EMPTY_SET);
				}
				break;
			case SET:
				{
				_localctx = new SetWithContentContext(_localctx);
				_ctx = _localctx;
				_prevctx = _localctx;
				setState(21);
				match(SET);
				}
				break;
			case ANY:
				{
				_localctx = new RangeSetContext(_localctx);
				_ctx = _localctx;
				_prevctx = _localctx;
				setState(22);
				match(ANY);
				setState(23);
				match(RANGE_SEPARATOR);
				setState(24);
				match(ANY);
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
			_ctx.stop = _input.LT(-1);
			setState(32);
			_errHandler.sync(this);
			_alt = getInterpreter().adaptivePredict(_input,4,_ctx);
			while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1 ) {
					if ( _parseListeners!=null ) triggerExitRuleEvent();
					_prevctx = _localctx;
					{
					{
					_localctx = new CombinationSetContext(new SetContext(_parentctx, _parentState));
					pushNewRecursionContext(_localctx, _startState, RULE_set);
					setState(27);
					if (!(precpred(_ctx, 1))) throw new FailedPredicateException(this, "precpred(_ctx, 1)");
					setState(28);
					match(RANGE_JOIN);
					setState(29);
					set(2);
					}
					} 
				}
				setState(34);
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,4,_ctx);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			unrollRecursionContexts(_parentctx);
		}
		return _localctx;
	}

	public static class Not_setContext extends ParserRuleContext {
		public Not_setContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_not_set; }
	 
		public Not_setContext() { }
		public void copyFrom(Not_setContext ctx) {
			super.copyFrom(ctx);
		}
	}
	public static class SetNegativeContext extends Not_setContext {
		public TerminalNode NOT() { return getToken(TheParser.NOT, 0); }
		public SetContext set() {
			return getRuleContext(SetContext.class,0);
		}
		public SetNegativeContext(Not_setContext ctx) { copyFrom(ctx); }
	}

	public final Not_setContext not_set() throws RecognitionException {
		Not_setContext _localctx = new Not_setContext(_ctx, getState());
		enterRule(_localctx, 6, RULE_not_set);
		try {
			_localctx = new SetNegativeContext(_localctx);
			enterOuterAlt(_localctx, 1);
			{
			setState(35);
			match(NOT);
			setState(36);
			set(0);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public boolean sempred(RuleContext _localctx, int ruleIndex, int predIndex) {
		switch (ruleIndex) {
		case 2:
			return set_sempred((SetContext)_localctx, predIndex);
		}
		return true;
	}
	private boolean set_sempred(SetContext _localctx, int predIndex) {
		switch (predIndex) {
		case 0:
			return precpred(_ctx, 1);
		}
		return true;
	}

	public static final String _serializedATN =
		"\3\u608b\ua72a\u8133\ub9ed\u417c\u3be7\u7786\u5964\3\20)\4\2\t\2\4\3\t"+
		"\3\4\4\t\4\4\5\t\5\3\2\3\2\3\2\6\2\16\n\2\r\2\16\2\17\3\3\3\3\5\3\24\n"+
		"\3\3\4\3\4\3\4\3\4\3\4\3\4\5\4\34\n\4\3\4\3\4\3\4\7\4!\n\4\f\4\16\4$\13"+
		"\4\3\5\3\5\3\5\3\5\2\3\6\6\2\4\6\b\2\2\2+\2\r\3\2\2\2\4\23\3\2\2\2\6\33"+
		"\3\2\2\2\b%\3\2\2\2\n\16\5\4\3\2\13\16\5\6\4\2\f\16\5\b\5\2\r\n\3\2\2"+
		"\2\r\13\3\2\2\2\r\f\3\2\2\2\16\17\3\2\2\2\17\r\3\2\2\2\17\20\3\2\2\2\20"+
		"\3\3\2\2\2\21\24\7\7\2\2\22\24\7\b\2\2\23\21\3\2\2\2\23\22\3\2\2\2\24"+
		"\5\3\2\2\2\25\26\b\4\1\2\26\34\7\t\2\2\27\34\7\n\2\2\30\31\7\20\2\2\31"+
		"\32\7\r\2\2\32\34\7\20\2\2\33\25\3\2\2\2\33\27\3\2\2\2\33\30\3\2\2\2\34"+
		"\"\3\2\2\2\35\36\f\3\2\2\36\37\7\16\2\2\37!\5\6\4\4 \35\3\2\2\2!$\3\2"+
		"\2\2\" \3\2\2\2\"#\3\2\2\2#\7\3\2\2\2$\"\3\2\2\2%&\7\f\2\2&\'\5\6\4\2"+
		"\'\t\3\2\2\2\7\r\17\23\33\"";
	public static final ATN _ATN =
		new ATNDeserializer().deserialize(_serializedATN.toCharArray());
	static {
		_decisionToDFA = new DFA[_ATN.getNumberOfDecisions()];
		for (int i = 0; i < _ATN.getNumberOfDecisions(); i++) {
			_decisionToDFA[i] = new DFA(_ATN.getDecisionState(i), i);
		}
	}
}