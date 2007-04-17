using System;
using System.Text;
using System.Collections;

namespace Refal
{
	public class FormatCodeVisitor : BaseCodeVisitor
	{
		StringBuilder sb = new StringBuilder();
		int indentLevel = 0;

		public FormatCodeVisitor()
		{
		}

		public string Text
		{
			get { return sb.ToString(); }
		}

		private void Indent(int level)
		{
			for (int i = 0; i < level; i++)
				sb.Append("\t");
		}

		public override void VisitProgram(Program program)
		{
			foreach (Function function in program.FunctionList)
			{
				function.Accept(this);
				sb.Append("\r\n\r\n");
			}
		}

		public override void VisitExternalFunction(ExternalFunction function)
		{
			sb.AppendFormat("$EXTRN {0};\r\n", function.Name);
		}

		public override void VisitDefinedFunction(DefinedFunction function)
		{
			sb.AppendFormat("{0}{1} ", function.IsPublic ? "$ENTRY " : "", function.Name);

			function.Block.Accept(this);
		}

		public override void VisitBlock(Block block)
		{
			// opening brace is on the same line, hence no indentation
			sb.Append("{\r\n");

			indentLevel++;
			foreach (Sentence sentence in block.Sentences)
			{
				sentence.Accept(this);
				sb.Append(";\r\n");
			}
			indentLevel--;

			Indent(indentLevel);
			sb.Append("}");
		}

		public override void VisitSentence(Sentence sentence)
		{
			Indent(indentLevel);

			sentence.Pattern.Accept(this);

			if (sentence.Conditions != null)
			{
				sentence.Conditions.Accept(this);
			}

			if (sentence.Expression != null)
			{
				sb.Append(" = ");
				sentence.Expression.Accept(this);
			}
		}

		public override void VisitPattern(Pattern pattern)
		{
			for (int i = 0; i < pattern.Terms.Count; i++)
			{
				Term term = pattern.Terms[i] as Term;
				term.Accept(this);

				if (i < pattern.Terms.Count - 1)
					sb.Append(" ");
			}
		}

		public override void VisitExpression(Expression expression)
		{
			for (int i = 0; i < expression.Terms.Count; i++)
			{
				Term term = expression.Terms[i] as Term;
				term.Accept(this);

				if (i < expression.Terms.Count - 1)
					sb.Append(" ");
			}
		}

		public override void VisitFunctionCall(FunctionCall functionCall)
		{
			sb.AppendFormat("<{0} ", functionCall.FunctionName);
			functionCall.Expression.Accept(this);
			sb.Append(">");
		}

		public override void VisitCharacter(Character character)
		{
			sb.Append(character.Value);
		}

		public override void VisitCompoundSymbol(CompoundSymbol compoundSymbol)
		{
			sb.Append(compoundSymbol.Value);
		}

		public override void VisitIdentifier(Identifier identifier)
		{
			sb.Append(identifier.Value);
		}

		public override void VisitTrueIdentifier(TrueIdentifier identifier)
		{
			sb.Append("True");
		}

		public override void VisitFalseIdentifier(FalseIdentifier falseIdentifier)
		{
			sb.Append("False");
		}

		public override void VisitSymbolVariable(SymbolVariable symbolVariable)
		{
			sb.Append("s.");
			sb.Append(symbolVariable.Index);
		}

		public override void VisitTermVariable(TermVariable termVariable)
		{
			sb.Append("t.");
			sb.Append(termVariable.Index);
		}

		public override void VisitExpressionVariable(ExpressionVariable expressionVariable)
		{
			sb.Append("e.");
			sb.Append(expressionVariable.Index);
		}

		public override void VisitExpressionInParentheses(ExpressionInParentheses expressionInParentheses)
		{
			sb.Append("(");
			expressionInParentheses.Expression.Accept(this);
			sb.Append(")");
		}

		public override void VisitPatternInParentheses(PatternInParentheses patternInParentheses)
		{
			sb.Append("(");
			patternInParentheses.Pattern.Accept(this);
			sb.Append(")");
		}

		public override void VisitMacrodigit(Macrodigit macrodigit)
		{
			sb.AppendFormat("{0}", macrodigit.Value);
		}

		public override void VisitConditions(Conditions conditions)
		{
			sb.Append(", ");
			conditions.Expression.Accept(this);
			sb.Append(": ");

			if (conditions.Block != null)
			{
				conditions.Block.Accept(this);
			}
			else if (conditions.MoreConditions != null)
			{
				conditions.MoreConditions.Accept(this);
			}
		}
	}
}
