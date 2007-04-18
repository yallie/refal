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

			return base.Add(symbol);
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

		public void BindVariables(Pattern pattern)
		{
			foreach (string name in variables.Keys)
			{
				if (pattern.Variables.Contains(name))
				{
					Variable var = (Variable)variables[name];
					var.Value = pattern.GetVariable(name);
					// first occurance of the variable is in another pattern
					var.FirstOccurance = -1;
				}
			}
		}
	}

	public abstract class Variable
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
	}
}
