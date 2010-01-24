using System;
using System.Collections;
using Irony.Ast;
using Irony.Parsing;
using System.Collections.Generic;
using Irony.Interpreter;
using Refal.Runtime;

namespace Refal
{
	public class FunctionCall : Term
	{
		string functionName;
		Expression expression;
		SourceSpan? nameSpan;

		public override void Init(ParsingContext context, ParseTreeNode parseNode)
		{
			base.Init(context, parseNode);
			
			foreach (ParseTreeNode node in parseNode.ChildNodes)
			{
				if (node.AstNode is FunctionName)
				{
					var fn = node.AstNode as FunctionName;
					FunctionName = fn.Name;
					nameSpan = fn.Span;
				}
				else if (node.AstNode is Expression)
				{
					Expression = (node.AstNode as Expression);
				}
			}
		}

		public override IEnumerable GetChildNodes()
		{
			return Expression.GetChildNodes();
		}

		public string FunctionName
		{
			get { return functionName; }
			set { functionName = value.Replace("-", "__"); }
		}

		public Expression Expression
		{
			get { return expression; }
			set { expression = value; }
		}

		public override string ToString()
		{
			return "call " + FunctionName;
		}

		public override void Evaluate(EvaluationContext context, AstMode mode)
		{
			Expression.Evaluate(context, mode);

			object value;
			if (context.TryGetValue(FunctionName, out value))
			{
				ICallTarget function = value as ICallTarget;
				if (function == null)
					context.ThrowError(this, "This identifier cannot be called: {0}", FunctionName);

				function.Call(context);
				return;
			}

			context.ThrowError(this, "Unknown identifier: {0}", FunctionName);
		}

		public override SourceLocation GetErrorAnchor()
		{
			return (nameSpan != null ? nameSpan.Value : Span).Location;
		}
	}
}
