using System;
using System.Collections;
using Irony.Ast;
using Irony.Parsing;
using System.Collections.Generic;
using Irony.Interpreter;
using Refal.Runtime;

namespace Refal
{
	public class ExternalFunctionList : SyntaxNode
	{
		List<IdentifierNode> identifiers = new List<IdentifierNode>();

		public IList<IdentifierNode> Identifiers
		{
			get { return identifiers; }
		}

		public override void Init(ParsingContext context, ParseTreeNode parseNode)
		{
			base.Init(context, parseNode);

			foreach (ParseTreeNode node in parseNode.ChildNodes)
			{
				if (node.AstNode is IdentifierList)
				{
					foreach (IdentifierNode id in (node.AstNode as IdentifierList).Identifiers)
						Identifiers.Add(id);
				}
			}
		}

		public override IEnumerable GetChildNodes()
		{
			yield break; // never goes to the final AST
		}

		public override void Evaluate(EvaluationContext context, AstMode mode)
		{
			throw new NotImplementedException(); // never evaluates
		}
	}
}
