using System;
using Irony.Ast;
using Irony.Parsing;
using System.Collections.Generic;
using Irony.Interpreter;
using Refal.Runtime;

namespace Refal
{
	/// <summary>
	/// ExtenalFunctionList is a helper AST Node used to build list of ExternalFunctions
	/// It is not used in the final AST
	/// </summary>
	public class ExternalFunctionList : AuxiliaryNode
	{
		public IList<IdentifierNode> Identifiers { get; private set; }

		public ExternalFunctionList()
		{
			Identifiers = new List<IdentifierNode>();
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
	}
}
