using System;
using Irony.Ast;
using Irony.Parsing;
using System.Collections.Generic;
using Irony.Interpreter;
using Refal.Runtime;

namespace Refal
{
	using IronySymbol = Irony.Parsing.Symbol;

	/// <summary>
	/// FunctionName is helper node used internally
	/// It is not used in the AST
	/// </summary>
	public class FunctionName : AuxiliaryNode
	{
		public IronySymbol Name { get; private set;}

		public override void Init(ParsingContext context, ParseTreeNode parseNode)
		{
			base.Init(context, parseNode);

			foreach (ParseTreeNode node in parseNode.ChildNodes)
			{
				if (node.AstNode is IdentifierNode)
				{
					Name = (node.AstNode as IdentifierNode).Symbol;
				}
				else if (node.Term is KeyTerm)
				{
					Name = context.Symbols.TextToSymbol(node.Term.Name);
				}
			}
		}
	}
}
