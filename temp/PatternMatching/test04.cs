
using System;
using System.Collections;

namespace Refal.Runtime
{
	public class Program : RefalBase
	{
		static void Main()
		{
			Go(new PassiveExpression());
			Console.ReadLine();
		}

		public static PassiveExpression Go(PassiveExpression expression)
		{
			Pattern pattern1 = new Pattern();
			if (RefalBase.Match(expression, pattern1))
			{
				return PassiveExpression.Build(Prout(PassiveExpression.Build(Chpm(PassiveExpression.Build("++312a=-3+=-".ToCharArray())))));
			};

			throw new RecognitionImpossibleException("Recognition impossible");
		}

		private static PassiveExpression Chpm(PassiveExpression expression)
		{
			Pattern pattern2 = new Pattern("+".ToCharArray(), new ExpressionVariable("e.1"));
			if (RefalBase.Match(expression, pattern2))
			{
				return PassiveExpression.Build("-".ToCharArray(), Chpm(PassiveExpression.Build(pattern2.GetVariable("e.1"))));
			};

			Pattern pattern3 = new Pattern(new SymbolVariable("s.1"), new ExpressionVariable("e.2"));
			if (RefalBase.Match(expression, pattern3))
			{
				return PassiveExpression.Build(pattern3.GetVariable("s.1"), Chpm(PassiveExpression.Build(pattern3.GetVariable("e.2"))));
			};

			Pattern pattern4 = new Pattern();
			if (RefalBase.Match(expression, pattern4))
			{
				return PassiveExpression.Build();
			};

			throw new RecognitionImpossibleException("Recognition impossible");
		}

	}
}

