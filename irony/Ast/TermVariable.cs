using System;
using Irony.Ast;
using Irony.Parsing;
using System.Collections.Generic;
using Irony.Interpreter;
using Refal.Runtime;

namespace Refal
{
	/// <summary>
	/// Variable of form t.X that can be bound either to a symbol or to expression in a structure braces
	/// </summary>
	public class TermVariable : Variable
	{
		public override string Index
		{
			get { return base.Index; }
			protected set { base.Index = "t." + value; }
		}

		public override void EvaluateNode(EvaluationContext context, AstMode mode)
		{
			if (mode == AstMode.Read)
			{
				base.EvaluateNode(context, mode);
				return;
			}

			context.Data.Push(new Runtime.TermVariable(Index));
		}
	}
}
