using System;
using System.Collections;

namespace Refal.Runtime
{
	/* Passive expression is expression that don't contain
	   execution braces: <>

	   It is basically a collection of symbols (single characters,
	   strings - called 'compound characters' and treated as symbols,
	   macrodigits and identifiers, which can be thought of as a
	   special case of compound characters)
	*/

	public class PassiveExpression : CollectionBase
	{
		public PassiveExpression()
		{
		}

		public PassiveExpression(params object[] symbols)
		{
			foreach (object symbol in symbols)
				Add(symbol);
		}

		public object this[int index]
		{
			get { return List[index]; }
		}

		public int Add(object symbol)
		{
			if (symbol is char[])
			{
				int index = -1;

				foreach (char c in (char[])symbol)
					index = List.Add(c);

				return index;
			}
			else if (symbol != null)
				return List.Add(symbol);

			return -1;
		}

		public bool IsEmpty
		{
			get { return List.Count == 0; }
		}

/*		public void Print()
		{
			foreach (object value in this)
			{
				Console.WriteLine("-> {0}", value);
			}
		}

		static void Main()
		{
			PassiveExpression ex = new PassiveExpression("Hello".ToCharArray(), 1, 2, 3, "World");
			ex.Print();
		}
*/
	}
}
