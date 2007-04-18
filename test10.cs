
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
				return PassiveExpression.Build(_Open(PassiveExpression.Build("r".ToCharArray(), _InputFile(PassiveExpression.Build()), "test10.ref".ToCharArray())), _Output(PassiveExpression.Build(new OpeningBrace(), new ClosingBrace(), new OpeningBrace(), _InputFile(PassiveExpression.Build()), new ClosingBrace())));
			};

			throw new RecognitionImpossibleException("Recognition impossible");
		}

		private static PassiveExpression _InputFile(PassiveExpression expression)
		{
			Pattern pattern2 = new Pattern();
			if (RefalBase.Match(expression, pattern2))
			{
				return PassiveExpression.Build(1);
			};

			throw new RecognitionImpossibleException("Recognition impossible");
		}

		private static PassiveExpression _Output(PassiveExpression expression)
		{
			Pattern pattern3 = new Pattern(new OpeningBrace(), new ClosingBrace(), new OpeningBrace(), new SymbolVariable("s.D"), new ClosingBrace());
			if (RefalBase.Match(expression, pattern3))
			{
				return PassiveExpression.Build(_Output(PassiveExpression.Build(new OpeningBrace(), _Get(PassiveExpression.Build(pattern3.GetVariable("s.D"))), new ClosingBrace(), new OpeningBrace(), pattern3.GetVariable("s.D"), new ClosingBrace())));
			};

			Pattern pattern4 = new Pattern(new OpeningBrace(), 0, new ClosingBrace(), new OpeningBrace(), new SymbolVariable("s.D"), new ClosingBrace());
			if (RefalBase.Match(expression, pattern4))
			{
				return PassiveExpression.Build();
			};

			Pattern pattern5 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.1"), new ClosingBrace(), new OpeningBrace(), new SymbolVariable("s.D"), new ClosingBrace());
			if (RefalBase.Match(expression, pattern5))
			{
				return PassiveExpression.Build(_Prout(PassiveExpression.Build(pattern5.GetVariable("e.1"))), _Output(PassiveExpression.Build(new OpeningBrace(), _Get(PassiveExpression.Build(pattern5.GetVariable("s.D"))), new ClosingBrace(), new OpeningBrace(), pattern5.GetVariable("s.D"), new ClosingBrace())));
			};

			throw new RecognitionImpossibleException("Recognition impossible");
		}

	}
}

