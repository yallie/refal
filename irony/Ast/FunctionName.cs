using System;
using Irony.Ast;
using Irony.Parsing;
using System.Collections.Generic;
using Irony.Interpreter;
using Refal.Runtime;

namespace Refal
{
	/// <summary>
	/// FunctionName is helper node used internally
	/// It is not used in the AST
	/// </summary>
	public class FunctionName : SyntaxNode
	{
		public string Name { get; private set;}

		static IDictionary<string, string> Operations { get; set; }

		static FunctionName()
		{
			// setup arithmetic operations, see http://refal.ru/refer_r5.html#C
			Operations = new Dictionary<string, string>();
			Operations["+"] = "Add";
			Operations["-"] = "Sub";
			Operations["*"] = "Mul";
			Operations["/"] = "Div";
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
					Name = Operations[node.Term.Name];
				}
			}
		}

		public override System.Collections.IEnumerable GetChildNodes()
		{
			yield break;
		}

		public override void Evaluate(EvaluationContext context, AstMode mode)
		{
			// nothing
		}
	}
}
