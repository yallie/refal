using System;
using System.Collections;

namespace Refal.Runtime
{
	/* Pattern is much like expression, but it can include free variables.
	   Patterns containing parentheses () presented via nested patterns.
	*/

	public class Pattern : PassiveExpression
	{
		public Pattern()
		{
		}

		public Pattern(params object[] terms) : base(terms)
		{
		}
	}

	public abstract class Variable
	{
		string name;

		public Variable()
		{
		}

		public Variable(string name)
		{
			this.name = name;
		}
	}

	public class SymbolVariable : Variable
	{
		object symbol = null;

		public SymbolVariable(string name) : base(name)
		{
		}

		public object Symbol
		{
			get { return symbol; }
			set { symbol = value; }
		}
	}

	public class TermVariable : Variable
	{
		// term is either a symbol or an expression in structure brackets
		object symbol = null;
		PassiveExpression expression;

		public TermVariable(string name) : base(name)
		{
		}

		public object Symbol
		{
			get { return symbol; }
			set { symbol = value; }
		}

		public PassiveExpression ExpressionInBrackets
		{
			get { return expression; }
			set { expression = value; }
		}
	}

	public class ExpressionVariable : Variable
	{
		PassiveExpression expression;

		public ExpressionVariable(string name) : base(name)
		{
		}

		public PassiveExpression Expression
		{
			get { return expression; }
			set { expression = value; }
		}
	}
}
