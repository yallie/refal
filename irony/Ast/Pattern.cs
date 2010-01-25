using System;
using System.Collections.Generic;
using Irony.Ast;
using Irony.Parsing;
using Irony.Interpreter;
using Refal.Runtime;

namespace Refal
{
	/// <summary>
	/// Pattern is a passive expression that may contain free variables
	/// </summary>
	public class Pattern : SyntaxNode
	{
		public const string LastPattern = "last-pattern";

		public IList<Term> Terms { get; private set; }

		public bool IsEmpty
		{
			get { return Terms.Count == 0; }
		}

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

		public override string ToString()
		{
			return "pattern";
		}
	}
}
