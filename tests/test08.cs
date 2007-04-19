
using System;
using System.Collections;

namespace Refal.Runtime
{
	public class test08 : RefalBase
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
				return PassiveExpression.Build(_Output(PassiveExpression.Build()));
			};

			throw new RecognitionImpossibleException("Recognition impossible. Last expression: " + expression.ToString());
		}

		private static PassiveExpression _Output(PassiveExpression expression)
		{
			Pattern pattern2 = new Pattern();
			if (pattern2.Match(expression))
			{
				return PassiveExpression.Build(_Output(PassiveExpression.Build(_Card(PassiveExpression.Build()))));
			};

			Pattern pattern3 = new Pattern(0);
			if (pattern3.Match(expression))
			{
				return PassiveExpression.Build();
			};

			Pattern pattern4 = new Pattern(new ExpressionVariable("e.1"));
			if (pattern4.Match(expression))
			{
				return PassiveExpression.Build(_Prout(PassiveExpression.Build(pattern4.GetVariable("e.1"))), _Output(PassiveExpression.Build(_Card(PassiveExpression.Build()))));
			};

			throw new RecognitionImpossibleException("Recognition impossible. Last expression: " + expression.ToString());
		}
	}
}

