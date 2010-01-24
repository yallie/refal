using System;
using System.Collections;
using Irony.Ast;
using Irony.Parsing;
using System.Collections.Generic;
using Irony.Interpreter;
using Refal.Runtime;

namespace Refal
{
	public class ExpressionInParentheses : Term
	{
		Expression expression;

		public override void Init(ParsingContext context, ParseTreeNode parseNode)
		{
			base.Init(context, parseNode);

			foreach (ParseTreeNode node in parseNode.ChildNodes)
			{
				if (node.AstNode is Expression)
					expression = (node.AstNode as Expression);
			}
		}

		public override IEnumerable GetChildNodes()
		{
			return Expression.GetChildNodes();
		}

		public Expression Expression
		{
			get { return expression; }
		}

		public override string ToString()
		{
			return "(expression)";
		}

		public override void Evaluate(EvaluationContext context, AstMode mode)
		{
			context.Data.Push(new OpeningBrace());
			Expression.Evaluate(context, mode);
			context.Data.Push(new ClosingBrace());
		}
	}
}
