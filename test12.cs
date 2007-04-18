
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
				return PassiveExpression.Build(Prout(PassiveExpression.Build(Xxin(PassiveExpression.Build("10_inout.ex".ToCharArray())))));
			};

			throw new RecognitionImpossibleException("Recognition impossible");
		}



	}
}

