//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.13.2
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from C:/Projects/Grammar_Project/Assets/Simple_Grammar (ANTLR)/Simple_Grammar.g4 by ANTLR 4.13.2

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

using System;
using System.IO;
using System.Text;
using System.Diagnostics;
using System.Collections.Generic;
using Antlr4.Runtime;
using Antlr4.Runtime.Atn;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using DFA = Antlr4.Runtime.Dfa.DFA;

[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.13.2")]
[System.CLSCompliant(false)]
public partial class Simple_GrammarParser : Parser {
	protected static DFA[] decisionToDFA;
	protected static PredictionContextCache sharedContextCache = new PredictionContextCache();
	public const int
		T__0=1, T__1=2;
	public const int
		RULE_s = 0, RULE_t = 1;
	public static readonly string[] ruleNames = {
		"s", "t"
	};

	private static readonly string[] _LiteralNames = {
		null, "'a'", "'b'"
	};
	private static readonly string[] _SymbolicNames = {
	};
	public static readonly IVocabulary DefaultVocabulary = new Vocabulary(_LiteralNames, _SymbolicNames);

	[NotNull]
	public override IVocabulary Vocabulary
	{
		get
		{
			return DefaultVocabulary;
		}
	}

	public override string GrammarFileName { get { return "Simple_Grammar.g4"; } }

	public override string[] RuleNames { get { return ruleNames; } }

	public override int[] SerializedAtn { get { return _serializedATN; } }

	static Simple_GrammarParser() {
		decisionToDFA = new DFA[_ATN.NumberOfDecisions];
		for (int i = 0; i < _ATN.NumberOfDecisions; i++) {
			decisionToDFA[i] = new DFA(_ATN.GetDecisionState(i), i);
		}
	}

		public Simple_GrammarParser(ITokenStream input) : this(input, Console.Out, Console.Error) { }

		public Simple_GrammarParser(ITokenStream input, TextWriter output, TextWriter errorOutput)
		: base(input, output, errorOutput)
	{
		Interpreter = new ParserATNSimulator(this, _ATN, decisionToDFA, sharedContextCache);
	}

	public partial class SContext : ParserRuleContext {
		[System.Diagnostics.DebuggerNonUserCode] public TContext t() {
			return GetRuleContext<TContext>(0);
		}
		public SContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_s; } }
		[System.Diagnostics.DebuggerNonUserCode]
		public override void EnterRule(IParseTreeListener listener) {
			ISimple_GrammarListener typedListener = listener as ISimple_GrammarListener;
			if (typedListener != null) typedListener.EnterS(this);
		}
		[System.Diagnostics.DebuggerNonUserCode]
		public override void ExitRule(IParseTreeListener listener) {
			ISimple_GrammarListener typedListener = listener as ISimple_GrammarListener;
			if (typedListener != null) typedListener.ExitS(this);
		}
	}

	[RuleVersion(0)]
	public SContext s() {
		SContext _localctx = new SContext(Context, State);
		EnterRule(_localctx, 0, RULE_s);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 4;
			t(0);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class TContext : ParserRuleContext {
		[System.Diagnostics.DebuggerNonUserCode] public TContext t() {
			return GetRuleContext<TContext>(0);
		}
		public TContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_t; } }
		[System.Diagnostics.DebuggerNonUserCode]
		public override void EnterRule(IParseTreeListener listener) {
			ISimple_GrammarListener typedListener = listener as ISimple_GrammarListener;
			if (typedListener != null) typedListener.EnterT(this);
		}
		[System.Diagnostics.DebuggerNonUserCode]
		public override void ExitRule(IParseTreeListener listener) {
			ISimple_GrammarListener typedListener = listener as ISimple_GrammarListener;
			if (typedListener != null) typedListener.ExitT(this);
		}
	}

	[RuleVersion(0)]
	public TContext t() {
		return t(0);
	}

	private TContext t(int _p) {
		ParserRuleContext _parentctx = Context;
		int _parentState = State;
		TContext _localctx = new TContext(Context, _parentState);
		TContext _prevctx = _localctx;
		int _startState = 2;
		EnterRecursionRule(_localctx, 2, RULE_t, _p);
		try {
			int _alt;
			EnterOuterAlt(_localctx, 1);
			{
			State = 10;
			ErrorHandler.Sync(this);
			switch (TokenStream.LA(1)) {
			case T__0:
				{
				State = 7;
				Match(T__0);
				State = 8;
				t(3);
				}
				break;
			case T__1:
				{
				State = 9;
				Match(T__1);
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
			Context.Stop = TokenStream.LT(-1);
			State = 16;
			ErrorHandler.Sync(this);
			_alt = Interpreter.AdaptivePredict(TokenStream,1,Context);
			while ( _alt!=2 && _alt!=global::Antlr4.Runtime.Atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1 ) {
					if ( ParseListeners!=null )
						TriggerExitRuleEvent();
					_prevctx = _localctx;
					{
					{
					_localctx = new TContext(_parentctx, _parentState);
					PushNewRecursionContext(_localctx, _startState, RULE_t);
					State = 12;
					if (!(Precpred(Context, 2))) throw new FailedPredicateException(this, "Precpred(Context, 2)");
					State = 13;
					Match(T__1);
					}
					} 
				}
				State = 18;
				ErrorHandler.Sync(this);
				_alt = Interpreter.AdaptivePredict(TokenStream,1,Context);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			UnrollRecursionContexts(_parentctx);
		}
		return _localctx;
	}

	public override bool Sempred(RuleContext _localctx, int ruleIndex, int predIndex) {
		switch (ruleIndex) {
		case 1: return t_sempred((TContext)_localctx, predIndex);
		}
		return true;
	}
	private bool t_sempred(TContext _localctx, int predIndex) {
		switch (predIndex) {
		case 0: return Precpred(Context, 2);
		}
		return true;
	}

	private static int[] _serializedATN = {
		4,1,2,20,2,0,7,0,2,1,7,1,1,0,1,0,1,1,1,1,1,1,1,1,3,1,11,8,1,1,1,1,1,5,
		1,15,8,1,10,1,12,1,18,9,1,1,1,0,1,2,2,0,2,0,0,19,0,4,1,0,0,0,2,10,1,0,
		0,0,4,5,3,2,1,0,5,1,1,0,0,0,6,7,6,1,-1,0,7,8,5,1,0,0,8,11,3,2,1,3,9,11,
		5,2,0,0,10,6,1,0,0,0,10,9,1,0,0,0,11,16,1,0,0,0,12,13,10,2,0,0,13,15,5,
		2,0,0,14,12,1,0,0,0,15,18,1,0,0,0,16,14,1,0,0,0,16,17,1,0,0,0,17,3,1,0,
		0,0,18,16,1,0,0,0,2,10,16
	};

	public static readonly ATN _ATN =
		new ATNDeserializer().Deserialize(_serializedATN);


}
