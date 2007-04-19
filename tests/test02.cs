
using System;
using System.Collections;

namespace Refal.Runtime
{
	public class test02 : RefalBase
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
				return PassiveExpression.Build(_Prout(PassiveExpression.Build("Hello".ToCharArray())));
			};

			throw new RecognitionImpossibleException("Recognition impossible. Last expression: " + expression.ToString());
		}
	}
}

