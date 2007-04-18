
using System;
using System.Collections;

namespace Refal.Runtime
{
	public class Program : RefalBase
	{
		static void Main(string[] args)
		{
			RefalBase.commandLineArguments = args;

			_Go(new PassiveExpression());

			RefalBase.CloseFiles();
		}

		public static PassiveExpression _Go(PassiveExpression expression)
		{
			Pattern pattern1 = new Pattern();
			if (RefalBase.Match(expression, pattern1))
			{
				return PassiveExpression.Build(_Job(PassiveExpression.Build()));
			};

			throw new RecognitionImpossibleException("Recognition impossible. Last expression: " + ExpressionToString(expression, 0));
		}

		private static PassiveExpression _Job(PassiveExpression expression)
		{
			Pattern pattern2 = new Pattern();
			if (RefalBase.Match(expression, pattern2))
			{
				return PassiveExpression.Build(_Prout(PassiveExpression.Build("Type expression to evaluate. To end: empty line. ".ToCharArray())), _Prout(PassiveExpression.Build("To end session: empty expression".ToCharArray())), _Prout(PassiveExpression.Build()), _Check__end(PassiveExpression.Build(_Inp__met(PassiveExpression.Build()))));
			};

			throw new RecognitionImpossibleException("Recognition impossible. Last expression: " + ExpressionToString(expression, 0));
		}

		private static PassiveExpression _Check__end(PassiveExpression expression)
		{
			Pattern pattern3 = new Pattern();
			if (RefalBase.Match(expression, pattern3))
			{
				return PassiveExpression.Build(_Prout(PassiveExpression.Build("End of session".ToCharArray())));
			};

			Pattern pattern4 = new Pattern("*".ToCharArray(), "Error");
			if (RefalBase.Match(expression, pattern4))
			{
				return PassiveExpression.Build(_Job(PassiveExpression.Build()));
			};

			Pattern pattern5 = new Pattern(new ExpressionVariable("e.X"));
			if (RefalBase.Match(expression, pattern5))
			{
				return PassiveExpression.Build(_Out(PassiveExpression.Build(_UpD(PassiveExpression.Build(pattern5.GetVariable("e.X"))))));
			};

			throw new RecognitionImpossibleException("Recognition impossible. Last expression: " + ExpressionToString(expression, 0));
		}

		private static PassiveExpression _Out(PassiveExpression expression)
		{
			Pattern pattern6 = new Pattern(new ExpressionVariable("e.X"));
			if (RefalBase.Match(expression, pattern6))
			{
				return PassiveExpression.Build(_Prout(PassiveExpression.Build("The result is:".ToCharArray())), _Prout(PassiveExpression.Build(pattern6.GetVariable("e.X"))), _Prout(PassiveExpression.Build()), _Job(PassiveExpression.Build()));
			};

			throw new RecognitionImpossibleException("Recognition impossible. Last expression: " + ExpressionToString(expression, 0));
		}



		private static PassiveExpression _Inp__met(PassiveExpression expression)
		{
			Pattern pattern7 = new Pattern();
			if (RefalBase.Match(expression, pattern7))
			{
				return PassiveExpression.Build(_Parse(PassiveExpression.Build(new OpeningBrace(), "$".ToCharArray(), new ClosingBrace(), _Read__in(PassiveExpression.Build(_Card(PassiveExpression.Build()))))));
			};

			throw new RecognitionImpossibleException("Recognition impossible. Last expression: " + ExpressionToString(expression, 0));
		}

		private static PassiveExpression _Read__in(PassiveExpression expression)
		{
			Pattern pattern8 = new Pattern();
			if (RefalBase.Match(expression, pattern8))
			{
				return PassiveExpression.Build();
			};

			Pattern pattern9 = new Pattern(0);
			if (RefalBase.Match(expression, pattern9))
			{
				return PassiveExpression.Build();
			};

			Pattern pattern10 = new Pattern(new ExpressionVariable("e.1"));
			if (RefalBase.Match(expression, pattern10))
			{
				return PassiveExpression.Build(" ".ToCharArray(), pattern10.GetVariable("e.1"), _Read__in(PassiveExpression.Build(_Card(PassiveExpression.Build()))));
			};

			throw new RecognitionImpossibleException("Recognition impossible. Last expression: " + ExpressionToString(expression, 0));
		}

		private static PassiveExpression _Parse(PassiveExpression expression)
		{
			Pattern pattern11 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), " ".ToCharArray(), new ExpressionVariable("e.2"));
			if (RefalBase.Match(expression, pattern11))
			{
				return PassiveExpression.Build(_Parse(PassiveExpression.Build(new OpeningBrace(), pattern11.GetVariable("e.1"), new ClosingBrace(), pattern11.GetVariable("e.2"))));
			};

			Pattern pattern12 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), "\\t".ToCharArray(), new ExpressionVariable("e.2"));
			if (RefalBase.Match(expression, pattern12))
			{
				return PassiveExpression.Build(_Parse(PassiveExpression.Build(new OpeningBrace(), pattern12.GetVariable("e.1"), new ClosingBrace(), pattern12.GetVariable("e.2"))));
			};

			Pattern pattern13 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), "(".ToCharArray(), new ExpressionVariable("e.2"));
			if (RefalBase.Match(expression, pattern13))
			{
				return PassiveExpression.Build(_Parse(PassiveExpression.Build(new OpeningBrace(), new OpeningBrace(), pattern13.GetVariable("e.1"), new ClosingBrace(), new ClosingBrace(), pattern13.GetVariable("e.2"))));
			};

			Pattern pattern14 = new Pattern(new OpeningBrace(), new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), new ExpressionVariable("e.2"), new ClosingBrace(), ")".ToCharArray(), new ExpressionVariable("e.3"));
			if (RefalBase.Match(expression, pattern14))
			{
				return PassiveExpression.Build(_Parse(PassiveExpression.Build(new OpeningBrace(), pattern14.GetVariable("e.1"), new OpeningBrace(), pattern14.GetVariable("e.2"), new ClosingBrace(), new ClosingBrace(), pattern14.GetVariable("e.3"))));
			};

			Pattern pattern15 = new Pattern(new OpeningBrace(), "$".ToCharArray(), new ExpressionVariable("e.1"), new ClosingBrace(), ")".ToCharArray(), new ExpressionVariable("e.3"));
			if (RefalBase.Match(expression, pattern15))
			{
				return PassiveExpression.Build(_Prout(PassiveExpression.Build()), "*".ToCharArray(), "Error", _Prout(PassiveExpression.Build("*** ERROR: Unbalanced \')\' in input".ToCharArray())), _Prout(PassiveExpression.Build(pattern15.GetVariable("e.1"), ")".ToCharArray())));
			};

			Pattern pattern16 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), "<".ToCharArray(), new ExpressionVariable("e.2"));
			if (RefalBase.Match(expression, pattern16))
			{
				return PassiveExpression.Build(_Parse(PassiveExpression.Build(new OpeningBrace(), new OpeningBrace(), pattern16.GetVariable("e.1"), "*".ToCharArray(), new ClosingBrace(), new ClosingBrace(), pattern16.GetVariable("e.2"))));
			};

			Pattern pattern17 = new Pattern(new OpeningBrace(), new OpeningBrace(), new ExpressionVariable("e.1"), "*".ToCharArray(), new ClosingBrace(), new SymbolVariable("s.F"), new ExpressionVariable("e.2"), new ClosingBrace(), ">".ToCharArray(), new ExpressionVariable("e.3"));
			if (RefalBase.Match(expression, pattern17))
			{
				expression = PassiveExpression.Build(_Checkf(PassiveExpression.Build(pattern17.GetVariable("s.F"))));
				{
					Pattern pattern18 = new Pattern("Mul");
					pattern18.CopyBoundVariables(pattern17);
					if (RefalBase.Match(expression, pattern18))
					{
						expression = PassiveExpression.Build(pattern18.GetVariable("e.2"));
						Pattern pattern19 = new Pattern("V".ToCharArray(), new ExpressionVariable("e.4"));
						pattern19.CopyBoundVariables(pattern18);
						if (RefalBase.Match(expression, pattern19))
						{
							return PassiveExpression.Build(_Parse(PassiveExpression.Build(new OpeningBrace(), pattern19.GetVariable("e.1"), new OpeningBrace(), "!".ToCharArray(), "Mul", pattern19.GetVariable("e.4"), new ClosingBrace(), new ClosingBrace(), pattern19.GetVariable("e.3"))));
						}
					};

					Pattern pattern20 = new Pattern(new SymbolVariable("s.F1"));
					pattern20.CopyBoundVariables(pattern17);
					if (RefalBase.Match(expression, pattern20))
					{
						return PassiveExpression.Build(_Parse(PassiveExpression.Build(new OpeningBrace(), pattern20.GetVariable("e.1"), new OpeningBrace(), "!".ToCharArray(), pattern20.GetVariable("s.F1"), pattern20.GetVariable("e.2"), new ClosingBrace(), new ClosingBrace(), pattern20.GetVariable("e.3"))));
					};

					Pattern pattern21 = new Pattern();
					pattern21.CopyBoundVariables(pattern17);
					if (RefalBase.Match(expression, pattern21))
					{
						return PassiveExpression.Build(_Prout(PassiveExpression.Build()), "*".ToCharArray(), "Error", _Prout(PassiveExpression.Build("Invalid function name ".ToCharArray(), pattern21.GetVariable("s.F"))));
					};

					throw new RecognitionImpossibleException("Recognition impossible. Last expression: " + ExpressionToString(expression, 0));
				}
			};

			Pattern pattern23 = new Pattern(new OpeningBrace(), new OpeningBrace(), new ExpressionVariable("e.1"), "*".ToCharArray(), new ClosingBrace(), new ExpressionVariable("e.2"), new ClosingBrace(), ">".ToCharArray(), new ExpressionVariable("e.3"));
			if (RefalBase.Match(expression, pattern23))
			{
				return PassiveExpression.Build(_Prout(PassiveExpression.Build()), "*".ToCharArray(), "Error", _Prout(PassiveExpression.Build("*** ERROR: No function name in input".ToCharArray())), _Prout(PassiveExpression.Build(pattern23.GetVariable("e.1"), "*(?????".ToCharArray())));
			};

			Pattern pattern24 = new Pattern(new OpeningBrace(), "$".ToCharArray(), new ExpressionVariable("e.1"), new ClosingBrace(), ">".ToCharArray(), new ExpressionVariable("e.3"));
			if (RefalBase.Match(expression, pattern24))
			{
				return PassiveExpression.Build(_Prout(PassiveExpression.Build()), "*".ToCharArray(), "Error", _Prout(PassiveExpression.Build("*** ERROR: Unbalanced \'>\' in input".ToCharArray())), _Prout(PassiveExpression.Build(pattern24.GetVariable("e.1"), ">".ToCharArray())));
			};

			Pattern pattern25 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), "*".ToCharArray(), new ExpressionVariable("e.2"));
			if (RefalBase.Match(expression, pattern25))
			{
				return PassiveExpression.Build(_Parse(PassiveExpression.Build(new OpeningBrace(), pattern25.GetVariable("e.1"), "*V".ToCharArray(), new ClosingBrace(), pattern25.GetVariable("e.2"))));
			};

			Pattern pattern26 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), "\'".ToCharArray(), new ExpressionVariable("e.2"));
			if (RefalBase.Match(expression, pattern26))
			{
				return PassiveExpression.Build(_Quotes(PassiveExpression.Build("Parse", "\'".ToCharArray(), new OpeningBrace(), pattern26.GetVariable("e.1"), new ClosingBrace(), pattern26.GetVariable("e.2"))));
			};

			Pattern pattern27 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), "\"".ToCharArray(), new ExpressionVariable("e.2"));
			if (RefalBase.Match(expression, pattern27))
			{
				return PassiveExpression.Build(_Quotes(PassiveExpression.Build("Parse", "\"".ToCharArray(), new OpeningBrace(), new OpeningBrace(), pattern27.GetVariable("e.1"), new ClosingBrace(), new ClosingBrace(), pattern27.GetVariable("e.2"))));
			};

			Pattern pattern28 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), "\\\\".ToCharArray(), new ExpressionVariable("e.2"));
			if (RefalBase.Match(expression, pattern28))
			{
				expression = PassiveExpression.Build(pattern28.GetVariable("e.2"));
				{
					Pattern pattern29 = new Pattern("x".ToCharArray(), new SymbolVariable("s.d1"), new SymbolVariable("s.d2"), new ExpressionVariable("e.3"));
					pattern29.CopyBoundVariables(pattern28);
					if (RefalBase.Match(expression, pattern29))
					{
						expression = PassiveExpression.Build(_Hex(PassiveExpression.Build(pattern29.GetVariable("s.d1"), pattern29.GetVariable("s.d2"))));
						Pattern pattern30 = new Pattern(new SymbolVariable("s.hex"));
						pattern30.CopyBoundVariables(pattern29);
						if (RefalBase.Match(expression, pattern30))
						{
							return PassiveExpression.Build(_Parse(PassiveExpression.Build(new OpeningBrace(), pattern30.GetVariable("e.1"), pattern30.GetVariable("s.hex"), new ClosingBrace(), pattern30.GetVariable("e.3"))));
						}
					};

					Pattern pattern31 = new Pattern(new SymbolVariable("s.A"), new ExpressionVariable("e.3"));
					pattern31.CopyBoundVariables(pattern28);
					if (RefalBase.Match(expression, pattern31))
					{
						expression = PassiveExpression.Build(_Escape(PassiveExpression.Build(pattern31.GetVariable("s.A"))));
						Pattern pattern32 = new Pattern(new SymbolVariable("s.A1"));
						pattern32.CopyBoundVariables(pattern31);
						if (RefalBase.Match(expression, pattern32))
						{
							return PassiveExpression.Build(_Parse(PassiveExpression.Build(new OpeningBrace(), pattern32.GetVariable("e.1"), pattern32.GetVariable("s.A1"), new ClosingBrace(), pattern32.GetVariable("e.3"))));
						}
					};

					Pattern pattern33 = new Pattern(new ExpressionVariable("e.3"));
					pattern33.CopyBoundVariables(pattern28);
					if (RefalBase.Match(expression, pattern33))
					{
						return PassiveExpression.Build(_Prout(PassiveExpression.Build()), "*".ToCharArray(), "Error", _Prout(PassiveExpression.Build("*** ERROR: Unexpected escape sequnce in input".ToCharArray())), _Prout(PassiveExpression.Build("*** ".ToCharArray(), pattern33.GetVariable("e.1"))), _Prout(PassiveExpression.Build("*** ".ToCharArray(), "\\\\".ToCharArray(), pattern33.GetVariable("e.3"))));
					};

					throw new RecognitionImpossibleException("Recognition impossible. Last expression: " + ExpressionToString(expression, 0));
				}
			};

			Pattern pattern35 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), new SymbolVariable("s.A"), new ExpressionVariable("e.2"));
			if (RefalBase.Match(expression, pattern35))
			{
				return PassiveExpression.Build(_Parse1(PassiveExpression.Build(new OpeningBrace(), pattern35.GetVariable("e.1"), new ClosingBrace(), new OpeningBrace(), _Type(PassiveExpression.Build(pattern35.GetVariable("s.A"))), new ClosingBrace(), pattern35.GetVariable("s.A"), pattern35.GetVariable("e.2"))));
			};

			Pattern pattern36 = new Pattern(new OpeningBrace(), "$".ToCharArray(), new ExpressionVariable("e.1"), new ClosingBrace());
			if (RefalBase.Match(expression, pattern36))
			{
				return PassiveExpression.Build(pattern36.GetVariable("e.1"));
			};

			Pattern pattern37 = new Pattern(new OpeningBrace(), new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), new ExpressionVariable("e.2"), new ClosingBrace());
			if (RefalBase.Match(expression, pattern37))
			{
				return PassiveExpression.Build(_Prout(PassiveExpression.Build()), "*".ToCharArray(), "Error", _Prout(PassiveExpression.Build("*** ERROR: Unbalanced \'(\' or \'<\' in input".ToCharArray())), _Prout__lm(PassiveExpression.Build(new OpeningBrace(), pattern37.GetVariable("e.1"), new ClosingBrace(), pattern37.GetVariable("e.2"))));
			};

			throw new RecognitionImpossibleException("Recognition impossible. Last expression: " + ExpressionToString(expression, 0));
		}

		private static PassiveExpression _Checkf(PassiveExpression expression)
		{
			Pattern pattern38 = new Pattern("+".ToCharArray());
			if (RefalBase.Match(expression, pattern38))
			{
				return PassiveExpression.Build("Add");
			};

			Pattern pattern39 = new Pattern("-".ToCharArray());
			if (RefalBase.Match(expression, pattern39))
			{
				return PassiveExpression.Build("Sub");
			};

			Pattern pattern40 = new Pattern("*".ToCharArray());
			if (RefalBase.Match(expression, pattern40))
			{
				return PassiveExpression.Build("Mul");
			};

			Pattern pattern41 = new Pattern("/".ToCharArray());
			if (RefalBase.Match(expression, pattern41))
			{
				return PassiveExpression.Build("Div");
			};

			Pattern pattern42 = new Pattern(new SymbolVariable("s.F"));
			if (RefalBase.Match(expression, pattern42))
			{
				expression = PassiveExpression.Build(_Type(PassiveExpression.Build(pattern42.GetVariable("s.F"))));
				Pattern pattern43 = new Pattern("Wi".ToCharArray(), new ExpressionVariable("e.F1"));
				pattern43.CopyBoundVariables(pattern42);
				if (RefalBase.Match(expression, pattern43))
				{
					return PassiveExpression.Build(pattern43.GetVariable("s.F"));
				}
			};

			Pattern pattern44 = new Pattern(new SymbolVariable("s.F"));
			if (RefalBase.Match(expression, pattern44))
			{
				return PassiveExpression.Build();
			};

			throw new RecognitionImpossibleException("Recognition impossible. Last expression: " + ExpressionToString(expression, 0));
		}

		private static PassiveExpression _Prout__lm(PassiveExpression expression)
		{
			Pattern pattern45 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.1"), "*".ToCharArray(), new ClosingBrace(), new ExpressionVariable("e.2"));
			if (RefalBase.Match(expression, pattern45))
			{
				return PassiveExpression.Build(_Prout__lm(PassiveExpression.Build(pattern45.GetVariable("e.1"))), _Prout(PassiveExpression.Build("*** *(".ToCharArray(), pattern45.GetVariable("e.2"))));
			};

			Pattern pattern46 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), new ExpressionVariable("e.2"));
			if (RefalBase.Match(expression, pattern46))
			{
				return PassiveExpression.Build(_Prout__lm(PassiveExpression.Build(pattern46.GetVariable("e.1"))), _Prout(PassiveExpression.Build("*** (".ToCharArray(), pattern46.GetVariable("e.2"))));
			};

			Pattern pattern47 = new Pattern("$".ToCharArray(), new ExpressionVariable("e.1"));
			if (RefalBase.Match(expression, pattern47))
			{
				return PassiveExpression.Build(_Prout(PassiveExpression.Build("*** ".ToCharArray(), pattern47.GetVariable("e.1"))));
			};

			throw new RecognitionImpossibleException("Recognition impossible. Last expression: " + ExpressionToString(expression, 0));
		}

		private static PassiveExpression _Escape(PassiveExpression expression)
		{
			Pattern pattern48 = new Pattern("\\\\".ToCharArray());
			if (RefalBase.Match(expression, pattern48))
			{
				return PassiveExpression.Build("\\\\".ToCharArray());
			};

			Pattern pattern49 = new Pattern("n".ToCharArray());
			if (RefalBase.Match(expression, pattern49))
			{
				return PassiveExpression.Build("\\n".ToCharArray());
			};

			Pattern pattern50 = new Pattern("r".ToCharArray());
			if (RefalBase.Match(expression, pattern50))
			{
				return PassiveExpression.Build("\\r".ToCharArray());
			};

			Pattern pattern51 = new Pattern("t".ToCharArray());
			if (RefalBase.Match(expression, pattern51))
			{
				return PassiveExpression.Build("\\t".ToCharArray());
			};

			Pattern pattern52 = new Pattern("\'".ToCharArray());
			if (RefalBase.Match(expression, pattern52))
			{
				return PassiveExpression.Build("\'".ToCharArray());
			};

			Pattern pattern53 = new Pattern("\"".ToCharArray());
			if (RefalBase.Match(expression, pattern53))
			{
				return PassiveExpression.Build("\"".ToCharArray());
			};

			Pattern pattern54 = new Pattern("(".ToCharArray());
			if (RefalBase.Match(expression, pattern54))
			{
				return PassiveExpression.Build("(".ToCharArray());
			};

			Pattern pattern55 = new Pattern(")".ToCharArray());
			if (RefalBase.Match(expression, pattern55))
			{
				return PassiveExpression.Build(")".ToCharArray());
			};

			Pattern pattern56 = new Pattern("<".ToCharArray());
			if (RefalBase.Match(expression, pattern56))
			{
				return PassiveExpression.Build("<".ToCharArray());
			};

			Pattern pattern57 = new Pattern(">".ToCharArray());
			if (RefalBase.Match(expression, pattern57))
			{
				return PassiveExpression.Build(">".ToCharArray());
			};

			Pattern pattern58 = new Pattern(new ExpressionVariable("e.3"));
			if (RefalBase.Match(expression, pattern58))
			{
				return PassiveExpression.Build();
			};

			throw new RecognitionImpossibleException("Recognition impossible. Last expression: " + ExpressionToString(expression, 0));
		}

		private static PassiveExpression _Quotes(PassiveExpression expression)
		{
			Pattern pattern59 = new Pattern(new SymbolVariable("s.Fun"), new SymbolVariable("s.Q"), new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), "\\\\".ToCharArray(), new ExpressionVariable("e.2"));
			if (RefalBase.Match(expression, pattern59))
			{
				expression = PassiveExpression.Build(pattern59.GetVariable("e.2"));
				{
					Pattern pattern60 = new Pattern("x".ToCharArray(), new SymbolVariable("s.d1"), new SymbolVariable("s.d2"), new ExpressionVariable("e.3"));
					pattern60.CopyBoundVariables(pattern59);
					if (RefalBase.Match(expression, pattern60))
					{
						expression = PassiveExpression.Build(_Hex(PassiveExpression.Build(pattern60.GetVariable("s.d1"), pattern60.GetVariable("s.d2"))));
						Pattern pattern61 = new Pattern(new SymbolVariable("s.hex"));
						pattern61.CopyBoundVariables(pattern60);
						if (RefalBase.Match(expression, pattern61))
						{
							return PassiveExpression.Build(_Quotes(PassiveExpression.Build(pattern61.GetVariable("s.Fun"), pattern61.GetVariable("s.Q"), new OpeningBrace(), pattern61.GetVariable("e.1"), pattern61.GetVariable("s.hex"), new ClosingBrace(), pattern61.GetVariable("e.3"))));
						}
					};

					Pattern pattern62 = new Pattern(new SymbolVariable("s.A"), new ExpressionVariable("e.3"));
					pattern62.CopyBoundVariables(pattern59);
					if (RefalBase.Match(expression, pattern62))
					{
						expression = PassiveExpression.Build(_Escape(PassiveExpression.Build(pattern62.GetVariable("s.A"))));
						Pattern pattern63 = new Pattern(new SymbolVariable("s.A1"));
						pattern63.CopyBoundVariables(pattern62);
						if (RefalBase.Match(expression, pattern63))
						{
							return PassiveExpression.Build(_Quotes(PassiveExpression.Build(pattern63.GetVariable("s.Fun"), pattern63.GetVariable("s.Q"), new OpeningBrace(), pattern63.GetVariable("e.1"), pattern63.GetVariable("s.A1"), new ClosingBrace(), pattern63.GetVariable("e.3"))));
						}
					};

					Pattern pattern64 = new Pattern(new ExpressionVariable("e.3"));
					pattern64.CopyBoundVariables(pattern59);
					if (RefalBase.Match(expression, pattern64))
					{
						return PassiveExpression.Build(_Prout(PassiveExpression.Build()), "*".ToCharArray(), "Error", _Prout(PassiveExpression.Build("*** ERROR: Unexpected escape sequnce in input".ToCharArray())), _Prout(PassiveExpression.Build("*** ".ToCharArray(), pattern64.GetVariable("e.1"))), _Prout(PassiveExpression.Build("*** ".ToCharArray(), "\\\\".ToCharArray(), pattern64.GetVariable("e.3"))));
					};

					throw new RecognitionImpossibleException("Recognition impossible. Last expression: " + ExpressionToString(expression, 0));
				}
			};

			Pattern pattern66 = new Pattern(new SymbolVariable("s.Fun"), "\'".ToCharArray(), new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), "\'".ToCharArray(), new ExpressionVariable("e.2"));
			if (RefalBase.Match(expression, pattern66))
			{
				return PassiveExpression.Build(_Mu(PassiveExpression.Build(pattern66.GetVariable("s.Fun"), new OpeningBrace(), pattern66.GetVariable("e.1"), new ClosingBrace(), pattern66.GetVariable("e.2"))));
			};

			Pattern pattern67 = new Pattern(new SymbolVariable("s.Fun"), "\"".ToCharArray(), new OpeningBrace(), new OpeningBrace(), new ExpressionVariable("e.0"), new ClosingBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), "\"".ToCharArray(), new ExpressionVariable("e.2"));
			if (RefalBase.Match(expression, pattern67))
			{
				return PassiveExpression.Build(_Mu(PassiveExpression.Build(pattern67.GetVariable("s.Fun"), new OpeningBrace(), pattern67.GetVariable("e.0"), _Implode_Ext(PassiveExpression.Build(pattern67.GetVariable("e.1"))), new ClosingBrace(), pattern67.GetVariable("e.2"))));
			};

			Pattern pattern68 = new Pattern(new SymbolVariable("s.Fun"), new SymbolVariable("s.Q"), new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), "EOF");
			if (RefalBase.Match(expression, pattern68))
			{
				return PassiveExpression.Build(_QuotesError(PassiveExpression.Build(pattern68.GetVariable("s.Q"), new OpeningBrace(), pattern68.GetVariable("e.1"), new ClosingBrace())));
			};

			Pattern pattern69 = new Pattern(new SymbolVariable("s.Fun"), "\'".ToCharArray(), new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), "*".ToCharArray(), new ExpressionVariable("e.2"));
			if (RefalBase.Match(expression, pattern69))
			{
				return PassiveExpression.Build(_Quotes(PassiveExpression.Build(pattern69.GetVariable("s.Fun"), "\'".ToCharArray(), new OpeningBrace(), pattern69.GetVariable("e.1"), "*V".ToCharArray(), new ClosingBrace(), pattern69.GetVariable("e.2"))));
			};

			Pattern pattern70 = new Pattern(new SymbolVariable("s.Fun"), new SymbolVariable("s.Q"), new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), new SymbolVariable("s.A"), new ExpressionVariable("e.2"));
			if (RefalBase.Match(expression, pattern70))
			{
				return PassiveExpression.Build(_Quotes(PassiveExpression.Build(pattern70.GetVariable("s.Fun"), pattern70.GetVariable("s.Q"), new OpeningBrace(), pattern70.GetVariable("e.1"), pattern70.GetVariable("s.A"), new ClosingBrace(), pattern70.GetVariable("e.2"))));
			};

			Pattern pattern71 = new Pattern(new SymbolVariable("s.Fun"), new SymbolVariable("s.Q"), new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace());
			if (RefalBase.Match(expression, pattern71))
			{
				return PassiveExpression.Build(_QuotesError(PassiveExpression.Build(pattern71.GetVariable("s.Q"), new OpeningBrace(), pattern71.GetVariable("e.1"), new ClosingBrace())));
			};

			throw new RecognitionImpossibleException("Recognition impossible. Last expression: " + ExpressionToString(expression, 0));
		}

		private static PassiveExpression _QuotesError(PassiveExpression expression)
		{
			Pattern pattern72 = new Pattern(new SymbolVariable("s.Q"), new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace());
			if (RefalBase.Match(expression, pattern72))
			{
				return PassiveExpression.Build(_Prout(PassiveExpression.Build()), "*".ToCharArray(), "Error", _Prout(PassiveExpression.Build("*** ERROR: Unbalanced quote in input".ToCharArray())), _Prout(PassiveExpression.Build("*** ".ToCharArray(), pattern72.GetVariable("e.1"))), _Prout(PassiveExpression.Build("*** ".ToCharArray(), pattern72.GetVariable("s.Q"))));
			};

			throw new RecognitionImpossibleException("Recognition impossible. Last expression: " + ExpressionToString(expression, 0));
		}

		private static PassiveExpression _Hex(PassiveExpression expression)
		{
			Pattern pattern73 = new Pattern(new SymbolVariable("s.d1"), new SymbolVariable("s.d2"));
			if (RefalBase.Match(expression, pattern73))
			{
				expression = PassiveExpression.Build(_Hex(PassiveExpression.Build(pattern73.GetVariable("s.d1"))));
				Pattern pattern74 = new Pattern(new SymbolVariable("s.h1"));
				pattern74.CopyBoundVariables(pattern73);
				if (RefalBase.Match(expression, pattern74))
				{
					expression = PassiveExpression.Build(_Hex(PassiveExpression.Build(pattern74.GetVariable("s.d2"))));
					Pattern pattern75 = new Pattern(new SymbolVariable("s.h2"));
					pattern75.CopyBoundVariables(pattern74);
					if (RefalBase.Match(expression, pattern75))
					{
						return PassiveExpression.Build(_Chr(PassiveExpression.Build(_Add(PassiveExpression.Build(new OpeningBrace(), _Mul(PassiveExpression.Build(pattern75.GetVariable("s.h1"), 16)), new ClosingBrace(), pattern75.GetVariable("s.h2"))))));
					}
				}
			};

			Pattern pattern76 = new Pattern(new SymbolVariable("s.h"));
			if (RefalBase.Match(expression, pattern76))
			{
				expression = PassiveExpression.Build(_Type(PassiveExpression.Build(pattern76.GetVariable("s.h"))));
				Pattern pattern77 = new Pattern("D".ToCharArray(), new ExpressionVariable("e.D1"));
				pattern77.CopyBoundVariables(pattern76);
				if (RefalBase.Match(expression, pattern77))
				{
					return PassiveExpression.Build(pattern77.GetVariable("s.h"));
				}
			};

			Pattern pattern78 = new Pattern("A".ToCharArray());
			if (RefalBase.Match(expression, pattern78))
			{
				return PassiveExpression.Build(10);
			};

			Pattern pattern79 = new Pattern("a".ToCharArray());
			if (RefalBase.Match(expression, pattern79))
			{
				return PassiveExpression.Build(10);
			};

			Pattern pattern80 = new Pattern("B".ToCharArray());
			if (RefalBase.Match(expression, pattern80))
			{
				return PassiveExpression.Build(11);
			};

			Pattern pattern81 = new Pattern("b".ToCharArray());
			if (RefalBase.Match(expression, pattern81))
			{
				return PassiveExpression.Build(11);
			};

			Pattern pattern82 = new Pattern("C".ToCharArray());
			if (RefalBase.Match(expression, pattern82))
			{
				return PassiveExpression.Build(12);
			};

			Pattern pattern83 = new Pattern("c".ToCharArray());
			if (RefalBase.Match(expression, pattern83))
			{
				return PassiveExpression.Build(12);
			};

			Pattern pattern84 = new Pattern("D".ToCharArray());
			if (RefalBase.Match(expression, pattern84))
			{
				return PassiveExpression.Build(13);
			};

			Pattern pattern85 = new Pattern("d".ToCharArray());
			if (RefalBase.Match(expression, pattern85))
			{
				return PassiveExpression.Build(13);
			};

			Pattern pattern86 = new Pattern("E".ToCharArray());
			if (RefalBase.Match(expression, pattern86))
			{
				return PassiveExpression.Build(14);
			};

			Pattern pattern87 = new Pattern("e".ToCharArray());
			if (RefalBase.Match(expression, pattern87))
			{
				return PassiveExpression.Build(14);
			};

			Pattern pattern88 = new Pattern("F".ToCharArray());
			if (RefalBase.Match(expression, pattern88))
			{
				return PassiveExpression.Build(15);
			};

			Pattern pattern89 = new Pattern("f".ToCharArray());
			if (RefalBase.Match(expression, pattern89))
			{
				return PassiveExpression.Build(15);
			};

			Pattern pattern90 = new Pattern(new ExpressionVariable("e.hs"));
			if (RefalBase.Match(expression, pattern90))
			{
				return PassiveExpression.Build();
			};

			throw new RecognitionImpossibleException("Recognition impossible. Last expression: " + ExpressionToString(expression, 0));
		}

		private static PassiveExpression _Parse1(PassiveExpression expression)
		{
			Pattern pattern91 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), new OpeningBrace(), "L".ToCharArray(), new ExpressionVariable("e.A1"), new ClosingBrace(), new SymbolVariable("s.A"), new ExpressionVariable("e.2"));
			if (RefalBase.Match(expression, pattern91))
			{
				return PassiveExpression.Build(_Word(PassiveExpression.Build(new OpeningBrace(), pattern91.GetVariable("e.1"), new ClosingBrace(), new OpeningBrace(), pattern91.GetVariable("s.A"), new ClosingBrace(), pattern91.GetVariable("e.2"))));
			};

			Pattern pattern92 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), new OpeningBrace(), "D".ToCharArray(), new ExpressionVariable("e.A1"), new ClosingBrace(), new SymbolVariable("s.A"), new ExpressionVariable("e.2"));
			if (RefalBase.Match(expression, pattern92))
			{
				return PassiveExpression.Build(_Number(PassiveExpression.Build(new OpeningBrace(), pattern92.GetVariable("e.1"), new ClosingBrace(), new OpeningBrace(), pattern92.GetVariable("s.A"), new ClosingBrace(), pattern92.GetVariable("e.2"))));
			};

			Pattern pattern93 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), new OpeningBrace(), new SymbolVariable("s.T"), new ExpressionVariable("e.A1"), new ClosingBrace(), new SymbolVariable("s.A"), new ExpressionVariable("e.2"));
			if (RefalBase.Match(expression, pattern93))
			{
				return PassiveExpression.Build(_Parse(PassiveExpression.Build(new OpeningBrace(), pattern93.GetVariable("e.1"), pattern93.GetVariable("s.A"), new ClosingBrace(), pattern93.GetVariable("e.2"))));
			};

			throw new RecognitionImpossibleException("Recognition impossible. Last expression: " + ExpressionToString(expression, 0));
		}

		private static PassiveExpression _Word(PassiveExpression expression)
		{
			Pattern pattern94 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), new OpeningBrace(), new ExpressionVariable("e.W"), new ClosingBrace(), new SymbolVariable("s.B"), new ExpressionVariable("e.2"));
			if (RefalBase.Match(expression, pattern94))
			{
				expression = PassiveExpression.Build(_Type(PassiveExpression.Build(pattern94.GetVariable("s.B"))));
				{
					Pattern pattern95 = new Pattern("L".ToCharArray(), new ExpressionVariable("e.B1"));
					pattern95.CopyBoundVariables(pattern94);
					if (RefalBase.Match(expression, pattern95))
					{
						return PassiveExpression.Build(_Word(PassiveExpression.Build(new OpeningBrace(), pattern95.GetVariable("e.1"), new ClosingBrace(), new OpeningBrace(), pattern95.GetVariable("e.W"), pattern95.GetVariable("s.B"), new ClosingBrace(), pattern95.GetVariable("e.2"))));
					};

					Pattern pattern96 = new Pattern("D".ToCharArray(), new ExpressionVariable("e.B1"));
					pattern96.CopyBoundVariables(pattern94);
					if (RefalBase.Match(expression, pattern96))
					{
						return PassiveExpression.Build(_Word(PassiveExpression.Build(new OpeningBrace(), pattern96.GetVariable("e.1"), new ClosingBrace(), new OpeningBrace(), pattern96.GetVariable("e.W"), pattern96.GetVariable("s.B"), new ClosingBrace(), pattern96.GetVariable("e.2"))));
					};

					Pattern pattern97 = new Pattern("Ol-".ToCharArray());
					pattern97.CopyBoundVariables(pattern94);
					if (RefalBase.Match(expression, pattern97))
					{
						return PassiveExpression.Build(_Word(PassiveExpression.Build(new OpeningBrace(), pattern97.GetVariable("e.1"), new ClosingBrace(), new OpeningBrace(), pattern97.GetVariable("e.W"), "-".ToCharArray(), new ClosingBrace(), pattern97.GetVariable("e.2"))));
					};

					Pattern pattern98 = new Pattern("Ou_".ToCharArray());
					pattern98.CopyBoundVariables(pattern94);
					if (RefalBase.Match(expression, pattern98))
					{
						return PassiveExpression.Build(_Word(PassiveExpression.Build(new OpeningBrace(), pattern98.GetVariable("e.1"), new ClosingBrace(), new OpeningBrace(), pattern98.GetVariable("e.W"), "_".ToCharArray(), new ClosingBrace(), pattern98.GetVariable("e.2"))));
					};

					Pattern pattern99 = new Pattern(new SymbolVariable("s.T"), new ExpressionVariable("e.B1"));
					pattern99.CopyBoundVariables(pattern94);
					if (RefalBase.Match(expression, pattern99))
					{
						return PassiveExpression.Build(_Parse(PassiveExpression.Build(new OpeningBrace(), pattern99.GetVariable("e.1"), _Implode_Ext(PassiveExpression.Build(pattern99.GetVariable("e.W"))), new ClosingBrace(), pattern99.GetVariable("s.B"), pattern99.GetVariable("e.2"))));
					};

					throw new RecognitionImpossibleException("Recognition impossible. Last expression: " + ExpressionToString(expression, 0));
				}
			};

			Pattern pattern101 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), new OpeningBrace(), new ExpressionVariable("e.W"), new ClosingBrace());
			if (RefalBase.Match(expression, pattern101))
			{
				return PassiveExpression.Build(_Parse(PassiveExpression.Build(new OpeningBrace(), pattern101.GetVariable("e.1"), _Implode_Ext(PassiveExpression.Build(pattern101.GetVariable("e.W"))), new ClosingBrace())));
			};

			throw new RecognitionImpossibleException("Recognition impossible. Last expression: " + ExpressionToString(expression, 0));
		}

		private static PassiveExpression _Number(PassiveExpression expression)
		{
			Pattern pattern102 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), new OpeningBrace(), new ExpressionVariable("e.N"), new ClosingBrace(), new SymbolVariable("s.X"), new ExpressionVariable("e.2"));
			if (RefalBase.Match(expression, pattern102))
			{
				expression = PassiveExpression.Build(_Type(PassiveExpression.Build(pattern102.GetVariable("s.X"))));
				Pattern pattern103 = new Pattern("D".ToCharArray(), new ExpressionVariable("e.X1"));
				pattern103.CopyBoundVariables(pattern102);
				if (RefalBase.Match(expression, pattern103))
				{
					return PassiveExpression.Build(_Number(PassiveExpression.Build(new OpeningBrace(), pattern103.GetVariable("e.1"), new ClosingBrace(), new OpeningBrace(), pattern103.GetVariable("e.N"), pattern103.GetVariable("s.X"), new ClosingBrace(), pattern103.GetVariable("e.2"))));
				}
			};

			Pattern pattern104 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), new OpeningBrace(), new ExpressionVariable("e.N"), new ClosingBrace(), new ExpressionVariable("e.X"));
			if (RefalBase.Match(expression, pattern104))
			{
				return PassiveExpression.Build(_Parse(PassiveExpression.Build(new OpeningBrace(), pattern104.GetVariable("e.1"), _Numb(PassiveExpression.Build(pattern104.GetVariable("e.N"))), new ClosingBrace(), pattern104.GetVariable("e.X"))));
			};

			throw new RecognitionImpossibleException("Recognition impossible. Last expression: " + ExpressionToString(expression, 0));
		}

	}
}

