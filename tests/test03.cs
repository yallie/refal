
using System;
using System.Collections;

namespace Refal.Runtime
{
	public class test03 : RefalBase
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
				return PassiveExpression.Build(_Prout(PassiveExpression.Build(_Pal(PassiveExpression.Build("sator arepo tenet opera rotas".ToCharArray())))));
			};

			throw new RecognitionImpossibleException("Recognition impossible. Last expression: " + expression.ToString());
		}

		private static PassiveExpression _Pal(PassiveExpression expression)
		{
			Pattern pattern2 = new Pattern();
			if (pattern2.Match(expression))
			{
				return PassiveExpression.Build(true);
			};

			Pattern pattern3 = new Pattern(new SymbolVariable("s.1"));
			if (pattern3.Match(expression))
			{
				return PassiveExpression.Build(true);
			};

			Pattern pattern4 = new Pattern(new SymbolVariable("s.1"), new ExpressionVariable("e.2"), new SymbolVariable("s.1"));
			if (pattern4.Match(expression))
			{
				return PassiveExpression.Build(_Pal(PassiveExpression.Build(pattern4.GetVariable("e.2"))));
			};

			Pattern pattern5 = new Pattern(new ExpressionVariable("e.1"));
			if (pattern5.Match(expression))
			{
				return PassiveExpression.Build(false);
			};

			throw new RecognitionImpossibleException("Recognition impossible. Last expression: " + expression.ToString());
		}
	}
}

