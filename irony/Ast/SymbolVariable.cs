using System;
using Irony.Ast;
using Irony.Parsing;
using System.Collections.Generic;
using Irony.Interpreter;
using Refal.Runtime;

namespace Refal
{
	/// <summary>
	/// Variable of form s.X that can be bound to single symbol
	/// </summary>
	public class SymbolVariable : Variable
	{
		public override string Index
		{
			get { return base.Index; }
			protected set { base.Index = "s." + value; }
		}

		public override void Evaluate(EvaluationContext context, AstMode mode)
		{
			if (mode == AstMode.Read)
			{
				base.Evaluate(context, mode);
				return;
			}

			context.Data.Push(new Runtime.SymbolVariable(Index));
		}
	}
}
