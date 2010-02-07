using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Irony.Ast;
using Irony.Parsing;

namespace Refal
{
	/// <summary>
	/// Initializes Refal literal nodes
	/// </summary>
	public static class LiteralValueNodeHelper
	{
		/// <summary>
		/// Converts identifiers to compound symbols (strings in double quotes),
		/// expands character strings (in single quotes) to arrays of characters
		/// </summary>
		public static void InitNode(ParsingContext context, ParseTreeNode parseNode)
		{
			LiteralValueNode literal = null;

			foreach (ParseTreeNode node in parseNode.ChildNodes)
			{
				if (node.AstNode is LiteralValueNode)
				{
					literal = node.AstNode as LiteralValueNode;
					if (node.Term.Name == "Char")
					{
						literal.Value = literal.Value.ToString().ToCharArray();
					}
				}
				else
				{
					literal = new LiteralValueNode()
					{
						Span = node.Span
					};

					if (node.AstNode is IdentifierNode)
					{
						var id = node.AstNode as IdentifierNode;
						literal.Value = id.Symbol.Text;
					}
					else
					{
						switch (node.Term.Name)
						{
							case "True":
							case "False":
								literal.Value = node.Term.Name;
								break;

							default:
								throw new ArgumentOutOfRangeException("Unknown keyword: " + node.Term.Name);
						}
					}
				}
			}

			parseNode.AstNode = literal;
		}
	}
}
