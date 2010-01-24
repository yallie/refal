using System;
using Irony.Ast;
using Irony.Parsing;
using System.Collections.Generic;
using Irony.Interpreter;
using Refal.Runtime;

namespace Refal
{
	public class RSentence : SyntaxNode
	{
		Conditions conditions;
		Expression expression;
		
		public override void Init(ParsingContext context, ParseTreeNode parseNode)
		{
			base.Init(context, parseNode);
			
			foreach (ParseTreeNode node in parseNode.ChildNodes)
			{
				if (node.AstNode is Expression)
				{
					expression = node.AstNode as Expression;
				}
				else if (node.AstNode is Conditions)
				{
					conditions = node.AstNode as Conditions;
				}
			}
		}

		public Conditions Conditions
		{
			get { return conditions; }
		}

		public Expression Expression
		{
			get { return expression; }
		}

		public override System.Collections.IEnumerable GetChildNodes()
		{
			yield break; // never goes to the final AST
		}

		public override void Evaluate(EvaluationContext context, AstMode mode)
		{
			throw new NotImplementedException(); // never evaluates
		}
	}
}
