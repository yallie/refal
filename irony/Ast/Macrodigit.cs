using System;
using Irony.Ast;
using Irony.Parsing;
using System.Collections.Generic;
using Irony.Interpreter;
using Refal.Runtime;

namespace Refal
{
	/// <summary>
	/// Macrodigit is an integer number in the range of 0 to 2^32
	/// </summary>
	public class Macrodigit : Symb
	{
		public int Value { get; private set; }

		public Macrodigit(int value)
		{
			Value = value;
		}

		public override void EvaluateNode(EvaluationContext context, AstMode mode)
		{
			context.Data.Push(Value);
		}

		public override string ToString()
		{
			return Value.ToString();
		}
	}
}
