
using System;
using System.Collections;

namespace Refal.Runtime
{
	public class test13 : RefalBase
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
				return PassiveExpression.Build(_Job(PassiveExpression.Build(_Card(PassiveExpression.Build()))));
			};

			throw new RecognitionImpossibleException("Recognition impossible. Last expression: " + expression.ToString());
		}

		private static PassiveExpression _Job(PassiveExpression expression)
		{
			Pattern pattern2 = new Pattern(0);
			if (pattern2.Match(expression))
			{
				return PassiveExpression.Build();
			};

			Pattern pattern3 = new Pattern(new ExpressionVariable("e.X"));
			if (pattern3.Match(expression))
			{
				return PassiveExpression.Build(_Prout(PassiveExpression.Build(_Trans__line(PassiveExpression.Build(pattern3.GetVariable("e.X"))))), _Job(PassiveExpression.Build(_Card(PassiveExpression.Build()))));
			};

			throw new RecognitionImpossibleException("Recognition impossible. Last expression: " + expression.ToString());
		}

		private static PassiveExpression _Trans__line(PassiveExpression expression)
		{
			Pattern pattern4 = new Pattern(" ".ToCharArray(), new ExpressionVariable("e.X"));
			if (pattern4.Match(expression))
			{
				return PassiveExpression.Build(_Trans__line(PassiveExpression.Build(pattern4.GetVariable("e.X"))));
			};

			Pattern pattern5 = new Pattern(new ExpressionVariable("e.Word"), " ".ToCharArray(), new ExpressionVariable("e.Rest"));
			if (pattern5.Match(expression))
			{
				return PassiveExpression.Build(_Trans(PassiveExpression.Build(new OpeningBrace(), pattern5.GetVariable("e.Word"), new ClosingBrace(), _Table(PassiveExpression.Build()))), " ".ToCharArray(), _Trans__line(PassiveExpression.Build(pattern5.GetVariable("e.Rest"))));
			};

			Pattern pattern6 = new Pattern();
			if (pattern6.Match(expression))
			{
				return PassiveExpression.Build();
			};

			Pattern pattern7 = new Pattern(new ExpressionVariable("e.Word"));
			if (pattern7.Match(expression))
			{
				return PassiveExpression.Build(_Trans(PassiveExpression.Build(new OpeningBrace(), pattern7.GetVariable("e.Word"), new ClosingBrace(), _Table(PassiveExpression.Build()))), " ".ToCharArray());
			};

			throw new RecognitionImpossibleException("Recognition impossible. Last expression: " + expression.ToString());
		}

		private static PassiveExpression _Table(PassiveExpression expression)
		{
			Pattern pattern8 = new Pattern();
			if (pattern8.Match(expression))
			{
				return PassiveExpression.Build(new OpeningBrace(), new OpeningBrace(), "cane".ToCharArray(), new ClosingBrace(), "dog".ToCharArray(), new ClosingBrace(), new OpeningBrace(), new OpeningBrace(), "gatto".ToCharArray(), new ClosingBrace(), "cat".ToCharArray(), new ClosingBrace(), new OpeningBrace(), new OpeningBrace(), "cavallo".ToCharArray(), new ClosingBrace(), "horse".ToCharArray(), new ClosingBrace(), new OpeningBrace(), new OpeningBrace(), "rana".ToCharArray(), new ClosingBrace(), "frog".ToCharArray(), new ClosingBrace(), new OpeningBrace(), new OpeningBrace(), "porco".ToCharArray(), new ClosingBrace(), "pig".ToCharArray(), new ClosingBrace());
			};

			throw new RecognitionImpossibleException("Recognition impossible. Last expression: " + expression.ToString());
		}

		private static PassiveExpression _Trans(PassiveExpression expression)
		{
			Pattern pattern9 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.It"), new ClosingBrace(), new ExpressionVariable("e.1"), new OpeningBrace(), new OpeningBrace(), new ExpressionVariable("e.It"), new ClosingBrace(), new ExpressionVariable("e.Eng"), new ClosingBrace(), new ExpressionVariable("e.2"));
			if (pattern9.Match(expression))
			{
				return PassiveExpression.Build(pattern9.GetVariable("e.Eng"));
			};

			Pattern pattern10 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.It"), new ClosingBrace(), new ExpressionVariable("e.1"));
			if (pattern10.Match(expression))
			{
				return PassiveExpression.Build("***".ToCharArray());
			};

			throw new RecognitionImpossibleException("Recognition impossible. Last expression: " + expression.ToString());
		}
	}
}

