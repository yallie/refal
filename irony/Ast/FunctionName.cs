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

		private IDictionary<string, IronySymbol> GetOperationsTable(ParsingContext context)
		{
			var operations = new Dictionary<string, IronySymbol>();

			// register arithmetic operations, see http://refal.ru/refer_r5.html#C
			operations["+"] = context.Symbols.TextToSymbol("Add");
			operations["-"] = context.Symbols.TextToSymbol("Sub");
			operations["*"] = context.Symbols.TextToSymbol("Mul");
			operations["/"] = context.Symbols.TextToSymbol("Div");

			return operations;
		}

		public override void Init(ParsingContext context, ParseTreeNode parseNode)
		{
			base.Init(context, parseNode);

			foreach (ParseTreeNode node in parseNode.ChildNodes)
			{
				if (node.AstNode is IdentifierNode)
				{
					Name = (node.AstNode as IdentifierNode).Symbol;
				}
				else
				{
					// convert standard arithmetic operation name
					Name = GetOperationsTable(context)[node.Term.Name];
				}
			}
		}
	}
}
