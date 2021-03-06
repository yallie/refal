
using System;
using System.Collections;

namespace Refal.Runtime
{
	public class test14 : RefalBase
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
			if (pattern1.Match(expression))
			{
				return PassiveExpression.Build(_Open(PassiveExpression.Build("r".ToCharArray(), 1, _Arg(PassiveExpression.Build(1)))), _Open(PassiveExpression.Build("w".ToCharArray(), 2, _Arg(PassiveExpression.Build(2)))), _Mbprep(PassiveExpression.Build(_Next(PassiveExpression.Build()))));
			};

			throw new RecognitionImpossibleException("Recognition impossible. Last expression: " + expression.ToString());
		}

		private static PassiveExpression _Next(PassiveExpression expression)
		{
			Pattern pattern2 = new Pattern();
			if (pattern2.Match(expression))
			{
				expression = PassiveExpression.Build(_Get(PassiveExpression.Build(1)));
				{
					Pattern pattern3 = new Pattern("*".ToCharArray(), new ExpressionVariable("e.1"));
					pattern3.CopyBoundVariables(pattern2);
					if (pattern3.Match(expression))
					{
						return PassiveExpression.Build(_Putz(PassiveExpression.Build(2, "*".ToCharArray(), pattern3.GetVariable("e.1"))), _Next(PassiveExpression.Build()));
					};

					Pattern pattern4 = new Pattern(new ExpressionVariable("e.1"));
					pattern4.CopyBoundVariables(pattern2);
					if (pattern4.Match(expression))
					{
						return PassiveExpression.Build(pattern4.GetVariable("e.1"));
					};

					throw new RecognitionImpossibleException("Recognition impossible. Last expression: " + expression.ToString());
				}
			};

			throw new RecognitionImpossibleException("Recognition impossible. Last expression: " + expression.ToString());
		}

		private static PassiveExpression _Mbprep(PassiveExpression expression)
		{
			Pattern pattern6 = new Pattern(0);
			if (pattern6.Match(expression))
			{
				return PassiveExpression.Build();
			};

			Pattern pattern7 = new Pattern(new ExpressionVariable("e.1"));
			if (pattern7.Match(expression))
			{
				expression = PassiveExpression.Build(_Lookm0(PassiveExpression.Build(pattern7.GetVariable("e.1"))));
				{
					Pattern pattern8 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.A"), new ClosingBrace(), "[".ToCharArray(), new ExpressionVariable("e.B"));
					pattern8.CopyBoundVariables(pattern7);
					if (pattern8.Match(expression))
					{
						return PassiveExpression.Build(_Out(PassiveExpression.Build(pattern8.GetVariable("e.A"), _Lookp(PassiveExpression.Build(new OpeningBrace(), new OpeningBrace(), "[".ToCharArray(), new ClosingBrace(), new ClosingBrace(), new OpeningBrace(), pattern8.GetVariable("e.B"), new ClosingBrace())))));
					};

					Pattern pattern9 = new Pattern(new ExpressionVariable("e.1"));
					pattern9.CopyBoundVariables(pattern7);
					if (pattern9.Match(expression))
					{
						return PassiveExpression.Build(_Putz(PassiveExpression.Build(2, pattern9.GetVariable("e.1"))), _Mbprep(PassiveExpression.Build(_Next(PassiveExpression.Build()))));
					};

					throw new RecognitionImpossibleException("Recognition impossible. Last expression: " + expression.ToString());
				}
			};

			throw new RecognitionImpossibleException("Recognition impossible. Last expression: " + expression.ToString());
		}

		private static PassiveExpression _Lookm0(PassiveExpression expression)
		{
			Pattern pattern11 = new Pattern(new ExpressionVariable("e.1"), "[".ToCharArray(), new ExpressionVariable("e.2"));
			if (pattern11.Match(expression))
			{
				return PassiveExpression.Build(_Lookm(PassiveExpression.Build(new OpeningBrace(), new ClosingBrace(), pattern11.GetVariable("e.1"), "[".ToCharArray(), pattern11.GetVariable("e.2"))));
			};

			Pattern pattern12 = new Pattern(new ExpressionVariable("e.1"));
			if (pattern12.Match(expression))
			{
				return PassiveExpression.Build(pattern12.GetVariable("e.1"));
			};

			throw new RecognitionImpossibleException("Recognition impossible. Last expression: " + expression.ToString());
		}

		private static PassiveExpression _Lookm(PassiveExpression expression)
		{
			Pattern pattern13 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), "[".ToCharArray(), new ExpressionVariable("e.2"));
			if (pattern13.Match(expression))
			{
				return PassiveExpression.Build(new OpeningBrace(), pattern13.GetVariable("e.1"), new ClosingBrace(), "[".ToCharArray(), pattern13.GetVariable("e.2"));
			};

			Pattern pattern14 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), "\"".ToCharArray(), new ExpressionVariable("e.2"));
			if (pattern14.Match(expression))
			{
				expression = PassiveExpression.Build(_Quotes(PassiveExpression.Build("\"".ToCharArray(), new OpeningBrace(), new ClosingBrace(), pattern14.GetVariable("e.2"))));
				Pattern pattern15 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.0"), new ClosingBrace(), new ExpressionVariable("e.3"));
				pattern15.CopyBoundVariables(pattern14);
				if (pattern15.Match(expression))
				{
					return PassiveExpression.Build(_Lookm(PassiveExpression.Build(new OpeningBrace(), pattern15.GetVariable("e.1"), pattern15.GetVariable("e.0"), new ClosingBrace(), pattern15.GetVariable("e.3"))));
				}
			};

			Pattern pattern16 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), "\'".ToCharArray(), new ExpressionVariable("e.2"));
			if (pattern16.Match(expression))
			{
				expression = PassiveExpression.Build(_Quotes(PassiveExpression.Build("\'".ToCharArray(), new OpeningBrace(), new ClosingBrace(), pattern16.GetVariable("e.2"))));
				Pattern pattern17 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.0"), new ClosingBrace(), new ExpressionVariable("e.3"));
				pattern17.CopyBoundVariables(pattern16);
				if (pattern17.Match(expression))
				{
					return PassiveExpression.Build(_Lookm(PassiveExpression.Build(new OpeningBrace(), pattern17.GetVariable("e.1"), pattern17.GetVariable("e.0"), new ClosingBrace(), pattern17.GetVariable("e.3"))));
				}
			};

			Pattern pattern18 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), "/*".ToCharArray(), new ExpressionVariable("e.2"), "*/".ToCharArray(), new ExpressionVariable("e.3"));
			if (pattern18.Match(expression))
			{
				return PassiveExpression.Build(_Lookm(PassiveExpression.Build(new OpeningBrace(), pattern18.GetVariable("e.1"), "/*".ToCharArray(), pattern18.GetVariable("e.2"), "*/".ToCharArray(), new ClosingBrace(), pattern18.GetVariable("e.3"))));
			};

			Pattern pattern19 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), new TermVariable("t.A"), new ExpressionVariable("e.2"));
			if (pattern19.Match(expression))
			{
				return PassiveExpression.Build(_Lookm(PassiveExpression.Build(new OpeningBrace(), pattern19.GetVariable("e.1"), pattern19.GetVariable("t.A"), new ClosingBrace(), pattern19.GetVariable("e.2"))));
			};

			Pattern pattern20 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace());
			if (pattern20.Match(expression))
			{
				return PassiveExpression.Build(pattern20.GetVariable("e.1"));
			};

			throw new RecognitionImpossibleException("Recognition impossible. Last expression: " + expression.ToString());
		}

		private static PassiveExpression _Lookp(PassiveExpression expression)
		{
			Pattern pattern21 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.ML"), new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), new ClosingBrace(), new OpeningBrace(), "[".ToCharArray(), new ExpressionVariable("e.2"), new ClosingBrace());
			if (pattern21.Match(expression))
			{
				return PassiveExpression.Build(_Lookp(PassiveExpression.Build(new OpeningBrace(), pattern21.GetVariable("e.ML"), new OpeningBrace(), pattern21.GetVariable("e.1"), new ClosingBrace(), new OpeningBrace(), "[".ToCharArray(), new ClosingBrace(), new ClosingBrace(), new OpeningBrace(), pattern21.GetVariable("e.2"), new ClosingBrace())));
			};

			Pattern pattern22 = new Pattern(new OpeningBrace(), new OpeningBrace(), "[".ToCharArray(), new ExpressionVariable("e.1"), new ClosingBrace(), new ClosingBrace(), new OpeningBrace(), "]".ToCharArray(), new ExpressionVariable("e.2"), new ClosingBrace());
			if (pattern22.Match(expression))
			{
				return PassiveExpression.Build(_Transl(PassiveExpression.Build(new OpeningBrace(), new OpeningBrace(), new ClosingBrace(), new ClosingBrace(), new OpeningBrace(), "[".ToCharArray(), pattern22.GetVariable("e.1"), "]".ToCharArray(), new ClosingBrace())), pattern22.GetVariable("e.2"));
			};

			Pattern pattern23 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.ML"), new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), new OpeningBrace(), "[".ToCharArray(), new ExpressionVariable("e.2"), new ClosingBrace(), new ClosingBrace(), new OpeningBrace(), "]".ToCharArray(), new ExpressionVariable("e.3"), new ClosingBrace());
			if (pattern23.Match(expression))
			{
				return PassiveExpression.Build(_Lookp(PassiveExpression.Build(new OpeningBrace(), pattern23.GetVariable("e.ML"), new OpeningBrace(), pattern23.GetVariable("e.1"), _Transl(PassiveExpression.Build(new OpeningBrace(), new OpeningBrace(), new ClosingBrace(), new ClosingBrace(), new OpeningBrace(), "[".ToCharArray(), pattern23.GetVariable("e.2"), "]".ToCharArray(), new ClosingBrace())), new ClosingBrace(), new ClosingBrace(), new OpeningBrace(), pattern23.GetVariable("e.3"), new ClosingBrace())));
			};

			Pattern pattern24 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.L"), new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), new ClosingBrace(), new OpeningBrace(), "\"".ToCharArray(), new ExpressionVariable("e.2"), new ClosingBrace());
			if (pattern24.Match(expression))
			{
				expression = PassiveExpression.Build(_Quotes(PassiveExpression.Build("\"".ToCharArray(), new OpeningBrace(), new ClosingBrace(), pattern24.GetVariable("e.2"))));
				Pattern pattern25 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.0"), new ClosingBrace(), new ExpressionVariable("e.3"));
				pattern25.CopyBoundVariables(pattern24);
				if (pattern25.Match(expression))
				{
					return PassiveExpression.Build(_Lookp(PassiveExpression.Build(new OpeningBrace(), pattern25.GetVariable("e.L"), new OpeningBrace(), pattern25.GetVariable("e.1"), new OpeningBrace(), pattern25.GetVariable("e.0"), new ClosingBrace(), new ClosingBrace(), new ClosingBrace(), new OpeningBrace(), pattern25.GetVariable("e.3"), new ClosingBrace())));
				}
			};

			Pattern pattern26 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.L"), new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), new ClosingBrace(), new OpeningBrace(), "\'".ToCharArray(), new ExpressionVariable("e.2"), new ClosingBrace());
			if (pattern26.Match(expression))
			{
				expression = PassiveExpression.Build(_Quotes(PassiveExpression.Build("\'".ToCharArray(), new OpeningBrace(), new ClosingBrace(), pattern26.GetVariable("e.2"))));
				Pattern pattern27 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.0"), new ClosingBrace(), new ExpressionVariable("e.3"));
				pattern27.CopyBoundVariables(pattern26);
				if (pattern27.Match(expression))
				{
					return PassiveExpression.Build(_Lookp(PassiveExpression.Build(new OpeningBrace(), pattern27.GetVariable("e.L"), new OpeningBrace(), pattern27.GetVariable("e.1"), new OpeningBrace(), pattern27.GetVariable("e.0"), new ClosingBrace(), new ClosingBrace(), new ClosingBrace(), new OpeningBrace(), pattern27.GetVariable("e.3"), new ClosingBrace())));
				}
			};

			Pattern pattern28 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.L"), new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), new ClosingBrace(), new OpeningBrace(), "/*".ToCharArray(), new ExpressionVariable("e.2"), "*/".ToCharArray(), new ExpressionVariable("e.3"), new ClosingBrace());
			if (pattern28.Match(expression))
			{
				return PassiveExpression.Build(_Lookp(PassiveExpression.Build(new OpeningBrace(), pattern28.GetVariable("e.L"), new OpeningBrace(), pattern28.GetVariable("e.1"), new OpeningBrace(), "/*".ToCharArray(), pattern28.GetVariable("e.2"), "*/".ToCharArray(), new ClosingBrace(), new ClosingBrace(), new ClosingBrace(), new OpeningBrace(), pattern28.GetVariable("e.3"), new ClosingBrace())));
			};

			Pattern pattern29 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.L"), new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), new ClosingBrace(), new OpeningBrace(), 0, new ClosingBrace());
			if (pattern29.Match(expression))
			{
				return PassiveExpression.Build(_Ermes(PassiveExpression.Build("ERROR: No pair for [".ToCharArray())));
			};

			Pattern pattern30 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.L"), new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), new ClosingBrace(), new OpeningBrace(), new SymbolVariable("s.A"), new ExpressionVariable("e.2"), new ClosingBrace());
			if (pattern30.Match(expression))
			{
				return PassiveExpression.Build(_Lookp(PassiveExpression.Build(new OpeningBrace(), pattern30.GetVariable("e.L"), new OpeningBrace(), pattern30.GetVariable("e.1"), pattern30.GetVariable("s.A"), new ClosingBrace(), new ClosingBrace(), new OpeningBrace(), pattern30.GetVariable("e.2"), new ClosingBrace())));
			};

			Pattern pattern31 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.L"), new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), new ClosingBrace(), new OpeningBrace(), new ClosingBrace());
			if (pattern31.Match(expression))
			{
				return PassiveExpression.Build(_Lookp(PassiveExpression.Build(new OpeningBrace(), pattern31.GetVariable("e.L"), new OpeningBrace(), pattern31.GetVariable("e.1"), ".EOL.", new ClosingBrace(), new ClosingBrace(), new OpeningBrace(), _Next(PassiveExpression.Build()), new ClosingBrace())));
			};

			throw new RecognitionImpossibleException("Recognition impossible. Last expression: " + expression.ToString());
		}

		private static PassiveExpression _Quotes(PassiveExpression expression)
		{
			Pattern pattern32 = new Pattern(new SymbolVariable("s.Q"), new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), new ExpressionVariable("e.0"), "\\\\".ToCharArray(), "\\\\".ToCharArray(), new ExpressionVariable("e.2"));
			if (pattern32.Match(expression))
			{
				return PassiveExpression.Build(_Quotes(PassiveExpression.Build(pattern32.GetVariable("s.Q"), new OpeningBrace(), pattern32.GetVariable("e.1"), pattern32.GetVariable("e.0"), "\\\\".ToCharArray(), "\\\\".ToCharArray(), new ClosingBrace(), pattern32.GetVariable("e.2"))));
			};

			Pattern pattern33 = new Pattern(new SymbolVariable("s.Q"), new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), new ExpressionVariable("e.0"), "\\\\".ToCharArray(), new SymbolVariable("s.Q"), new ExpressionVariable("e.2"));
			if (pattern33.Match(expression))
			{
				return PassiveExpression.Build(_Quotes(PassiveExpression.Build(pattern33.GetVariable("s.Q"), new OpeningBrace(), pattern33.GetVariable("e.1"), pattern33.GetVariable("e.0"), "\\\\".ToCharArray(), pattern33.GetVariable("s.Q"), new ClosingBrace(), pattern33.GetVariable("e.2"))));
			};

			Pattern pattern34 = new Pattern(new SymbolVariable("s.Q"), new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), new ExpressionVariable("e.0"), new SymbolVariable("s.Q"), new ExpressionVariable("e.2"));
			if (pattern34.Match(expression))
			{
				return PassiveExpression.Build(new OpeningBrace(), pattern34.GetVariable("s.Q"), pattern34.GetVariable("e.1"), pattern34.GetVariable("e.0"), pattern34.GetVariable("s.Q"), new ClosingBrace(), pattern34.GetVariable("e.2"));
			};

			Pattern pattern35 = new Pattern(new SymbolVariable("s.Q"), new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), new ExpressionVariable("e.2"));
			if (pattern35.Match(expression))
			{
				return PassiveExpression.Build(new OpeningBrace(), pattern35.GetVariable("s.Q"), new ClosingBrace(), pattern35.GetVariable("e.1"), pattern35.GetVariable("e.2"));
			};

			throw new RecognitionImpossibleException("Recognition impossible. Last expression: " + expression.ToString());
		}

		private static PassiveExpression _Out(PassiveExpression expression)
		{
			Pattern pattern36 = new Pattern(new ExpressionVariable("e.1"), ".EOL.", new ExpressionVariable("e.2"));
			if (pattern36.Match(expression))
			{
				return PassiveExpression.Build(_Putz(PassiveExpression.Build(2, _Elpar(PassiveExpression.Build(pattern36.GetVariable("e.1"))))), _Out(PassiveExpression.Build(pattern36.GetVariable("e.2"))));
			};

			Pattern pattern37 = new Pattern(new ExpressionVariable("e.1"));
			if (pattern37.Match(expression))
			{
				return PassiveExpression.Build(_Mbprep(PassiveExpression.Build(_Elpar(PassiveExpression.Build(pattern37.GetVariable("e.1"))))));
			};

			throw new RecognitionImpossibleException("Recognition impossible. Last expression: " + expression.ToString());
		}

		private static PassiveExpression _Elpar(PassiveExpression expression)
		{
			Pattern pattern38 = new Pattern(new ExpressionVariable("e.1"), new OpeningBrace(), new ExpressionVariable("e.2"), new ClosingBrace(), new ExpressionVariable("e.3"));
			if (pattern38.Match(expression))
			{
				return PassiveExpression.Build(pattern38.GetVariable("e.1"), pattern38.GetVariable("e.2"), _Elpar(PassiveExpression.Build(pattern38.GetVariable("e.3"))));
			};

			Pattern pattern39 = new Pattern(new ExpressionVariable("e.1"));
			if (pattern39.Match(expression))
			{
				return PassiveExpression.Build(pattern39.GetVariable("e.1"));
			};

			throw new RecognitionImpossibleException("Recognition impossible. Last expression: " + expression.ToString());
		}

		private static PassiveExpression _Transl(PassiveExpression expression)
		{
			Pattern pattern40 = new Pattern(new OpeningBrace(), new OpeningBrace(), new ClosingBrace(), new ClosingBrace(), new OpeningBrace(), "[.".ToCharArray(), new ExpressionVariable("e.1"), new ClosingBrace());
			if (pattern40.Match(expression))
			{
				return PassiveExpression.Build(_Transl(PassiveExpression.Build(new OpeningBrace(), new OpeningBrace(), "((".ToCharArray(), new ClosingBrace(), new ClosingBrace(), new OpeningBrace(), pattern40.GetVariable("e.1"), new ClosingBrace())));
			};

			Pattern pattern41 = new Pattern(new OpeningBrace(), new OpeningBrace(), new ClosingBrace(), new ClosingBrace(), new OpeningBrace(), "[".ToCharArray(), new ExpressionVariable("e.1"), new ClosingBrace());
			if (pattern41.Match(expression))
			{
				return PassiveExpression.Build(_Transl(PassiveExpression.Build(new OpeningBrace(), new OpeningBrace(), "(e.ML(".ToCharArray(), new ClosingBrace(), new ClosingBrace(), new OpeningBrace(), pattern41.GetVariable("e.1"), new ClosingBrace())));
			};

			Pattern pattern42 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.ML"), new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), new ClosingBrace(), new OpeningBrace(), "(".ToCharArray(), new ExpressionVariable("e.2"), new ClosingBrace());
			if (pattern42.Match(expression))
			{
				return PassiveExpression.Build(_Transl(PassiveExpression.Build(new OpeningBrace(), pattern42.GetVariable("e.ML"), new OpeningBrace(), pattern42.GetVariable("e.1"), new ClosingBrace(), new OpeningBrace(), new ClosingBrace(), new ClosingBrace(), new OpeningBrace(), pattern42.GetVariable("e.2"), new ClosingBrace())));
			};

			Pattern pattern43 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.ML"), new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), new OpeningBrace(), new ExpressionVariable("e.2"), new ClosingBrace(), new ClosingBrace(), new OpeningBrace(), ")".ToCharArray(), new ExpressionVariable("e.3"), new ClosingBrace());
			if (pattern43.Match(expression))
			{
				return PassiveExpression.Build(_Transl(PassiveExpression.Build(new OpeningBrace(), pattern43.GetVariable("e.ML"), new OpeningBrace(), pattern43.GetVariable("e.1"), "(".ToCharArray(), pattern43.GetVariable("e.2"), ")".ToCharArray(), new ClosingBrace(), new ClosingBrace(), new OpeningBrace(), pattern43.GetVariable("e.3"), new ClosingBrace())));
			};

			Pattern pattern44 = new Pattern(new OpeningBrace(), new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), new ClosingBrace(), new OpeningBrace(), ")".ToCharArray(), new ExpressionVariable("e.3"), new ClosingBrace());
			if (pattern44.Match(expression))
			{
				return PassiveExpression.Build(_Ermes(PassiveExpression.Build("ERROR: Unbalanced right parenth. before ^".ToCharArray())));
			};

			Pattern pattern45 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.ML"), new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), new ClosingBrace(), new OpeningBrace(), "]".ToCharArray(), new ClosingBrace());
			if (pattern45.Match(expression))
			{
				return PassiveExpression.Build(_Ermes(PassiveExpression.Build("ERROR: No pointer".ToCharArray())));
			};

			Pattern pattern46 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.ML"), new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), new ClosingBrace(), new OpeningBrace(), "^".ToCharArray(), new ExpressionVariable("e.2"), new ClosingBrace());
			if (pattern46.Match(expression))
			{
				return PassiveExpression.Build(_Trlmb(PassiveExpression.Build(pattern46.GetVariable("e.ML"), new OpeningBrace(), pattern46.GetVariable("e.1"), new ClosingBrace())), "))(".ToCharArray(), _Transla(PassiveExpression.Build(new OpeningBrace(), new OpeningBrace(), new ClosingBrace(), new ClosingBrace(), new OpeningBrace(), pattern46.GetVariable("e.2"), new ClosingBrace())));
			};

			Pattern pattern47 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.ML"), new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), new ClosingBrace(), new OpeningBrace(), new TermVariable("t.A"), new ExpressionVariable("e.2"), new ClosingBrace());
			if (pattern47.Match(expression))
			{
				return PassiveExpression.Build(_Transl(PassiveExpression.Build(new OpeningBrace(), pattern47.GetVariable("e.ML"), new OpeningBrace(), pattern47.GetVariable("e.1"), pattern47.GetVariable("t.A"), new ClosingBrace(), new ClosingBrace(), new OpeningBrace(), pattern47.GetVariable("e.2"), new ClosingBrace())));
			};

			throw new RecognitionImpossibleException("Recognition impossible. Last expression: " + expression.ToString());
		}

		private static PassiveExpression _Transla(PassiveExpression expression)
		{
			Pattern pattern48 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.ML"), new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), new ClosingBrace(), new OpeningBrace(), "(".ToCharArray(), new ExpressionVariable("e.2"), new ClosingBrace());
			if (pattern48.Match(expression))
			{
				return PassiveExpression.Build(_Transla(PassiveExpression.Build(new OpeningBrace(), pattern48.GetVariable("e.ML"), new OpeningBrace(), pattern48.GetVariable("e.1"), new ClosingBrace(), new OpeningBrace(), new ClosingBrace(), new ClosingBrace(), new OpeningBrace(), pattern48.GetVariable("e.2"), new ClosingBrace())));
			};

			Pattern pattern49 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.ML"), new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), new OpeningBrace(), new ExpressionVariable("e.2"), new ClosingBrace(), new ClosingBrace(), new OpeningBrace(), ")".ToCharArray(), new ExpressionVariable("e.3"), new ClosingBrace());
			if (pattern49.Match(expression))
			{
				return PassiveExpression.Build(_Transla(PassiveExpression.Build(new OpeningBrace(), pattern49.GetVariable("e.ML"), new OpeningBrace(), pattern49.GetVariable("e.1"), "(".ToCharArray(), pattern49.GetVariable("e.2"), ")".ToCharArray(), new ClosingBrace(), new ClosingBrace(), new OpeningBrace(), pattern49.GetVariable("e.3"), new ClosingBrace())));
			};

			Pattern pattern50 = new Pattern(new OpeningBrace(), new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), new ClosingBrace(), new OpeningBrace(), ")".ToCharArray(), new ExpressionVariable("e.2"), new ClosingBrace());
			if (pattern50.Match(expression))
			{
				return PassiveExpression.Build(_Transla(PassiveExpression.Build(new OpeningBrace(), new OpeningBrace(), pattern50.GetVariable("e.1"), ")(".ToCharArray(), new ClosingBrace(), new ClosingBrace(), new OpeningBrace(), pattern50.GetVariable("e.2"), new ClosingBrace())));
			};

			Pattern pattern51 = new Pattern(new OpeningBrace(), new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), new ClosingBrace(), new OpeningBrace(), ".]".ToCharArray(), new ClosingBrace());
			if (pattern51.Match(expression))
			{
				return PassiveExpression.Build(pattern51.GetVariable("e.1"), ")".ToCharArray());
			};

			Pattern pattern52 = new Pattern(new OpeningBrace(), new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), new ClosingBrace(), new OpeningBrace(), "]".ToCharArray(), new ClosingBrace());
			if (pattern52.Match(expression))
			{
				return PassiveExpression.Build(pattern52.GetVariable("e.1"), ")e.MR".ToCharArray());
			};

			Pattern pattern53 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.ML"), new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), new ClosingBrace(), new OpeningBrace(), "]".ToCharArray(), new ClosingBrace());
			if (pattern53.Match(expression))
			{
				return PassiveExpression.Build(_Ermes(PassiveExpression.Build("ERROR: Unbalanced left parenth. after pointer".ToCharArray())));
			};

			Pattern pattern54 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.ML"), new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), new ClosingBrace(), new OpeningBrace(), new TermVariable("t.A"), new ExpressionVariable("e.2"), new ClosingBrace());
			if (pattern54.Match(expression))
			{
				return PassiveExpression.Build(_Transla(PassiveExpression.Build(new OpeningBrace(), pattern54.GetVariable("e.ML"), new OpeningBrace(), pattern54.GetVariable("e.1"), pattern54.GetVariable("t.A"), new ClosingBrace(), new ClosingBrace(), new OpeningBrace(), pattern54.GetVariable("e.2"), new ClosingBrace())));
			};

			throw new RecognitionImpossibleException("Recognition impossible. Last expression: " + expression.ToString());
		}

		private static PassiveExpression _Trlmb(PassiveExpression expression)
		{
			Pattern pattern55 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), new OpeningBrace(), new ExpressionVariable("e.2"), new ClosingBrace(), new ExpressionVariable("e.L"));
			if (pattern55.Match(expression))
			{
				return PassiveExpression.Build(pattern55.GetVariable("e.1"), ")(".ToCharArray(), _Trlmb(PassiveExpression.Build(new OpeningBrace(), pattern55.GetVariable("e.2"), new ClosingBrace(), pattern55.GetVariable("e.L"))));
			};

			Pattern pattern56 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace());
			if (pattern56.Match(expression))
			{
				return PassiveExpression.Build(pattern56.GetVariable("e.1"));
			};

			throw new RecognitionImpossibleException("Recognition impossible. Last expression: " + expression.ToString());
		}

		private static PassiveExpression _Putz(PassiveExpression expression)
		{
			Pattern pattern57 = new Pattern(new SymbolVariable("s.C"), new ExpressionVariable("e.E"));
			if (pattern57.Match(expression))
			{
				return PassiveExpression.Build(_Destroy(PassiveExpression.Build(_Put(PassiveExpression.Build(pattern57.GetVariable("s.C"), pattern57.GetVariable("e.E"))))));
			};

			throw new RecognitionImpossibleException("Recognition impossible. Last expression: " + expression.ToString());
		}

		private static PassiveExpression _Destroy(PassiveExpression expression)
		{
			Pattern pattern58 = new Pattern(new ExpressionVariable("e.E"));
			if (pattern58.Match(expression))
			{
				return PassiveExpression.Build();
			};

			throw new RecognitionImpossibleException("Recognition impossible. Last expression: " + expression.ToString());
		}

		private static PassiveExpression _Ermes(PassiveExpression expression)
		{
			Pattern pattern59 = new Pattern(new ExpressionVariable("e.X"));
			if (pattern59.Match(expression))
			{
				return PassiveExpression.Build(_Prout(PassiveExpression.Build(_Put(PassiveExpression.Build(2, pattern59.GetVariable("e.X"))))));
			};

			throw new RecognitionImpossibleException("Recognition impossible. Last expression: " + expression.ToString());
		}
	}
}

