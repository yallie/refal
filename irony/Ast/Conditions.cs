using System;
using Irony.Ast;
using Irony.Parsing;
using System.Collections.Generic;
using Irony.Interpreter;
using Refal.Runtime;

namespace Refal
{
	/// <summary>
	/// Where- and When-clauses
	/// </summary>
	public class Conditions : SyntaxNode
	{
		public Expression Expression { get; private set; }

		public Pattern Pattern { get; private set; }

		public Conditions MoreConditions { get; private set; }

		public Expression ResultExpression { get; private set; }

		public Block Block { get; private set; }

		public override void Init(ParsingContext context, ParseTreeNode parseNode)
		{
			base.Init(context, parseNode);

			foreach (ParseTreeNode node in parseNode.ChildNodes)
			{
				if (node.AstNode is Expression)
				{
					Expression = (node.AstNode as Expression);
				}
				else if (node.AstNode is RConditions)
				{
					var rc = node.AstNode as RConditions;
					Pattern = rc.Pattern;
					Block = rc.Block;
					MoreConditions = rc.MoreConditions;
					ResultExpression = rc.ResultExpression;
				}
			}
		}

		public override System.Collections.IEnumerable GetChildNodes()
		{
			yield return Expression;

			if (Block != null)
				yield return Block;

			if (Pattern != null)
				yield return Pattern;

			if (ResultExpression != null)
				yield return ResultExpression;

			if (MoreConditions != null)
				yield return MoreConditions;
		}

		public override void Evaluate(EvaluationContext context, AstMode mode)
		{
			// evaluate expression
			Expression.Evaluate(context, AstMode.Read);

			// extract last recognized pattern (it contains bound variables)
			var lastPattern = context.CurrentFrame.Values[Pattern.LastPattern] as Runtime.Pattern;
			if (lastPattern == null)
			{
				context.ThrowError("Internal error: last recognized pattern is lost");
			}

			// with-clause
			if (Block != null)
			{
				Block.BlockPattern = lastPattern;
				Block.Evaluate(context, mode);

				// with-clause is always successful
				context.Data.Push(true);
			}

			// where-clause
			if (Pattern != null)
			{
				EvaluateWhereClause(lastPattern, context, mode);
			}
		}

		void EvaluateWhereClause(Runtime.Pattern lastPattern, EvaluationContext context, AstMode mode)
		{
			// instantiate where-clause pattern
			var patt = Pattern.Instantiate(context, mode);
			patt.CopyBoundVariables(lastPattern);

			// perform matching
			var expr = context.Data.Pop() as Runtime.PassiveExpression;
			var result = patt.Match(expr);
			if (result)
			{
				// store last recognized pattern as a local variable
				context.CurrentFrame.Values[Pattern.LastPattern] = patt;

				// match succeeded, return true
				if (ResultExpression != null)
				{
					ResultExpression.Evaluate(context, AstMode.Read);
					context.Data.Push(true);
					return;
				}

				// match succeeded? depends on more conditions
				if (MoreConditions != null)
				{
					// return true or false
					MoreConditions.Evaluate(context, AstMode.Read);
					return;
				}
			}

			// match failed, return false
			context.Data.Push(false);
		}

		public override string ToString()
		{
			if (Block != null)
				return "with-clause";

			return "where-clause";
		}
	}
}
