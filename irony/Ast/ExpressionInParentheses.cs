using System;
using Irony.Ast;
using Irony.Parsing;
using System.Collections.Generic;
using Irony.Interpreter;
using Refal.Runtime;

namespace Refal
{
	/// <summary>
	/// Expression in structure braces ()
	/// </summary>
	public class ExpressionInParentheses : AstNode
	{
		public Expression Expression { get; private set; }

		public override void Init(ParsingContext context, ParseTreeNode parseNode)
		{
			base.Init(context, parseNode);

			foreach (ParseTreeNode node in parseNode.ChildNodes)
			{
				if (node.AstNode is Expression)
					Expression = (node.AstNode as Expression);
			}
		}

		public override System.Collections.IEnumerable GetChildNodes()
		{
			return Expression.GetChildNodes();
		}

		public override void EvaluateNode(EvaluationContext context, AstMode mode)
		{
			context.Data.Push(new OpeningBrace());
			Expression.Evaluate(context, mode);
			context.Data.Push(new ClosingBrace());
		}

		public override string ToString()
		{
			return "(expression)";
		}
	}
}
