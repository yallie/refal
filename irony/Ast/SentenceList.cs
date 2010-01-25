using System;
using Irony.Ast;
using Irony.Parsing;
using System.Collections.Generic;
using Irony.Interpreter;
using Refal.Runtime;

namespace Refal
{
	/// <summary>
	/// SentenceList is a helper class used internally
	/// It is not used in the AST
	/// </summary>
	public class SentenceList : SyntaxNode
	{
		public IList<Sentence> Sentences { get; private set; }

		public SentenceList()
		{
			Sentences = new List<Sentence>();
		}

		public override void Init(ParsingContext context, ParseTreeNode parseNode)
		{
			base.Init(context, parseNode);
			
			foreach (ParseTreeNode node in parseNode.ChildNodes)
			{
				if (node.AstNode is Sentence)
					Sentences.Add(node.AstNode as Sentence);
			}
		}

		public override System.Collections.IEnumerable GetChildNodes()
		{
			throw new NotImplementedException();
		}

		public override void Evaluate(EvaluationContext context, AstMode mode)
		{
			throw new NotImplementedException(); // never evaluates
		}
	}
}
