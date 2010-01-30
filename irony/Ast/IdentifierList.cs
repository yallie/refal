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
	public class IdentifierList : AuxiliaryNode
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
	}
}
