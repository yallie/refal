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
					object ex = expression[exIndex];
					object pat = pattern[patIndex];

					bool needRollback = false;

					if (MatchStructureBraces(ex, pat, ref needRollback))
						continue;
					
					if (!needRollback && MatchTwoSymbols(ex, pat, ref needRollback))
						continue;
					
					if (!needRollback && MatchSimpleVariables(ex, pat, ref needRollback))
						continue;
					
					if (!needRollback && MatchTermSubexpression(ex, pat, ref needRollback))
						continue;
					
					if (!needRollback && MatchExpressionVariable(ex, pat, ref needRollback))
						continue;

					// roll back to the last expression variable
					if (!RollBackToLastExpression())
					{
						// can't roll back => matching failed
						match = false;
					}

				} // while()

				if (match && patIndex >= pattern.Count && exIndex >= expression.Count)
					return true;

				// check for special case: expression has ended, but pattern contains a few expression variables
				// in that case, matching should succeed, with all remaining variables taking empty values
				if (match && exIndex >= expression.Count && patIndex < pattern.Count)
				{
					while (match && patIndex < pattern.Count)
					{
						object pat = pattern[patIndex++];
						if (!(pat is ExpressionVariable))
						{
							match = false;
							break;
						}

						ExpressionVariable var = (ExpressionVariable)pat;
						var.Expression = new PassiveExpression();
					}

					if (match && patIndex >= pattern.Count)
						return true;
				}

				// if can roll back, try once more
				if (!RollBackToLastExpression())
				{
					// can't to roll back => matching has failed
					return false;
				}
			}
		}

		private bool MatchStructureBraces(object ex, object pat, ref bool needRollback)
		{
			// braces can only match itself
			if (pat is OpeningBrace)
			{
				if (ex is OpeningBrace)
				{
					exIndex++; patIndex++;
					return true;
				}
				else
					needRollback = true;
			}
			else if (pat is ClosingBrace)
			{
				if (ex is ClosingBrace)
				{
					exIndex++; patIndex++;
					return true;
				}
				else
					needRollback = true;
			}

			// match successfull
			return false;
		}

		private bool MatchTwoSymbols(object ex, object pat, ref bool needRollback)
		{
			// symbol matches single symbol
			if (!(pat is Variable))
			{
				// check for () is redundant because of ex.Equals(pat)
				if (ex.Equals(pat) && !(ex is StructureBrace))
				{
					exIndex++; patIndex++;
					return true;
				}
				else
					needRollback = true;
			}

			return false;
		}

		private bool MatchSimpleVariables(object ex, object pat, ref bool needRollback)
		{
			// symbol or term variable matches single symbol
			if ((pat is SymbolVariable || pat is TermVariable) && !(ex is StructureBrace))
			{
				Variable var = pat as Variable;

				if (patIndex == var.FirstOccurance)
				{
					var.Value = ex;
					exIndex++; patIndex++;
					return true;
				}
				else if (ex.Equals(var.Value))
				{
					exIndex++; patIndex++;
					return true;
				}
				else 
				{
					// symbol don't match the bound variable,
					// roll back to the last expression variable
					needRollback = true;
				}
			}

			return false;
		}

		private bool MatchTermSubexpression(object ex, object pat, ref bool needRollback)
		{
			// term variable matches subexpression in structure braces
			if ((pat is TermVariable) && (ex is StructureBrace))
			{
				TermVariable term = pat as TermVariable;

				// first occurance => copy subexpression
				if (patIndex == term.FirstOccurance)
				{
					if (ex is OpeningBrace)
					{
						term.Expression.Add(ex);
						exIndex++; patIndex++;

						// extract subexpression within the structure braces
						int rank = 1;
						while (exIndex < expression.Count && rank > 0)
						{
							ex = expression[exIndex++];
							term.Expression.Add(ex);

							if (ex is OpeningBrace)
								rank++;
							else if (ex is ClosingBrace)
								rank--;
						}

						// subexpression with surrounding braces is extracted
						if (rank == 0)
							return true;
						else
						{
							// closing structure brace not found => rolling back
							// in fact, this can only be caused by unmatched braces...
							needRollback = true;
						}
					}
					else 
					{
						needRollback = true;
					}
				}
				else // not the first occurance => compare expression
				{
					if (CompareExpressions(expression, exIndex, term.Expression))
					{
						exIndex += term.Expression.Count; patIndex++;
						return true;
					}
					else
						needRollback = true;
				}
			}

			return false;
		}

		private bool MatchExpressionVariable(object ex, object pat, ref bool needRollback)
		{
			// expression variable can match nothing, symbol(s), 
			// and expression(s) within structure braces
			if (pat is ExpressionVariable)
			{
				ExpressionVariable var = pat as ExpressionVariable;

				if (patIndex == var.FirstOccurance)
				{
					if (var.Expression == null)
					{
						// start with empty expression
						var.Expression = new PassiveExpression();
						RollbackInfo info = new RollbackInfo();
						info.exIndex = exIndex;
						info.patIndex = patIndex;
						rollBackStack.Push(info);
						patIndex++;
						return true;
					}
					else
					{
						// continue adding terms to expression
						if (!(ex is StructureBrace))
						{
							var.Expression.Add(ex);
							RollbackInfo info = new RollbackInfo();
							info.exIndex = exIndex + 1;
							info.patIndex = patIndex;
							rollBackStack.Push(info);
							exIndex++; patIndex++;
							return true;
						}
						// handle structure braces, extract subexpression
						else if (ex is OpeningBrace)
						{
							var.Expression.Add(ex);
							exIndex++;

							// extract subexpression within the structure braces
							int rank = 1;
							while (exIndex < expression.Count && rank > 0)
							{
								ex = expression[exIndex++];
								var.Expression.Add(ex);

								if (ex is OpeningBrace)
									rank++;
								else if (ex is ClosingBrace)
									rank--;
							}

							// subexpression with surrounding braces is extracted
							if (rank == 0)
								return true;
							else
							{
								// closing structure brace not found => rolling back
								// this can only be caused by unmatched braces => error in compiler
								needRollback = true;
							}
						}
						else
						{
							// extra closing brace ) => roll back
							needRollback = true;
						}
					}
				}
				else // not the first occurance, compare expressions
				{
					if (CompareExpressions(expression, exIndex, var.Expression))
					{
						exIndex += var.Expression.Count; patIndex++;
						return true;
					}
					else
						// expression don't match, roll back
						needRollback = true;
				}
			}

			return false;
		}

		private static bool CompareExpressions(PassiveExpression expression, int exIndex, PassiveExpression varExpression)
		{
			if (expression == null && varExpression == null)
				return true;

			if (expression != null && varExpression != null)
			{
				for (int i = 0; i < varExpression.Count; i++)
				{
					if (i + exIndex >= expression.Count)
						return false;

					object ex = expression[i + exIndex];
					object varEx = varExpression[i];

					if (ex is OpeningBrace)
					{
						if (!(varEx is OpeningBrace))
							return false;
					}
					else if (ex is ClosingBrace)
					{
						if (!(varEx is ClosingBrace))
							return false;
					}
					else if (!ex.Equals(varEx))
						return false;
				}

				return true;
			}

			return false;
		}

		private bool RollBackToLastExpression()
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

/*		// kept for test purposes only
		static void Main()
		{
			PassiveExpression ex = new PassiveExpression("Hello", "Dirty", "World");
			Pattern p = new Pattern("Hello", new SymbolVariable("s.1"), "World");
			TestPatternMatching(ex, p);

			ex = new PassiveExpression(1, "World");
			p = new Pattern(new SymbolVariable("s.1"), new SymbolVariable("s.1"));
			TestPatternMatching(ex, p);

			ex = new PassiveExpression(1, 2, "World");
			p = new Pattern(new SymbolVariable("s.1"), new TermVariable("t.1"), new SymbolVariable("s.2"));
			TestPatternMatching(ex, p);

			ex = new PassiveExpression("Hey", new PassiveExpression(1, 2, 3), "Jude");
			p = new Pattern(new SymbolVariable("s.1"), new TermVariable("t.1"), new SymbolVariable("s.2"));
			TestPatternMatching(ex, p);

			ex = new PassiveExpression("Hey", new PassiveExpression(1, 2, 3), "Jude");
			p = new Pattern(new ExpressionVariable("e.1"));
			TestPatternMatching(ex, p);

			ex = new PassiveExpression("Hey", 1, 3, new PassiveExpression(1, 2, 3), "Jude", "Hey");
			p = new Pattern(new SymbolVariable("s.1"), new ExpressionVariable("e.1"), new SymbolVariable("s.1"));
			TestPatternMatching(ex, p);

//			Console.ReadLine();
		}

		static void TestPatternMatching(PassiveExpression ex, Pattern p)
		{
			Console.WriteLine("--------------------");

			if (Match(ex, p))
			{
				foreach (string name in p.Variables.Keys)
				{
					Console.WriteLine("{0} = {1}", name, p.GetVariable(name));
				}
			}
			else
			{
				Console.WriteLine("Expression don't match the pattern");
			}
		}*/
	}
}