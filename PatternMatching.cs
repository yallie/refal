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
		}

		static void TestPatternMatching(PassiveExpression ex, Pattern p)
		{
			Console.WriteLine("--------------------");

			if (Match(ex, p))
			{
				Console.WriteLine("s.1 = {0}", p.GetVariable("s.1"));
			}
			else
			{
				Console.WriteLine("Expression don't match the pattern");
			}
		}

		static bool Match(PassiveExpression expression, Pattern pattern)
		{
			bool match = true;

			// assume that expression and pattern aren't empty
			int exIndex = 0, patIndex = 0;

			while (patIndex < pattern.Count && exIndex < expression.Count)
			{
				object ex = expression[exIndex];
				object pat = pattern[patIndex];

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
						(pat as Variable).Value = ex;
						exIndex++; patIndex++;
						continue;
					}

					// TODO: symbol vs. subpattern
					// TODO: symbol vs. expression variable
				}
				else // ex is a subexpression
				{
					// subexpression vs term variable
					if (pat is TermVariable)
					{
						(pat as TermVariable).Expression = ex as PassiveExpression;
						exIndex++; patIndex++;
						continue;
					}

					// TODO: subexpression vs. symbol variable
					// TODO: subexpression vs. expression variable
					// TODO: subexpression vs. subpattern
				}
			}

			if (match && patIndex >= pattern.Count && exIndex >= expression.Count)
				return true;

			return false;
		}
	}
}