
using System;
using System.Collections;

namespace Refal.Runtime
{
	public class test15 : RefalBase
	{
		public static PassiveExpression _Pair(PassiveExpression expression)
		{
			Pattern pattern1 = new Pattern(new ExpressionVariable("e.X"));
			if (pattern1.Match(expression))
			{
				return PassiveExpression.Build(_Pair1(PassiveExpression.Build(new OpeningBrace(), new OpeningBrace(), new ClosingBrace(), new ClosingBrace(), new OpeningBrace(), pattern1.GetVariable("e.X"), new ClosingBrace())));
			};

			throw new RecognitionImpossibleException("Recognition impossible. Last expression: " + expression.ToString());
		}

		private static PassiveExpression _Pair1(PassiveExpression expression)
		{
			Pattern pattern2 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.L"), new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), new ClosingBrace(), new OpeningBrace(), "(".ToCharArray(), new ExpressionVariable("e.2"), new ClosingBrace(), new ExpressionVariable("e.R"));
			if (pattern2.Match(expression))
			{
				return PassiveExpression.Build(_Pair1(PassiveExpression.Build(new OpeningBrace(), pattern2.GetVariable("e.L"), new OpeningBrace(), pattern2.GetVariable("e.1"), new ClosingBrace(), new OpeningBrace(), new ClosingBrace(), new ClosingBrace(), new OpeningBrace(), pattern2.GetVariable("e.2"), new ClosingBrace(), pattern2.GetVariable("e.R"))));
			};

			Pattern pattern3 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.L"), new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), new OpeningBrace(), new ExpressionVariable("e.2"), new ClosingBrace(), new ClosingBrace(), new OpeningBrace(), ")".ToCharArray(), new ExpressionVariable("e.3"), new ClosingBrace(), new ExpressionVariable("e.R"));
			if (pattern3.Match(expression))
			{
				return PassiveExpression.Build(_Pair1(PassiveExpression.Build(new OpeningBrace(), pattern3.GetVariable("e.L"), new OpeningBrace(), pattern3.GetVariable("e.1"), new OpeningBrace(), pattern3.GetVariable("e.2"), new ClosingBrace(), new ClosingBrace(), new ClosingBrace(), new OpeningBrace(), pattern3.GetVariable("e.3"), new ClosingBrace(), pattern3.GetVariable("e.R"))));
			};

			Pattern pattern4 = new Pattern(new OpeningBrace(), new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), new ClosingBrace(), new OpeningBrace(), ")".ToCharArray(), new ExpressionVariable("e.3"), new ClosingBrace(), new ExpressionVariable("e.R"));
			if (pattern4.Match(expression))
			{
				return PassiveExpression.Build(_Prout(PassiveExpression.Build("*** ERROR: Unbalanced \")\"".ToCharArray(), "found by Pair:".ToCharArray())), _Prout(PassiveExpression.Build("*** ".ToCharArray(), pattern4.GetVariable("e.1"), ")".ToCharArray())));
			};

			Pattern pattern5 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.L"), new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), new ClosingBrace(), new OpeningBrace(), new SymbolVariable("s.2"), new ExpressionVariable("e.3"), new ClosingBrace(), new ExpressionVariable("e.R"));
			if (pattern5.Match(expression))
			{
				return PassiveExpression.Build(_Pair1(PassiveExpression.Build(new OpeningBrace(), pattern5.GetVariable("e.L"), new OpeningBrace(), pattern5.GetVariable("e.1"), pattern5.GetVariable("s.2"), new ClosingBrace(), new ClosingBrace(), new OpeningBrace(), pattern5.GetVariable("e.3"), new ClosingBrace(), pattern5.GetVariable("e.R"))));
			};

			Pattern pattern6 = new Pattern(new OpeningBrace(), new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), new ClosingBrace(), new OpeningBrace(), new ClosingBrace());
			if (pattern6.Match(expression))
			{
				return PassiveExpression.Build(pattern6.GetVariable("e.1"));
			};

			Pattern pattern7 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.L"), new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), new ClosingBrace(), new OpeningBrace(), new ClosingBrace());
			if (pattern7.Match(expression))
			{
				return PassiveExpression.Build(_Prout(PassiveExpression.Build("*** ERROR: Unbalanced \"(\" ".ToCharArray(), "found by Pair:".ToCharArray())), _Prlmb(PassiveExpression.Build(pattern7.GetVariable("e.L"))));
			};

			throw new RecognitionImpossibleException("Recognition impossible. Last expression: " + expression.ToString());
		}

		public static PassiveExpression _PairArg(PassiveExpression expression)
		{
			Pattern pattern8 = new Pattern(new SymbolVariable("s.numb"));
			if (pattern8.Match(expression))
			{
				return PassiveExpression.Build(_Pair(PassiveExpression.Build(_Arg(PassiveExpression.Build(pattern8.GetVariable("s.numb"))))));
			};

			throw new RecognitionImpossibleException("Recognition impossible. Last expression: " + expression.ToString());
		}

		private static PassiveExpression _Prlmb(PassiveExpression expression)
		{
			Pattern pattern9 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), new ExpressionVariable("e.2"));
			if (pattern9.Match(expression))
			{
				return PassiveExpression.Build(_Prout(PassiveExpression.Build("*** ".ToCharArray(), pattern9.GetVariable("e.1"), "(".ToCharArray())), _Prlmb(PassiveExpression.Build(pattern9.GetVariable("e.2"))));
			};

			Pattern pattern10 = new Pattern();
			if (pattern10.Match(expression))
			{
				return PassiveExpression.Build();
			};

			throw new RecognitionImpossibleException("Recognition impossible. Last expression: " + expression.ToString());
		}

		public static PassiveExpression _Input(PassiveExpression expression)
		{
			Pattern pattern11 = new Pattern();
			if (pattern11.Match(expression))
			{
				return PassiveExpression.Build(_Input1(PassiveExpression.Build(0)));
			};

			Pattern pattern12 = new Pattern(new SymbolVariable("s.C"));
			if (pattern12.Match(expression))
			{
				expression = PassiveExpression.Build(_Type(PassiveExpression.Build(pattern12.GetVariable("s.C"))));
				Pattern pattern13 = new Pattern("N".ToCharArray(), new ExpressionVariable("e.C1"));
				pattern13.CopyBoundVariables(pattern12);
				if (pattern13.Match(expression))
				{
					return PassiveExpression.Build(_Input1(PassiveExpression.Build(pattern13.GetVariable("s.C"))));
				}
			};

			Pattern pattern14 = new Pattern(new ExpressionVariable("e.File"));
			if (pattern14.Match(expression))
			{
				expression = PassiveExpression.Build(_Status(PassiveExpression.Build("r".ToCharArray(), pattern14.GetVariable("e.File"))));
				{
					Pattern pattern15 = new Pattern("New", new SymbolVariable("s.C"));
					pattern15.CopyBoundVariables(pattern14);
					if (pattern15.Match(expression))
					{
						return PassiveExpression.Build(_Open(PassiveExpression.Build("r".ToCharArray(), pattern15.GetVariable("s.C"), pattern15.GetVariable("e.File"))), _Input1(PassiveExpression.Build(pattern15.GetVariable("s.C"))));
					};

					Pattern pattern16 = new Pattern("Old", new SymbolVariable("s.C"));
					pattern16.CopyBoundVariables(pattern14);
					if (pattern16.Match(expression))
					{
						return PassiveExpression.Build(_Input1(PassiveExpression.Build(pattern16.GetVariable("s.C"))));
					};

					throw new RecognitionImpossibleException("Recognition impossible. Last expression: " + expression.ToString());
				}
			};

			throw new RecognitionImpossibleException("Recognition impossible. Last expression: " + expression.ToString());
		}

		private static PassiveExpression _Status(PassiveExpression expression)
		{
			Pattern pattern18 = new Pattern(new SymbolVariable("s.M"), new ExpressionVariable("e.File"));
			if (pattern18.Match(expression))
			{
				expression = PassiveExpression.Build(_F__table(PassiveExpression.Build()));
				{
					Pattern pattern19 = new Pattern(new ExpressionVariable("e.1"), new SymbolVariable("s.C"), new OpeningBrace(), new SymbolVariable("s.M"), "/".ToCharArray(), new ExpressionVariable("e.File"), new ClosingBrace(), new ExpressionVariable("e.2"));
					pattern19.CopyBoundVariables(pattern18);
					if (pattern19.Match(expression))
					{
						return PassiveExpression.Build("Old", pattern19.GetVariable("s.C"), _Br(PassiveExpression.Build(153443950, "=".ToCharArray(), pattern19.GetVariable("e.1"), pattern19.GetVariable("s.C"), new OpeningBrace(), pattern19.GetVariable("s.M"), "/".ToCharArray(), pattern19.GetVariable("e.File"), new ClosingBrace(), pattern19.GetVariable("e.2"))));
					};

					Pattern pattern20 = new Pattern(new ExpressionVariable("e.1"), new SymbolVariable("s.C"), new OpeningBrace(), new SymbolVariable("s.M1"), "/".ToCharArray(), new ExpressionVariable("e.File"), new ClosingBrace(), new ExpressionVariable("e.2"));
					pattern20.CopyBoundVariables(pattern18);
					if (pattern20.Match(expression))
					{
						return PassiveExpression.Build("New", pattern20.GetVariable("s.C"), _Br(PassiveExpression.Build(153443950, "=".ToCharArray(), pattern20.GetVariable("e.1"), pattern20.GetVariable("s.C"), new OpeningBrace(), pattern20.GetVariable("s.M"), "/".ToCharArray(), pattern20.GetVariable("e.File"), new ClosingBrace(), pattern20.GetVariable("e.2"))));
					};

					Pattern pattern21 = new Pattern(new ExpressionVariable("e.1"), new SymbolVariable("s.C"), new SymbolVariable("s.X"), new ExpressionVariable("e.2"));
					pattern21.CopyBoundVariables(pattern18);
					if (pattern21.Match(expression))
					{
						return PassiveExpression.Build("New", pattern21.GetVariable("s.C"), _Br(PassiveExpression.Build(153443950, "=".ToCharArray(), pattern21.GetVariable("e.1"), pattern21.GetVariable("s.C"), new OpeningBrace(), pattern21.GetVariable("s.M"), "/".ToCharArray(), pattern21.GetVariable("e.File"), new ClosingBrace(), pattern21.GetVariable("s.X"), pattern21.GetVariable("e.2"))));
					};

					Pattern pattern22 = new Pattern(new ExpressionVariable("e.1"));
					pattern22.CopyBoundVariables(pattern18);
					if (pattern22.Match(expression))
					{
						return PassiveExpression.Build(_Prout(PassiveExpression.Build("Sorry. Can\'t open ", pattern22.GetVariable("e.File"), ". No more channels")));
					};

					throw new RecognitionImpossibleException("Recognition impossible. Last expression: " + expression.ToString());
				}
			};

			throw new RecognitionImpossibleException("Recognition impossible. Last expression: " + expression.ToString());
		}

		private static PassiveExpression _F__table(PassiveExpression expression)
		{
			Pattern pattern24 = new Pattern();
			if (pattern24.Match(expression))
			{
				expression = PassiveExpression.Build(_Dg(PassiveExpression.Build(153443950)));
				{
					Pattern pattern25 = new Pattern();
					pattern25.CopyBoundVariables(pattern24);
					if (pattern25.Match(expression))
					{
						return PassiveExpression.Build(19, 18, 17, 16, 15, 14, 13, 12, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1, "X".ToCharArray());
					};

					Pattern pattern26 = new Pattern(new ExpressionVariable("e.1"));
					pattern26.CopyBoundVariables(pattern24);
					if (pattern26.Match(expression))
					{
						return PassiveExpression.Build(pattern26.GetVariable("e.1"));
					};

					throw new RecognitionImpossibleException("Recognition impossible. Last expression: " + expression.ToString());
				}
			};

			throw new RecognitionImpossibleException("Recognition impossible. Last expression: " + expression.ToString());
		}

		private static PassiveExpression _Input1(PassiveExpression expression)
		{
			Pattern pattern28 = new Pattern(new SymbolVariable("s.C"));
			if (pattern28.Match(expression))
			{
				return PassiveExpression.Build(_Parse(PassiveExpression.Build(new OpeningBrace(), "$".ToCharArray(), new ClosingBrace(), _Read__in(PassiveExpression.Build(pattern28.GetVariable("s.C"), _Get(PassiveExpression.Build(pattern28.GetVariable("s.C"))))))));
			};

			throw new RecognitionImpossibleException("Recognition impossible. Last expression: " + expression.ToString());
		}

		private static PassiveExpression _Read__in(PassiveExpression expression)
		{
			Pattern pattern29 = new Pattern(new SymbolVariable("s.C"));
			if (pattern29.Match(expression))
			{
				return PassiveExpression.Build();
			};

			Pattern pattern30 = new Pattern(new SymbolVariable("s.C"), 0);
			if (pattern30.Match(expression))
			{
				return PassiveExpression.Build();
			};

			Pattern pattern31 = new Pattern(new SymbolVariable("s.C"), new ExpressionVariable("e.1"));
			if (pattern31.Match(expression))
			{
				return PassiveExpression.Build(" ".ToCharArray(), pattern31.GetVariable("e.1"), _Read__in(PassiveExpression.Build(pattern31.GetVariable("s.C"), _Get(PassiveExpression.Build(pattern31.GetVariable("s.C"))))));
			};

			throw new RecognitionImpossibleException("Recognition impossible. Last expression: " + expression.ToString());
		}

		public static PassiveExpression _InputArg(PassiveExpression expression)
		{
			Pattern pattern32 = new Pattern(new SymbolVariable("s.numb"));
			if (pattern32.Match(expression))
			{
				return PassiveExpression.Build(_Parse(PassiveExpression.Build(new OpeningBrace(), "$".ToCharArray(), new ClosingBrace(), _Arg(PassiveExpression.Build(pattern32.GetVariable("s.numb"))))));
			};

			throw new RecognitionImpossibleException("Recognition impossible. Last expression: " + expression.ToString());
		}

		private static PassiveExpression _Parse(PassiveExpression expression)
		{
			Pattern pattern33 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), " ".ToCharArray(), new ExpressionVariable("e.2"));
			if (pattern33.Match(expression))
			{
				return PassiveExpression.Build(_Parse(PassiveExpression.Build(new OpeningBrace(), pattern33.GetVariable("e.1"), new ClosingBrace(), pattern33.GetVariable("e.2"))));
			};

			Pattern pattern34 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), "\\t".ToCharArray(), new ExpressionVariable("e.2"));
			if (pattern34.Match(expression))
			{
				return PassiveExpression.Build(_Parse(PassiveExpression.Build(new OpeningBrace(), pattern34.GetVariable("e.1"), new ClosingBrace(), pattern34.GetVariable("e.2"))));
			};

			Pattern pattern35 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), "(".ToCharArray(), new ExpressionVariable("e.2"));
			if (pattern35.Match(expression))
			{
				return PassiveExpression.Build(_Parse(PassiveExpression.Build(new OpeningBrace(), new OpeningBrace(), pattern35.GetVariable("e.1"), new ClosingBrace(), new ClosingBrace(), pattern35.GetVariable("e.2"))));
			};

			Pattern pattern36 = new Pattern(new OpeningBrace(), new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), new ExpressionVariable("e.2"), new ClosingBrace(), ")".ToCharArray(), new ExpressionVariable("e.3"));
			if (pattern36.Match(expression))
			{
				return PassiveExpression.Build(_Parse(PassiveExpression.Build(new OpeningBrace(), pattern36.GetVariable("e.1"), new OpeningBrace(), pattern36.GetVariable("e.2"), new ClosingBrace(), new ClosingBrace(), pattern36.GetVariable("e.3"))));
			};

			Pattern pattern37 = new Pattern(new OpeningBrace(), "$".ToCharArray(), new ExpressionVariable("e.1"), new ClosingBrace(), ")".ToCharArray(), new ExpressionVariable("e.3"));
			if (pattern37.Match(expression))
			{
				return PassiveExpression.Build(_Prout(PassiveExpression.Build()), _Prout(PassiveExpression.Build()), _Prout(PassiveExpression.Build("*** ERROR: Unbalanced \')\' in input".ToCharArray())), _Prout(PassiveExpression.Build(pattern37.GetVariable("e.1"), ")".ToCharArray())));
			};

			Pattern pattern38 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), "\'".ToCharArray(), new ExpressionVariable("e.2"));
			if (pattern38.Match(expression))
			{
				return PassiveExpression.Build(_Quotes(PassiveExpression.Build("Parse", "\'".ToCharArray(), new OpeningBrace(), pattern38.GetVariable("e.1"), new ClosingBrace(), pattern38.GetVariable("e.2"))));
			};

			Pattern pattern39 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), "\"".ToCharArray(), new ExpressionVariable("e.2"));
			if (pattern39.Match(expression))
			{
				return PassiveExpression.Build(_Quotes(PassiveExpression.Build("Parse", "\"".ToCharArray(), new OpeningBrace(), new OpeningBrace(), pattern39.GetVariable("e.1"), new ClosingBrace(), new ClosingBrace(), pattern39.GetVariable("e.2"))));
			};

			Pattern pattern40 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), "\\\\".ToCharArray(), new ExpressionVariable("e.2"));
			if (pattern40.Match(expression))
			{
				expression = PassiveExpression.Build(pattern40.GetVariable("e.2"));
				{
					Pattern pattern41 = new Pattern("x".ToCharArray(), new SymbolVariable("s.d1"), new SymbolVariable("s.d2"), new ExpressionVariable("e.3"));
					pattern41.CopyBoundVariables(pattern40);
					if (pattern41.Match(expression))
					{
						expression = PassiveExpression.Build(_Hex(PassiveExpression.Build(pattern41.GetVariable("s.d1"), pattern41.GetVariable("s.d2"))));
						Pattern pattern42 = new Pattern(new SymbolVariable("s.hex"));
						pattern42.CopyBoundVariables(pattern41);
						if (pattern42.Match(expression))
						{
							return PassiveExpression.Build(_Parse(PassiveExpression.Build(new OpeningBrace(), pattern42.GetVariable("e.1"), pattern42.GetVariable("s.hex"), new ClosingBrace(), pattern42.GetVariable("e.3"))));
						}
					};

					Pattern pattern43 = new Pattern(new SymbolVariable("s.A"), new ExpressionVariable("e.3"));
					pattern43.CopyBoundVariables(pattern40);
					if (pattern43.Match(expression))
					{
						expression = PassiveExpression.Build(_Escape(PassiveExpression.Build(pattern43.GetVariable("s.A"))));
						Pattern pattern44 = new Pattern(new SymbolVariable("s.A1"));
						pattern44.CopyBoundVariables(pattern43);
						if (pattern44.Match(expression))
						{
							return PassiveExpression.Build(_Parse(PassiveExpression.Build(new OpeningBrace(), pattern44.GetVariable("e.1"), pattern44.GetVariable("s.A1"), new ClosingBrace(), pattern44.GetVariable("e.3"))));
						}
					};

					Pattern pattern45 = new Pattern(new ExpressionVariable("e.3"));
					pattern45.CopyBoundVariables(pattern40);
					if (pattern45.Match(expression))
					{
						return PassiveExpression.Build(_Prout(PassiveExpression.Build()), "*".ToCharArray(), "Error", _Prout(PassiveExpression.Build("*** ERROR: Unexpected escape sequnce in input".ToCharArray())), _Prout(PassiveExpression.Build("*** ".ToCharArray(), pattern45.GetVariable("e.1"))), _Prout(PassiveExpression.Build("*** ".ToCharArray(), "\\\\".ToCharArray(), pattern45.GetVariable("e.3"))));
					};

					throw new RecognitionImpossibleException("Recognition impossible. Last expression: " + expression.ToString());
				}
			};

			Pattern pattern47 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), new SymbolVariable("s.A"), new ExpressionVariable("e.2"));
			if (pattern47.Match(expression))
			{
				return PassiveExpression.Build(_Parse1(PassiveExpression.Build(new OpeningBrace(), pattern47.GetVariable("e.1"), new ClosingBrace(), new OpeningBrace(), _Type(PassiveExpression.Build(pattern47.GetVariable("s.A"))), new ClosingBrace(), pattern47.GetVariable("s.A"), pattern47.GetVariable("e.2"))));
			};

			Pattern pattern48 = new Pattern(new OpeningBrace(), "$".ToCharArray(), new ExpressionVariable("e.1"), new ClosingBrace());
			if (pattern48.Match(expression))
			{
				return PassiveExpression.Build(pattern48.GetVariable("e.1"));
			};

			Pattern pattern49 = new Pattern(new OpeningBrace(), new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), new ExpressionVariable("e.2"), new ClosingBrace());
			if (pattern49.Match(expression))
			{
				return PassiveExpression.Build(_Prout(PassiveExpression.Build()), _Prout(PassiveExpression.Build()), _Prout(PassiveExpression.Build("*** ERROR: Unbalanced \'(\' in input".ToCharArray())), _Prout__lm(PassiveExpression.Build(new OpeningBrace(), pattern49.GetVariable("e.1"), new ClosingBrace(), pattern49.GetVariable("e.2"))));
			};

			throw new RecognitionImpossibleException("Recognition impossible. Last expression: " + expression.ToString());
		}

		private static PassiveExpression _Prout__lm(PassiveExpression expression)
		{
			Pattern pattern50 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), new ExpressionVariable("e.2"));
			if (pattern50.Match(expression))
			{
				return PassiveExpression.Build(_Prout__lm(PassiveExpression.Build(pattern50.GetVariable("e.1"))), _Prout(PassiveExpression.Build("*** (".ToCharArray(), pattern50.GetVariable("e.2"))));
			};

			Pattern pattern51 = new Pattern("$".ToCharArray(), new ExpressionVariable("e.1"));
			if (pattern51.Match(expression))
			{
				return PassiveExpression.Build(_Prout(PassiveExpression.Build("*** ".ToCharArray(), pattern51.GetVariable("e.1"))));
			};

			throw new RecognitionImpossibleException("Recognition impossible. Last expression: " + expression.ToString());
		}

		private static PassiveExpression _Escape(PassiveExpression expression)
		{
			Pattern pattern52 = new Pattern("\\\\".ToCharArray());
			if (pattern52.Match(expression))
			{
				return PassiveExpression.Build("\\\\".ToCharArray());
			};

			Pattern pattern53 = new Pattern("n".ToCharArray());
			if (pattern53.Match(expression))
			{
				return PassiveExpression.Build("\\n".ToCharArray());
			};

			Pattern pattern54 = new Pattern("r".ToCharArray());
			if (pattern54.Match(expression))
			{
				return PassiveExpression.Build("\\r".ToCharArray());
			};

			Pattern pattern55 = new Pattern("t".ToCharArray());
			if (pattern55.Match(expression))
			{
				return PassiveExpression.Build("\\t".ToCharArray());
			};

			Pattern pattern56 = new Pattern("\'".ToCharArray());
			if (pattern56.Match(expression))
			{
				return PassiveExpression.Build("\'".ToCharArray());
			};

			Pattern pattern57 = new Pattern("\"".ToCharArray());
			if (pattern57.Match(expression))
			{
				return PassiveExpression.Build("\"".ToCharArray());
			};

			Pattern pattern58 = new Pattern("(".ToCharArray());
			if (pattern58.Match(expression))
			{
				return PassiveExpression.Build("(".ToCharArray());
			};

			Pattern pattern59 = new Pattern(")".ToCharArray());
			if (pattern59.Match(expression))
			{
				return PassiveExpression.Build(")".ToCharArray());
			};

			Pattern pattern60 = new Pattern("<".ToCharArray());
			if (pattern60.Match(expression))
			{
				return PassiveExpression.Build("<".ToCharArray());
			};

			Pattern pattern61 = new Pattern(">".ToCharArray());
			if (pattern61.Match(expression))
			{
				return PassiveExpression.Build(">".ToCharArray());
			};

			Pattern pattern62 = new Pattern(new ExpressionVariable("e.3"));
			if (pattern62.Match(expression))
			{
				return PassiveExpression.Build();
			};

			throw new RecognitionImpossibleException("Recognition impossible. Last expression: " + expression.ToString());
		}

		private static PassiveExpression _Quotes(PassiveExpression expression)
		{
			Pattern pattern63 = new Pattern(new SymbolVariable("s.Fun"), new SymbolVariable("s.Q"), new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), "\\\\".ToCharArray(), new ExpressionVariable("e.2"));
			if (pattern63.Match(expression))
			{
				expression = PassiveExpression.Build(pattern63.GetVariable("e.2"));
				{
					Pattern pattern64 = new Pattern("x".ToCharArray(), new SymbolVariable("s.d1"), new SymbolVariable("s.d2"), new ExpressionVariable("e.3"));
					pattern64.CopyBoundVariables(pattern63);
					if (pattern64.Match(expression))
					{
						expression = PassiveExpression.Build(_Hex(PassiveExpression.Build(pattern64.GetVariable("s.d1"), pattern64.GetVariable("s.d2"))));
						Pattern pattern65 = new Pattern(new SymbolVariable("s.hex"));
						pattern65.CopyBoundVariables(pattern64);
						if (pattern65.Match(expression))
						{
							return PassiveExpression.Build(_Quotes(PassiveExpression.Build(pattern65.GetVariable("s.Fun"), pattern65.GetVariable("s.Q"), new OpeningBrace(), pattern65.GetVariable("e.1"), pattern65.GetVariable("s.hex"), new ClosingBrace(), pattern65.GetVariable("e.3"))));
						}
					};

					Pattern pattern66 = new Pattern(new SymbolVariable("s.A"), new ExpressionVariable("e.3"));
					pattern66.CopyBoundVariables(pattern63);
					if (pattern66.Match(expression))
					{
						expression = PassiveExpression.Build(_Escape(PassiveExpression.Build(pattern66.GetVariable("s.A"))));
						Pattern pattern67 = new Pattern(new SymbolVariable("s.A1"));
						pattern67.CopyBoundVariables(pattern66);
						if (pattern67.Match(expression))
						{
							return PassiveExpression.Build(_Quotes(PassiveExpression.Build(pattern67.GetVariable("s.Fun"), pattern67.GetVariable("s.Q"), new OpeningBrace(), pattern67.GetVariable("e.1"), pattern67.GetVariable("s.A1"), new ClosingBrace(), pattern67.GetVariable("e.3"))));
						}
					};

					Pattern pattern68 = new Pattern(new ExpressionVariable("e.3"));
					pattern68.CopyBoundVariables(pattern63);
					if (pattern68.Match(expression))
					{
						return PassiveExpression.Build(_Prout(PassiveExpression.Build()), "*".ToCharArray(), "Error", _Prout(PassiveExpression.Build("*** ERROR: Unexpected escape sequnce in input".ToCharArray())), _Prout(PassiveExpression.Build("*** ".ToCharArray(), pattern68.GetVariable("e.1"))), _Prout(PassiveExpression.Build("*** ".ToCharArray(), "\\\\".ToCharArray(), pattern68.GetVariable("e.3"))));
					};

					throw new RecognitionImpossibleException("Recognition impossible. Last expression: " + expression.ToString());
				}
			};

			Pattern pattern70 = new Pattern(new SymbolVariable("s.Fun"), "\'".ToCharArray(), new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), "\'".ToCharArray(), new ExpressionVariable("e.2"));
			if (pattern70.Match(expression))
			{
				return PassiveExpression.Build(_Mu(PassiveExpression.Build(pattern70.GetVariable("s.Fun"), new OpeningBrace(), pattern70.GetVariable("e.1"), new ClosingBrace(), pattern70.GetVariable("e.2"))));
			};

			Pattern pattern71 = new Pattern(new SymbolVariable("s.Fun"), "\"".ToCharArray(), new OpeningBrace(), new OpeningBrace(), new ExpressionVariable("e.0"), new ClosingBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), "\"".ToCharArray(), new ExpressionVariable("e.2"));
			if (pattern71.Match(expression))
			{
				return PassiveExpression.Build(_Mu(PassiveExpression.Build(pattern71.GetVariable("s.Fun"), new OpeningBrace(), pattern71.GetVariable("e.0"), _Implode_Ext(PassiveExpression.Build(pattern71.GetVariable("e.1"))), new ClosingBrace(), pattern71.GetVariable("e.2"))));
			};

			Pattern pattern72 = new Pattern(new SymbolVariable("s.Fun"), new SymbolVariable("s.Q"), new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), "EOF");
			if (pattern72.Match(expression))
			{
				return PassiveExpression.Build(_QuotesError(PassiveExpression.Build(pattern72.GetVariable("s.Q"), new OpeningBrace(), pattern72.GetVariable("e.1"), new ClosingBrace())));
			};

			Pattern pattern73 = new Pattern(new SymbolVariable("s.Fun"), new SymbolVariable("s.Q"), new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), new SymbolVariable("s.A"), new ExpressionVariable("e.2"));
			if (pattern73.Match(expression))
			{
				return PassiveExpression.Build(_Quotes(PassiveExpression.Build(pattern73.GetVariable("s.Fun"), pattern73.GetVariable("s.Q"), new OpeningBrace(), pattern73.GetVariable("e.1"), pattern73.GetVariable("s.A"), new ClosingBrace(), pattern73.GetVariable("e.2"))));
			};

			Pattern pattern74 = new Pattern(new SymbolVariable("s.Fun"), new SymbolVariable("s.Q"), new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace());
			if (pattern74.Match(expression))
			{
				return PassiveExpression.Build(_QuotesError(PassiveExpression.Build(pattern74.GetVariable("s.Q"), new OpeningBrace(), pattern74.GetVariable("e.1"), new ClosingBrace())));
			};

			throw new RecognitionImpossibleException("Recognition impossible. Last expression: " + expression.ToString());
		}

		private static PassiveExpression _QuotesError(PassiveExpression expression)
		{
			Pattern pattern75 = new Pattern(new SymbolVariable("s.Q"), new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace());
			if (pattern75.Match(expression))
			{
				return PassiveExpression.Build(_Prout(PassiveExpression.Build()), "*".ToCharArray(), "Error", _Prout(PassiveExpression.Build("*** ERROR: Unbalanced quote in input".ToCharArray())), _Prout(PassiveExpression.Build("*** ".ToCharArray(), pattern75.GetVariable("e.1"))), _Prout(PassiveExpression.Build("*** ".ToCharArray(), pattern75.GetVariable("s.Q"))));
			};

			throw new RecognitionImpossibleException("Recognition impossible. Last expression: " + expression.ToString());
		}

		private static PassiveExpression _Hex(PassiveExpression expression)
		{
			Pattern pattern76 = new Pattern(new SymbolVariable("s.d1"), new SymbolVariable("s.d2"));
			if (pattern76.Match(expression))
			{
				expression = PassiveExpression.Build(_Hex(PassiveExpression.Build(pattern76.GetVariable("s.d1"))));
				Pattern pattern77 = new Pattern(new SymbolVariable("s.h1"));
				pattern77.CopyBoundVariables(pattern76);
				if (pattern77.Match(expression))
				{
					expression = PassiveExpression.Build(_Hex(PassiveExpression.Build(pattern77.GetVariable("s.d2"))));
					Pattern pattern78 = new Pattern(new SymbolVariable("s.h2"));
					pattern78.CopyBoundVariables(pattern77);
					if (pattern78.Match(expression))
					{
						return PassiveExpression.Build(_Chr(PassiveExpression.Build(_Add(PassiveExpression.Build(new OpeningBrace(), _Mul(PassiveExpression.Build(pattern78.GetVariable("s.h1"), 16)), new ClosingBrace(), pattern78.GetVariable("s.h2"))))));
					}
				}
			};

			Pattern pattern79 = new Pattern(new SymbolVariable("s.h"));
			if (pattern79.Match(expression))
			{
				expression = PassiveExpression.Build(_Type(PassiveExpression.Build(pattern79.GetVariable("s.h"))));
				Pattern pattern80 = new Pattern("D".ToCharArray(), new ExpressionVariable("e.D1"));
				pattern80.CopyBoundVariables(pattern79);
				if (pattern80.Match(expression))
				{
					return PassiveExpression.Build(pattern80.GetVariable("s.h"));
				}
			};

			Pattern pattern81 = new Pattern("A".ToCharArray());
			if (pattern81.Match(expression))
			{
				return PassiveExpression.Build(10);
			};

			Pattern pattern82 = new Pattern("a".ToCharArray());
			if (pattern82.Match(expression))
			{
				return PassiveExpression.Build(10);
			};

			Pattern pattern83 = new Pattern("B".ToCharArray());
			if (pattern83.Match(expression))
			{
				return PassiveExpression.Build(11);
			};

			Pattern pattern84 = new Pattern("b".ToCharArray());
			if (pattern84.Match(expression))
			{
				return PassiveExpression.Build(11);
			};

			Pattern pattern85 = new Pattern("C".ToCharArray());
			if (pattern85.Match(expression))
			{
				return PassiveExpression.Build(12);
			};

			Pattern pattern86 = new Pattern("c".ToCharArray());
			if (pattern86.Match(expression))
			{
				return PassiveExpression.Build(12);
			};

			Pattern pattern87 = new Pattern("D".ToCharArray());
			if (pattern87.Match(expression))
			{
				return PassiveExpression.Build(13);
			};

			Pattern pattern88 = new Pattern("d".ToCharArray());
			if (pattern88.Match(expression))
			{
				return PassiveExpression.Build(13);
			};

			Pattern pattern89 = new Pattern("E".ToCharArray());
			if (pattern89.Match(expression))
			{
				return PassiveExpression.Build(14);
			};

			Pattern pattern90 = new Pattern("e".ToCharArray());
			if (pattern90.Match(expression))
			{
				return PassiveExpression.Build(14);
			};

			Pattern pattern91 = new Pattern("F".ToCharArray());
			if (pattern91.Match(expression))
			{
				return PassiveExpression.Build(15);
			};

			Pattern pattern92 = new Pattern("f".ToCharArray());
			if (pattern92.Match(expression))
			{
				return PassiveExpression.Build(15);
			};

			Pattern pattern93 = new Pattern(new ExpressionVariable("e.hs"));
			if (pattern93.Match(expression))
			{
				return PassiveExpression.Build();
			};

			throw new RecognitionImpossibleException("Recognition impossible. Last expression: " + expression.ToString());
		}

		private static PassiveExpression _Parse1(PassiveExpression expression)
		{
			Pattern pattern94 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), new OpeningBrace(), "L".ToCharArray(), new ExpressionVariable("e.A1"), new ClosingBrace(), new SymbolVariable("s.A"), new ExpressionVariable("e.2"));
			if (pattern94.Match(expression))
			{
				return PassiveExpression.Build(_Word(PassiveExpression.Build(new OpeningBrace(), pattern94.GetVariable("e.1"), new ClosingBrace(), new OpeningBrace(), pattern94.GetVariable("s.A"), new ClosingBrace(), pattern94.GetVariable("e.2"))));
			};

			Pattern pattern95 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), new OpeningBrace(), "D".ToCharArray(), new ExpressionVariable("e.A1"), new ClosingBrace(), new SymbolVariable("s.A"), new ExpressionVariable("e.2"));
			if (pattern95.Match(expression))
			{
				return PassiveExpression.Build(_Number(PassiveExpression.Build(new OpeningBrace(), pattern95.GetVariable("e.1"), new ClosingBrace(), new OpeningBrace(), pattern95.GetVariable("s.A"), new ClosingBrace(), pattern95.GetVariable("e.2"))));
			};

			Pattern pattern96 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), new OpeningBrace(), new SymbolVariable("s.T"), new ExpressionVariable("e.A1"), new ClosingBrace(), new SymbolVariable("s.A"), new ExpressionVariable("e.2"));
			if (pattern96.Match(expression))
			{
				return PassiveExpression.Build(_Parse(PassiveExpression.Build(new OpeningBrace(), pattern96.GetVariable("e.1"), pattern96.GetVariable("s.A"), new ClosingBrace(), pattern96.GetVariable("e.2"))));
			};

			throw new RecognitionImpossibleException("Recognition impossible. Last expression: " + expression.ToString());
		}

		private static PassiveExpression _Word(PassiveExpression expression)
		{
			Pattern pattern97 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), new OpeningBrace(), new ExpressionVariable("e.W"), new ClosingBrace(), new SymbolVariable("s.B"), new ExpressionVariable("e.2"));
			if (pattern97.Match(expression))
			{
				expression = PassiveExpression.Build(_Type(PassiveExpression.Build(pattern97.GetVariable("s.B"))));
				{
					Pattern pattern98 = new Pattern("L".ToCharArray(), new ExpressionVariable("e.B1"));
					pattern98.CopyBoundVariables(pattern97);
					if (pattern98.Match(expression))
					{
						return PassiveExpression.Build(_Word(PassiveExpression.Build(new OpeningBrace(), pattern98.GetVariable("e.1"), new ClosingBrace(), new OpeningBrace(), pattern98.GetVariable("e.W"), pattern98.GetVariable("s.B"), new ClosingBrace(), pattern98.GetVariable("e.2"))));
					};

					Pattern pattern99 = new Pattern("D".ToCharArray(), new ExpressionVariable("e.B1"));
					pattern99.CopyBoundVariables(pattern97);
					if (pattern99.Match(expression))
					{
						return PassiveExpression.Build(_Word(PassiveExpression.Build(new OpeningBrace(), pattern99.GetVariable("e.1"), new ClosingBrace(), new OpeningBrace(), pattern99.GetVariable("e.W"), pattern99.GetVariable("s.B"), new ClosingBrace(), pattern99.GetVariable("e.2"))));
					};

					Pattern pattern100 = new Pattern("Ol-".ToCharArray());
					pattern100.CopyBoundVariables(pattern97);
					if (pattern100.Match(expression))
					{
						return PassiveExpression.Build(_Word(PassiveExpression.Build(new OpeningBrace(), pattern100.GetVariable("e.1"), new ClosingBrace(), new OpeningBrace(), pattern100.GetVariable("e.W"), "-".ToCharArray(), new ClosingBrace(), pattern100.GetVariable("e.2"))));
					};

					Pattern pattern101 = new Pattern("Ou_".ToCharArray());
					pattern101.CopyBoundVariables(pattern97);
					if (pattern101.Match(expression))
					{
						return PassiveExpression.Build(_Word(PassiveExpression.Build(new OpeningBrace(), pattern101.GetVariable("e.1"), new ClosingBrace(), new OpeningBrace(), pattern101.GetVariable("e.W"), "_".ToCharArray(), new ClosingBrace(), pattern101.GetVariable("e.2"))));
					};

					Pattern pattern102 = new Pattern(new SymbolVariable("s.T"), new ExpressionVariable("e.B1"));
					pattern102.CopyBoundVariables(pattern97);
					if (pattern102.Match(expression))
					{
						return PassiveExpression.Build(_Parse(PassiveExpression.Build(new OpeningBrace(), pattern102.GetVariable("e.1"), _Implode_Ext(PassiveExpression.Build(pattern102.GetVariable("e.W"))), new ClosingBrace(), pattern102.GetVariable("s.B"), pattern102.GetVariable("e.2"))));
					};

					throw new RecognitionImpossibleException("Recognition impossible. Last expression: " + expression.ToString());
				}
			};

			Pattern pattern104 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), new OpeningBrace(), new ExpressionVariable("e.W"), new ClosingBrace());
			if (pattern104.Match(expression))
			{
				return PassiveExpression.Build(_Parse(PassiveExpression.Build(new OpeningBrace(), pattern104.GetVariable("e.1"), _Implode_Ext(PassiveExpression.Build(pattern104.GetVariable("e.W"))), new ClosingBrace())));
			};

			throw new RecognitionImpossibleException("Recognition impossible. Last expression: " + expression.ToString());
		}

		private static PassiveExpression _Number(PassiveExpression expression)
		{
			Pattern pattern105 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), new OpeningBrace(), new ExpressionVariable("e.N"), new ClosingBrace(), new SymbolVariable("s.X"), new ExpressionVariable("e.2"));
			if (pattern105.Match(expression))
			{
				expression = PassiveExpression.Build(_Type(PassiveExpression.Build(pattern105.GetVariable("s.X"))));
				Pattern pattern106 = new Pattern("D".ToCharArray(), new ExpressionVariable("e.X1"));
				pattern106.CopyBoundVariables(pattern105);
				if (pattern106.Match(expression))
				{
					return PassiveExpression.Build(_Number(PassiveExpression.Build(new OpeningBrace(), pattern106.GetVariable("e.1"), new ClosingBrace(), new OpeningBrace(), pattern106.GetVariable("e.N"), pattern106.GetVariable("s.X"), new ClosingBrace(), pattern106.GetVariable("e.2"))));
				}
			};

			Pattern pattern107 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), new OpeningBrace(), new ExpressionVariable("e.N"), new ClosingBrace(), new ExpressionVariable("e.X"));
			if (pattern107.Match(expression))
			{
				return PassiveExpression.Build(_Parse(PassiveExpression.Build(new OpeningBrace(), pattern107.GetVariable("e.1"), _Numb(PassiveExpression.Build(pattern107.GetVariable("e.N"))), new ClosingBrace(), pattern107.GetVariable("e.X"))));
			};

			throw new RecognitionImpossibleException("Recognition impossible. Last expression: " + expression.ToString());
		}

		public static PassiveExpression _Xxout(PassiveExpression expression)
		{
			Pattern pattern108 = new Pattern(new SymbolVariable("s.C"), new ExpressionVariable("e.X"));
			if (pattern108.Match(expression))
			{
				expression = PassiveExpression.Build(_Type(PassiveExpression.Build(pattern108.GetVariable("s.C"))));
				Pattern pattern109 = new Pattern("N".ToCharArray(), new ExpressionVariable("e.C1"));
				pattern109.CopyBoundVariables(pattern108);
				if (pattern109.Match(expression))
				{
					return PassiveExpression.Build(_Xxout1(PassiveExpression.Build(pattern109.GetVariable("s.C"), pattern109.GetVariable("e.X"))));
				}
			};

			Pattern pattern110 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.File"), new ClosingBrace(), new ExpressionVariable("e.X"));
			if (pattern110.Match(expression))
			{
				expression = PassiveExpression.Build(_Status(PassiveExpression.Build("w".ToCharArray(), pattern110.GetVariable("e.File"))));
				{
					Pattern pattern111 = new Pattern("New", new SymbolVariable("s.C"));
					pattern111.CopyBoundVariables(pattern110);
					if (pattern111.Match(expression))
					{
						return PassiveExpression.Build(_Open(PassiveExpression.Build("w".ToCharArray(), pattern111.GetVariable("s.C"), pattern111.GetVariable("e.File"))), _Xxout1(PassiveExpression.Build(pattern111.GetVariable("s.C"), pattern111.GetVariable("e.X"))));
					};

					Pattern pattern112 = new Pattern("Old", new SymbolVariable("s.C"));
					pattern112.CopyBoundVariables(pattern110);
					if (pattern112.Match(expression))
					{
						return PassiveExpression.Build(_Xxout1(PassiveExpression.Build(pattern112.GetVariable("s.C"), pattern112.GetVariable("e.X"))));
					};

					throw new RecognitionImpossibleException("Recognition impossible. Last expression: " + expression.ToString());
				}
			};

			throw new RecognitionImpossibleException("Recognition impossible. Last expression: " + expression.ToString());
		}

		private static PassiveExpression _Xxout1(PassiveExpression expression)
		{
			Pattern pattern114 = new Pattern(new SymbolVariable("s.C"), new ExpressionVariable("e.X"));
			if (pattern114.Match(expression))
			{
				return PassiveExpression.Build(_Cut__Put(PassiveExpression.Build(pattern114.GetVariable("s.C"), _ConS(PassiveExpression.Build(pattern114.GetVariable("e.X"))))));
			};

			throw new RecognitionImpossibleException("Recognition impossible. Last expression: " + expression.ToString());
		}

		private static PassiveExpression _ConS(PassiveExpression expression)
		{
			Pattern pattern115 = new Pattern(new SymbolVariable("s.A"), new ExpressionVariable("e.1"));
			if (pattern115.Match(expression))
			{
				expression = PassiveExpression.Build(_Type(PassiveExpression.Build(pattern115.GetVariable("s.A"))));
				{
					Pattern pattern116 = new Pattern("W".ToCharArray(), new ExpressionVariable("e.A1"));
					pattern116.CopyBoundVariables(pattern115);
					if (pattern116.Match(expression))
					{
						return PassiveExpression.Build(_WORD(PassiveExpression.Build(_String(PassiveExpression.Build(new OpeningBrace(), "Word", "NoQuote", new ClosingBrace(), _Explode(PassiveExpression.Build(pattern116.GetVariable("s.A"))))))), " ".ToCharArray(), _ConS(PassiveExpression.Build(pattern116.GetVariable("e.1"))));
					};

					Pattern pattern117 = new Pattern("N".ToCharArray(), new ExpressionVariable("e.A1"));
					pattern117.CopyBoundVariables(pattern115);
					if (pattern117.Match(expression))
					{
						return PassiveExpression.Build(_Symb(PassiveExpression.Build(pattern117.GetVariable("s.A"))), " ".ToCharArray(), _ConS(PassiveExpression.Build(pattern117.GetVariable("e.1"))));
					};

					Pattern pattern118 = new Pattern(new SymbolVariable("s.T"), new ExpressionVariable("e.A1"));
					pattern118.CopyBoundVariables(pattern115);
					if (pattern118.Match(expression))
					{
						return PassiveExpression.Build("\'".ToCharArray(), _String(PassiveExpression.Build(new OpeningBrace(), "Chars", "NoQuote", new ClosingBrace(), pattern118.GetVariable("s.A"), pattern118.GetVariable("e.1"))));
					};

					throw new RecognitionImpossibleException("Recognition impossible. Last expression: " + expression.ToString());
				}
			};

			Pattern pattern120 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), new ExpressionVariable("e.2"));
			if (pattern120.Match(expression))
			{
				return PassiveExpression.Build("(".ToCharArray(), _ConS(PassiveExpression.Build(pattern120.GetVariable("e.1"))), ")".ToCharArray(), _ConS(PassiveExpression.Build(pattern120.GetVariable("e.2"))));
			};

			Pattern pattern121 = new Pattern();
			if (pattern121.Match(expression))
			{
				return PassiveExpression.Build();
			};

			throw new RecognitionImpossibleException("Recognition impossible. Last expression: " + expression.ToString());
		}

		private static PassiveExpression _WORD(PassiveExpression expression)
		{
			Pattern pattern122 = new Pattern(new ExpressionVariable("e.word"), "Quote");
			if (pattern122.Match(expression))
			{
				return PassiveExpression.Build("\"".ToCharArray(), pattern122.GetVariable("e.word"), "\"".ToCharArray());
			};

			Pattern pattern123 = new Pattern(new ExpressionVariable("e.word"), "NoQuote");
			if (pattern123.Match(expression))
			{
				return PassiveExpression.Build(pattern123.GetVariable("e.word"));
			};

			throw new RecognitionImpossibleException("Recognition impossible. Last expression: " + expression.ToString());
		}

		private static PassiveExpression _String(PassiveExpression expression)
		{
			Pattern pattern124 = new Pattern(new OpeningBrace(), new SymbolVariable("s.flag"), new ExpressionVariable("e.Quote"), new ClosingBrace(), "\\\\".ToCharArray(), new ExpressionVariable("e.1"));
			if (pattern124.Match(expression))
			{
				return PassiveExpression.Build("\\\\".ToCharArray(), "\\\\".ToCharArray(), _String(PassiveExpression.Build(new OpeningBrace(), pattern124.GetVariable("s.flag"), "Quote", new ClosingBrace(), pattern124.GetVariable("e.1"))));
			};

			Pattern pattern125 = new Pattern(new OpeningBrace(), new SymbolVariable("s.flag"), new ExpressionVariable("e.Quote"), new ClosingBrace(), "\'".ToCharArray(), new ExpressionVariable("e.1"));
			if (pattern125.Match(expression))
			{
				return PassiveExpression.Build("\\\\".ToCharArray(), "\'".ToCharArray(), _String(PassiveExpression.Build(new OpeningBrace(), pattern125.GetVariable("s.flag"), "Quote", new ClosingBrace(), pattern125.GetVariable("e.1"))));
			};

			Pattern pattern126 = new Pattern(new OpeningBrace(), new SymbolVariable("s.flag"), new ExpressionVariable("e.Quote"), new ClosingBrace(), "\"".ToCharArray(), new ExpressionVariable("e.1"));
			if (pattern126.Match(expression))
			{
				return PassiveExpression.Build("\\\\".ToCharArray(), "\"".ToCharArray(), _String(PassiveExpression.Build(new OpeningBrace(), pattern126.GetVariable("s.flag"), "Quote", new ClosingBrace(), pattern126.GetVariable("e.1"))));
			};

			Pattern pattern127 = new Pattern(new OpeningBrace(), new SymbolVariable("s.flag"), new ExpressionVariable("e.Quote"), new ClosingBrace(), "\\t".ToCharArray(), new ExpressionVariable("e.1"));
			if (pattern127.Match(expression))
			{
				return PassiveExpression.Build("\\\\".ToCharArray(), "t".ToCharArray(), _String(PassiveExpression.Build(new OpeningBrace(), pattern127.GetVariable("s.flag"), "Quote", new ClosingBrace(), pattern127.GetVariable("e.1"))));
			};

			Pattern pattern128 = new Pattern(new OpeningBrace(), new SymbolVariable("s.flag"), new ExpressionVariable("e.Quote"), new ClosingBrace(), "\\n".ToCharArray(), new ExpressionVariable("e.1"));
			if (pattern128.Match(expression))
			{
				return PassiveExpression.Build("\\\\".ToCharArray(), "n".ToCharArray(), _String(PassiveExpression.Build(new OpeningBrace(), pattern128.GetVariable("s.flag"), "Quote", new ClosingBrace(), pattern128.GetVariable("e.1"))));
			};

			Pattern pattern129 = new Pattern(new OpeningBrace(), new SymbolVariable("s.flag"), new ExpressionVariable("e.Quote"), new ClosingBrace(), "\\r".ToCharArray(), new ExpressionVariable("e.1"));
			if (pattern129.Match(expression))
			{
				return PassiveExpression.Build("\\\\".ToCharArray(), "r".ToCharArray(), _String(PassiveExpression.Build(new OpeningBrace(), pattern129.GetVariable("s.flag"), "Quote", new ClosingBrace(), pattern129.GetVariable("e.1"))));
			};

			Pattern pattern130 = new Pattern(new TermVariable("t.flag"), new SymbolVariable("s.A"), new ExpressionVariable("e.1"));
			if (pattern130.Match(expression))
			{
				expression = PassiveExpression.Build(_Type(PassiveExpression.Build(pattern130.GetVariable("s.A"))));
				{
					Pattern pattern131 = new Pattern("W".ToCharArray(), new ExpressionVariable("e.Word"));
					pattern131.CopyBoundVariables(pattern130);
					if (pattern131.Match(expression))
					{
						return PassiveExpression.Build(_String(PassiveExpression.Build(pattern131.GetVariable("t.flag"))), _WORD(PassiveExpression.Build(_String(PassiveExpression.Build(new OpeningBrace(), "Word", "NoQuote", new ClosingBrace(), _Explode(PassiveExpression.Build(pattern131.GetVariable("s.A"))))))), " ".ToCharArray(), _ConS(PassiveExpression.Build(pattern131.GetVariable("e.1"))));
					};

					Pattern pattern132 = new Pattern("N".ToCharArray(), new ExpressionVariable("e.Number"));
					pattern132.CopyBoundVariables(pattern130);
					if (pattern132.Match(expression))
					{
						return PassiveExpression.Build(_String(PassiveExpression.Build(pattern132.GetVariable("t.flag"))), _Symb(PassiveExpression.Build(pattern132.GetVariable("s.A"))), " ".ToCharArray(), _ConS(PassiveExpression.Build(pattern132.GetVariable("e.1"))));
					};

					Pattern pattern133 = new Pattern("L".ToCharArray(), new ExpressionVariable("e.Letter"));
					pattern133.CopyBoundVariables(pattern130);
					if (pattern133.Match(expression))
					{
						return PassiveExpression.Build(pattern133.GetVariable("s.A"), _String(PassiveExpression.Build(pattern133.GetVariable("t.flag"), pattern133.GetVariable("e.1"))));
					};

					Pattern pattern134 = new Pattern("D".ToCharArray(), new ExpressionVariable("e.Digit"));
					pattern134.CopyBoundVariables(pattern130);
					if (pattern134.Match(expression))
					{
						return PassiveExpression.Build(pattern134.GetVariable("s.A"), _String(PassiveExpression.Build(pattern134.GetVariable("t.flag"), pattern134.GetVariable("e.1"))));
					};

					Pattern pattern135 = new Pattern("P".ToCharArray(), new ExpressionVariable("e.Printable"));
					pattern135.CopyBoundVariables(pattern130);
					if (pattern135.Match(expression))
					{
						return PassiveExpression.Build(pattern135.GetVariable("s.A"), _String(PassiveExpression.Build(pattern135.GetVariable("t.flag"), pattern135.GetVariable("e.1"))));
					};

					Pattern pattern136 = new Pattern(new SymbolVariable("s.T"), new ExpressionVariable("e.A1"));
					pattern136.CopyBoundVariables(pattern130);
					if (pattern136.Match(expression))
					{
						expression = PassiveExpression.Build(pattern136.GetVariable("t.flag"));
						Pattern pattern137 = new Pattern(new OpeningBrace(), new SymbolVariable("s.flag"), new ExpressionVariable("e.Quote"), new ClosingBrace());
						pattern137.CopyBoundVariables(pattern136);
						if (pattern137.Match(expression))
						{
							return PassiveExpression.Build("\\\\x".ToCharArray(), _ToHex(PassiveExpression.Build(_Ord(PassiveExpression.Build(pattern137.GetVariable("s.A"))))), _String(PassiveExpression.Build(new OpeningBrace(), pattern137.GetVariable("s.flag"), "Quote", new ClosingBrace(), pattern137.GetVariable("e.1"))));
						}
					};

					throw new RecognitionImpossibleException("Recognition impossible. Last expression: " + expression.ToString());
				}
			};

			Pattern pattern139 = new Pattern(new TermVariable("t.flag"), new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), new ExpressionVariable("e.2"));
			if (pattern139.Match(expression))
			{
				return PassiveExpression.Build(_String(PassiveExpression.Build(pattern139.GetVariable("t.flag"))), "(".ToCharArray(), _ConS(PassiveExpression.Build(pattern139.GetVariable("e.1"))), ")".ToCharArray(), _ConS(PassiveExpression.Build(pattern139.GetVariable("e.2"))));
			};

			Pattern pattern140 = new Pattern(new OpeningBrace(), "Word", new SymbolVariable("s.Quote"), new ClosingBrace());
			if (pattern140.Match(expression))
			{
				return PassiveExpression.Build(pattern140.GetVariable("s.Quote"));
			};

			Pattern pattern141 = new Pattern(new OpeningBrace(), "Chars", new ExpressionVariable("e.Quote"), new ClosingBrace());
			if (pattern141.Match(expression))
			{
				return PassiveExpression.Build("\'".ToCharArray());
			};

			throw new RecognitionImpossibleException("Recognition impossible. Last expression: " + expression.ToString());
		}

		private static PassiveExpression _ToHex(PassiveExpression expression)
		{
			Pattern pattern142 = new Pattern(new SymbolVariable("s.digit"));
			if (pattern142.Match(expression))
			{
				expression = PassiveExpression.Build(_Divmod(PassiveExpression.Build(new OpeningBrace(), pattern142.GetVariable("s.digit"), new ClosingBrace(), 16)));
				Pattern pattern143 = new Pattern(new OpeningBrace(), new SymbolVariable("s.q"), new ClosingBrace(), new SymbolVariable("s.r"));
				pattern143.CopyBoundVariables(pattern142);
				if (pattern143.Match(expression))
				{
					return PassiveExpression.Build(_ToHexDig(PassiveExpression.Build(pattern143.GetVariable("s.q"))), _ToHexDig(PassiveExpression.Build(pattern143.GetVariable("s.r"))));
				}
			};

			throw new RecognitionImpossibleException("Recognition impossible. Last expression: " + expression.ToString());
		}

		private static PassiveExpression _ToHexDig(PassiveExpression expression)
		{
			Pattern pattern144 = new Pattern(15);
			if (pattern144.Match(expression))
			{
				return PassiveExpression.Build("F".ToCharArray());
			};

			Pattern pattern145 = new Pattern(14);
			if (pattern145.Match(expression))
			{
				return PassiveExpression.Build("E".ToCharArray());
			};

			Pattern pattern146 = new Pattern(13);
			if (pattern146.Match(expression))
			{
				return PassiveExpression.Build("D".ToCharArray());
			};

			Pattern pattern147 = new Pattern(12);
			if (pattern147.Match(expression))
			{
				return PassiveExpression.Build("C".ToCharArray());
			};

			Pattern pattern148 = new Pattern(11);
			if (pattern148.Match(expression))
			{
				return PassiveExpression.Build("B".ToCharArray());
			};

			Pattern pattern149 = new Pattern(10);
			if (pattern149.Match(expression))
			{
				return PassiveExpression.Build("A".ToCharArray());
			};

			Pattern pattern150 = new Pattern(new SymbolVariable("s.d"));
			if (pattern150.Match(expression))
			{
				return PassiveExpression.Build(_Symb(PassiveExpression.Build(pattern150.GetVariable("s.d"))));
			};

			throw new RecognitionImpossibleException("Recognition impossible. Last expression: " + expression.ToString());
		}

		private static PassiveExpression _Cut__Put(PassiveExpression expression)
		{
			Pattern pattern151 = new Pattern(new SymbolVariable("s.C"));
			if (pattern151.Match(expression))
			{
				return PassiveExpression.Build();
			};

			Pattern pattern152 = new Pattern(new SymbolVariable("s.C"), new ExpressionVariable("e.X"));
			if (pattern152.Match(expression))
			{
				expression = PassiveExpression.Build(_First(PassiveExpression.Build(75, pattern152.GetVariable("e.X"))));
				{
					Pattern pattern153 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace());
					pattern153.CopyBoundVariables(pattern152);
					if (pattern153.Match(expression))
					{
						return PassiveExpression.Build(_Putout(PassiveExpression.Build(pattern153.GetVariable("s.C"), pattern153.GetVariable("e.1"))));
					};

					Pattern pattern154 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), new ExpressionVariable("e.2"));
					pattern154.CopyBoundVariables(pattern152);
					if (pattern154.Match(expression))
					{
						return PassiveExpression.Build(_Putout(PassiveExpression.Build(pattern154.GetVariable("s.C"), pattern154.GetVariable("e.1"))), _Cut__Put(PassiveExpression.Build(pattern154.GetVariable("s.C"), pattern154.GetVariable("e.2"))));
					};

					throw new RecognitionImpossibleException("Recognition impossible. Last expression: " + expression.ToString());
				}
			};

			throw new RecognitionImpossibleException("Recognition impossible. Last expression: " + expression.ToString());
		}

		public static PassiveExpression _Xxinr(PassiveExpression expression)
		{
			Pattern pattern156 = new Pattern();
			if (pattern156.Match(expression))
			{
				return PassiveExpression.Build(_Xxinr1(PassiveExpression.Build(0)));
			};

			Pattern pattern157 = new Pattern(new SymbolVariable("s.C"));
			if (pattern157.Match(expression))
			{
				expression = PassiveExpression.Build(_Type(PassiveExpression.Build(pattern157.GetVariable("s.C"))));
				Pattern pattern158 = new Pattern("N".ToCharArray(), new ExpressionVariable("e.C1"));
				pattern158.CopyBoundVariables(pattern157);
				if (pattern158.Match(expression))
				{
					return PassiveExpression.Build(_Xxinr1(PassiveExpression.Build(pattern158.GetVariable("s.C"))));
				}
			};

			Pattern pattern159 = new Pattern(new ExpressionVariable("e.File"));
			if (pattern159.Match(expression))
			{
				expression = PassiveExpression.Build(_Status(PassiveExpression.Build("r".ToCharArray(), pattern159.GetVariable("e.File"))));
				{
					Pattern pattern160 = new Pattern("New", new SymbolVariable("s.C"));
					pattern160.CopyBoundVariables(pattern159);
					if (pattern160.Match(expression))
					{
						return PassiveExpression.Build(_Open(PassiveExpression.Build("r".ToCharArray(), pattern160.GetVariable("s.C"), pattern160.GetVariable("e.File"))), _Xxinr1(PassiveExpression.Build(pattern160.GetVariable("s.C"))));
					};

					Pattern pattern161 = new Pattern("Old", new SymbolVariable("s.C"));
					pattern161.CopyBoundVariables(pattern159);
					if (pattern161.Match(expression))
					{
						return PassiveExpression.Build(_Xxinr1(PassiveExpression.Build(pattern161.GetVariable("s.C"))));
					};

					throw new RecognitionImpossibleException("Recognition impossible. Last expression: " + expression.ToString());
				}
			};

			throw new RecognitionImpossibleException("Recognition impossible. Last expression: " + expression.ToString());
		}

		private static PassiveExpression _Xxinr1(PassiveExpression expression)
		{
			Pattern pattern163 = new Pattern(new SymbolVariable("s.C"));
			if (pattern163.Match(expression))
			{
				return PassiveExpression.Build(_Mescp(PassiveExpression.Build(new OpeningBrace(), "$".ToCharArray(), new ClosingBrace(), _Read__all(PassiveExpression.Build(pattern163.GetVariable("s.C"), _Get(PassiveExpression.Build(pattern163.GetVariable("s.C"))))))));
			};

			throw new RecognitionImpossibleException("Recognition impossible. Last expression: " + expression.ToString());
		}

		private static PassiveExpression _Read__all(PassiveExpression expression)
		{
			Pattern pattern164 = new Pattern(new SymbolVariable("s.C"), 0);
			if (pattern164.Match(expression))
			{
				return PassiveExpression.Build("EOF");
			};

			Pattern pattern165 = new Pattern(new SymbolVariable("s.C"), new ExpressionVariable("e.X"));
			if (pattern165.Match(expression))
			{
				return PassiveExpression.Build(pattern165.GetVariable("e.X"), _Read__all(PassiveExpression.Build(pattern165.GetVariable("s.C"), _Get(PassiveExpression.Build(pattern165.GetVariable("s.C"))))));
			};

			throw new RecognitionImpossibleException("Recognition impossible. Last expression: " + expression.ToString());
		}

		private static PassiveExpression _Mescp(PassiveExpression expression)
		{
			Pattern pattern166 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), "\'".ToCharArray(), new ExpressionVariable("e.2"));
			if (pattern166.Match(expression))
			{
				return PassiveExpression.Build(_Quotes(PassiveExpression.Build("Mescp", "\'".ToCharArray(), new OpeningBrace(), pattern166.GetVariable("e.1"), new ClosingBrace(), pattern166.GetVariable("e.2"))));
			};

			Pattern pattern167 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), "\"".ToCharArray(), new ExpressionVariable("e.2"));
			if (pattern167.Match(expression))
			{
				return PassiveExpression.Build(_Quotes(PassiveExpression.Build("Mescp", "\"".ToCharArray(), new OpeningBrace(), new OpeningBrace(), pattern167.GetVariable("e.1"), new ClosingBrace(), new ClosingBrace(), pattern167.GetVariable("e.2"))));
			};

			Pattern pattern168 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), "\\\\".ToCharArray(), new ExpressionVariable("e.2"));
			if (pattern168.Match(expression))
			{
				return PassiveExpression.Build(_Prout(PassiveExpression.Build()), "*".ToCharArray(), "Error", _Prout(PassiveExpression.Build("*** ERROR: Unexpected escape sequnce in input".ToCharArray())), _Prout(PassiveExpression.Build("*** ".ToCharArray(), pattern168.GetVariable("e.1"))), _Prout(PassiveExpression.Build("*** ".ToCharArray(), "\\\\".ToCharArray(), pattern168.GetVariable("e.2"))));
			};

			Pattern pattern169 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), "(".ToCharArray(), new ExpressionVariable("e.2"));
			if (pattern169.Match(expression))
			{
				return PassiveExpression.Build(_Mescp(PassiveExpression.Build(new OpeningBrace(), new OpeningBrace(), pattern169.GetVariable("e.1"), new ClosingBrace(), new ClosingBrace(), pattern169.GetVariable("e.2"))));
			};

			Pattern pattern170 = new Pattern(new OpeningBrace(), new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), new ExpressionVariable("e.2"), new ClosingBrace(), ")".ToCharArray(), new ExpressionVariable("e.3"));
			if (pattern170.Match(expression))
			{
				return PassiveExpression.Build(_Mescp(PassiveExpression.Build(new OpeningBrace(), pattern170.GetVariable("e.1"), new OpeningBrace(), pattern170.GetVariable("e.2"), new ClosingBrace(), new ClosingBrace(), pattern170.GetVariable("e.3"))));
			};

			Pattern pattern171 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), ")".ToCharArray(), new ExpressionVariable("e.3"));
			if (pattern171.Match(expression))
			{
				return PassiveExpression.Build(_Prout(PassiveExpression.Build()), _Prout(PassiveExpression.Build()), _Prout(PassiveExpression.Build("Unbalanced ) in input".ToCharArray())), _Prout(PassiveExpression.Build(pattern171.GetVariable("e.1"), " ***)***".ToCharArray())));
			};

			Pattern pattern172 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), "EOF");
			if (pattern172.Match(expression))
			{
				return PassiveExpression.Build(_Mescp(PassiveExpression.Build(new OpeningBrace(), pattern172.GetVariable("e.1"), new ClosingBrace())));
			};

			Pattern pattern173 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), new SymbolVariable("s.A"), new ExpressionVariable("e.2"));
			if (pattern173.Match(expression))
			{
				return PassiveExpression.Build(_Mescp1(PassiveExpression.Build(new OpeningBrace(), _Type(PassiveExpression.Build(pattern173.GetVariable("s.A"))), new ClosingBrace(), new OpeningBrace(), pattern173.GetVariable("e.1"), new ClosingBrace(), new OpeningBrace(), pattern173.GetVariable("s.A"), new ClosingBrace(), pattern173.GetVariable("e.2"))));
			};

			Pattern pattern174 = new Pattern(new OpeningBrace(), "$".ToCharArray(), new ExpressionVariable("e.1"), new ClosingBrace());
			if (pattern174.Match(expression))
			{
				return PassiveExpression.Build(pattern174.GetVariable("e.1"));
			};

			Pattern pattern175 = new Pattern(new OpeningBrace(), new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), new ExpressionVariable("e.2"), new ClosingBrace());
			if (pattern175.Match(expression))
			{
				return PassiveExpression.Build(_Prout(PassiveExpression.Build()), _Prout(PassiveExpression.Build()), _Prout(PassiveExpression.Build("Unbalanced (... in input".ToCharArray())), _Prout(PassiveExpression.Build(pattern175.GetVariable("e.1"), " ***(***".ToCharArray())));
			};

			throw new RecognitionImpossibleException("Recognition impossible. Last expression: " + expression.ToString());
		}

		private static PassiveExpression _Mescp1(PassiveExpression expression)
		{
			Pattern pattern176 = new Pattern(new OpeningBrace(), "L".ToCharArray(), new ExpressionVariable("e.A1"), new ClosingBrace(), new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), new OpeningBrace(), new SymbolVariable("s.A"), new ExpressionVariable("e.2"), new ClosingBrace(), " ".ToCharArray(), new ExpressionVariable("e.3"));
			if (pattern176.Match(expression))
			{
				return PassiveExpression.Build(_Mescp(PassiveExpression.Build(new OpeningBrace(), pattern176.GetVariable("e.1"), _Implode(PassiveExpression.Build(pattern176.GetVariable("s.A"), pattern176.GetVariable("e.2"))), new ClosingBrace(), pattern176.GetVariable("e.3"))));
			};

			Pattern pattern177 = new Pattern(new OpeningBrace(), "L".ToCharArray(), new ExpressionVariable("e.A1"), new ClosingBrace(), new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), new OpeningBrace(), new SymbolVariable("s.A"), new ExpressionVariable("e.2"), new ClosingBrace(), "EOF");
			if (pattern177.Match(expression))
			{
				return PassiveExpression.Build(_Mescp(PassiveExpression.Build(new OpeningBrace(), pattern177.GetVariable("e.1"), _Implode(PassiveExpression.Build(pattern177.GetVariable("s.A"), pattern177.GetVariable("e.2"))), new ClosingBrace())));
			};

			Pattern pattern178 = new Pattern(new OpeningBrace(), "D".ToCharArray(), new ExpressionVariable("e.A1"), new ClosingBrace(), new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), new OpeningBrace(), new SymbolVariable("s.A"), new ExpressionVariable("e.2"), new ClosingBrace(), " ".ToCharArray(), new ExpressionVariable("e.3"));
			if (pattern178.Match(expression))
			{
				return PassiveExpression.Build(_Mescp(PassiveExpression.Build(new OpeningBrace(), pattern178.GetVariable("e.1"), _Numb(PassiveExpression.Build(pattern178.GetVariable("s.A"), pattern178.GetVariable("e.2"))), new ClosingBrace(), pattern178.GetVariable("e.3"))));
			};

			Pattern pattern179 = new Pattern(new OpeningBrace(), "D".ToCharArray(), new ExpressionVariable("e.A1"), new ClosingBrace(), new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), new OpeningBrace(), new SymbolVariable("s.A"), new ExpressionVariable("e.2"), new ClosingBrace(), "EOF");
			if (pattern179.Match(expression))
			{
				return PassiveExpression.Build(_Mescp(PassiveExpression.Build(new OpeningBrace(), pattern179.GetVariable("e.1"), _Numb(PassiveExpression.Build(pattern179.GetVariable("s.A"), pattern179.GetVariable("e.2"))), new ClosingBrace())));
			};

			Pattern pattern180 = new Pattern(new OpeningBrace(), new SymbolVariable("s.T"), new ExpressionVariable("e.A1"), new ClosingBrace(), new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), new OpeningBrace(), new SymbolVariable("s.A"), new ExpressionVariable("e.2"), new ClosingBrace(), new SymbolVariable("s.X"), new ExpressionVariable("e.3"));
			if (pattern180.Match(expression))
			{
				return PassiveExpression.Build(_Mescp1(PassiveExpression.Build(new OpeningBrace(), pattern180.GetVariable("s.T"), pattern180.GetVariable("e.A1"), new ClosingBrace(), new OpeningBrace(), pattern180.GetVariable("e.1"), new ClosingBrace(), new OpeningBrace(), pattern180.GetVariable("s.A"), pattern180.GetVariable("e.2"), pattern180.GetVariable("s.X"), new ClosingBrace(), pattern180.GetVariable("e.3"))));
			};

			throw new RecognitionImpossibleException("Recognition impossible. Last expression: " + expression.ToString());
		}
	}
}

