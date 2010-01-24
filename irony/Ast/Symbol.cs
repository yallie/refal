using System;
using System.Collections;
using Irony.Ast;
using Irony.Parsing;
using System.Collections.Generic;
using Irony.Interpreter;
using Refal.Runtime;

namespace Refal
{
	public abstract class Symb : Term
	{
		public static void CreateSymbolNode(ParsingContext context, ParseTreeNode parseNode)
		{
			Symb symNode = null;

			foreach (ParseTreeNode node in parseNode.ChildNodes)
			{
				if (node.AstNode is LiteralValueNode)
				{
					LiteralValueNode lv = node.AstNode as LiteralValueNode;

					switch (node.Term.Name)
					{
						case "Number":
							symNode = new Macrodigit(Convert.ToInt32(lv.Value));
							break;

						case "Char":
							symNode = new Character(lv.Value.ToString());
							break;

						case "String":
							symNode = new CompoundSymbol(lv.Value.ToString());
							break;

						default:
							throw new ArgumentOutOfRangeException("Unknown liveral value: " + node.Term.Name);
					}
				}
				else if (node.AstNode is IdentifierNode)
				{
					symNode = new Identifier((node.AstNode as IdentifierNode).Symbol);
				}
				else
				{
					switch (node.Term.Name)
					{
						case "True":
							symNode = new TrueIdentifier();
							break;

						case "False":
							symNode = new FalseIdentifier();
							break;

						default:
							throw new ArgumentOutOfRangeException("Unknown keyword: " + node.Term.Name);
					}
				}
			}

			symNode.span = parseNode.Span;
			parseNode.AstNode = symNode;
		}
	}
}
