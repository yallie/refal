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
	const int _Else = 14;
	const int _Semicolon = 15;
	const int _LBracket = 16;
	const int _LParen = 17;
	const int _LBrace = 18;
	const int _RBracket = 19;
	const int _RParen = 20;
	const int _RBrace = 21;
	const int maxT = 24;

	const bool T = true;
	const bool x = false;
	const int minErrDist = 2;

	public static Token t;    // last recognized token
	public static Token la;   // lookahead token
	static int errDist = minErrDist;

static bool IsPattern()
	{
		Scanner.ResetPeek();
		Token tok = Scanner.Peek();
		return la.kind == _Semicolon && tok.kind != _RBrace;
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
		Function();
		while (la.kind == 1) {
			Function();
		}
	}

	static void Function() {
		Expect(1);
		Expect(18);
		Pattern();
		while (IsPattern()) {
			Expect(15);
			Pattern();
		}
		if (la.kind == 15) {
			Get();
		}
		Expect(21);
	}

	static void Pattern() {
		if (la.kind == 22) {
			LExpression();
		}
		Expect(6);
		if (la.kind == 23) {
			RExpression();
		}
	}

	static void LExpression() {
		Expect(22);
	}

	static void RExpression() {
		Expect(23);
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
		{T,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x}

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
			case 14: s = "Else expected"; break;
			case 15: s = "Semicolon expected"; break;
			case 16: s = "LBracket expected"; break;
			case 17: s = "LParen expected"; break;
			case 18: s = "LBrace expected"; break;
			case 19: s = "RBracket expected"; break;
			case 20: s = "RParen expected"; break;
			case 21: s = "RBrace expected"; break;
			case 22: s = "\"e.1\" expected"; break;
			case 23: s = "\"<>\" expected"; break;
			case 24: s = "??? expected"; break;

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