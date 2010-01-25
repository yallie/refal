using System;
using Irony.Ast;
using Irony.Parsing;
using System.Collections.Generic;
using Irony.Interpreter;
using Refal.Runtime;

namespace Refal
{
	/// <summary>
	/// Compound symbol is enclosed in double quotes: "a+b"
	/// It is treated as a single symbol
	/// </summary>
	public class CompoundSymbol : Symb
	{
		public string Value { get; set; }

		public CompoundSymbol(string value)
		{
			Value = value;
		}

		public override void Evaluate(EvaluationContext context, AstMode mode)
		{
			context.Data.Push(Value);
		}

		public override string ToString()
		{
			return string.Format("\"{0}\"", Value);
		}
	}
}
