
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

				// expression is empty, pattern isn't
				return false;
			}

			// pattern is empty while expression isn't
			if (pattern == null || pattern.IsEmpty)
				return false;

			// handle simple cases
			if (pattern.Count == 1)
			{
				if (pattern[0] is ExpressionVariable)
				{
					// bind free variable to an expression
					ExpressionVariable var = (ExpressionVariable)pattern[0];
					var.Expression = expression;
					return true;
				}

				// check for single symbol or single term
				if (expression.Count == 1)
				{
					// symbol matches symbol
					if (pattern[0].Equals(expression[0]) && !(pattern[0] is Variable))
						return true;

					// term ::= symbol | (expression)
					if (expression[0] is PassiveExpression && pattern[0] is TermVariable)
					{
						// bind free variable to an expression
						TermVariable var = (TermVariable)pattern[0];
						PassiveExpression expr = (PassiveExpression)expression[0];
						var.Expression = expr;
						return true;
					}

					// either term or symbol
					if (pattern[0] is Variable && !(expression[0] is PassiveExpression))
					{
						Variable var = (Variable)pattern[0];
						var.Value = expression[0];
						return true;
					}
				}
			}

			// both pattern and expression are not empty, perform non-trivial matching
			return PatternMatchHelper.Match(expression, pattern);
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

