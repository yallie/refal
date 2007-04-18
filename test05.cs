
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
				return PassiveExpression.Build(Prout(PassiveExpression.Build(Chpm(PassiveExpression.Build("++312a=-3+=-".ToCharArray())))));
			};

			throw new RecognitionImpossibleException("Recognition impossible");
		}

		private static PassiveExpression Chpm(PassiveExpression expression)
		{
			Pattern pattern2 = new Pattern(new ExpressionVariable("e.1"), "+".ToCharArray(), new ExpressionVariable("e.2"));
			if (RefalBase.Match(expression, pattern2))
			{
				return PassiveExpression.Build(pattern2.GetVariable("e.1"), "-".ToCharArray(), Chpm(PassiveExpression.Build(pattern2.GetVariable("e.2"))));
			};

			Pattern pattern3 = new Pattern(new ExpressionVariable("e.1"));
			if (RefalBase.Match(expression, pattern3))
			{
				return PassiveExpression.Build(pattern3.GetVariable("e.1"));
			};

			throw new RecognitionImpossibleException("Recognition impossible");
		}

	}
}

