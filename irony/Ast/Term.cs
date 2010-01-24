using System;
using Irony.Ast;
using Irony.Parsing;
using System.Collections.Generic;
using Irony.Interpreter;
using Refal.Runtime;

namespace Refal
{
	public abstract class Term : SyntaxNode
	{
		public override void Init(ParsingContext context, ParseTreeNode parseNode)
		{
			base.Init(context, parseNode); // TODO
		}

		public override System.Collections.IEnumerable GetChildNodes()
		{
			yield break; // TODO
		}
	}
}
