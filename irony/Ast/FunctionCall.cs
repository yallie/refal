using System;
using Irony.Ast;
using Irony.Parsing;
using System.Collections.Generic;
using Irony.Interpreter;
using Refal.Runtime;

namespace Refal
{
	/// <summary>
	/// Function call
	/// </summary>
	public class FunctionCall : Term
	{
		string _functionName;

		public string FunctionName
		{
			get { return _functionName; }
			private set { _functionName = value.Replace("-", "__"); }
		}

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

		public override void Evaluate(EvaluationContext context, AstMode mode)
		{
			Expression.Evaluate(context, mode);

			object value;
			if (context.TryGetValue(FunctionName, out value))
			{
				ICallTarget function = value as ICallTarget;
				if (function == null)
					context.ThrowError(this, "This identifier cannot be called: {0}", FunctionName);

				try
				{
					function.Call(context);
					return;
				}
				catch (Exception ex)
				{
					context.ThrowError(this, ex.Message);
				}
			}

			context.ThrowError(this, "Unknown identifier: {0}", FunctionName);
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
