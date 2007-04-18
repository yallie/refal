
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
				return PassiveExpression.Build(
					Prout(PassiveExpression.Build("0 + 0 = ", 
						RAdd(PassiveExpression.Build(
							PassiveExpression.CreateSubexpression("0".ToCharArray()), 
							PassiveExpression.CreateSubexpression("0".ToCharArray()))))
					), 
					Prout(PassiveExpression.Build("0' + 0 = ", 
						RAdd(PassiveExpression.Build(
							PassiveExpression.CreateSubexpression("0\'".ToCharArray()), 
							PassiveExpression.CreateSubexpression("0".ToCharArray()))))
					), 
					Prout(PassiveExpression.Build("0 + 0' = ", 
						RAdd(PassiveExpression.Build(
							PassiveExpression.CreateSubexpression("0".ToCharArray()), 
							PassiveExpression.CreateSubexpression("0\'".ToCharArray()))))
					), 
					Prout(PassiveExpression.Build("0' + 0' = ", 
						RAdd(PassiveExpression.Build(
							PassiveExpression.CreateSubexpression("0\'".ToCharArray()), 
							PassiveExpression.CreateSubexpression("0\'".ToCharArray()))))
					), 
					Prout(PassiveExpression.Build("0'' + 0''' = ", 
						RAdd(PassiveExpression.Build(
							PassiveExpression.CreateSubexpression("0\'\'".ToCharArray()), 
							PassiveExpression.CreateSubexpression("0\'\'\'".ToCharArray()))))
					)
				);
			};

			throw new RecognitionImpossibleException("Recognition impossible");
		}

		private static PassiveExpression RAdd(PassiveExpression expression)
		{
			Pattern pattern2 = new Pattern(new Pattern(new ExpressionVariable("e.1")), new Pattern("0".ToCharArray()));
			if (RefalBase.Match(expression, pattern2))
			{
				return PassiveExpression.Build(pattern2.GetVariable("e.1"));
			};

			Pattern pattern3 = new Pattern(new Pattern(new ExpressionVariable("e.1")), new Pattern(new ExpressionVariable("e.2"), "\'".ToCharArray()));
			if (RefalBase.Match(expression, pattern3))
			{
				return PassiveExpression.Build(RAdd(PassiveExpression.Build(PassiveExpression.CreateSubexpression(pattern3.GetVariable("e.1")), PassiveExpression.CreateSubexpression(pattern3.GetVariable("e.2")))), "\'".ToCharArray());
			};

			Pattern pattern4 = new Pattern(new ExpressionVariable("e.1"));
			if (RefalBase.Match(expression, pattern4))
			{
				return PassiveExpression.Build();
			};

			throw new RecognitionImpossibleException("Recognition impossible");
		}

	}
}

