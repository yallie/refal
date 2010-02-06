using System;
using Irony.Ast;
using Irony.Parsing;
using System.Collections.Generic;
using Irony.Interpreter;
using Refal.Runtime;

namespace Refal
{
	/// <summary>
	/// RConditions is a helper class used internally
	/// It is not used in the final AST
	/// </summary>
	public class RConditions : AuxiliaryNode
	{
		public Pattern Pattern { get; private set; }

		public Block Block { get; private set; }

		public Conditions MoreConditions { get; private set; }

		public Expression ResultExpression { get; private set; }

		public override void Init(ParsingContext context, ParseTreeNode parseNode)
		{
			base.Init(context, parseNode);

			foreach (ParseTreeNode node in parseNode.ChildNodes)
			{
				if (node.AstNode is Pattern)
				{
					Pattern = (node.AstNode as Pattern);
				}
				else if (node.AstNode is Block)
				{
					Block = (node.AstNode as Block);
				}
				else if (node.AstNode is Expression)
				{
					ResultExpression = (node.AstNode as Expression);
				}
				else if (node.AstNode is Conditions)
				{
					MoreConditions = (node.AstNode as Conditions);
				}
			}
		}
	}
}
