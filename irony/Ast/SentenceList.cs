using System;
using System.Collections;
using Irony.Ast;
using Irony.Parsing;
using System.Collections.Generic;
using Irony.Interpreter;
using Refal.Runtime;

namespace Refal
{
	public class SentenceList : SyntaxNode
	{
		List<Sentence> sentences = new List<Sentence>();

		public override void Init(ParsingContext context, ParseTreeNode parseNode)
		{
			base.Init(context, parseNode);
			
			foreach (ParseTreeNode node in parseNode.ChildNodes)
			{
				if (node.AstNode is Sentence)
					Sentences.Add(node.AstNode as Sentence);
			}
		}

		public override IEnumerable GetChildNodes()
		{
			throw new NotImplementedException();
		}
		
		public IList<Sentence> Sentences
		{
			get { return sentences; }
		}

		public override void Evaluate(EvaluationContext context, AstMode mode)
		{
			throw new NotImplementedException(); // never evaluates
		}
	}
}
