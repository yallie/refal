using System;
using System.Collections;

namespace Refal.Runtime
{
	public class Pattern : CollectionBase
	{
		public Pattern()
		{
		}

		public Pattern(params object[] terms)
		{
			foreach (object term in terms)
				Add(term);
		}

		public object this[int index]
		{
			get { return List[index]; }
		}

		public int Add(object term)
		{
			if (term is char[])
			{
				int index = -1;

				foreach (char c in (char[])term)
					index = List.Add(c);

				return index;
			}
			else if (symbol != null)
				return List.Add(symbol);

			return -1;
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
		object 
	}

	public class TermVariable : Variable
	{
	}

	public class ExpressionVariable : Variable
	{
	}
}
