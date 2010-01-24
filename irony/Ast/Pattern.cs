using System;
using System.Collections;
using Irony.Ast;
using Irony.Parsing;
using System.Collections.Generic;
using Irony.Interpreter;
using Refal.Runtime;

namespace Refal
{
	public class Pattern : SyntaxNode
	{
		public const string LastPattern = "last-pattern";
		List<Term> terms = new List<Term>();
		private Runtime.Pattern pattern;

		public override void Init(ParsingContext context, ParseTreeNode parseNode)
		{
			base.Init(context, parseNode);
			
			foreach (ParseTreeNode node in parseNode.ChildNodes)
			{
				if (node.AstNode is Term)
					Terms.Add(node.AstNode as Term);
			}
		}

		public override IEnumerable GetChildNodes()
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
			return "pattern";
		}

		public override void Evaluate(EvaluationContext context, AstMode mode)
		{
			foreach (Term term in Terms)
			{
				term.Evaluate(context, mode);
			}
		}

		public Runtime.Pattern Instantiate(EvaluationContext context, AstMode mode)
		{
			// evaluate pattern and instantiate Runtime.Pattern
			return new Runtime.Pattern(EvaluateTerms(context, mode));
		}
	}
}
