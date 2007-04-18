using System;
using System.Collections;

namespace Refal.Runtime
{
	/* Pattern is much like expression, but it can include free variables.
	   Patterns containing parentheses () presented via nested patterns.
	*/

	public class Pattern : PassiveExpression
	{
		IDictionary variables = new Hashtable();

		public Pattern()
		{
		}

		public Pattern(params object[] terms) : base(terms)
		{
		}

		public override int Add(object symbol)
		{
			// handle variables in a special way
			if (symbol is Variable)
			{
				// don't add duplicate variables, add same instances instead
				Variable variable = (Variable)symbol;
				if (variables.Contains(variable.Name))
					return base.Add(variables[variable.Name]);

				// index variables by names
				variables[variable.Name] = variable;
				int index = base.Add(variable);
				variable.FirstOccurance = index;
				return index;
			}

			// don't wrap pattern item
			if (symbol is PatternItem)
				return base.Add(symbol);

			// decompose char[] into chars, wrap each char as symbol
			if (symbol is char[])
			{
				int index = -1;
				foreach (char c in (char[])symbol)
					index = List.Add(new Symbol(c));
				return index;
			}

			// wrap any other object as symbol pattern
			return base.Add(new Symbol(symbol));
		}

		public IDictionary Variables
		{
			get { return variables; }
		}

		public object GetVariable(string name)
		{
			if (variables[name] == null)
				return null;

			return ((Variable)variables[name]).Value;
		}

		public void CopyBoundVariables(Pattern pattern)
		{
			foreach (string name in pattern.Variables.Keys)
			{
				if (variables.Contains(name))
				{
					Variable var = (Variable)variables[name];
					var.Value = pattern.GetVariable(name);
					// first occurance of the variable is in another pattern
					var.FirstOccurance = -1;
				}
				else
				{
					// copy bound variable from another pattern
					Variable var = (Variable)pattern.Variables[name];
					variables[name] = var;
					var.FirstOccurance = -1;
				}
			}
		}
	}

	public enum MatchResult
	{
		Failure, // expression item don't match pattern item
		Success, // matches unambiguously
		PartialSuccess // matches ambiguously (can find another match)
	}

	public abstract class PatternItem
	{
		public PatternItem()
		{
		}

		// match expression at exIndex pointer, advance pointer as needed
		public abstract MatchResult Match(PassiveExpression expression, ref int exIndex, int patIndex);
	}

	public class Symbol : PatternItem
	{
		object value = null;

		public Symbol(object value)
		{
			this.value = value;
		}

		public object Value
		{
			get { return value; }
			set { this.value = value; }
		}

		public override MatchResult Match(PassiveExpression expression, ref int exIndex, int patIndex)
		{
			// symbol matches single symbol
			if (Value.Equals(expression[exIndex]))
			{
				exIndex++;
				return MatchResult.Success;
			}

			return MatchResult.Failure;
		}
	}

	public abstract class StructureBrace : PatternItem
	{
		public StructureBrace()
		{
		}

		public override MatchResult Match(PassiveExpression expression, ref int exIndex, int patIndex)
		{
			// opening brace <=> opening brace, closing brace <=> closing brace
			if (expression[exIndex].GetType() == GetType())
			{
				exIndex++;
				return MatchResult.Success;
			}

			return MatchResult.Failure;
		}
	}

	public class OpeningBrace : StructureBrace
	{
		public OpeningBrace()
		{
		}
	}

	public class ClosingBrace : StructureBrace
	{
		public ClosingBrace()
		{
		}
	}

	public abstract class Variable : PatternItem
	{
		string name;
		int firstOccurance = -1;
		object boundValue = null;

		public Variable()
		{
		}

		public Variable(string name)
		{
			this.name = name;
		}

		public string Name
		{
			get { return name; }
			set { name = value; }
		}

		public int FirstOccurance
		{
			get { return firstOccurance; }
			set { firstOccurance = value; }
		}

		public object Value
		{
			get { return boundValue; }
			set { boundValue = value; }
		}

		public override MatchResult Match(PassiveExpression expression, ref int exIndex, int patIndex)
		{
			if (patIndex == firstOccurance)
				return MatchAny(expression, ref exIndex);

			return MatchSame(expression, ref exIndex);
		}

		protected abstract MatchResult MatchAny(PassiveExpression expression, ref int exIndex);

		protected abstract MatchResult MatchSame(PassiveExpression expression, ref int exIndex);
	}

	public class SymbolVariable : Variable
	{
		public SymbolVariable(string name) : base(name)
		{
		}

		public object Symbol
		{
			get { return base.Value; }
			set { base.Value = value; }
		}

		public override string ToString()
		{
			return string.Format("SymbolVariable {0}, value = {1}", Name, Value);
		}

		protected override MatchResult MatchAny(PassiveExpression expression, ref int exIndex)
		{
			object ex = expression[exIndex];

			// match anything except braces
			if (!(ex is StructureBrace))
			{
				this.Symbol = ex;
				exIndex++;
				return MatchResult.Success;
			}

			return MatchResult.Failure;
		}

		protected override MatchResult MatchSame(PassiveExpression expression, ref int exIndex)
		{
			object ex = expression[exIndex];

			// match the bound value
			if (Value.Equals(ex))
			{
				exIndex++;
				return MatchResult.Success;
			}

			return MatchResult.Failure;
		}
	}

	public class TermVariable : Variable
	{
		// term is either a symbol or an expression in structure brackets
		public TermVariable(string name) : base(name)
		{
		}

		// return value if it's not an expression
		public object Symbol
		{
			get { return (base.Value is PassiveExpression ? null : base.Value); }
			set { base.Value = value; }
		}

		// return value if it's passive expression
		public PassiveExpression Expression
		{
			get { return base.Value as PassiveExpression; }
			set { base.Value = value; }
		}

		public override string ToString()
		{
			return string.Format("TermVariable {0}, value = {1}", Name, Value);
		}

		protected override MatchResult MatchAny(PassiveExpression expression, ref int exIndex)
		{
			object ex = expression[exIndex];

			// match single symbol
			if (!(ex is StructureBrace))
			{
				this.Symbol = ex;
				exIndex++;
				return MatchResult.Success;
			}

			// match subexpression
			else if (ex is OpeningBrace)
			{
				this.Expression = new PassiveExpression();
				this.Expression.Add(ex);
				exIndex++;

				// extract subexpression within the structure braces
				int rank = 1;
				while (exIndex < expression.Count && rank > 0)
				{
					ex = expression[exIndex++];
					this.Expression.Add(ex);

					if (ex is OpeningBrace)
						rank++;
					else if (ex is ClosingBrace)
						rank--;
				}

				// subexpression with surrounding braces is extracted
				if (rank == 0)
					return MatchResult.Success;
			}

			// unmatched braces
			return MatchResult.Failure;
		}

		protected override MatchResult MatchSame(PassiveExpression expression, ref int exIndex)
		{
			// match same symbol
			if (this.Symbol != null)
			{
				object ex = expression[exIndex];

				// match the bound value
				if (Symbol.Equals(ex))
				{
					exIndex++;
					return MatchResult.Success;
				}
			}

			// match same subexpression
			else if (this.Expression != null)
			{
				if (expression.CompareToExpression(exIndex, this.Expression))
				{
					exIndex += this.Expression.Count;
					return MatchResult.Success;
				}
			}

			return MatchResult.Failure;
		}
	}

	public class ExpressionVariable : Variable
	{
		public ExpressionVariable(string name) : base(name)
		{
		}

		public PassiveExpression Expression
		{
			get { return base.Value as PassiveExpression; }
			set { base.Value = value; }
		}

		public override string ToString()
		{
			return string.Format("ExpressionVariable {0}, value = {1}", Name, Value);
		}

		protected override MatchResult MatchAny(PassiveExpression expression, ref int exIndex)
		{
			if (this.Expression == null)
			{
				// start with empty expression, don't advance exIndex
				this.Expression = new PassiveExpression();

				// save exIndex and patIndex for roll back operation
				return MatchResult.PartialSuccess;
			}
			else
			{
				// continue adding terms to expression
				object ex = expression[exIndex++];

				// add single symbol
				if (!(ex is StructureBrace))
				{
					this.Expression.Add(ex);

					// save exIndex + 1 and patIndex
					return MatchResult.PartialSuccess;
				}
				else if (ex is OpeningBrace)
				{
					// add subexpression
					this.Expression.Add(ex);

					// extract subexpression within the structure braces
					int rank = 1;
					while (exIndex < expression.Count && rank > 0)
					{
						ex = expression[exIndex++];
						this.Expression.Add(ex);

						if (ex is OpeningBrace)
							rank++;
						else if (ex is ClosingBrace)
							rank--;
					}

					// subexpression with surrounding braces is extracted
					if (rank == 0)
					{
						// save exIndex and patIndex
						return MatchResult.PartialSuccess;
					}
				}

				return MatchResult.Failure;
			}
		}

		protected override MatchResult MatchSame(PassiveExpression expression, ref int exIndex)
		{
			if (expression.CompareToExpression(exIndex, this.Expression))
			{
				exIndex += this.Expression.Count;
				return MatchResult.Success;
			}

			return MatchResult.Failure;
		}
	}
}
