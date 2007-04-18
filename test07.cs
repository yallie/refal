
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
				return PassiveExpression.Build(Prout(PassiveExpression.Build("0 + 0 =", AddB(PassiveExpression.Build(new OpeningBrace(), "0".ToCharArray(), new ClosingBrace(), new OpeningBrace(), "0".ToCharArray(), new ClosingBrace())))), Prout(PassiveExpression.Build("0 + 1 =", AddB(PassiveExpression.Build(new OpeningBrace(), "0".ToCharArray(), new ClosingBrace(), new OpeningBrace(), "1".ToCharArray(), new ClosingBrace())))), Prout(PassiveExpression.Build("1 + 0 =", AddB(PassiveExpression.Build(new OpeningBrace(), "1".ToCharArray(), new ClosingBrace(), new OpeningBrace(), "0".ToCharArray(), new ClosingBrace())))), Prout(PassiveExpression.Build("1 + 1 =", AddB(PassiveExpression.Build(new OpeningBrace(), "1".ToCharArray(), new ClosingBrace(), new OpeningBrace(), "1".ToCharArray(), new ClosingBrace())))), Prout(PassiveExpression.Build("10 + 01 =", AddB(PassiveExpression.Build(new OpeningBrace(), "10".ToCharArray(), new ClosingBrace(), new OpeningBrace(), "01".ToCharArray(), new ClosingBrace())))), Prout(PassiveExpression.Build("1011 + 0110 =", AddB(PassiveExpression.Build(new OpeningBrace(), "1011".ToCharArray(), new ClosingBrace(), new OpeningBrace(), "0110".ToCharArray(), new ClosingBrace())))), Prout(PassiveExpression.Build("101100001 + 10110101 =", AddB(PassiveExpression.Build(new OpeningBrace(), "101100001".ToCharArray(), new ClosingBrace(), new OpeningBrace(), "10110101".ToCharArray(), new ClosingBrace())))), Prout(PassiveExpression.Build("asdbn + ddd =", AddB(PassiveExpression.Build(new OpeningBrace(), "asdbn".ToCharArray(), new ClosingBrace(), new OpeningBrace(), "ddd".ToCharArray(), new ClosingBrace())))));
			};

			throw new RecognitionImpossibleException("Recognition impossible");
		}

		private static PassiveExpression AddB(PassiveExpression expression)
		{
			Pattern pattern2 = new Pattern(new OpeningBrace(), new ClosingBrace(), new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace());
			if (RefalBase.Match(expression, pattern2))
			{
				return PassiveExpression.Build(pattern2.GetVariable("e.1"));
			};

			Pattern pattern3 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), new OpeningBrace(), new ClosingBrace());
			if (RefalBase.Match(expression, pattern3))
			{
				return PassiveExpression.Build(pattern3.GetVariable("e.1"));
			};

			Pattern pattern4 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.1"), "0".ToCharArray(), new ClosingBrace(), new OpeningBrace(), new ExpressionVariable("e.2"), new SymbolVariable("s.3"), new ClosingBrace());
			if (RefalBase.Match(expression, pattern4))
			{
				return PassiveExpression.Build(AddB(PassiveExpression.Build(new OpeningBrace(), pattern4.GetVariable("e.1"), new ClosingBrace(), new OpeningBrace(), pattern4.GetVariable("e.2"), new ClosingBrace())), pattern4.GetVariable("s.3"));
			};

			Pattern pattern5 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.1"), new SymbolVariable("s.3"), new ClosingBrace(), new OpeningBrace(), new ExpressionVariable("e.2"), "0".ToCharArray(), new ClosingBrace());
			if (RefalBase.Match(expression, pattern5))
			{
				return PassiveExpression.Build(AddB(PassiveExpression.Build(new OpeningBrace(), pattern5.GetVariable("e.1"), new ClosingBrace(), new OpeningBrace(), pattern5.GetVariable("e.2"), new ClosingBrace())), pattern5.GetVariable("s.3"));
			};

			Pattern pattern6 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.1"), "1".ToCharArray(), new ClosingBrace(), new OpeningBrace(), new ExpressionVariable("e.2"), "1".ToCharArray(), new ClosingBrace());
			if (RefalBase.Match(expression, pattern6))
			{
				return PassiveExpression.Build(AddB(PassiveExpression.Build(new OpeningBrace(), "1".ToCharArray(), new ClosingBrace(), new OpeningBrace(), AddB(PassiveExpression.Build(new OpeningBrace(), pattern6.GetVariable("e.1"), new ClosingBrace(), new OpeningBrace(), pattern6.GetVariable("e.2"), new ClosingBrace())), new ClosingBrace())), "0".ToCharArray());
			};

			Pattern pattern7 = new Pattern(new ExpressionVariable("e.1"));
			if (RefalBase.Match(expression, pattern7))
			{
				return PassiveExpression.Build("** error! **".ToCharArray());
			};

			throw new RecognitionImpossibleException("Recognition impossible");
		}

	}
}

