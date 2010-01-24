// Refal5.NET runtime
// Written by Alexey Yakovlev <yallie@yandex.ru>
// http://refal.codeplex.com

using System;

namespace Refal.Runtime
{
	/// <summary>
	/// Pattern contains pattern items of three types: symbols, braces and variables.
	/// Pattern items can match given expression
	/// </summary>
	public abstract class PatternItem
	{
		public PatternItem()
		{
		}

		// match expression at exIndex pointer, advance pointer as needed
		public abstract MatchResult Match(PassiveExpression expression, ref int exIndex, int patIndex);
	}

	public enum MatchResult
	{
		Failure, // expression item don't match pattern item
		Success, // item matches unambiguously
		PartialSuccess // item matches ambiguously (can find another match)
	}

	public class Symbol : PatternItem
	{
		object value = null;

		public Symbol(object value)
		{
			this.value = value;
		}

		public object Value
		{
			get { return value; }
			set { this.value = value; }
		}

		public override MatchResult Match(PassiveExpression expression, ref int exIndex, int patIndex)
		{
			if (expression == null || exIndex >= expression.Count)
				return MatchResult.Failure;

			// symbol matches single symbol
			if (Value.Equals(expression[exIndex]))
			{
				exIndex++;
				return MatchResult.Success;
			}

			return MatchResult.Failure;
		}
	}

	public abstract class StructureBrace : PatternItem
	{
		public StructureBrace()
		{
		}

		public override MatchResult Match(PassiveExpression expression, ref int exIndex, int patIndex)
		{
			if (expression == null || exIndex >= expression.Count)
				return MatchResult.Failure;

			// opening brace <=> opening brace, closing brace <=> closing brace
			if (expression[exIndex].GetType() == GetType())
			{
				exIndex++;
				return MatchResult.Success;
			}

			return MatchResult.Failure;
		}
	}

	public class OpeningBrace : StructureBrace
	{
		public OpeningBrace()
		{
		}

		public override string ToString()
		{
			return "(";
		}
	}

	public class ClosingBrace : StructureBrace
	{
		public ClosingBrace()
		{
		}

		public override string ToString()
		{
			return ")";
		}
	}
}
