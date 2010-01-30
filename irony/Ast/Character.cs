using System;
using Irony.Ast;
using Irony.Parsing;
using System.Collections.Generic;
using Irony.Interpreter;
using Refal.Runtime;

namespace Refal
{
	/// <summary>
	/// Character is enclosed in single quotes: 'sample'
	/// It is treated as a sequence of symbols
	/// </summary>
	public class Character : Symb
	{
		public string Value { get; set; }

		public Character(string value)
		{
			Value = value;
		}

		public override void EvaluateNode(EvaluationContext context, AstMode mode)
		{
			context.Data.Push(Value.ToCharArray());
		}

		public override string ToString()
		{
			return string.Format("'{0}'", Value);
		}
	}
}
