using System;
using Irony.Ast;
using Irony.Parsing;
using System.Collections.Generic;
using Irony.Interpreter;
using Refal.Runtime;

namespace Refal
{
	using IronySymbol = Irony.Parsing.Symbol;

	/// <summary>
	/// Function call
	/// </summary>
	public class FunctionCall : Term
	{
		public IronySymbol FunctionName { get; private set; } // TODO: value.Replace("-", "__") on set

		public Expression Expression { get; private set; }

		private SourceSpan? NameSpan { get; set; }

		public override void Init(ParsingContext context, ParseTreeNode parseNode)
		{
			base.Init(context, parseNode);

			foreach (ParseTreeNode node in parseNode.ChildNodes)
			{
				if (node.AstNode is FunctionName)
				{
					var fn = node.AstNode as FunctionName;
					FunctionName = fn.Name;
					NameSpan = fn.Span;
				}
				else if (node.AstNode is Expression)
				{
					Expression = (node.AstNode as Expression);
				}
			}
		}

		public override System.Collections.IEnumerable GetChildNodes()
		{
			return Expression.GetChildNodes();
		}

		public override void EvaluateNode(EvaluationContext context, AstMode mode)
		{
			Expression.Evaluate(context, mode);

			object value;
			if (context.TryGetValue(FunctionName, out value))
			{
				ICallTarget function = value as ICallTarget;
				if (function == null)
					context.ThrowError("This identifier cannot be called: {0}", FunctionName);

				function.Call(context);
				return;
			}

			context.ThrowError("Unknown identifier: {0}", FunctionName.Text);
		}

		public override SourceLocation GetErrorAnchor()
		{
			return (NameSpan != null ? NameSpan.Value : Span).Location;
		}

		public override string ToString()
		{
			return "call " + FunctionName;
		}
	}
}
