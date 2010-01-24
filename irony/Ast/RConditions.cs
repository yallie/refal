using System;
using Irony.Ast;
using Irony.Parsing;
using System.Collections.Generic;
using Irony.Interpreter;
using Refal.Runtime;

namespace Refal
{
	public class RConditions : SyntaxNode
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
				if (node.AstNode is Block)
				{
					Block = (node.AstNode as Block);
				}
				else if (node.AstNode == null && node.ChildNodes.Count > 0)
				{
					foreach (ParseTreeNode n in node.ChildNodes)
					{
						if (n.AstNode is Expression)
						{
							ResultExpression = (n.AstNode as Expression);
						}
						else if (n.AstNode is Conditions)
						{
							MoreConditions = (n.AstNode as Conditions);
						}
					}
				}
			}
		}

		public override System.Collections.IEnumerable GetChildNodes()
		{
			yield break; // this node never goes to final AST
		}

		public override void Evaluate(EvaluationContext context, AstMode mode)
		{
			// do nothing
		}
	}
}
