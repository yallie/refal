using System;
using System.Collections;

namespace Refal.Runtime
{
	class PatternMatchHelper
	{
		public static bool Match(PassiveExpression expression, Pattern pattern)
		{
			return new PatternMatchHelper().InternalMatch(expression, pattern);
		}

		private PatternMatchHelper()
		{
		}

		private bool InternalMatch(PassiveExpression expression, Pattern pattern)
		{
			// assume at least pattern != null
			int exIndex = 0, patIndex = 0;

			while (patIndex < pattern.Count || exIndex < expression.Count)
			{
				if (patIndex >= pattern.Count)
				{
					if (!RollBackToLastPartialMatch(pattern, ref exIndex, ref patIndex))
						return false;

					continue;
				}

				switch (pattern[patIndex].Match(expression, ref exIndex, patIndex++))
				{
					case MatchResult.Success:
						continue;
						
					case MatchResult.PartialSuccess:
						SaveRollBackInfo(exIndex, patIndex - 1);
						continue;
				}

				if (!RollBackToLastPartialMatch(pattern, ref exIndex, ref patIndex))
					return false;
			}

			return true;
		}

		// stack holding positions of the last expression variables
		Stack rollBackStack = new Stack();

		struct RollbackInfo
		{
			public int exIndex;
			public int patIndex; 
		}

		private void SaveRollBackInfo(int exIndex, int patIndex)
		{
			RollbackInfo info = new RollbackInfo();
			info.exIndex = exIndex;
			info.patIndex = patIndex;
			rollBackStack.Push(info);
		}

		private bool RollBackToLastPartialMatch(Pattern pattern, ref int exIndex, ref int patIndex)
		{
			if (rollBackStack.Count == 0)
				return false;

			// restore state up to the last expression variable
			RollbackInfo info = (RollbackInfo)rollBackStack.Pop();
			exIndex = info.exIndex;
			patIndex = info.patIndex;

			// clear values of all bound variables starting later than patIndex
			pattern.ClearBoundValues(patIndex);
			return true;
		}
	}
}