using System;
using System.Collections;
using Irony.Ast;
using Irony.Parsing;
using System.Collections.Generic;
using Irony.Interpreter;
using Refal.Runtime;

namespace Refal
{
	public class Block : SyntaxNode
	{
		List<Sentence> sentences = new List<Sentence>();

		public override void Init(ParsingContext context, ParseTreeNode parseNode)
		{
			base.Init(context, parseNode);

			foreach (ParseTreeNode node in parseNode.ChildNodes)
			{
				// copy sentences to block
				if (node.AstNode is SentenceList)
				{
					SentenceList sl = node.AstNode as SentenceList;
					foreach (Sentence s in sl.Sentences)
						Sentences.Add(s);
				}
			}
		}

		public override IEnumerable GetChildNodes()
		{
			foreach (Sentence s in Sentences)
				yield return s;
		}

		public IList<Sentence> Sentences
		{
			get { return sentences; }
		}

		public Runtime.Pattern BlockPattern { get; set; }

		public override void Evaluate(EvaluationContext context, AstMode mode)
		{
			foreach (Sentence sentence in Sentences)
			{
				sentence.BlockPattern = BlockPattern;
				sentence.Evaluate(context, mode);

				// if some sentence is evaluated to true, then stop
				var result = context.Data.Pop();
				if (Convert.ToBoolean(result) == true)
					return;
			}

			context.ThrowError("Recognition impossible");
		}

		public override string ToString()
		{
			return "block";
		}
	}
}
