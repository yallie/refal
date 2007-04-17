
using System;
using System.Text;
using System.Collections;

namespace Refal.Runtime
{
	public class RefalBase
	{
		public static bool Match(PassiveExpression expression, Pattern pattern)
		{
			if (expression == null || expression.IsEmpty)
			{
				// empty expression matches empty pattern
				if (pattern == null || pattern.IsEmpty)
					return true;

				// any expression matches the pattern consisting of a
				// single expression variable (like e.1)
				if (pattern.Count == 1 && pattern[0] is ExpressionVariable)
				{
					// bind free variable to an expression
					ExpressionVariable var = (ExpressionVariable)pattern[0];
					var.Expression = expression;
					return true;
				}

				return false;
			}

			// both pattern and expression are not empty, perform matching
			// TODO

			return false;
		}

		// Standard RTL routines

		public static PassiveExpression Prout(PassiveExpression expression)
		{
			if (expression == null)
				return null;

			StringBuilder sb = new StringBuilder();

			foreach (object value in expression)
			{
				sb.Append(value.ToString());

				if (!(value is char))
					sb.Append(' ');
			}

			Console.WriteLine("{0}", sb.ToString());

			return null;
		}
	}

	public class RecognitionImpossibleException : Exception
	{
		public RecognitionImpossibleException() : base()
		{
		}

		public RecognitionImpossibleException(string msg) : base(msg)
		{
		}
	}
}

