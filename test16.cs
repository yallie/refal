
using System;
using System.Collections;

namespace Refal.Runtime
{
	public class Program : RefalBase
	{
		static void Main()
		{
			Go(new PassiveExpression());
		}

		public static PassiveExpression Go(PassiveExpression expression)
		{
			Pattern pattern1 = new Pattern();
			if (RefalBase.Match(expression, pattern1))
			{
				return PassiveExpression.Build(Introduction(PassiveExpression.Build()), Loop(PassiveExpression.Build()));
			};

			throw new RecognitionImpossibleException("Recognition impossible");
		}

		private static PassiveExpression Introduction(PassiveExpression expression)
		{
			Pattern pattern2 = new Pattern();
			if (RefalBase.Match(expression, pattern2))
			{
				return PassiveExpression.Build(Prout(PassiveExpression.Build("This is a program to translate an arithmetic expression".ToCharArray())), Prout(PassiveExpression.Build("into a code for a one-address computer. Primary operands".ToCharArray())), Prout(PassiveExpression.Build("are identifiers and whole numbers. Operations are:".ToCharArray())), Prout(PassiveExpression.Build("+, -, *, /, ^with usual priorities. Parentheses as usual.".ToCharArray())), Prout(PassiveExpression.Build("Example: Joe^2*5/(SUM + 318)".ToCharArray())), Prout(PassiveExpression.Build()));
			};

			throw new RecognitionImpossibleException("Recognition impossible");
		}

		private static PassiveExpression Invitation(PassiveExpression expression)
		{
			Pattern pattern3 = new Pattern();
			if (RefalBase.Match(expression, pattern3))
			{
				return PassiveExpression.Build(Prout(PassiveExpression.Build()), Prout(PassiveExpression.Build()), Prout(PassiveExpression.Build("Type in an expression (one line), or Ctrl-Z to terminate".ToCharArray())));
			};

			throw new RecognitionImpossibleException("Recognition impossible");
		}

		private static PassiveExpression Loop(PassiveExpression expression)
		{
			Pattern pattern4 = new Pattern();
			if (RefalBase.Match(expression, pattern4))
			{
				return PassiveExpression.Build(Nil(PassiveExpression.Build(Dgall(PassiveExpression.Build()))), Invitation(PassiveExpression.Build()), Inout(PassiveExpression.Build(Card(PassiveExpression.Build()))));
			};

			throw new RecognitionImpossibleException("Recognition impossible");
		}

		private static PassiveExpression Nil(PassiveExpression expression)
		{
			Pattern pattern5 = new Pattern(new ExpressionVariable("e.X"));
			if (RefalBase.Match(expression, pattern5))
			{
				return PassiveExpression.Build();
			};

			throw new RecognitionImpossibleException("Recognition impossible");
		}

		private static PassiveExpression Inout(PassiveExpression expression)
		{
			Pattern pattern6 = new Pattern(0);
			if (RefalBase.Match(expression, pattern6))
			{
				return PassiveExpression.Build(Prout(PassiveExpression.Build("End of session".ToCharArray())));
			};

			Pattern pattern7 = new Pattern(new ExpressionVariable("e.X"));
			if (RefalBase.Match(expression, pattern7))
			{
				return PassiveExpression.Build(Out(PassiveExpression.Build(Translate(PassiveExpression.Build(Lex(PassiveExpression.Build(new OpeningBrace(), "$".ToCharArray(), new ClosingBrace(), pattern7.GetVariable("e.X"))))))));
			};

			throw new RecognitionImpossibleException("Recognition impossible");
		}

		private static PassiveExpression Out(PassiveExpression expression)
		{
			Pattern pattern8 = new Pattern();
			if (RefalBase.Match(expression, pattern8))
			{
				return PassiveExpression.Build(Loop(PassiveExpression.Build()));
			};

			Pattern pattern9 = new Pattern(new ExpressionVariable("e.1"));
			if (RefalBase.Match(expression, pattern9))
			{
				return PassiveExpression.Build(Prout(PassiveExpression.Build("The translation is:".ToCharArray())), Write(PassiveExpression.Build(pattern9.GetVariable("e.1"))), Loop(PassiveExpression.Build()));
			};

			throw new RecognitionImpossibleException("Recognition impossible");
		}

		private static PassiveExpression Lex(PassiveExpression expression)
		{
			Pattern pattern10 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), "(".ToCharArray(), new ExpressionVariable("e.2"));
			if (RefalBase.Match(expression, pattern10))
			{
				return PassiveExpression.Build(Lex(PassiveExpression.Build(new OpeningBrace(), new OpeningBrace(), pattern10.GetVariable("e.1"), new ClosingBrace(), new ClosingBrace(), pattern10.GetVariable("e.2"))));
			};

			Pattern pattern11 = new Pattern(new OpeningBrace(), new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), new ExpressionVariable("e.2"), new ClosingBrace(), ")".ToCharArray(), new ExpressionVariable("e.3"));
			if (RefalBase.Match(expression, pattern11))
			{
				return PassiveExpression.Build(Lex(PassiveExpression.Build(new OpeningBrace(), pattern11.GetVariable("e.1"), new OpeningBrace(), pattern11.GetVariable("e.2"), new ClosingBrace(), new ClosingBrace(), pattern11.GetVariable("e.3"))));
			};

			Pattern pattern12 = new Pattern(new OpeningBrace(), "$".ToCharArray(), new ExpressionVariable("e.1"), new ClosingBrace(), ")".ToCharArray(), new ExpressionVariable("e.3"));
			if (RefalBase.Match(expression, pattern12))
			{
				return PassiveExpression.Build(Prout(PassiveExpression.Build("ERROR: Unpaired right parenthsis:".ToCharArray())), Prout(PassiveExpression.Build(pattern12.GetVariable("e.1"), ")".ToCharArray())));
			};

			Pattern pattern13 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), " ".ToCharArray(), new ExpressionVariable("e.2"));
			if (RefalBase.Match(expression, pattern13))
			{
				return PassiveExpression.Build(Lex(PassiveExpression.Build(new OpeningBrace(), pattern13.GetVariable("e.1"), new ClosingBrace(), pattern13.GetVariable("e.2"))));
			};

			Pattern pattern14 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), "\t".ToCharArray(), new ExpressionVariable("e.2"));
			if (RefalBase.Match(expression, pattern14))
			{
				return PassiveExpression.Build(Lex(PassiveExpression.Build(new OpeningBrace(), pattern14.GetVariable("e.1"), new ClosingBrace(), pattern14.GetVariable("e.2"))));
			};

			Pattern pattern15 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), "\'".ToCharArray(), new ExpressionVariable("e.2"));
			if (RefalBase.Match(expression, pattern15))
			{
				expression = PassiveExpression.Build(Quotes(PassiveExpression.Build("\'".ToCharArray(), new OpeningBrace(), new ClosingBrace(), pattern15.GetVariable("e.2"))));
				{
					Pattern pattern16 = new Pattern(new OpeningBrace(), new ClosingBrace(), new ExpressionVariable("e.3"));
					pattern16.CopyBoundVariables(pattern15);
					if (RefalBase.Match(expression, pattern16))
					{
						return PassiveExpression.Build(Prout(PassiveExpression.Build("ERROR: Unpaired quote \'".ToCharArray(), pattern16.GetVariable("e.2"))));
					};

					Pattern pattern17 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.0"), new ClosingBrace(), new ExpressionVariable("e.3"));
					pattern17.CopyBoundVariables(pattern15);
					if (RefalBase.Match(expression, pattern17))
					{
						return PassiveExpression.Build(Lex(PassiveExpression.Build(new OpeningBrace(), pattern17.GetVariable("e.1"), pattern17.GetVariable("e.0"), new ClosingBrace(), pattern17.GetVariable("e.2"))));
					};

					throw new RecognitionImpossibleException("Recognition impossible");
				}
			};

			Pattern pattern19 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), new SymbolVariable("s.A"), new ExpressionVariable("e.2"));
			if (RefalBase.Match(expression, pattern19))
			{
				expression = PassiveExpression.Build(Type(PassiveExpression.Build(pattern19.GetVariable("s.A"))));
				{
					Pattern pattern20 = new Pattern("L".ToCharArray(), new ExpressionVariable("e.A1"));
					pattern20.CopyBoundVariables(pattern19);
					if (RefalBase.Match(expression, pattern20))
					{
						expression = PassiveExpression.Build(Id__tail(PassiveExpression.Build(new OpeningBrace(), pattern20.GetVariable("s.A"), new ClosingBrace(), pattern20.GetVariable("e.2"))));
						Pattern pattern21 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.Id"), new ClosingBrace(), new ExpressionVariable("e.3"));
						pattern21.CopyBoundVariables(pattern20);
						if (RefalBase.Match(expression, pattern21))
						{
							return PassiveExpression.Build(Lex(PassiveExpression.Build(new OpeningBrace(), pattern21.GetVariable("e.1"), Implode(PassiveExpression.Build(pattern21.GetVariable("e.Id"))), new ClosingBrace(), pattern21.GetVariable("e.3"))));
						}
					};

					Pattern pattern22 = new Pattern("D".ToCharArray(), new ExpressionVariable("e.A1"));
					pattern22.CopyBoundVariables(pattern19);
					if (RefalBase.Match(expression, pattern22))
					{
						expression = PassiveExpression.Build(D__string(PassiveExpression.Build(new OpeningBrace(), pattern22.GetVariable("s.A"), new ClosingBrace(), pattern22.GetVariable("e.2"))));
						Pattern pattern23 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.D-Str"), new ClosingBrace(), new ExpressionVariable("e.3"));
						pattern23.CopyBoundVariables(pattern22);
						if (RefalBase.Match(expression, pattern23))
						{
							return PassiveExpression.Build(Lex(PassiveExpression.Build(new OpeningBrace(), pattern23.GetVariable("e.1"), Numb(PassiveExpression.Build(pattern23.GetVariable("e.D-Str"))), new ClosingBrace(), pattern23.GetVariable("e.3"))));
						}
					};

					Pattern pattern24 = new Pattern(new SymbolVariable("s.T"), new ExpressionVariable("e.A1"));
					pattern24.CopyBoundVariables(pattern19);
					if (RefalBase.Match(expression, pattern24))
					{
						return PassiveExpression.Build(Lex(PassiveExpression.Build(new OpeningBrace(), pattern24.GetVariable("e.1"), pattern24.GetVariable("s.A"), new ClosingBrace(), pattern24.GetVariable("e.2"))));
					};

					throw new RecognitionImpossibleException("Recognition impossible");
				}
			};

			Pattern pattern26 = new Pattern(new OpeningBrace(), "$".ToCharArray(), new ExpressionVariable("e.1"), new ClosingBrace());
			if (RefalBase.Match(expression, pattern26))
			{
				return PassiveExpression.Build(pattern26.GetVariable("e.1"));
			};

			Pattern pattern27 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.M"), new ClosingBrace());
			if (RefalBase.Match(expression, pattern27))
			{
				return PassiveExpression.Build(Prout(PassiveExpression.Build("ERROR: Unpaired left parentheses".ToCharArray())), Pr__lmb(PassiveExpression.Build(pattern27.GetVariable("e.M"))));
			};

			throw new RecognitionImpossibleException("Recognition impossible");
		}

		private static PassiveExpression Quotes(PassiveExpression expression)
		{
			Pattern pattern28 = new Pattern(new SymbolVariable("s.Q"), new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), new ExpressionVariable("e.0"), "\\".ToCharArray(), "\\".ToCharArray(), new ExpressionVariable("e.2"));
			if (RefalBase.Match(expression, pattern28))
			{
				return PassiveExpression.Build(Quotes(PassiveExpression.Build(pattern28.GetVariable("s.Q"), new OpeningBrace(), pattern28.GetVariable("e.1"), pattern28.GetVariable("e.0"), "\\".ToCharArray(), "\\".ToCharArray(), new ClosingBrace(), pattern28.GetVariable("e.2"))));
			};

			Pattern pattern29 = new Pattern(new SymbolVariable("s.Q"), new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), new ExpressionVariable("e.0"), "\\".ToCharArray(), new SymbolVariable("s.Q"), new ExpressionVariable("e.2"));
			if (RefalBase.Match(expression, pattern29))
			{
				return PassiveExpression.Build(Quotes(PassiveExpression.Build(pattern29.GetVariable("s.Q"), new OpeningBrace(), pattern29.GetVariable("e.1"), pattern29.GetVariable("e.0"), "\\".ToCharArray(), pattern29.GetVariable("s.Q"), new ClosingBrace(), pattern29.GetVariable("e.2"))));
			};

			Pattern pattern30 = new Pattern(new SymbolVariable("s.Q"), new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), new ExpressionVariable("e.0"), new SymbolVariable("s.Q"), new ExpressionVariable("e.2"));
			if (RefalBase.Match(expression, pattern30))
			{
				return PassiveExpression.Build(new OpeningBrace(), pattern30.GetVariable("e.1"), pattern30.GetVariable("e.0"), new ClosingBrace(), pattern30.GetVariable("e.2"));
			};

			Pattern pattern31 = new Pattern(new SymbolVariable("s.Q"), new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), new ExpressionVariable("e.2"));
			if (RefalBase.Match(expression, pattern31))
			{
				return PassiveExpression.Build(new OpeningBrace(), new ClosingBrace(), pattern31.GetVariable("e.1"), pattern31.GetVariable("e.2"));
			};

			throw new RecognitionImpossibleException("Recognition impossible");
		}

		private static PassiveExpression Id__tail(PassiveExpression expression)
		{
			Pattern pattern32 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), new SymbolVariable("s.A"), new ExpressionVariable("e.2"));
			if (RefalBase.Match(expression, pattern32))
			{
				expression = PassiveExpression.Build(Type(PassiveExpression.Build(pattern32.GetVariable("s.A"))));
				{
					Pattern pattern33 = new Pattern("L".ToCharArray(), new ExpressionVariable("e.3"));
					pattern33.CopyBoundVariables(pattern32);
					if (RefalBase.Match(expression, pattern33))
					{
						return PassiveExpression.Build(Id__tail(PassiveExpression.Build(new OpeningBrace(), pattern33.GetVariable("e.1"), pattern33.GetVariable("s.A"), new ClosingBrace(), pattern33.GetVariable("e.2"))));
					};

					Pattern pattern34 = new Pattern("D".ToCharArray(), new ExpressionVariable("e.3"));
					pattern34.CopyBoundVariables(pattern32);
					if (RefalBase.Match(expression, pattern34))
					{
						return PassiveExpression.Build(Id__tail(PassiveExpression.Build(new OpeningBrace(), pattern34.GetVariable("e.1"), pattern34.GetVariable("s.A"), new ClosingBrace(), pattern34.GetVariable("e.2"))));
					};

					Pattern pattern35 = new Pattern(new SymbolVariable("s.T"), new ExpressionVariable("e.3"));
					pattern35.CopyBoundVariables(pattern32);
					if (RefalBase.Match(expression, pattern35))
					{
						return PassiveExpression.Build(new OpeningBrace(), pattern35.GetVariable("e.1"), new ClosingBrace(), pattern35.GetVariable("s.A"), pattern35.GetVariable("e.2"));
					};

					throw new RecognitionImpossibleException("Recognition impossible");
				}
			};

			Pattern pattern37 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace());
			if (RefalBase.Match(expression, pattern37))
			{
				return PassiveExpression.Build(new OpeningBrace(), pattern37.GetVariable("e.1"), new ClosingBrace());
			};

			throw new RecognitionImpossibleException("Recognition impossible");
		}

		private static PassiveExpression D__string(PassiveExpression expression)
		{
			Pattern pattern38 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), new SymbolVariable("s.A"), new ExpressionVariable("e.2"));
			if (RefalBase.Match(expression, pattern38))
			{
				expression = PassiveExpression.Build(Type(PassiveExpression.Build(pattern38.GetVariable("s.A"))));
				Pattern pattern39 = new Pattern("D".ToCharArray(), new ExpressionVariable("e.A1"));
				pattern39.CopyBoundVariables(pattern38);
				if (RefalBase.Match(expression, pattern39))
				{
					return PassiveExpression.Build(D__string(PassiveExpression.Build(new OpeningBrace(), pattern39.GetVariable("e.1"), pattern39.GetVariable("s.A"), new ClosingBrace(), pattern39.GetVariable("e.2"))));
				}
			};

			Pattern pattern40 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), new ExpressionVariable("e.2"));
			if (RefalBase.Match(expression, pattern40))
			{
				return PassiveExpression.Build(new OpeningBrace(), pattern40.GetVariable("e.1"), new ClosingBrace(), pattern40.GetVariable("e.2"));
			};

			throw new RecognitionImpossibleException("Recognition impossible");
		}

		private static PassiveExpression Pr__lmb(PassiveExpression expression)
		{
			Pattern pattern41 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), new ExpressionVariable("e.2"));
			if (RefalBase.Match(expression, pattern41))
			{
				return PassiveExpression.Build(Pr__lmb(PassiveExpression.Build(pattern41.GetVariable("e.1"))), Prout(PassiveExpression.Build("(".ToCharArray(), pattern41.GetVariable("e.2"))));
			};

			Pattern pattern42 = new Pattern("$".ToCharArray(), new ExpressionVariable("e.1"));
			if (RefalBase.Match(expression, pattern42))
			{
				return PassiveExpression.Build();
			};

			throw new RecognitionImpossibleException("Recognition impossible");
		}

		private static PassiveExpression Translate(PassiveExpression expression)
		{
			Pattern pattern43 = new Pattern();
			if (RefalBase.Match(expression, pattern43))
			{
				return PassiveExpression.Build();
			};

			Pattern pattern44 = new Pattern(new ExpressionVariable("e.1"));
			if (RefalBase.Match(expression, pattern44))
			{
				return PassiveExpression.Build(Code__gen(PassiveExpression.Build(new OpeningBrace(), 1, new ClosingBrace(), Parse(PassiveExpression.Build(pattern44.GetVariable("e.1"))), Dg(PassiveExpression.Build("compl".ToCharArray())))));
			};

			throw new RecognitionImpossibleException("Recognition impossible");
		}

		private static PassiveExpression Last1(PassiveExpression expression)
		{
			Pattern pattern45 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.A"), new SymbolVariable("s.X"), new ExpressionVariable("e.B"), new ClosingBrace(), new ExpressionVariable("e.1"), new SymbolVariable("s.X"), new OpeningBrace(), new ExpressionVariable("e.2"), new ClosingBrace());
			if (RefalBase.Match(expression, pattern45))
			{
				return PassiveExpression.Build(pattern45.GetVariable("e.1"), pattern45.GetVariable("s.X"), new OpeningBrace(), pattern45.GetVariable("e.2"), new ClosingBrace());
			};

			Pattern pattern46 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.A"), new ClosingBrace(), new ExpressionVariable("e.1"), new TermVariable("t.X"), new OpeningBrace(), new ExpressionVariable("e.2"), new ClosingBrace());
			if (RefalBase.Match(expression, pattern46))
			{
				return PassiveExpression.Build(Last1(PassiveExpression.Build(new OpeningBrace(), pattern46.GetVariable("e.A"), new ClosingBrace(), pattern46.GetVariable("e.1"), new OpeningBrace(), pattern46.GetVariable("t.X"), pattern46.GetVariable("e.2"), new ClosingBrace())));
			};

			Pattern pattern47 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.A"), new ClosingBrace(), new OpeningBrace(), new ExpressionVariable("e.2"), new ClosingBrace());
			if (RefalBase.Match(expression, pattern47))
			{
				return PassiveExpression.Build(new OpeningBrace(), pattern47.GetVariable("e.2"), new ClosingBrace());
			};

			throw new RecognitionImpossibleException("Recognition impossible");
		}

		private static PassiveExpression Parse(PassiveExpression expression)
		{
			Pattern pattern48 = new Pattern(new ExpressionVariable("e.Exp"));
			if (RefalBase.Match(expression, pattern48))
			{
				expression = PassiveExpression.Build(Last1(PassiveExpression.Build(new OpeningBrace(), "+-".ToCharArray(), new ClosingBrace(), pattern48.GetVariable("e.Exp"), new OpeningBrace(), new ClosingBrace())));
				Pattern pattern49 = new Pattern(new ExpressionVariable("e.1"), new SymbolVariable("s.Op"), new OpeningBrace(), new ExpressionVariable("e.2"), new ClosingBrace());
				pattern49.CopyBoundVariables(pattern48);
				if (RefalBase.Match(expression, pattern49))
				{
					return PassiveExpression.Build(pattern49.GetVariable("s.Op"), new OpeningBrace(), Parse(PassiveExpression.Build(pattern49.GetVariable("e.1"))), new ClosingBrace(), Parse(PassiveExpression.Build(pattern49.GetVariable("e.2"))));
				}
			};

			Pattern pattern50 = new Pattern(new ExpressionVariable("e.Exp"));
			if (RefalBase.Match(expression, pattern50))
			{
				expression = PassiveExpression.Build(Last1(PassiveExpression.Build(new OpeningBrace(), "*/".ToCharArray(), new ClosingBrace(), pattern50.GetVariable("e.Exp"), new OpeningBrace(), new ClosingBrace())));
				Pattern pattern51 = new Pattern(new ExpressionVariable("e.1"), new SymbolVariable("s.Op"), new OpeningBrace(), new ExpressionVariable("e.2"), new ClosingBrace());
				pattern51.CopyBoundVariables(pattern50);
				if (RefalBase.Match(expression, pattern51))
				{
					return PassiveExpression.Build(pattern51.GetVariable("s.Op"), new OpeningBrace(), Parse(PassiveExpression.Build(pattern51.GetVariable("e.1"))), new ClosingBrace(), Parse(PassiveExpression.Build(pattern51.GetVariable("e.2"))));
				}
			};

			Pattern pattern52 = new Pattern(new ExpressionVariable("e.1"), "^".ToCharArray(), new ExpressionVariable("e.2"));
			if (RefalBase.Match(expression, pattern52))
			{
				return PassiveExpression.Build("^".ToCharArray(), new OpeningBrace(), Parse(PassiveExpression.Build(pattern52.GetVariable("e.1"))), new ClosingBrace(), Parse(PassiveExpression.Build(pattern52.GetVariable("e.2"))));
			};

			Pattern pattern53 = new Pattern(new SymbolVariable("s.Symb"));
			if (RefalBase.Match(expression, pattern53))
			{
				expression = PassiveExpression.Build(Type(PassiveExpression.Build(pattern53.GetVariable("s.Symb"))));
				Pattern pattern54 = new Pattern("Wi".ToCharArray(), new ExpressionVariable("e.S"));
				pattern54.CopyBoundVariables(pattern53);
				if (RefalBase.Match(expression, pattern54))
				{
					return PassiveExpression.Build(pattern54.GetVariable("s.Symb"));
				}
			};

			Pattern pattern55 = new Pattern(new SymbolVariable("s.Symb"));
			if (RefalBase.Match(expression, pattern55))
			{
				expression = PassiveExpression.Build(Type(PassiveExpression.Build(pattern55.GetVariable("s.Symb"))));
				Pattern pattern56 = new Pattern("N".ToCharArray(), new ExpressionVariable("e.S"));
				pattern56.CopyBoundVariables(pattern55);
				if (RefalBase.Match(expression, pattern56))
				{
					return PassiveExpression.Build(pattern56.GetVariable("s.Symb"));
				}
			};

			Pattern pattern57 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.Exp"), new ClosingBrace());
			if (RefalBase.Match(expression, pattern57))
			{
				return PassiveExpression.Build(Parse(PassiveExpression.Build(pattern57.GetVariable("e.Exp"))));
			};

			Pattern pattern58 = new Pattern();
			if (RefalBase.Match(expression, pattern58))
			{
				return PassiveExpression.Build();
			};

			Pattern pattern59 = new Pattern(new ExpressionVariable("e.Exp"));
			if (RefalBase.Match(expression, pattern59))
			{
				return PassiveExpression.Build(Prout(PassiveExpression.Build("Syntax error. Cannot parse ".ToCharArray(), pattern59.GetVariable("e.Exp"))), Br(PassiveExpression.Build("compl=fail".ToCharArray())));
			};

			throw new RecognitionImpossibleException("Recognition impossible");
		}

		private static PassiveExpression Code__gen(PassiveExpression expression)
		{
			Pattern pattern60 = new Pattern(new ExpressionVariable("e.1"), "fail".ToCharArray());
			if (RefalBase.Match(expression, pattern60))
			{
				return PassiveExpression.Build();
			};

			Pattern pattern61 = new Pattern(new OpeningBrace(), new SymbolVariable("s.N"), new ClosingBrace(), "-".ToCharArray(), new OpeningBrace(), new ClosingBrace(), new ExpressionVariable("e.2"));
			if (RefalBase.Match(expression, pattern61))
			{
				return PassiveExpression.Build(Code__gen(PassiveExpression.Build(new OpeningBrace(), pattern61.GetVariable("s.N"), new ClosingBrace(), pattern61.GetVariable("e.2"))), "Minus ;".ToCharArray());
			};

			Pattern pattern62 = new Pattern(new OpeningBrace(), new SymbolVariable("s.N"), new ClosingBrace(), new SymbolVariable("s.Op"), new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), new SymbolVariable("s.2"));
			if (RefalBase.Match(expression, pattern62))
			{
				return PassiveExpression.Build(Code__gen(PassiveExpression.Build(new OpeningBrace(), pattern62.GetVariable("s.N"), new ClosingBrace(), pattern62.GetVariable("e.1"))), Code__op(PassiveExpression.Build(pattern62.GetVariable("s.Op"))), Outform(PassiveExpression.Build(pattern62.GetVariable("s.2"))), ";".ToCharArray());
			};

			Pattern pattern63 = new Pattern(new OpeningBrace(), new SymbolVariable("s.N"), new ClosingBrace(), "+".ToCharArray(), new OpeningBrace(), new SymbolVariable("s.1"), new ClosingBrace(), new ExpressionVariable("e.2"));
			if (RefalBase.Match(expression, pattern63))
			{
				return PassiveExpression.Build(Code__gen(PassiveExpression.Build(new OpeningBrace(), pattern63.GetVariable("s.N"), new ClosingBrace(), pattern63.GetVariable("e.2"))), Code__op(PassiveExpression.Build("+".ToCharArray())), Outform(PassiveExpression.Build(pattern63.GetVariable("s.1"))), ";".ToCharArray());
			};

			Pattern pattern64 = new Pattern(new OpeningBrace(), new SymbolVariable("s.N"), new ClosingBrace(), "*".ToCharArray(), new OpeningBrace(), new SymbolVariable("s.1"), new ClosingBrace(), new ExpressionVariable("e.2"));
			if (RefalBase.Match(expression, pattern64))
			{
				return PassiveExpression.Build(Code__gen(PassiveExpression.Build(new OpeningBrace(), pattern64.GetVariable("s.N"), new ClosingBrace(), pattern64.GetVariable("e.2"))), Code__op(PassiveExpression.Build("*".ToCharArray())), Outform(PassiveExpression.Build(pattern64.GetVariable("s.1"))), ";".ToCharArray());
			};

			Pattern pattern65 = new Pattern(new OpeningBrace(), new SymbolVariable("s.N"), new ClosingBrace(), new SymbolVariable("s.Op"), new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), new ExpressionVariable("e.2"));
			if (RefalBase.Match(expression, pattern65))
			{
				return PassiveExpression.Build(Code__gen(PassiveExpression.Build(new OpeningBrace(), pattern65.GetVariable("s.N"), new ClosingBrace(), pattern65.GetVariable("e.2"))), "STORE R+".ToCharArray(), Symb(PassiveExpression.Build(pattern65.GetVariable("s.N"))), ";".ToCharArray(), Code__gen(PassiveExpression.Build(new OpeningBrace(), Add(PassiveExpression.Build(new OpeningBrace(), pattern65.GetVariable("s.N"), new ClosingBrace(), 1)), new ClosingBrace(), pattern65.GetVariable("e.1"))), Code__op(PassiveExpression.Build(pattern65.GetVariable("s.Op"))), "R+".ToCharArray(), Symb(PassiveExpression.Build(pattern65.GetVariable("s.N"))), ";".ToCharArray());
			};

			Pattern pattern66 = new Pattern(new OpeningBrace(), new SymbolVariable("s.N"), new ClosingBrace(), new SymbolVariable("s.Symb"));
			if (RefalBase.Match(expression, pattern66))
			{
				return PassiveExpression.Build("LOAD ".ToCharArray(), Outform(PassiveExpression.Build(pattern66.GetVariable("s.Symb"))), ";".ToCharArray());
			};

			Pattern pattern67 = new Pattern(new OpeningBrace(), new SymbolVariable("s.N"), new ClosingBrace(), new ExpressionVariable("e.X"));
			if (RefalBase.Match(expression, pattern67))
			{
				return PassiveExpression.Build(new OpeningBrace(), "Syntax error".ToCharArray(), new ClosingBrace(), ";".ToCharArray());
			};

			throw new RecognitionImpossibleException("Recognition impossible");
		}

		private static PassiveExpression Outform(PassiveExpression expression)
		{
			Pattern pattern68 = new Pattern(new SymbolVariable("s.S"));
			if (RefalBase.Match(expression, pattern68))
			{
				expression = PassiveExpression.Build(Type(PassiveExpression.Build(pattern68.GetVariable("s.S"))));
				{
					Pattern pattern69 = new Pattern("Wi".ToCharArray(), new ExpressionVariable("e.S1"));
					pattern69.CopyBoundVariables(pattern68);
					if (RefalBase.Match(expression, pattern69))
					{
						return PassiveExpression.Build(Explode(PassiveExpression.Build(pattern69.GetVariable("s.S"))));
					};

					Pattern pattern70 = new Pattern("N".ToCharArray(), new ExpressionVariable("e.S1"));
					pattern70.CopyBoundVariables(pattern68);
					if (RefalBase.Match(expression, pattern70))
					{
						return PassiveExpression.Build(Symb(PassiveExpression.Build(pattern70.GetVariable("s.S"))));
					};

					throw new RecognitionImpossibleException("Recognition impossible");
				}
			};

			throw new RecognitionImpossibleException("Recognition impossible");
		}

		private static PassiveExpression Code__op(PassiveExpression expression)
		{
			Pattern pattern72 = new Pattern("+".ToCharArray());
			if (RefalBase.Match(expression, pattern72))
			{
				return PassiveExpression.Build("ADD ".ToCharArray());
			};

			Pattern pattern73 = new Pattern("-".ToCharArray());
			if (RefalBase.Match(expression, pattern73))
			{
				return PassiveExpression.Build("SUB ".ToCharArray());
			};

			Pattern pattern74 = new Pattern("*".ToCharArray());
			if (RefalBase.Match(expression, pattern74))
			{
				return PassiveExpression.Build("MUL ".ToCharArray());
			};

			Pattern pattern75 = new Pattern("/".ToCharArray());
			if (RefalBase.Match(expression, pattern75))
			{
				return PassiveExpression.Build("DIV ".ToCharArray());
			};

			Pattern pattern76 = new Pattern("^".ToCharArray());
			if (RefalBase.Match(expression, pattern76))
			{
				return PassiveExpression.Build("POW ".ToCharArray());
			};

			throw new RecognitionImpossibleException("Recognition impossible");
		}

		private static PassiveExpression Write(PassiveExpression expression)
		{
			Pattern pattern77 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), ";".ToCharArray(), new ExpressionVariable("e.2"));
			if (RefalBase.Match(expression, pattern77))
			{
				return PassiveExpression.Build(Prout(PassiveExpression.Build(pattern77.GetVariable("e.1"))));
			};

			Pattern pattern78 = new Pattern(new ExpressionVariable("e.1"), ";".ToCharArray(), new ExpressionVariable("e.2"));
			if (RefalBase.Match(expression, pattern78))
			{
				return PassiveExpression.Build(Prout(PassiveExpression.Build("      ".ToCharArray(), pattern78.GetVariable("e.1"))), Write(PassiveExpression.Build(pattern78.GetVariable("e.2"))));
			};

			Pattern pattern79 = new Pattern();
			if (RefalBase.Match(expression, pattern79))
			{
				return PassiveExpression.Build();
			};

			throw new RecognitionImpossibleException("Recognition impossible");
		}

	}
}

