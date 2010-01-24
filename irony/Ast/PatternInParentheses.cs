using System;
using Irony.Ast;
using Irony.Parsing;
using System.Collections.Generic;
using Irony.Interpreter;
using Refal.Runtime;

namespace Refal
{
	public class PatternInParentheses : Term
	{
		Pattern pattern;

		public override void Init(ParsingContext context, ParseTreeNode parseNode)
		{
			base.Init(context, parseNode);

			foreach (ParseTreeNode node in parseNode.ChildNodes)
			{
				if (node.AstNode is Pattern)
					pattern = (node.AstNode as Pattern);
			}
		}

		public override System.Collections.IEnumerable GetChildNodes()
		{
			return Pattern.GetChildNodes();
		}

		public Pattern Pattern
		{
			get { return pattern; }
		}

		public override string ToString()
		{
			return "(pattern)";
		}

		public override void Evaluate(EvaluationContext context, AstMode mode)
		{
			context.Data.Push(new OpeningBrace());
			Pattern.Evaluate(context, mode);
			context.Data.Push(new ClosingBrace());
		}
	}
}
