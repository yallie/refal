using System;
using Irony.Ast;
using Irony.Parsing;
using System.Collections.Generic;
using Irony.Interpreter;
using Refal.Runtime;

namespace Refal
{
	/// <summary>
	/// Base node for all functions
	/// </summary>
	public abstract class Function : SyntaxNode, ICallTarget
	{
		protected string _name = "";

		public string Name
		{
			get { return _name; }
			set { _name = value.Replace("-", "__"); }
		}

		public override void Evaluate(EvaluationContext context, AstMode mode)
		{
			// define function
			context.SetValue(Name, this);
		}

		public abstract void Call(EvaluationContext context);
	}
}
