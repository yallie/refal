using System;
using Irony.Ast;
using Irony.Parsing;
using System.Collections.Generic;
using Irony.Interpreter;
using Refal.Runtime;

namespace Refal
{
	/// <summary>
	/// Pattern enclosed in structure braces ()
	/// </summary>
	public class PatternInParentheses : AstNode
	{
		public Pattern Pattern { get; private set; }

		public override void Init(ParsingContext context, ParseTreeNode parseNode)
		{
			base.Init(context, parseNode);

			foreach (var node in parseNode.ChildNodes)
			{
				if (node.AstNode is Pattern)
					Pattern = (node.AstNode as Pattern);
			}
		}

		public override System.Collections.IEnumerable GetChildNodes()
		{
			return Pattern.GetChildNodes();
		}

		public override void EvaluateNode(EvaluationContext context, AstMode mode)
		{
			context.Data.Push(new OpeningBrace());
			Pattern.Evaluate(context, mode);
			context.Data.Push(new ClosingBrace());
		}

		public override string ToString()
		{
			return "(pattern)";
		}
	}
}
