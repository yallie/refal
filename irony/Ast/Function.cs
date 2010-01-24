using System;
using Irony.Ast;
using Irony.Parsing;
using System.Collections.Generic;
using Irony.Interpreter;
using Refal.Runtime;

namespace Refal
{
	public abstract class Function : SyntaxNode, ICallTarget
	{
		protected string name = "";

		public string Name
		{
			get { return name; }
			set { name = value.Replace("-", "__"); }
		}

		public override void Evaluate(EvaluationContext context, AstMode mode)
		{
			// define function
			context.SetValue(Name, this);
		}

		public abstract void Call(EvaluationContext context);
	}
}
