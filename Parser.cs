using System.Collections;
using System.CodeDom;

using System;

namespace Refal {



public class Parser {
	const int _EOF = 0;
	const int _Identifier = 1;
	const int _String = 2;
	const int _Character = 3;
	const int _Integer = 4;
	const int _Real = 5;
	const int _Equal = 6;
	const int _Bool = 7;
	const int _Char = 8;
	const int _Float = 9;
	const int _Int = 10;
	const int _Null = 11;
	const int _Object = 12;
	const int _Str = 13;
	const int _True = 14;
	const int _False = 15;
	const int _Else = 16;
	const int _Entry = 17;
	const int _Extern = 18;
	const int _Semicolon = 19;
	const int _LBracket = 20;
	const int _LParen = 21;
	const int _LBrace = 22;
	const int _LEval = 23;
	const int _RBracket = 24;
	const int _RParen = 25;
	const int _RBrace = 26;
	const int _REval = 27;
	const int maxT = 34;

	const bool T = true;
	const bool x = false;
	const int minErrDist = 2;

	public static Token t;    // last recognized token
	public static Token la;   // lookahead token
	static int errDist = minErrDist;

static bool IsSentence()
	{
		Scanner.ResetPeek();
		Token tok = Scanner.Peek();
		return la.kind == _Semicolon && tok.kind != _RBrace;
	}

/*-------------------------------------------------------------------------*/
/*	AST built by compiler                                                   */
/*-------------------------------------------------------------------------*/

	static CodeBuilder cb = new CodeBuilder();
	public static Program Program
	{
		get { return cb.Program; }
	}

/*-------------------------------------------------------------------------*/
/* scanner and parser                                                      */
/*-------------------------------------------------------------------------*/



	static void SynErr (int n) {
		if (errDist >= minErrDist) Errors.SynErr(la.line, la.col, n);
		errDist = 0;
	}

	public static void SemErr (string msg) {
		if (errDist >= minErrDist) Errors.Error(t.line, t.col, msg);
		errDist = 0;
	}
	
	static void Get () {
		for (;;) {
			t = la;
			la = Scanner.Scan();
			if (la.kind <= maxT) { ++errDist; break; }

			la = t;
		}
	}
	
	static void Expect (int n) {
		if (la.kind==n) Get(); else { SynErr(n); }
	}
	
	static bool StartOf (int s) {
		return set[s, la.kind];
	}
	
	static void ExpectWeak (int n, int follow) {
		if (la.kind == n) Get();
		else {
			SynErr(n);
			while (!StartOf(follow)) Get();
		}
	}
	
	static bool WeakSeparator (int n, int syFol, int repFol) {
		bool[] s = new bool[maxT+1];
		if (la.kind == n) { Get(); return true; }
		else if (StartOf(repFol)) return false;
		else {
			for (int i=0; i <= maxT; i++) {
				s[i] = set[syFol, i] || set[repFol, i] || set[0, i];
			}
			SynErr(n);
			while (!s[la.kind]) Get();
			return StartOf(syFol);
		}
	}
	
	static void Refal() {
		Definition();
		while (la.kind == 1 || la.kind == 17 || la.kind == 18) {
			Definition();
		}
	}

	static void Definition() {
		if (la.kind == 1 || la.kind == 17) {
			Function();
		} else if (la.kind == 18) {
			External();
		} else SynErr(35);
	}

	static void Function() {
		cb.BeginFunction(); 
		Block block; 
		if (la.kind == 17) {
			Get();
			cb.MarkFunctionAsPublic(); 
		}
		Expect(1);
		cb.SetFunctionName(t.val); 
		Block(out block);
		if (la.kind == 19) {
			Get();
		}
		cb.EndFunction(block); 
	}

	static void External() {
		Expect(18);
		Expect(1);
		cb.AddExternalFunction(t.val); 
		while (la.kind == 28) {
			Get();
			Expect(1);
			cb.AddExternalFunction(t.val); 
		}
		Expect(19);
	}

	static void Block(out Block block) {
		block = new Block();
		Sentence sentence; 
		Expect(22);
		Sentence(out sentence);
		block.Sentences.Add(sentence); 
		while (IsSentence()) {
			Expect(19);
			Sentence(out sentence);
			block.Sentences.Add(sentence); 
		}
		if (la.kind == 19) {
			Get();
		}
		Expect(26);
	}

	static void Sentence(out Sentence sentence) {
		sentence = new Sentence();
		Pattern pattern;
		Expression expression;
		Conditions conditions; 
		Pattern(out pattern);
		sentence.Pattern = pattern; 
		if (la.kind == 6) {
			Get();
			Expression(out expression);
			sentence.Expression = expression; 
		} else if (la.kind == 28 || la.kind == 29) {
			WhereOrWithClause(out conditions, out expression);
			sentence.Conditions = conditions; 
			   sentence.Expression = expression; 
		} else SynErr(36);
	}

	static void Pattern(out Pattern pattern) {
		pattern = new Pattern(); 
		Variable variable;
		Symbol symbol;
		Pattern innerPattern; 
		while (StartOf(1)) {
			if (la.kind == 31 || la.kind == 32 || la.kind == 33) {
				Variable(out variable);
				pattern.Terms.Add(variable); 
			} else if (StartOf(2)) {
				Symbol(out symbol);
				pattern.Terms.Add(symbol); 
			} else {
				Get();
				Pattern(out innerPattern);
				Expect(25);
				pattern.Terms.Add(new PatternInParentheses(innerPattern)); 
			}
		}
	}

	static void Expression(out Expression expression) {
		expression = new Expression();
		Variable variable; 
		Symbol symbol; 
		FunctionCall call; 
		Expression innerExpression; 
		while (StartOf(3)) {
			if (la.kind == 23) {
				Call(out call);
				expression.Terms.Add(call); 
			} else if (la.kind == 31 || la.kind == 32 || la.kind == 33) {
				Variable(out variable);
				// expression can not contain free variables
				variable.IsBound = true;
				expression.Terms.Add(variable); 
			} else if (StartOf(2)) {
				Symbol(out symbol);
				expression.Terms.Add(symbol); 
			} else {
				Get();
				Expression(out innerExpression);
				Expect(25);
				expression.Terms.Add(new ExpressionInParentheses(innerExpression)); 
			}
		}
	}

	static void WhereOrWithClause(out Conditions conditions,
out Expression expression) {
		conditions = new Conditions();
		 expression = null;
		 Block block; Pattern pattern;
		 Expression condExpression;
		 Conditions moreConditions; 
		if (la.kind == 28) {
			Get();
		} else if (la.kind == 29) {
			Get();
		} else SynErr(37);
		Expression(out condExpression);
		Expect(30);
		conditions.Expression = condExpression; 
		if (la.kind == 22) {
			Block(out block);
			conditions.Block = block; 
		} else if (StartOf(4)) {
			Pattern(out pattern);
			conditions.Pattern = pattern; 
			if (la.kind == 6) {
				Get();
				Expression(out expression);
			} else if (la.kind == 28 || la.kind == 29) {
				WhereOrWithClause(out moreConditions, out expression);
				conditions.MoreConditions = moreConditions; 
			} else SynErr(38);
		} else SynErr(39);
	}

	static void Variable(out Variable variable) {
		variable = null; 
		if (la.kind == 31) {
			Get();
			variable = new ExpressionVariable(); 
		} else if (la.kind == 32) {
			Get();
			variable = new SymbolVariable(); 
		} else if (la.kind == 33) {
			Get();
			variable = new TermVariable(); 
		} else SynErr(40);
		if (la.kind == 4) {
			Get();
		} else if (la.kind == 1) {
			Get();
		} else SynErr(41);
		if (variable != null) variable.Index = t.val; 
	}

	static void Symbol(out Symbol symbol) {
		symbol = null; 
		switch (la.kind) {
		case 2: {
			Get();
			symbol = new CompoundSymbol(t.val); 
			break;
		}
		case 3: {
			Get();
			symbol = new Character(t.val); 
			break;
		}
		case 4: {
			Get();
			symbol = new Macrodigit(Convert.ToInt32(t.val)); 
			break;
		}
		case 14: {
			Get();
			symbol = new TrueIdentifier(t.val); 
			break;
		}
		case 15: {
			Get();
			symbol = new FalseIdentifier(t.val); 
			break;
		}
		case 1: {
			Get();
			symbol = new Identifier(t.val); 
			break;
		}
		default: SynErr(42); break;
		}
	}

	static void Call(out FunctionCall call) {
		call = new FunctionCall(); 
		Expression expression; 
		Expect(23);
		Expect(1);
		call.FunctionName = t.val; 
		Expression(out expression);
		Expect(27);
		call.Expression = expression; 
	}



	public static void Parse() {
		la = new Token();
		la.val = "";		
		Get();
		Refal();

    Expect(0);
    Buffer.Close();
	}

	static bool[,] set = {
		{T,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x},
		{x,T,T,T, T,x,x,x, x,x,x,x, x,x,T,T, x,x,x,x, x,T,x,x, x,x,x,x, x,x,x,T, T,T,x,x},
		{x,T,T,T, T,x,x,x, x,x,x,x, x,x,T,T, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x},
		{x,T,T,T, T,x,x,x, x,x,x,x, x,x,T,T, x,x,x,x, x,T,x,T, x,x,x,x, x,x,x,T, T,T,x,x},
		{x,T,T,T, T,x,T,x, x,x,x,x, x,x,T,T, x,x,x,x, x,T,x,x, x,x,x,x, T,T,x,T, T,T,x,x}

	};
} // end Parser


public class Errors {
	public static int count = 0;                                    // number of errors detected
  public static string errMsgFormat = "-- line {0} col {1}: {2}"; // 0=line, 1=column, 2=text
	
	public static void SynErr (int line, int col, int n) {
		string s;
		switch (n) {
			case 0: s = "EOF expected"; break;
			case 1: s = "Identifier expected"; break;
			case 2: s = "String expected"; break;
			case 3: s = "Character expected"; break;
			case 4: s = "Integer expected"; break;
			case 5: s = "Real expected"; break;
			case 6: s = "Equal expected"; break;
			case 7: s = "Bool expected"; break;
			case 8: s = "Char expected"; break;
			case 9: s = "Float expected"; break;
			case 10: s = "Int expected"; break;
			case 11: s = "Null expected"; break;
			case 12: s = "Object expected"; break;
			case 13: s = "Str expected"; break;
			case 14: s = "True expected"; break;
			case 15: s = "False expected"; break;
			case 16: s = "Else expected"; break;
			case 17: s = "Entry expected"; break;
			case 18: s = "Extern expected"; break;
			case 19: s = "Semicolon expected"; break;
			case 20: s = "LBracket expected"; break;
			case 21: s = "LParen expected"; break;
			case 22: s = "LBrace expected"; break;
			case 23: s = "LEval expected"; break;
			case 24: s = "RBracket expected"; break;
			case 25: s = "RParen expected"; break;
			case 26: s = "RBrace expected"; break;
			case 27: s = "REval expected"; break;
			case 28: s = "\",\" expected"; break;
			case 29: s = "\"&\" expected"; break;
			case 30: s = "\":\" expected"; break;
			case 31: s = "\"e.\" expected"; break;
			case 32: s = "\"s.\" expected"; break;
			case 33: s = "\"t.\" expected"; break;
			case 34: s = "??? expected"; break;
			case 35: s = "invalid Definition"; break;
			case 36: s = "invalid Sentence"; break;
			case 37: s = "invalid WhereOrWithClause"; break;
			case 38: s = "invalid WhereOrWithClause"; break;
			case 39: s = "invalid WhereOrWithClause"; break;
			case 40: s = "invalid Variable"; break;
			case 41: s = "invalid Variable"; break;
			case 42: s = "invalid Symbol"; break;

			default: s = "error " + n; break;
		}
		Console.WriteLine(Errors.errMsgFormat, line, col, s);
		count++;
	}

	public static void SemErr (int line, int col, int n) {
		Console.WriteLine(errMsgFormat, line, col, ("error " + n));
		count++;
	}

	public static void Error (int line, int col, string s) {
		Console.WriteLine(errMsgFormat, line, col, s);
		count++;
	}

	public static void Exception (string s) {
		Console.WriteLine(s); 
		System.Environment.Exit(1);
	}
} // Errors

}