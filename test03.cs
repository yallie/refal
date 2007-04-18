
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
				return PassiveExpression.Build(_Prout(PassiveExpression.Build(_Pal(PassiveExpression.Build("sator arepo tenet opera rotas".ToCharArray())))));
			};

			throw new RecognitionImpossibleException("Recognition impossible");
		}

		private static PassiveExpression _Pal(PassiveExpression expression)
		{
			Pattern pattern2 = new Pattern();
			if (RefalBase.Match(expression, pattern2))
			{
				return PassiveExpression.Build(true);
			};

			Pattern pattern3 = new Pattern(new SymbolVariable("s.1"));
			if (RefalBase.Match(expression, pattern3))
			{
				return PassiveExpression.Build(true);
			};

			Pattern pattern4 = new Pattern(new SymbolVariable("s.1"), new ExpressionVariable("e.2"), new SymbolVariable("s.1"));
			if (RefalBase.Match(expression, pattern4))
			{
				return PassiveExpression.Build(_Pal(PassiveExpression.Build(pattern4.GetVariable("e.2"))));
			};

			Pattern pattern5 = new Pattern(new ExpressionVariable("e.1"));
			if (RefalBase.Match(expression, pattern5))
			{
				return PassiveExpression.Build(false);
			};

			throw new RecognitionImpossibleException("Recognition impossible");
		}

	}
}

