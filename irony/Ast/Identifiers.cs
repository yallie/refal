using System;
using System.Collections;
using Irony.Ast;
using Irony.Parsing;
using System.Collections.Generic;
using Irony.Interpreter;
using Refal.Runtime;

namespace Refal
{
	public class Identifier : CompoundSymbol
	{
		public Identifier(string value) : base(value)
		{
		}
	}

	public class TrueIdentifier : Identifier
	{
		public TrueIdentifier() : base("True")
		{
		}
	}

	public class FalseIdentifier : Identifier
	{
		public FalseIdentifier() : base("False")
		{
		}
	}
}
