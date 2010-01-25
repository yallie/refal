using System;
using Irony.Ast;
using Irony.Parsing;
using System.Collections.Generic;
using Irony.Interpreter;
using Refal.Runtime;

namespace Refal
{
	/// <summary>
	/// Any identifier
	/// </summary>
	public class Identifier : CompoundSymbol
	{
		public Identifier(string value) : base(value)
		{
		}
	}

	/// <summary>
	/// Same as "True" compound symbol
	/// </summary>
	public class TrueIdentifier : Identifier
	{
		public TrueIdentifier() : base("True")
		{
		}
	}

	/// <summary>
	/// Same as "False" compound symbol
	/// </summary>
	public class FalseIdentifier : Identifier
	{
		public FalseIdentifier() : base("False")
		{
		}
	}
}
