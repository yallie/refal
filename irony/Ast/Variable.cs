using System;
using Irony.Ast;
using Irony.Parsing;
using System.Collections.Generic;
using Irony.Interpreter;
using Refal.Runtime;

namespace Refal
{
	/// <summary>
	/// Variable is a part of refal expression that can be bound to a value
	/// Being part of a pattern is not bound to a value and is called "free variable"
	/// In an expression to the right of "=" variable is bound to a value
	/// </summary>
	public abstract class Variable : Term
	{
		public virtual string Index { get; protected set; }

		public static void CreateVariableNode(ParsingContext context, ParseTreeNode parseNode)
		{
			Variable varNode = null;

			foreach (ParseTreeNode nt in parseNode.ChildNodes)
			{
				// (e | s | t)
				if (varNode == null)
				{
					foreach (ParseTreeNode node in nt.ChildNodes)
					{
						switch (node.Term.Name)
						{
							case "s":
								varNode = new SymbolVariable();
								break;

							case "e":
								varNode = new ExpressionVariable();
								break;

							case "t":
								varNode = new TermVariable();
								break;

							default:
								throw new ArgumentOutOfRangeException("Unknown variable type: " + node.Term.Name);
						}
					}
					continue;
				}

				if (nt.Term.Name == ".")
					continue;

				// Number | Identifier
				foreach (ParseTreeNode node in nt.ChildNodes)
				{
					if (node.AstNode is LiteralValueNode)
					{
						varNode.Index = (node.AstNode as LiteralValueNode).Value.ToString();
					}
					else if (node.AstNode is IdentifierNode)
					{
						varNode.Index = (node.AstNode as IdentifierNode).Symbol.Text;
					}
				}
			}

			varNode.Span = parseNode.Span;
			parseNode.AstNode = varNode;
		}

		public override void Evaluate(EvaluationContext context, AstMode mode)
		{
			// read variable from last recognized pattern
			if (mode == AstMode.Read)
			{
				if (context.CurrentFrame.GetLastPattern() == null)
					context.ThrowError("No pattern recognized");

				// push variable contents onto stack
				var pattern = context.CurrentFrame.GetLastPattern();
				context.Data.Push(pattern.GetVariable(Index));
			}
		}

		public override string ToString()
		{
			return Index;
		}
	}
}
