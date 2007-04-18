
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
				return PassiveExpression.Build(_Prout(PassiveExpression.Build(_Order(PassiveExpression.Build("x".ToCharArray(), "r".ToCharArray())))));
			};

			throw new RecognitionImpossibleException("Recognition impossible. Last expression: " + ExpressionToString(expression, 0));
		}

		private static PassiveExpression _Order(PassiveExpression expression)
		{
			Pattern pattern2 = new Pattern(new SymbolVariable("s.1"), new SymbolVariable("s.2"));
			if (RefalBase.Match(expression, pattern2))
			{
				return PassiveExpression.Build(_Order1(PassiveExpression.Build(_PreAlph(PassiveExpression.Build(pattern2.GetVariable("s.1"), pattern2.GetVariable("s.2"))), new OpeningBrace(), pattern2.GetVariable("s.1"), new ClosingBrace(), pattern2.GetVariable("s.2"))));
			};

			throw new RecognitionImpossibleException("Recognition impossible. Last expression: " + ExpressionToString(expression, 0));
		}

		private static PassiveExpression _Order1(PassiveExpression expression)
		{
			Pattern pattern3 = new Pattern(true, new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), new ExpressionVariable("e.2"));
			if (RefalBase.Match(expression, pattern3))
			{
				return PassiveExpression.Build(pattern3.GetVariable("e.1"), pattern3.GetVariable("e.2"));
			};

			Pattern pattern4 = new Pattern(false, new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), new ExpressionVariable("e.2"));
			if (RefalBase.Match(expression, pattern4))
			{
				return PassiveExpression.Build(pattern4.GetVariable("e.2"), pattern4.GetVariable("e.1"));
			};

			throw new RecognitionImpossibleException("Recognition impossible. Last expression: " + ExpressionToString(expression, 0));
		}

		private static PassiveExpression _PreAlph(PassiveExpression expression)
		{
			Pattern pattern5 = new Pattern(new SymbolVariable("s.1"), new SymbolVariable("s.1"));
			if (RefalBase.Match(expression, pattern5))
			{
				return PassiveExpression.Build(true);
			};

			Pattern pattern6 = new Pattern(new SymbolVariable("s.1"), new SymbolVariable("s.2"));
			if (RefalBase.Match(expression, pattern6))
			{
				expression = PassiveExpression.Build(_Alphabet(PassiveExpression.Build()));
				Pattern pattern7 = new Pattern(new ExpressionVariable("e.A"), new SymbolVariable("s.1"), new ExpressionVariable("e.B"), new SymbolVariable("s.2"), new ExpressionVariable("e.C"));
				pattern7.CopyBoundVariables(pattern6);
				if (RefalBase.Match(expression, pattern7))
				{
					return PassiveExpression.Build(true);
				}
			};

			Pattern pattern8 = new Pattern(new ExpressionVariable("e.Z"));
			if (RefalBase.Match(expression, pattern8))
			{
				return PassiveExpression.Build(false);
			};

			throw new RecognitionImpossibleException("Recognition impossible. Last expression: " + ExpressionToString(expression, 0));
		}

		private static PassiveExpression _Alphabet(PassiveExpression expression)
		{
			Pattern pattern9 = new Pattern();
			if (RefalBase.Match(expression, pattern9))
			{
				return PassiveExpression.Build("abcdefghijklmnopqrstuvwxyz".ToCharArray());
			};

			throw new RecognitionImpossibleException("Recognition impossible. Last expression: " + ExpressionToString(expression, 0));
		}

	}
}

