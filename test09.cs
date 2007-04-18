
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
				return PassiveExpression.Build(Open(PassiveExpression.Build("r".ToCharArray(), "File", "test09.ref")), Output(PassiveExpression.Build(new OpeningBrace(), new ClosingBrace(), new OpeningBrace(), "File", new ClosingBrace())));
			};

			throw new RecognitionImpossibleException("Recognition impossible");
		}

		private static PassiveExpression Output(PassiveExpression expression)
		{
			Pattern pattern2 = new Pattern(new OpeningBrace(), new ClosingBrace(), new OpeningBrace(), new SymbolVariable("s.D"), new ClosingBrace());
			if (RefalBase.Match(expression, pattern2))
			{
				return PassiveExpression.Build(Output(PassiveExpression.Build(new OpeningBrace(), Get(PassiveExpression.Build(pattern2.GetVariable("s.D"))), new ClosingBrace(), new OpeningBrace(), pattern2.GetVariable("s.D"), new ClosingBrace())));
			};

			Pattern pattern3 = new Pattern(new OpeningBrace(), 0, new ClosingBrace(), new OpeningBrace(), new SymbolVariable("s.D"), new ClosingBrace());
			if (RefalBase.Match(expression, pattern3))
			{
				return PassiveExpression.Build();
			};

			Pattern pattern4 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), new OpeningBrace(), new SymbolVariable("s.D"), new ClosingBrace());
			if (RefalBase.Match(expression, pattern4))
			{
				return PassiveExpression.Build(Prout(PassiveExpression.Build(pattern4.GetVariable("e.1"))), Output(PassiveExpression.Build(new OpeningBrace(), Get(PassiveExpression.Build(pattern4.GetVariable("s.D"))), new ClosingBrace(), new OpeningBrace(), pattern4.GetVariable("s.D"), new ClosingBrace())));
			};

			throw new RecognitionImpossibleException("Recognition impossible");
		}

	}
}

