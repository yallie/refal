using System;
using System.Collections;
using Irony.Ast;
using Irony.Parsing;
using System.Collections.Generic;
using Irony.Interpreter;
using Refal.Runtime;

namespace Refal
{
	public abstract class Variable : Term
	{
		string index;
		bool isBound = false;

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
						varNode.Index = (node.AstNode as IdentifierNode).Symbol;
					}
				}
			}

			varNode.span = parseNode.Span;
			parseNode.AstNode = varNode;
		}

		public virtual string Index
		{
			get { return index; }
			set { index = value; }
		}

		public bool IsBound
		{
			get { return isBound; }
			set { isBound = value; }
		}

		public override string ToString()
		{
			return Index;
		}

		public override void Evaluate(EvaluationContext context, AstMode mode)
		{
			// read variable from last recognized pattern
			if (mode == AstMode.Read)
			{
				if (context.CurrentFrame.Values[Pattern.LastPattern] == null)
					context.ThrowError(this, "No pattern recognized");

				// push variable contents onto stack
				var pattern = (Runtime.Pattern)context.CurrentFrame.Values[Pattern.LastPattern];
				context.Data.Push(pattern.GetVariable(Index));
			}
		}
	}
}
