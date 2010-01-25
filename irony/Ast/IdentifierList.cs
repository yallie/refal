using System;
using Irony.Ast;
using Irony.Parsing;
using System.Collections.Generic;
using Irony.Interpreter;
using Refal.Runtime;

namespace Refal
{
	/// <summary>
	/// IdentifierList is a helper node used internally
	/// It is not used in AST
	/// </summary>
	public class IdentifierList : SyntaxNode
	{
		public IList<IdentifierNode> Identifiers { get; private set; }

		public IdentifierList()
		{
			Identifiers = new List<IdentifierNode>();
		}

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
