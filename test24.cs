
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
				return PassiveExpression.Build(_Prout(PassiveExpression.Build(_PreAlph(PassiveExpression.Build("a".ToCharArray(), "z".ToCharArray())))));
			};

			throw new RecognitionImpossibleException("Recognition impossible. Last expression: " + ExpressionToString(expression, 0));
		}

		private static PassiveExpression _PreAlph(PassiveExpression expression)
		{
			Pattern pattern2 = new Pattern(new SymbolVariable("s.1"), new SymbolVariable("s.1"));
			if (RefalBase.Match(expression, pattern2))
			{
				return PassiveExpression.Build(true);
			};

			Pattern pattern3 = new Pattern(new SymbolVariable("s.1"), new SymbolVariable("s.2"));
			if (RefalBase.Match(expression, pattern3))
			{
				expression = PassiveExpression.Build(_Alphabet(PassiveExpression.Build()));
				Pattern pattern4 = new Pattern(new ExpressionVariable("e.A"), new SymbolVariable("s.1"), new ExpressionVariable("e.B"), new SymbolVariable("s.2"), new ExpressionVariable("e.C"));
				pattern4.CopyBoundVariables(pattern3);
				if (RefalBase.Match(expression, pattern4))
				{
					return PassiveExpression.Build(true);
				}
			};

			Pattern pattern5 = new Pattern(new ExpressionVariable("e.Z"));
			if (RefalBase.Match(expression, pattern5))
			{
				return PassiveExpression.Build(false);
			};

			throw new RecognitionImpossibleException("Recognition impossible. Last expression: " + ExpressionToString(expression, 0));
		}

		private static PassiveExpression _Alphabet(PassiveExpression expression)
		{
			Pattern pattern6 = new Pattern();
			if (RefalBase.Match(expression, pattern6))
			{
				return PassiveExpression.Build("abcdefghijklmnopqrstuvwxyz".ToCharArray());
			};

			throw new RecognitionImpossibleException("Recognition impossible. Last expression: " + ExpressionToString(expression, 0));
		}

	}
}

