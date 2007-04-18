
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
				return PassiveExpression.Build(Job(PassiveExpression.Build(Card(PassiveExpression.Build()))));
			};

			throw new RecognitionImpossibleException("Recognition impossible");
		}

		private static PassiveExpression Job(PassiveExpression expression)
		{
			Pattern pattern2 = new Pattern(0);
			if (RefalBase.Match(expression, pattern2))
			{
				return PassiveExpression.Build();
			};

			Pattern pattern3 = new Pattern(new ExpressionVariable("e.X"));
			if (RefalBase.Match(expression, pattern3))
			{
				return PassiveExpression.Build(Prout(PassiveExpression.Build(Trans__line(PassiveExpression.Build(pattern3.GetVariable("e.X"))))), Job(PassiveExpression.Build(Card(PassiveExpression.Build()))));
			};

			throw new RecognitionImpossibleException("Recognition impossible");
		}

		private static PassiveExpression Trans__line(PassiveExpression expression)
		{
			Pattern pattern4 = new Pattern(" ".ToCharArray(), new ExpressionVariable("e.X"));
			if (RefalBase.Match(expression, pattern4))
			{
				return PassiveExpression.Build(Trans__line(PassiveExpression.Build(pattern4.GetVariable("e.X"))));
			};

			Pattern pattern5 = new Pattern(new ExpressionVariable("e.Word"), " ".ToCharArray(), new ExpressionVariable("e.Rest"));
			if (RefalBase.Match(expression, pattern5))
			{
				return PassiveExpression.Build(Trans(PassiveExpression.Build(new OpeningBrace(), pattern5.GetVariable("e.Word"), new ClosingBrace(), Table(PassiveExpression.Build()))), " ".ToCharArray(), Trans__line(PassiveExpression.Build(pattern5.GetVariable("e.Rest"))));
			};

			Pattern pattern6 = new Pattern();
			if (RefalBase.Match(expression, pattern6))
			{
				return PassiveExpression.Build();
			};

			Pattern pattern7 = new Pattern(new ExpressionVariable("e.Word"));
			if (RefalBase.Match(expression, pattern7))
			{
				return PassiveExpression.Build(Trans(PassiveExpression.Build(new OpeningBrace(), pattern7.GetVariable("e.Word"), new ClosingBrace(), Table(PassiveExpression.Build()))), " ".ToCharArray());
			};

			throw new RecognitionImpossibleException("Recognition impossible");
		}

		private static PassiveExpression Table(PassiveExpression expression)
		{
			Pattern pattern8 = new Pattern();
			if (RefalBase.Match(expression, pattern8))
			{
				return PassiveExpression.Build(new OpeningBrace(), new OpeningBrace(), "cane".ToCharArray(), new ClosingBrace(), "dog".ToCharArray(), new ClosingBrace(), new OpeningBrace(), new OpeningBrace(), "gatto".ToCharArray(), new ClosingBrace(), "cat".ToCharArray(), new ClosingBrace(), new OpeningBrace(), new OpeningBrace(), "cavallo".ToCharArray(), new ClosingBrace(), "horse".ToCharArray(), new ClosingBrace(), new OpeningBrace(), new OpeningBrace(), "rana".ToCharArray(), new ClosingBrace(), "frog".ToCharArray(), new ClosingBrace(), new OpeningBrace(), new OpeningBrace(), "porco".ToCharArray(), new ClosingBrace(), "pig".ToCharArray(), new ClosingBrace());
			};

			throw new RecognitionImpossibleException("Recognition impossible");
		}

		private static PassiveExpression Trans(PassiveExpression expression)
		{
			Pattern pattern9 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.It"), new ClosingBrace(), new ExpressionVariable("e.1"), new OpeningBrace(), new OpeningBrace(), new ExpressionVariable("e.It"), new ClosingBrace(), new ExpressionVariable("e.Eng"), new ClosingBrace(), new ExpressionVariable("e.2"));
			if (RefalBase.Match(expression, pattern9))
			{
				return PassiveExpression.Build(pattern9.GetVariable("e.Eng"));
			};

			Pattern pattern10 = new Pattern(new OpeningBrace(), new ExpressionVariable("e.It"), new ClosingBrace(), new ExpressionVariable("e.1"));
			if (RefalBase.Match(expression, pattern10))
			{
				return PassiveExpression.Build("***".ToCharArray());
			};

			throw new RecognitionImpossibleException("Recognition impossible");
		}

	}
}

