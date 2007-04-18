
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
				return PassiveExpression.Build(_Prout(PassiveExpression.Build("0 + 0 = ", _RAdd(PassiveExpression.Build(new OpeningBrace(), "0".ToCharArray(), new ClosingBrace(), new OpeningBrace(), "0".ToCharArray(), new ClosingBrace())))), _Prout(PassiveExpression.Build("0\' + 0 = ", _RAdd(PassiveExpression.Build(new OpeningBrace(), "0\'".ToCharArray(), new ClosingBrace(), new OpeningBrace(), "0".ToCharArray(), new ClosingBrace())))), _Prout(PassiveExpression.Build("0 + 0\' = ", _RAdd(PassiveExpression.Build(new OpeningBrace(), "0".ToCharArray(), new ClosingBrace(), new OpeningBrace(), "0\'".ToCharArray(), new ClosingBrace())))), _Prout(PassiveExpression.Build("0\' + 0\' = ", _RAdd(PassiveExpression.Build(new OpeningBrace(), "0\'".ToCharArray(), new ClosingBrace(), new OpeningBrace(), "0\'".ToCharArray(), new ClosingBrace())))), _Prout(PassiveExpression.Build("0\'\' + 0\'\'\' = ", _RAdd(PassiveExpression.Build(new OpeningBrace(), "0\'\'".ToCharArray(), new ClosingBrace(), new OpeningBrace(), "0\'\'\'".ToCharArray(), new ClosingBrace())))));
			};

			throw new RecognitionImpossibleException("Recognition impossible");
		}

		private static PassiveExpression _RAdd(PassiveExpression expression)
		{
			Pattern pattern2 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), new OpeningBrace(), "0".ToCharArray(), new ClosingBrace());
			if (RefalBase.Match(expression, pattern2))
			{
				return PassiveExpression.Build(pattern2.GetVariable("e.1"));
			};

			Pattern pattern3 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), new OpeningBrace(), new ExpressionVariable("e.2"), "\'".ToCharArray(), new ClosingBrace());
			if (RefalBase.Match(expression, pattern3))
			{
				return PassiveExpression.Build(_RAdd(PassiveExpression.Build(new OpeningBrace(), pattern3.GetVariable("e.1"), new ClosingBrace(), new OpeningBrace(), pattern3.GetVariable("e.2"), new ClosingBrace())), "\'".ToCharArray());
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

