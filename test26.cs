
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
				return PassiveExpression.Build(Prout(PassiveExpression.Build(Order(PassiveExpression.Build("f".ToCharArray(), "a".ToCharArray())))));
			};

			throw new RecognitionImpossibleException("Recognition impossible");
		}

		private static PassiveExpression Order(PassiveExpression expression)
		{
			Pattern pattern2 = new Pattern(new SymbolVariable("s.1"), new SymbolVariable("s.2"));
			if (RefalBase.Match(expression, pattern2))
			{
				expression = PassiveExpression.Build(PreAlph(PassiveExpression.Build(pattern2.GetVariable("s.1"), pattern2.GetVariable("s.2"))));
				{
					Pattern pattern3 = new Pattern(true);
					pattern3.CopyBoundVariables(pattern2);
					if (RefalBase.Match(expression, pattern3))
					{
						return PassiveExpression.Build(pattern3.GetVariable("s.1"), pattern3.GetVariable("s.2"));
					};

					Pattern pattern4 = new Pattern(false);
					pattern4.CopyBoundVariables(pattern2);
					if (RefalBase.Match(expression, pattern4))
					{
						return PassiveExpression.Build(pattern4.GetVariable("s.2"), pattern4.GetVariable("s.1"));
					};

					throw new RecognitionImpossibleException("Recognition impossible");
				}
			};

			throw new RecognitionImpossibleException("Recognition impossible");
		}

		private static PassiveExpression PreAlph(PassiveExpression expression)
		{
			Pattern pattern6 = new Pattern(new SymbolVariable("s.1"), new SymbolVariable("s.1"));
			if (RefalBase.Match(expression, pattern6))
			{
				return PassiveExpression.Build(true);
			};

			Pattern pattern7 = new Pattern(new SymbolVariable("s.1"), new SymbolVariable("s.2"));
			if (RefalBase.Match(expression, pattern7))
			{
				expression = PassiveExpression.Build(Alphabet(PassiveExpression.Build()));
				Pattern pattern8 = new Pattern(new ExpressionVariable("e.A"), new SymbolVariable("s.1"), new ExpressionVariable("e.B"), new SymbolVariable("s.2"), new ExpressionVariable("e.C"));
				pattern8.CopyBoundVariables(pattern7);
				if (RefalBase.Match(expression, pattern8))
				{
					return PassiveExpression.Build(true);
				}
			};

			Pattern pattern9 = new Pattern(new ExpressionVariable("e.Z"));
			if (RefalBase.Match(expression, pattern9))
			{
				return PassiveExpression.Build(false);
			};

			throw new RecognitionImpossibleException("Recognition impossible");
		}

		private static PassiveExpression Alphabet(PassiveExpression expression)
		{
			Pattern pattern10 = new Pattern();
			if (RefalBase.Match(expression, pattern10))
			{
				return PassiveExpression.Build("abcdefghijklmnopqrstuvwxyz".ToCharArray());
			};

			throw new RecognitionImpossibleException("Recognition impossible");
		}

	}
}

