using System;
using Irony.Ast;
using Irony.Parsing;
using System.Collections.Generic;
using Irony.Interpreter;
using Refal.Runtime;

namespace Refal
{
	/// <summary>
	/// Variable of form e.X that can be bound to any expression
	/// </summary>
	public class ExpressionVariable : Variable
	{
		public override string Index
		{
			get { return base.Index; }
			protected set { base.Index = "e." + value; }
		}

		public override void Evaluate(EvaluationContext context, AstMode mode)
		{
			if (mode == AstMode.Read)
			{
				base.Evaluate(context, mode);
				return;
			}

			context.Data.Push(new Runtime.ExpressionVariable(Index));
		}
	}
}
