using System;
using System.Collections;
using Irony.Ast;
using Irony.Parsing;
using System.Collections.Generic;
using Irony.Interpreter;
using Refal.Runtime;

namespace Refal
{
	public class Sentence : SyntaxNode
	{
		// pattern { conditions } = expression;
		// or pattern conditions block;
		Pattern pattern;
		Conditions conditions;
		Expression expression;

		public override void Init(ParsingContext context, ParseTreeNode parseNode)
		{
			base.Init(context, parseNode);
			
			foreach (ParseTreeNode node in parseNode.ChildNodes)
			{
				if (node.AstNode is Pattern)
				{
					pattern = node.AstNode as Pattern;
				}
				else if (node.AstNode is RSentence)
				{
					conditions = (node.AstNode as RSentence).Conditions;
					expression = (node.AstNode as RSentence).Expression;
				}
			}
		}

		public override IEnumerable GetChildNodes()
		{
			yield return Pattern;

			if (Conditions != null)
				yield return Conditions;

			if (Expression != null)
				yield return Expression;
		}

		public Pattern Pattern
		{
			get { return pattern; }
			set { pattern = value; }
		}

		public Conditions Conditions
		{
			get { return conditions; }
		}

		public Expression Expression
		{
			get { return expression; }
		}

		public override string ToString()
		{
			return "match";
		}

		public Runtime.Pattern BlockPattern { get; set; }

		public override void Evaluate(EvaluationContext context, AstMode mode)
		{
			// evaluate pattern and copy bound variables of the current block
			var patt = Pattern.Instantiate(context, mode);
			if (BlockPattern != null)
			{
				patt.CopyBoundVariables(BlockPattern);
			}

			// pop expression from evaluation stack
			var expr = context.Data.Pop() as Runtime.PassiveExpression;

			// if pattern is recognized, calculate new expression and return true
			var result = patt.Match(expr);
			if (result)
			{
				// store last recognized pattern as a local variable
				context.CurrentFrame.Values[Pattern.LastPattern] = patt;

				// match succeeded, return expression
				if (Expression != null)
				{
					Expression.Evaluate(context, AstMode.Read);
					context.Data.Push(true);
					return;
				}

				// match succeeded? it depends on conditions
				if (Conditions != null)
				{
					Conditions.Evaluate(context, mode);

					// check if conditions succeeded
					result = Convert.ToBoolean(context.Data.Pop());
					if (result)
					{
						context.Data.Push(true);
						return;
					}
				}
			}

			// push expression back for the next sentence
			context.Data.Push(expr);
			context.Data.Push(false); // match failed
		}
	}
}
