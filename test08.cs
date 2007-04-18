
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
				return PassiveExpression.Build(Output(PassiveExpression.Build()));
			};

			throw new RecognitionImpossibleException("Recognition impossible");
		}

		private static PassiveExpression Output(PassiveExpression expression)
		{
			Pattern pattern2 = new Pattern();
			if (RefalBase.Match(expression, pattern2))
			{
				return PassiveExpression.Build(Output(PassiveExpression.Build(Card(PassiveExpression.Build()))));
			};

			Pattern pattern3 = new Pattern(0);
			if (RefalBase.Match(expression, pattern3))
			{
				return PassiveExpression.Build();
			};

			Pattern pattern4 = new Pattern(new ExpressionVariable("e.1"));
			if (RefalBase.Match(expression, pattern4))
			{
				return PassiveExpression.Build(Prout(PassiveExpression.Build(pattern4.GetVariable("e.1"))), Output(PassiveExpression.Build(Card(PassiveExpression.Build()))));
			};

			throw new RecognitionImpossibleException("Recognition impossible");
		}

	}
}

