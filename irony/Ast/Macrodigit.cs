using System;
using System.Collections;
using Irony.Ast;
using Irony.Parsing;
using System.Collections.Generic;
using Irony.Interpreter;
using Refal.Runtime;

namespace Refal
{
	public class Macrodigit : Symb
	{
		int digitValue;

		public Macrodigit()
		{
		}

		public Macrodigit(int value)
		{
			digitValue = value;
		}

		public int Value
		{
			get { return digitValue; }
			set { digitValue = value; }
		}

		public override string ToString()
		{
			return Value.ToString();
		}

		public override void Evaluate(EvaluationContext context, AstMode mode)
		{
			context.Data.Push(Value);
		}
	}
}
