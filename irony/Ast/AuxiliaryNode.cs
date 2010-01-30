using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Irony.Interpreter;
using Irony.Ast;

namespace Refal
{
	/// <summary>
	/// Base class for temporary AST nodes
	/// Temporary AST nodes are used internally
	/// They don't appear in the final AST
	/// </summary>
	public abstract class AuxiliaryNode : AstNode
	{
		public override System.Collections.IEnumerable GetChildNodes()
		{
			throw new NotImplementedException("Auxiliary nodes should not appear in the final AST");
		}

		public override void EvaluateNode(EvaluationContext context, AstMode mode)
		{
			throw new NotImplementedException("Auxiliary node cannot be interpreted");
		}
	}
}
