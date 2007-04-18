
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
				return PassiveExpression.Build(Open(PassiveExpression.Build("r".ToCharArray(), 1, Arg(PassiveExpression.Build(1)))), Open(PassiveExpression.Build("w".ToCharArray(), 2, Arg(PassiveExpression.Build(2)))), Mbprep(PassiveExpression.Build(Next(PassiveExpression.Build()))));
			};

			throw new RecognitionImpossibleException("Recognition impossible");
		}

		private static PassiveExpression Next(PassiveExpression expression)
		{
			Pattern pattern2 = new Pattern();
			if (RefalBase.Match(expression, pattern2))
			{
				expression = PassiveExpression.Build(Get(PassiveExpression.Build(1)));
				{
					Pattern pattern3 = new Pattern("*".ToCharArray(), new ExpressionVariable("e.1"));
					pattern3.CopyBoundVariables(pattern2);
					if (RefalBase.Match(expression, pattern3))
					{
						return PassiveExpression.Build(Putz(PassiveExpression.Build(2, "*".ToCharArray(), pattern3.GetVariable("e.1"))), Next(PassiveExpression.Build()));
					};

					Pattern pattern4 = new Pattern(new ExpressionVariable("e.1"));
					pattern4.CopyBoundVariables(pattern2);
					if (RefalBase.Match(expression, pattern4))
					{
						return PassiveExpression.Build(pattern4.GetVariable("e.1"));
					};

					throw new RecognitionImpossibleException("Recognition impossible");
				}
			};

			throw new RecognitionImpossibleException("Recognition impossible");
		}

		private static PassiveExpression Mbprep(PassiveExpression expression)
		{
			Pattern pattern6 = new Pattern(0);
			if (RefalBase.Match(expression, pattern6))
			{
				return PassiveExpression.Build();
			};

			Pattern pattern7 = new Pattern(new ExpressionVariable("e.1"));
			if (RefalBase.Match(expression, pattern7))
			{
				expression = PassiveExpression.Build(Lookm0(PassiveExpression.Build(pattern7.GetVariable("e.1"))));
				{
					Pattern pattern8 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.A"), new ClosingBrace(), "[".ToCharArray(), new ExpressionVariable("e.B"));
					pattern8.CopyBoundVariables(pattern7);
					if (RefalBase.Match(expression, pattern8))
					{
						return PassiveExpression.Build(Out(PassiveExpression.Build(pattern8.GetVariable("e.A"), Lookp(PassiveExpression.Build(new OpeningBrace(), new OpeningBrace(), "[".ToCharArray(), new ClosingBrace(), new ClosingBrace(), new OpeningBrace(), pattern8.GetVariable("e.B"), new ClosingBrace())))));
					};

					Pattern pattern9 = new Pattern(new ExpressionVariable("e.1"));
					pattern9.CopyBoundVariables(pattern7);
					if (RefalBase.Match(expression, pattern9))
					{
						return PassiveExpression.Build(Putz(PassiveExpression.Build(2, pattern9.GetVariable("e.1"))), Mbprep(PassiveExpression.Build(Next(PassiveExpression.Build()))));
					};

					throw new RecognitionImpossibleException("Recognition impossible");
				}
			};

			throw new RecognitionImpossibleException("Recognition impossible");
		}

		private static PassiveExpression Lookm0(PassiveExpression expression)
		{
			Pattern pattern11 = new Pattern(new ExpressionVariable("e.1"), "[".ToCharArray(), new ExpressionVariable("e.2"));
			if (RefalBase.Match(expression, pattern11))
			{
				return PassiveExpression.Build(Lookm(PassiveExpression.Build(new OpeningBrace(), new ClosingBrace(), pattern11.GetVariable("e.1"), "[".ToCharArray(), pattern11.GetVariable("e.2"))));
			};

			Pattern pattern12 = new Pattern(new ExpressionVariable("e.1"));
			if (RefalBase.Match(expression, pattern12))
			{
				return PassiveExpression.Build(pattern12.GetVariable("e.1"));
			};

			throw new RecognitionImpossibleException("Recognition impossible");
		}

		private static PassiveExpression Lookm(PassiveExpression expression)
		{
			Pattern pattern13 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), "[".ToCharArray(), new ExpressionVariable("e.2"));
			if (RefalBase.Match(expression, pattern13))
			{
				return PassiveExpression.Build(new OpeningBrace(), pattern13.GetVariable("e.1"), new ClosingBrace(), "[".ToCharArray(), pattern13.GetVariable("e.2"));
			};

			Pattern pattern14 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), "\"".ToCharArray(), new ExpressionVariable("e.2"));
			if (RefalBase.Match(expression, pattern14))
			{
				expression = PassiveExpression.Build(Quotes(PassiveExpression.Build("\"".ToCharArray(), new OpeningBrace(), new ClosingBrace(), pattern14.GetVariable("e.2"))));
				Pattern pattern15 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.0"), new ClosingBrace(), new ExpressionVariable("e.3"));
				pattern15.CopyBoundVariables(pattern14);
				if (RefalBase.Match(expression, pattern15))
				{
					return PassiveExpression.Build(Lookm(PassiveExpression.Build(new OpeningBrace(), pattern15.GetVariable("e.1"), pattern15.GetVariable("e.0"), new ClosingBrace(), pattern15.GetVariable("e.3"))));
				}
			};

			Pattern pattern16 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), "\'".ToCharArray(), new ExpressionVariable("e.2"));
			if (RefalBase.Match(expression, pattern16))
			{
				expression = PassiveExpression.Build(Quotes(PassiveExpression.Build("\'".ToCharArray(), new OpeningBrace(), new ClosingBrace(), pattern16.GetVariable("e.2"))));
				Pattern pattern17 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.0"), new ClosingBrace(), new ExpressionVariable("e.3"));
				pattern17.CopyBoundVariables(pattern16);
				if (RefalBase.Match(expression, pattern17))
				{
					return PassiveExpression.Build(Lookm(PassiveExpression.Build(new OpeningBrace(), pattern17.GetVariable("e.1"), pattern17.GetVariable("e.0"), new ClosingBrace(), pattern17.GetVariable("e.3"))));
				}
			};

			Pattern pattern18 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), "/*".ToCharArray(), new ExpressionVariable("e.2"), "*/".ToCharArray(), new ExpressionVariable("e.3"));
			if (RefalBase.Match(expression, pattern18))
			{
				return PassiveExpression.Build(Lookm(PassiveExpression.Build(new OpeningBrace(), pattern18.GetVariable("e.1"), "/*".ToCharArray(), pattern18.GetVariable("e.2"), "*/".ToCharArray(), new ClosingBrace(), pattern18.GetVariable("e.3"))));
			};

			Pattern pattern19 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), new TermVariable("t.A"), new ExpressionVariable("e.2"));
			if (RefalBase.Match(expression, pattern19))
			{
				return PassiveExpression.Build(Lookm(PassiveExpression.Build(new OpeningBrace(), pattern19.GetVariable("e.1"), pattern19.GetVariable("t.A"), new ClosingBrace(), pattern19.GetVariable("e.2"))));
			};

			Pattern pattern20 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace());
			if (RefalBase.Match(expression, pattern20))
			{
				return PassiveExpression.Build(pattern20.GetVariable("e.1"));
			};

			throw new RecognitionImpossibleException("Recognition impossible");
		}

		private static PassiveExpression Lookp(PassiveExpression expression)
		{
			Pattern pattern21 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.ML"), new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), new ClosingBrace(), new OpeningBrace(), "[".ToCharArray(), new ExpressionVariable("e.2"), new ClosingBrace());
			if (RefalBase.Match(expression, pattern21))
			{
				return PassiveExpression.Build(Lookp(PassiveExpression.Build(new OpeningBrace(), pattern21.GetVariable("e.ML"), new OpeningBrace(), pattern21.GetVariable("e.1"), new ClosingBrace(), new OpeningBrace(), "[".ToCharArray(), new ClosingBrace(), new ClosingBrace(), new OpeningBrace(), pattern21.GetVariable("e.2"), new ClosingBrace())));
			};

			Pattern pattern22 = new Pattern(new OpeningBrace(), new OpeningBrace(), "[".ToCharArray(), new ExpressionVariable("e.1"), new ClosingBrace(), new ClosingBrace(), new OpeningBrace(), "]".ToCharArray(), new ExpressionVariable("e.2"), new ClosingBrace());
			if (RefalBase.Match(expression, pattern22))
			{
				return PassiveExpression.Build(Transl(PassiveExpression.Build(new OpeningBrace(), new OpeningBrace(), new ClosingBrace(), new ClosingBrace(), new OpeningBrace(), "[".ToCharArray(), pattern22.GetVariable("e.1"), "]".ToCharArray(), new ClosingBrace())), pattern22.GetVariable("e.2"));
			};

			Pattern pattern23 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.ML"), new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), new OpeningBrace(), "[".ToCharArray(), new ExpressionVariable("e.2"), new ClosingBrace(), new ClosingBrace(), new OpeningBrace(), "]".ToCharArray(), new ExpressionVariable("e.3"), new ClosingBrace());
			if (RefalBase.Match(expression, pattern23))
			{
				return PassiveExpression.Build(Lookp(PassiveExpression.Build(new OpeningBrace(), pattern23.GetVariable("e.ML"), new OpeningBrace(), pattern23.GetVariable("e.1"), Transl(PassiveExpression.Build(new OpeningBrace(), new OpeningBrace(), new ClosingBrace(), new ClosingBrace(), new OpeningBrace(), "[".ToCharArray(), pattern23.GetVariable("e.2"), "]".ToCharArray(), new ClosingBrace())), new ClosingBrace(), new ClosingBrace(), new OpeningBrace(), pattern23.GetVariable("e.3"), new ClosingBrace())));
			};

			Pattern pattern24 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.L"), new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), new ClosingBrace(), new OpeningBrace(), "\"".ToCharArray(), new ExpressionVariable("e.2"), new ClosingBrace());
			if (RefalBase.Match(expression, pattern24))
			{
				expression = PassiveExpression.Build(Quotes(PassiveExpression.Build("\"".ToCharArray(), new OpeningBrace(), new ClosingBrace(), pattern24.GetVariable("e.2"))));
				Pattern pattern25 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.0"), new ClosingBrace(), new ExpressionVariable("e.3"));
				pattern25.CopyBoundVariables(pattern24);
				if (RefalBase.Match(expression, pattern25))
				{
					return PassiveExpression.Build(Lookp(PassiveExpression.Build(new OpeningBrace(), pattern25.GetVariable("e.L"), new OpeningBrace(), pattern25.GetVariable("e.1"), new OpeningBrace(), pattern25.GetVariable("e.0"), new ClosingBrace(), new ClosingBrace(), new ClosingBrace(), new OpeningBrace(), pattern25.GetVariable("e.3"), new ClosingBrace())));
				}
			};

			Pattern pattern26 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.L"), new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), new ClosingBrace(), new OpeningBrace(), "\'".ToCharArray(), new ExpressionVariable("e.2"), new ClosingBrace());
			if (RefalBase.Match(expression, pattern26))
			{
				expression = PassiveExpression.Build(Quotes(PassiveExpression.Build("\'".ToCharArray(), new OpeningBrace(), new ClosingBrace(), pattern26.GetVariable("e.2"))));
				Pattern pattern27 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.0"), new ClosingBrace(), new ExpressionVariable("e.3"));
				pattern27.CopyBoundVariables(pattern26);
				if (RefalBase.Match(expression, pattern27))
				{
					return PassiveExpression.Build(Lookp(PassiveExpression.Build(new OpeningBrace(), pattern27.GetVariable("e.L"), new OpeningBrace(), pattern27.GetVariable("e.1"), new OpeningBrace(), pattern27.GetVariable("e.0"), new ClosingBrace(), new ClosingBrace(), new ClosingBrace(), new OpeningBrace(), pattern27.GetVariable("e.3"), new ClosingBrace())));
				}
			};

			Pattern pattern28 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.L"), new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), new ClosingBrace(), new OpeningBrace(), "/*".ToCharArray(), new ExpressionVariable("e.2"), "*/".ToCharArray(), new ExpressionVariable("e.3"), new ClosingBrace());
			if (RefalBase.Match(expression, pattern28))
			{
				return PassiveExpression.Build(Lookp(PassiveExpression.Build(new OpeningBrace(), pattern28.GetVariable("e.L"), new OpeningBrace(), pattern28.GetVariable("e.1"), new OpeningBrace(), "/*".ToCharArray(), pattern28.GetVariable("e.2"), "*/".ToCharArray(), new ClosingBrace(), new ClosingBrace(), new ClosingBrace(), new OpeningBrace(), pattern28.GetVariable("e.3"), new ClosingBrace())));
			};

			Pattern pattern29 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.L"), new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), new ClosingBrace(), new OpeningBrace(), 0, new ClosingBrace());
			if (RefalBase.Match(expression, pattern29))
			{
				return PassiveExpression.Build(Ermes(PassiveExpression.Build("ERROR: No pair for [".ToCharArray())));
			};

			Pattern pattern30 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.L"), new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), new ClosingBrace(), new OpeningBrace(), new SymbolVariable("s.A"), new ExpressionVariable("e.2"), new ClosingBrace());
			if (RefalBase.Match(expression, pattern30))
			{
				return PassiveExpression.Build(Lookp(PassiveExpression.Build(new OpeningBrace(), pattern30.GetVariable("e.L"), new OpeningBrace(), pattern30.GetVariable("e.1"), pattern30.GetVariable("s.A"), new ClosingBrace(), new ClosingBrace(), new OpeningBrace(), pattern30.GetVariable("e.2"), new ClosingBrace())));
			};

			Pattern pattern31 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.L"), new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), new ClosingBrace(), new OpeningBrace(), new ClosingBrace());
			if (RefalBase.Match(expression, pattern31))
			{
				return PassiveExpression.Build(Lookp(PassiveExpression.Build(new OpeningBrace(), pattern31.GetVariable("e.L"), new OpeningBrace(), pattern31.GetVariable("e.1"), ".EOL.", new ClosingBrace(), new ClosingBrace(), new OpeningBrace(), Next(PassiveExpression.Build()), new ClosingBrace())));
			};

			throw new RecognitionImpossibleException("Recognition impossible");
		}

		private static PassiveExpression Quotes(PassiveExpression expression)
		{
			Pattern pattern32 = new Pattern(new SymbolVariable("s.Q"), new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), new ExpressionVariable("e.0"), "\\".ToCharArray(), "\\".ToCharArray(), new ExpressionVariable("e.2"));
			if (RefalBase.Match(expression, pattern32))
			{
				return PassiveExpression.Build(Quotes(PassiveExpression.Build(pattern32.GetVariable("s.Q"), new OpeningBrace(), pattern32.GetVariable("e.1"), pattern32.GetVariable("e.0"), "\\".ToCharArray(), "\\".ToCharArray(), new ClosingBrace(), pattern32.GetVariable("e.2"))));
			};

			Pattern pattern33 = new Pattern(new SymbolVariable("s.Q"), new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), new ExpressionVariable("e.0"), "\\".ToCharArray(), new SymbolVariable("s.Q"), new ExpressionVariable("e.2"));
			if (RefalBase.Match(expression, pattern33))
			{
				return PassiveExpression.Build(Quotes(PassiveExpression.Build(pattern33.GetVariable("s.Q"), new OpeningBrace(), pattern33.GetVariable("e.1"), pattern33.GetVariable("e.0"), "\\".ToCharArray(), pattern33.GetVariable("s.Q"), new ClosingBrace(), pattern33.GetVariable("e.2"))));
			};

			Pattern pattern34 = new Pattern(new SymbolVariable("s.Q"), new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), new ExpressionVariable("e.0"), new SymbolVariable("s.Q"), new ExpressionVariable("e.2"));
			if (RefalBase.Match(expression, pattern34))
			{
				return PassiveExpression.Build(new OpeningBrace(), pattern34.GetVariable("s.Q"), pattern34.GetVariable("e.1"), pattern34.GetVariable("e.0"), pattern34.GetVariable("s.Q"), new ClosingBrace(), pattern34.GetVariable("e.2"));
			};

			Pattern pattern35 = new Pattern(new SymbolVariable("s.Q"), new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), new ExpressionVariable("e.2"));
			if (RefalBase.Match(expression, pattern35))
			{
				return PassiveExpression.Build(new OpeningBrace(), pattern35.GetVariable("s.Q"), new ClosingBrace(), pattern35.GetVariable("e.1"), pattern35.GetVariable("e.2"));
			};

			throw new RecognitionImpossibleException("Recognition impossible");
		}

		private static PassiveExpression Out(PassiveExpression expression)
		{
			Pattern pattern36 = new Pattern(new ExpressionVariable("e.1"), ".EOL.", new ExpressionVariable("e.2"));
			if (RefalBase.Match(expression, pattern36))
			{
				return PassiveExpression.Build(Putz(PassiveExpression.Build(2, Elpar(PassiveExpression.Build(pattern36.GetVariable("e.1"))))), Out(PassiveExpression.Build(pattern36.GetVariable("e.2"))));
			};

			Pattern pattern37 = new Pattern(new ExpressionVariable("e.1"));
			if (RefalBase.Match(expression, pattern37))
			{
				return PassiveExpression.Build(Mbprep(PassiveExpression.Build(Elpar(PassiveExpression.Build(pattern37.GetVariable("e.1"))))));
			};

			throw new RecognitionImpossibleException("Recognition impossible");
		}

		private static PassiveExpression Elpar(PassiveExpression expression)
		{
			Pattern pattern38 = new Pattern(new ExpressionVariable("e.1"), new OpeningBrace(), new ExpressionVariable("e.2"), new ClosingBrace(), new ExpressionVariable("e.3"));
			if (RefalBase.Match(expression, pattern38))
			{
				return PassiveExpression.Build(pattern38.GetVariable("e.1"), pattern38.GetVariable("e.2"), Elpar(PassiveExpression.Build(pattern38.GetVariable("e.3"))));
			};

			Pattern pattern39 = new Pattern(new ExpressionVariable("e.1"));
			if (RefalBase.Match(expression, pattern39))
			{
				return PassiveExpression.Build(pattern39.GetVariable("e.1"));
			};

			throw new RecognitionImpossibleException("Recognition impossible");
		}

		private static PassiveExpression Transl(PassiveExpression expression)
		{
			Pattern pattern40 = new Pattern(new OpeningBrace(), new OpeningBrace(), new ClosingBrace(), new ClosingBrace(), new OpeningBrace(), "[.".ToCharArray(), new ExpressionVariable("e.1"), new ClosingBrace());
			if (RefalBase.Match(expression, pattern40))
			{
				return PassiveExpression.Build(Transl(PassiveExpression.Build(new OpeningBrace(), new OpeningBrace(), "((".ToCharArray(), new ClosingBrace(), new ClosingBrace(), new OpeningBrace(), pattern40.GetVariable("e.1"), new ClosingBrace())));
			};

			Pattern pattern41 = new Pattern(new OpeningBrace(), new OpeningBrace(), new ClosingBrace(), new ClosingBrace(), new OpeningBrace(), "[".ToCharArray(), new ExpressionVariable("e.1"), new ClosingBrace());
			if (RefalBase.Match(expression, pattern41))
			{
				return PassiveExpression.Build(Transl(PassiveExpression.Build(new OpeningBrace(), new OpeningBrace(), "(e.ML(".ToCharArray(), new ClosingBrace(), new ClosingBrace(), new OpeningBrace(), pattern41.GetVariable("e.1"), new ClosingBrace())));
			};

			Pattern pattern42 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.ML"), new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), new ClosingBrace(), new OpeningBrace(), "(".ToCharArray(), new ExpressionVariable("e.2"), new ClosingBrace());
			if (RefalBase.Match(expression, pattern42))
			{
				return PassiveExpression.Build(Transl(PassiveExpression.Build(new OpeningBrace(), pattern42.GetVariable("e.ML"), new OpeningBrace(), pattern42.GetVariable("e.1"), new ClosingBrace(), new OpeningBrace(), new ClosingBrace(), new ClosingBrace(), new OpeningBrace(), pattern42.GetVariable("e.2"), new ClosingBrace())));
			};

			Pattern pattern43 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.ML"), new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), new OpeningBrace(), new ExpressionVariable("e.2"), new ClosingBrace(), new ClosingBrace(), new OpeningBrace(), ")".ToCharArray(), new ExpressionVariable("e.3"), new ClosingBrace());
			if (RefalBase.Match(expression, pattern43))
			{
				return PassiveExpression.Build(Transl(PassiveExpression.Build(new OpeningBrace(), pattern43.GetVariable("e.ML"), new OpeningBrace(), pattern43.GetVariable("e.1"), "(".ToCharArray(), pattern43.GetVariable("e.2"), ")".ToCharArray(), new ClosingBrace(), new ClosingBrace(), new OpeningBrace(), pattern43.GetVariable("e.3"), new ClosingBrace())));
			};

			Pattern pattern44 = new Pattern(new OpeningBrace(), new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), new ClosingBrace(), new OpeningBrace(), ")".ToCharArray(), new ExpressionVariable("e.3"), new ClosingBrace());
			if (RefalBase.Match(expression, pattern44))
			{
				return PassiveExpression.Build(Ermes(PassiveExpression.Build("ERROR: Unbalanced right parenth. before ^".ToCharArray())));
			};

			Pattern pattern45 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.ML"), new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), new ClosingBrace(), new OpeningBrace(), "]".ToCharArray(), new ClosingBrace());
			if (RefalBase.Match(expression, pattern45))
			{
				return PassiveExpression.Build(Ermes(PassiveExpression.Build("ERROR: No pointer".ToCharArray())));
			};

			Pattern pattern46 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.ML"), new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), new ClosingBrace(), new OpeningBrace(), "^".ToCharArray(), new ExpressionVariable("e.2"), new ClosingBrace());
			if (RefalBase.Match(expression, pattern46))
			{
				return PassiveExpression.Build(Trlmb(PassiveExpression.Build(pattern46.GetVariable("e.ML"), new OpeningBrace(), pattern46.GetVariable("e.1"), new ClosingBrace())), "))(".ToCharArray(), Transla(PassiveExpression.Build(new OpeningBrace(), new OpeningBrace(), new ClosingBrace(), new ClosingBrace(), new OpeningBrace(), pattern46.GetVariable("e.2"), new ClosingBrace())));
			};

			Pattern pattern47 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.ML"), new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), new ClosingBrace(), new OpeningBrace(), new TermVariable("t.A"), new ExpressionVariable("e.2"), new ClosingBrace());
			if (RefalBase.Match(expression, pattern47))
			{
				return PassiveExpression.Build(Transl(PassiveExpression.Build(new OpeningBrace(), pattern47.GetVariable("e.ML"), new OpeningBrace(), pattern47.GetVariable("e.1"), pattern47.GetVariable("t.A"), new ClosingBrace(), new ClosingBrace(), new OpeningBrace(), pattern47.GetVariable("e.2"), new ClosingBrace())));
			};

			throw new RecognitionImpossibleException("Recognition impossible");
		}

		private static PassiveExpression Transla(PassiveExpression expression)
		{
			Pattern pattern48 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.ML"), new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), new ClosingBrace(), new OpeningBrace(), "(".ToCharArray(), new ExpressionVariable("e.2"), new ClosingBrace());
			if (RefalBase.Match(expression, pattern48))
			{
				return PassiveExpression.Build(Transla(PassiveExpression.Build(new OpeningBrace(), pattern48.GetVariable("e.ML"), new OpeningBrace(), pattern48.GetVariable("e.1"), new ClosingBrace(), new OpeningBrace(), new ClosingBrace(), new ClosingBrace(), new OpeningBrace(), pattern48.GetVariable("e.2"), new ClosingBrace())));
			};

			Pattern pattern49 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.ML"), new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), new OpeningBrace(), new ExpressionVariable("e.2"), new ClosingBrace(), new ClosingBrace(), new OpeningBrace(), ")".ToCharArray(), new ExpressionVariable("e.3"), new ClosingBrace());
			if (RefalBase.Match(expression, pattern49))
			{
				return PassiveExpression.Build(Transla(PassiveExpression.Build(new OpeningBrace(), pattern49.GetVariable("e.ML"), new OpeningBrace(), pattern49.GetVariable("e.1"), "(".ToCharArray(), pattern49.GetVariable("e.2"), ")".ToCharArray(), new ClosingBrace(), new ClosingBrace(), new OpeningBrace(), pattern49.GetVariable("e.3"), new ClosingBrace())));
			};

			Pattern pattern50 = new Pattern(new OpeningBrace(), new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), new ClosingBrace(), new OpeningBrace(), ")".ToCharArray(), new ExpressionVariable("e.2"), new ClosingBrace());
			if (RefalBase.Match(expression, pattern50))
			{
				return PassiveExpression.Build(Transla(PassiveExpression.Build(new OpeningBrace(), new OpeningBrace(), pattern50.GetVariable("e.1"), ")(".ToCharArray(), new ClosingBrace(), new ClosingBrace(), new OpeningBrace(), pattern50.GetVariable("e.2"), new ClosingBrace())));
			};

			Pattern pattern51 = new Pattern(new OpeningBrace(), new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), new ClosingBrace(), new OpeningBrace(), ".]".ToCharArray(), new ClosingBrace());
			if (RefalBase.Match(expression, pattern51))
			{
				return PassiveExpression.Build(pattern51.GetVariable("e.1"), ")".ToCharArray());
			};

			Pattern pattern52 = new Pattern(new OpeningBrace(), new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), new ClosingBrace(), new OpeningBrace(), "]".ToCharArray(), new ClosingBrace());
			if (RefalBase.Match(expression, pattern52))
			{
				return PassiveExpression.Build(pattern52.GetVariable("e.1"), ")e.MR".ToCharArray());
			};

			Pattern pattern53 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.ML"), new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), new ClosingBrace(), new OpeningBrace(), "]".ToCharArray(), new ClosingBrace());
			if (RefalBase.Match(expression, pattern53))
			{
				return PassiveExpression.Build(Ermes(PassiveExpression.Build("ERROR: Unbalanced left parenth. after pointer".ToCharArray())));
			};

			Pattern pattern54 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.ML"), new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), new ClosingBrace(), new OpeningBrace(), new TermVariable("t.A"), new ExpressionVariable("e.2"), new ClosingBrace());
			if (RefalBase.Match(expression, pattern54))
			{
				return PassiveExpression.Build(Transla(PassiveExpression.Build(new OpeningBrace(), pattern54.GetVariable("e.ML"), new OpeningBrace(), pattern54.GetVariable("e.1"), pattern54.GetVariable("t.A"), new ClosingBrace(), new ClosingBrace(), new OpeningBrace(), pattern54.GetVariable("e.2"), new ClosingBrace())));
			};

			throw new RecognitionImpossibleException("Recognition impossible");
		}

		private static PassiveExpression Trlmb(PassiveExpression expression)
		{
			Pattern pattern55 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), new OpeningBrace(), new ExpressionVariable("e.2"), new ClosingBrace(), new ExpressionVariable("e.L"));
			if (RefalBase.Match(expression, pattern55))
			{
				return PassiveExpression.Build(pattern55.GetVariable("e.1"), ")(".ToCharArray(), Trlmb(PassiveExpression.Build(new OpeningBrace(), pattern55.GetVariable("e.2"), new ClosingBrace(), pattern55.GetVariable("e.L"))));
			};

			Pattern pattern56 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace());
			if (RefalBase.Match(expression, pattern56))
			{
				return PassiveExpression.Build(pattern56.GetVariable("e.1"));
			};

			throw new RecognitionImpossibleException("Recognition impossible");
		}

		private static PassiveExpression Putz(PassiveExpression expression)
		{
			Pattern pattern57 = new Pattern(new SymbolVariable("s.C"), new ExpressionVariable("e.E"));
			if (RefalBase.Match(expression, pattern57))
			{
				return PassiveExpression.Build(Destroy(PassiveExpression.Build(Put(PassiveExpression.Build(pattern57.GetVariable("s.C"), pattern57.GetVariable("e.E"))))));
			};

			throw new RecognitionImpossibleException("Recognition impossible");
		}

		private static PassiveExpression Destroy(PassiveExpression expression)
		{
			Pattern pattern58 = new Pattern(new ExpressionVariable("e.E"));
			if (RefalBase.Match(expression, pattern58))
			{
				return PassiveExpression.Build();
			};

			throw new RecognitionImpossibleException("Recognition impossible");
		}

		private static PassiveExpression Ermes(PassiveExpression expression)
		{
			Pattern pattern59 = new Pattern(new ExpressionVariable("e.X"));
			if (RefalBase.Match(expression, pattern59))
			{
				return PassiveExpression.Build(Prout(PassiveExpression.Build(Put(PassiveExpression.Build(2, pattern59.GetVariable("e.X"))))));
			};

			throw new RecognitionImpossibleException("Recognition impossible");
		}

	}
}

