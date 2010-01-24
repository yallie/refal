using System;
using System.Collections;
using Irony.Ast;
using Irony.Parsing;
using System.Collections.Generic;
using Irony.Interpreter;
using Refal.Runtime;

namespace Refal
{
	public class DefinedFunction : Function
	{
		Block block;
		bool isPublic = false;

		public override void Init(ParsingContext context, ParseTreeNode parseNode)
		{
			base.Init(context, parseNode);

			foreach (ParseTreeNode node in parseNode.ChildNodes)
			{
				// it's better to rely on node type than node order
				if (node.AstNode is IdentifierNode)
				{
					Name = (node.AstNode as IdentifierNode).Symbol;
				}
				else if (node.AstNode is Block)
				{
					Block = (node.AstNode as Block);
				}
				else if (node.Term is KeyTerm && node.Term.Name == "$ENTRY")
				{
					IsPublic = true;
				}
			}
		}

		public override IEnumerable GetChildNodes()
		{
			return Block.GetChildNodes();
		}

		public Block Block
		{
			get { return block; }
			set { block = value; }
		}

		public bool IsPublic
		{
			get { return isPublic; }
			set { isPublic = value; }
		}

		public override string ToString()
		{
			return (IsPublic ? "public " : "private ") + Name;
		}

		public override void Call(EvaluationContext context)
		{
			context.PushFrame(Name, null, context.CurrentFrame); // AstNode argument
			Block.Evaluate(context, AstMode.None);
			context.PopFrame();
		}
	}
}
