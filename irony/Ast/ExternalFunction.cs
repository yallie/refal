using System;
using System.Collections;
using Irony.Ast;
using Irony.Parsing;
using System.Collections.Generic;
using Irony.Interpreter;
using Refal.Runtime;

namespace Refal
{
	public class ExternalFunction : Function
	{
		public void SetSpan(SourceSpan sourceSpan)
		{
			span = sourceSpan;
		}
		
		public override IEnumerable GetChildNodes()
		{
			yield break;
		}

		public override string ToString()
		{
			return "extern " + Name;
		}

		public override void Call(EvaluationContext context)
		{
			context.ThrowError(this, "Calling external function is not supported");
		}
	}
}
