using System;
using System.Collections;
using Irony.Ast;
using Irony.Parsing;
using System.Collections.Generic;
using Irony.Interpreter;
using Refal.Runtime;

namespace Refal
{
	public class SymbolVariable : Variable
	{
		public override string Index
		{
			get { return base.Index; }
			set { base.Index = "s." + value; }
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
