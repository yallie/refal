using System;
using System.Collections;

namespace Refal.Runtime
{
	class TestPatterns
	{
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
		}

		static bool Match(PassiveExpression expression, Pattern pattern)
		{
			bool match = true;
			Stack rollBackStack = new Stack();

			// assume that expression and pattern aren't empty
			int exIndex = 0, patIndex = 0;

			while (true)
			{
				while (match && patIndex < pattern.Count && exIndex < expression.Count)
				{
					object ex = expression[exIndex];
					object pat = pattern[patIndex];

					bool needRollback = false;

					// not a subexpression
					if (!(ex is PassiveExpression))
					{
						// simple match: symbol vs symbol
						if (!(pat is Pattern) && !(pat is Variable))
						{
							if (ex.Equals(pat))
							{
								exIndex++; patIndex++;
								continue;
							}
						}

						// symbol vs. symbol/term variable
						if (pat is SymbolVariable || pat is TermVariable)
						{
							Variable var = pat as Variable;

							if (patIndex == var.FirstOccurance)
							{
								var.Value = ex;
								exIndex++; patIndex++;
								continue;
							}
							else if (ex.Equals(var.Value))
							{
								exIndex++; patIndex++;
								continue;
							}
							else 
							{
								// symbol don't match the bound variable,
								// roll back to the last expression variable
								needRollback = true;
							}
						}

						// symbol vs. expression variable
						if (pat is ExpressionVariable)
						{
							ExpressionVariable var = pat as ExpressionVariable;

							if (patIndex == var.FirstOccurance)
							{
								if (var.Expression == null)
									var.Expression = new PassiveExpression();

								var.Expression.Add(ex);

								RollbackInfo info = new RollbackInfo();
								info.exIndex = exIndex + 1;
								info.patIndex = patIndex;
								rollBackStack.Push(info);

								// expression variable can eat the rest of expression
								exIndex++; patIndex++;

								continue;
							}
							else if (ex.Equals(var.Expression))
							{
								// probably can't go here as ex is symbol, but...?
								exIndex++; patIndex++;
								continue;
							}
							else
							{
								// expression don't match
								needRollback = true;
							}
						}
					
						// symbol vs. subpattern (don't match)
						if (pat is Pattern)
						{
							needRollback = true;
						}

						if (!needRollback)
						{
							// internal error: should never get here
							throw new Exception("Pattern contains unsupported kind " +
								"of object (trying to match symbol)");
						}
					}
					else // ex is a subexpression
					{
						// subexpression vs term variable
						if (pat is TermVariable)
						{
							Variable var = pat as Variable;

							if (patIndex == var.FirstOccurance)
							{
								(pat as TermVariable).Expression = ex as PassiveExpression;
								exIndex++; patIndex++;
								continue;
							}
							else if (ex.Equals(var.Value))
							{
								exIndex++; patIndex++;
								continue;
							}
							else
							{
								// expression don't match the bound variable
								needRollback = true;
							}
						}

						// subexpression vs. symbol variable (don't match)
						if (pat is SymbolVariable)
						{
							needRollback = true;
						}

						// subexpression vs. subpattern
						if (pat is Pattern)
						{
							PassiveExpression subexpression = ex as PassiveExpression;
							Pattern subpattern = pat as Pattern;
						
							if (Match(subexpression, subpattern))
							{
								exIndex++; patIndex++;
								continue;
							}
							else
							{
								// subexpression don't match the subpattern
								needRollback = true;
							}
						}

						// subexpression vs. expression variable
						if (pat is ExpressionVariable)
						{
							ExpressionVariable var = pat as ExpressionVariable;

							if (patIndex == var.FirstOccurance)
							{
								if (var.Expression == null)
									var.Expression = new PassiveExpression();

								var.Expression.Add(ex);

								RollbackInfo info = new RollbackInfo();
								info.exIndex = exIndex + 1;
								info.patIndex = patIndex;
								rollBackStack.Push(info);

								exIndex++; patIndex++;

								continue;
							}
							else if (ex.Equals(var.Expression))
							{
								exIndex++; patIndex++;
								continue;
							}
							else
							{
								// expression don't match
								needRollback = true;
							}
						}

						if (!needRollback)
						{
							// internal error: should never get here
							throw new Exception("Pattern contains unsupported kind " +
								"of object (trying to match subexpression)");
						}
					}

					// roll back to the last expression variable, if needed
					if (needRollback)
					{
						// can't roll back => matching failed
						if (rollBackStack.Count == 0)
						{
							match = false;
						}
						else
						{
							// restore state up to the last expression variable
							RollbackInfo info = (RollbackInfo)rollBackStack.Pop();
							exIndex = info.exIndex;
							patIndex = info.patIndex;
						}
					} // if (needRollback)

				} // while()

				if (match && patIndex >= pattern.Count && exIndex >= expression.Count)
					return true;

				// if can roll back, try once more
				if (rollBackStack.Count == 0)
					return false;
				else
				{
					// restore state up to the last expression variable and iterate once more!
					RollbackInfo info = (RollbackInfo)rollBackStack.Pop();
					exIndex = info.exIndex;
					patIndex = info.patIndex;
				}
			}
		}

		struct RollbackInfo
		{
			public int exIndex;
			public int patIndex; 
		}
	}
}