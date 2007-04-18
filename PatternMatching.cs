using System;
using System.Collections;

namespace Refal.Runtime
{
	class PatternMatchHelper
	{
		public static bool Match(PassiveExpression expression, Pattern pattern)
		{
			return new PatternMatchHelper(expression, pattern).Match();
		}

		private PatternMatchHelper(PassiveExpression expression, Pattern pattern)
		{
			this.expression = expression;
			this.pattern = pattern;
		}

		// expression and pattern to match
		PassiveExpression expression = null;
		Pattern pattern = null;

		// current expression and pattern element indices
		int exIndex = 0, patIndex = 0;

		// private implementation details
		bool match = true;

		// stack holding positions of the last expression variables
		Stack rollBackStack = new Stack();

		struct RollbackInfo
		{
			public int exIndex;
			public int patIndex; 
		}

		private bool Match()
		{
			while (true)
			{
				while (match && patIndex < pattern.Count && exIndex < expression.Count)
				{
					// assume that expression and pattern aren't empty
					PatternItem pat = pattern[patIndex] as PatternItem;

					MatchResult mr = pat.Match(expression, ref exIndex, patIndex);

					if (mr == MatchResult.Success)
					{
						patIndex++;
						continue;
					}
					
					if (mr == MatchResult.PartialSuccess)
					{
						RollbackInfo info = new RollbackInfo();
						info.exIndex = exIndex;
						info.patIndex = patIndex;
						rollBackStack.Push(info);
						patIndex++;
						continue;
					}
					else
					{
						// roll back to the last partial match to find another match
						// if can't roll back, then matching failed
						match = RollBackToLastPartialMatch();
					}
					
				} // while()

				if (match && patIndex >= pattern.Count && exIndex >= expression.Count)
					return true;

				// check for special case: expression has ended, but pattern contains a few expression variables
				// in that case, matching should succeed, with all remaining variables taking empty values
				if (match && exIndex >= expression.Count && patIndex < pattern.Count)
				{
					bool matchLastExpressions = true;

					while (matchLastExpressions && patIndex < pattern.Count)
					{
						object pat = pattern[patIndex++];
						if (!(pat is ExpressionVariable))
						{
							matchLastExpressions = false;
							break;
						}

						ExpressionVariable var = (ExpressionVariable)pat;
						var.Expression = new PassiveExpression();
					}

					if (matchLastExpressions && patIndex >= pattern.Count)
						return true;
				}

				// if can roll back, try once more
				if (!RollBackToLastPartialMatch())
				{
					// can't to roll back => matching has failed
					return false;
				}
			}
		}

		private bool RollBackToLastPartialMatch()
		{
			if (rollBackStack.Count == 0)
				return false;

			// restore state up to the last expression variable
			RollbackInfo info = (RollbackInfo)rollBackStack.Pop();
			exIndex = info.exIndex;
			patIndex = info.patIndex;

			// clear values of all bound variables starting later than patIndex
			ClearBoundValues(pattern, patIndex);
			return true;
		}

		private static void ClearBoundValues(Pattern pattern, int startFromIndex)
		{
			foreach (string name in pattern.Variables.Keys)
			{
				Variable var = (Variable)pattern.Variables[name];

				if (var.FirstOccurance > startFromIndex) // not >=!
					var.Value = null;
			}
		}
	}
}