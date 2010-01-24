using System;
using Irony.Ast;
using Irony.Parsing;
using System.Collections.Generic;
using Irony.Interpreter;
using Refal.Runtime;

namespace Refal
{
	public abstract class SyntaxNode : IAstNodeInit, IBrowsableAstNode, IInterpretedAstNode
	{
		protected SourceSpan span;

		public virtual void Init(ParsingContext context, ParseTreeNode parseNode)
		{
			span = parseNode.Span;
		}

		public SourceSpan Span
		{
			get { return span; }
		}

		public SourceLocation Location
		{
			get { return span.Location;}
		}

		public abstract System.Collections.IEnumerable GetChildNodes();

		public virtual SourceLocation GetErrorAnchor()
		{
			return span.Location;
		}

		public abstract void Evaluate(EvaluationContext context, AstMode mode);

		protected object[] EvaluateTerms(EvaluationContext context, AstMode mode)
		{
			// save initial stack position
			var initialCount = context.Data.Count;
			Evaluate(context, mode);

			// get terms from evaluation stack
			var args = new List<object>();
			while (context.Data.Count > initialCount)
				args.Add(context.Data.Pop());

			// restore original order
			args.Reverse();
			return args.ToArray();
		}
	}
}
