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

		public new PatternItem this[int index]
		{
			get { return List[index] as PatternItem; }
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

		public void ClearBoundValues(int startFromIndex)
		{
			foreach (string name in Variables.Keys)
			{
				Variable var = (Variable)Variables[name];

				if (var.FirstOccurance > startFromIndex) // not >=!
					var.Value = null;
			}
		}
	}
}
