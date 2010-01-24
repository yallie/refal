using System;
using Irony.Ast;
using Irony.Parsing;
using System.Collections.Generic;
using Irony.Interpreter;
using Refal.Runtime;

namespace Refal
{
	public class IdentifierList : SyntaxNode
	{
		List<IdentifierNode> identifiers = new List<IdentifierNode>();

		public override void Init(ParsingContext context, ParseTreeNode parseNode)
		{
			base.Init(context, parseNode);

			foreach (ParseTreeNode node in parseNode.ChildNodes)
			{
				if (node.AstNode is IdentifierNode)
				{
					Identifiers.Add(node.AstNode as IdentifierNode);
				}
			}
		}

		public IList<IdentifierNode> Identifiers
		{
			get { return identifiers; }
		}

		public override System.Collections.IEnumerable GetChildNodes()
		{
			yield break; // never goes to the final AST
		}

		public override void Evaluate(EvaluationContext context, AstMode mode)
		{
			throw new NotImplementedException(); // never evaluates
		}
	}
}
