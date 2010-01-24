using System;
using Irony.Ast;
using Irony.Parsing;
using System.Collections.Generic;
using Irony.Interpreter;
using Refal.Runtime;

namespace Refal
{
	public class Expression : SyntaxNode
	{
		List<Term> terms = new List<Term>();

		public override void Init(ParsingContext context, ParseTreeNode parseNode)
		{
			base.Init(context, parseNode);

			foreach (ParseTreeNode node in parseNode.ChildNodes)
			{
				if (node.AstNode is Term)
					Terms.Add(node.AstNode as Term);
			}
		}

		public override System.Collections.IEnumerable GetChildNodes()
		{
			foreach (Term term in Terms)
				yield return term;
		}

		public IList<Term> Terms
		{
			get { return terms; }
		}

		public bool IsEmpty
		{
			get { return terms.Count == 0; }
		}

		public override string ToString()
		{
			return "expression";
		}

		public override void Evaluate(EvaluationContext context, AstMode mode)
		{
			// evaluate terms
			var initialCount = context.Data.Count;
			foreach (Term term in Terms)
				term.Evaluate(context, mode);

			// build passive expression from terms
			var args = new List<object>();
			while (context.Data.Count > initialCount)
				args.Add(context.Data.Pop());

			// build expression and push onto stack
			args.Reverse();
			context.Data.Push(PassiveExpression.Build(args.ToArray()));
		}
	}
}
