
using System;
using System.Collections;

namespace Refal.Runtime
{
	public class test23 : RefalBase
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
				return PassiveExpression.Build(_Prout(PassiveExpression.Build(_PreAlph(PassiveExpression.Build("a".ToCharArray(), "c".ToCharArray())))));
			};

			throw new RecognitionImpossibleException("Recognition impossible. Last expression: " + expression.ToString());
		}

		private static PassiveExpression _PreAlph(PassiveExpression expression)
		{
			Pattern pattern2 = new Pattern(new SymbolVariable("s.1"), new SymbolVariable("s.1"));
			if (pattern2.Match(expression))
			{
				return PassiveExpression.Build(true);
			};

			Pattern pattern3 = new Pattern(new SymbolVariable("s.1"), new SymbolVariable("s.2"));
			if (pattern3.Match(expression))
			{
				return PassiveExpression.Build(_Before(PassiveExpression.Build(pattern3.GetVariable("s.1"), pattern3.GetVariable("s.2"), "In", _Alphabet(PassiveExpression.Build()))));
			};

			throw new RecognitionImpossibleException("Recognition impossible. Last expression: " + expression.ToString());
		}

		private static PassiveExpression _Before(PassiveExpression expression)
		{
			Pattern pattern4 = new Pattern(new SymbolVariable("s.1"), new SymbolVariable("s.2"), "In", new ExpressionVariable("e.A"), new SymbolVariable("s.1"), new ExpressionVariable("e.B"), new SymbolVariable("s.2"), new ExpressionVariable("e.C"));
			if (pattern4.Match(expression))
			{
				return PassiveExpression.Build(true);
			};

			Pattern pattern5 = new Pattern(new ExpressionVariable("e.Z"));
			if (pattern5.Match(expression))
			{
				return PassiveExpression.Build(false);
			};

			throw new RecognitionImpossibleException("Recognition impossible. Last expression: " + expression.ToString());
		}

		private static PassiveExpression _Alphabet(PassiveExpression expression)
		{
			Pattern pattern6 = new Pattern();
			if (pattern6.Match(expression))
			{
				return PassiveExpression.Build("abcdefghijklmnopqrstuvwxyz".ToCharArray());
			};

			throw new RecognitionImpossibleException("Recognition impossible. Last expression: " + expression.ToString());
		}
	}
}

