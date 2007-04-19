
/*-------------------------------------------------------------------------*/
/*                                                                         */
/*      CSharpVisitor, transforms AST into C# code                         */
/*      This file is a part of Refal5.NET project                          */
/*      Project license: http://www.gnu.org/licenses/lgpl.html             */
/*      Written by Y [11-06-06] <yallie@yandex.ru>                         */
/*                                                                         */
/*      Copyright (c) 2006-2007 Alexey Yakovlev                            */
/*      All Rights Reserved                                                */
/*                                                                         */
/*-------------------------------------------------------------------------*/

using System;
using System.Text;
using System.Collections;

namespace Refal
{
	public class CSharpCodeVisitor : BaseCodeVisitor
	{
		StringBuilder sb = new StringBuilder();
		int indentLevel = 2;
		int currentPatternIndex = 1;

		string blockPattern = null;
		Stack blockPatterns = new Stack();

		public CSharpCodeVisitor()
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
			sb.AppendFormat(@"
using System;
using System.Collections;

namespace Refal.Runtime
{{
	public class {0} : RefalBase
	{{", program.Name);

			if (program.EntryPoint != null)
			{
				sb.AppendFormat(@"
		static void Main(string[] args)
		{{
			RefalBase.commandLineArguments = args;

			_{0}(new PassiveExpression());

			RefalBase.CloseFiles();
		}}" + "\r\n", program.EntryPoint.Name);
			}

			foreach (Function function in program.FunctionList)
			{
				sb.Append("\r\n");
				function.Accept(this);
				sb.Append("\r\n");
			}

			sb.Append("\t}\r\n}\r\n");
		}

		public override void VisitExternalFunction(ExternalFunction function)
		{
			// do nothing
		}

		public override void VisitDefinedFunction(DefinedFunction function)
		{
			Indent(indentLevel);
			sb.AppendFormat("{0} static PassiveExpression _{1}(PassiveExpression expression)\r\n",
				function.IsPublic ? "public" : "private", function.Name);
			Indent(indentLevel);

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
				sb.Append(";\r\n\r\n");
			}

			Indent(indentLevel);
			sb.Append("throw new RecognitionImpossibleException(\"Recognition impossible. " +
				"Last expression: \" + expression.ToString());\r\n");

			indentLevel--;
			Indent(indentLevel);
			sb.Append("}");
		}

		public override void VisitSentence(Sentence sentence)
		{
			// create pattern
			Indent(indentLevel);
			sb.AppendFormat("Pattern pattern{0} = new Pattern(", currentPatternIndex);
			sentence.Pattern.Accept(this);
			sb.Append(");\r\n");

			// copy variables from block pattern
			if (blockPattern != null)
			{
				Indent(indentLevel);
				sb.AppendFormat("pattern{0}.CopyBoundVariables({1});\r\n", currentPatternIndex, blockPattern);
			}

			// match inout expression
			Indent(indentLevel);
			sb.AppendFormat("if (pattern{0}.Match(expression))\r\n", currentPatternIndex);
			Indent(indentLevel);
			sb.Append("{\r\n");

			indentLevel++;

			// where-clause
			if (sentence.Conditions != null)
				sentence.Conditions.Accept(this);

			if (sentence.Expression != null)
			{
				Indent(indentLevel);
				sb.Append("return PassiveExpression.Build(");
				sentence.Expression.Accept(this);
				sb.Append(");\r\n");
			}

			indentLevel--;

			Indent(indentLevel);
			sb.Append("}");

			currentPatternIndex++;
		}

		public override void VisitConditions(Conditions conditions)
		{
			Indent(indentLevel);
			sb.Append("expression = PassiveExpression.Build(");
			conditions.Expression.Accept(this);
			sb.Append(");\r\n");

			currentPatternIndex++;

			// conditions may contain either block (with-clause) or pattern (where-clause)
			if (conditions.Block != null)
			{
				// set pattern containing bound variables for the current block
				if (blockPattern != null)
				{
					blockPatterns.Push(blockPattern);
				}
				blockPattern = string.Format("pattern{0}", currentPatternIndex - 1);

				// emit block
				Indent(indentLevel);
				conditions.Block.Accept(this);
				sb.Append("\r\n");

				// block is finished, restore parent block pattern
				blockPattern = null;
				if (blockPatterns.Count > 0)
				{
					blockPattern = (string)blockPatterns.Pop();
				}
			}

			// pattern (where-clause)
			if (conditions.Pattern != null)
			{
				Indent(indentLevel);
				sb.AppendFormat("Pattern pattern{0} = new Pattern(", currentPatternIndex);
				conditions.Pattern.Accept(this);
				sb.Append(");\r\n");

				Indent(indentLevel);
				sb.AppendFormat("pattern{0}.CopyBoundVariables(pattern{1});\r\n", currentPatternIndex, currentPatternIndex - 1);

				Indent(indentLevel);
				sb.AppendFormat("if (pattern{0}.Match(expression))\r\n", currentPatternIndex);
				Indent(indentLevel);
				sb.Append("{\r\n");
				indentLevel++;

				// more conditions
				if (conditions.MoreConditions != null)
					conditions.MoreConditions.Accept(this);

				// and resulting expression
				if (conditions.ResultExpression != null)
				{
					Indent(indentLevel);
					sb.Append("return PassiveExpression.Build(");
					conditions.ResultExpression.Accept(this);
					sb.Append(");\r\n");
				}

				indentLevel--;
				Indent(indentLevel);
				sb.Append("}\r\n");
			}
		}

		public override void VisitPattern(Pattern pattern)
		{
			for (int i = 0; i < pattern.Terms.Count; i++)
			{
				Term term = pattern.Terms[i] as Term;
				term.Accept(this);

				if (i < pattern.Terms.Count - 1)
					sb.Append(", ");
			}
		}

		public override void VisitExpression(Expression expression)
		{
			for (int i = 0; i < expression.Terms.Count; i++)
			{
				Term term = expression.Terms[i] as Term;
				term.Accept(this);

				if (i < expression.Terms.Count - 1)
					sb.Append(", ");
			}
		}

		public override void VisitFunctionCall(FunctionCall functionCall)
		{
			sb.AppendFormat("_{0}(PassiveExpression.Build(", functionCall.FunctionName);
			functionCall.Expression.Accept(this);
			sb.Append("))");
		}

		public override void VisitCharacter(Character character)
		{
			string charValue = character.Value;

			if (charValue.StartsWith("'"))
				charValue = charValue.Substring(1);

			if (charValue.EndsWith("'"))
				charValue = charValue.Substring(0, charValue.Length - 1);
				
			sb.AppendFormat("\"{0}\".ToCharArray()", EscapeString(charValue));
		}

		public override void VisitCompoundSymbol(CompoundSymbol compoundSymbol)
		{
			string symValue = compoundSymbol.Value;

			if (symValue.StartsWith("\""))
				symValue = symValue.Substring(1);

			if (symValue.EndsWith("\""))
				symValue = symValue.Substring(0, symValue.Length - 1);
				
			sb.AppendFormat("\"{0}\"", EscapeString(symValue));
		}

		public static string EscapeString(string s)
		{
			return s.Replace("\\(", "(").Replace("\\)", ")").
				Replace("\\<", "<").Replace("\\>", ">").
				Replace("\\\"", "\"").Replace("\\\'", "\'").
				Replace("\\", "\\\\").Replace("\"", "\\\"").Replace("'", "\\'");
		}

		public override void VisitIdentifier(Identifier identifier)
		{
			sb.AppendFormat("\"{0}\"", identifier.Value);
		}

		public override void VisitTrueIdentifier(TrueIdentifier identifier)
		{
			sb.Append("true");
		}

		public override void VisitFalseIdentifier(FalseIdentifier falseIdentifier)
		{
			sb.Append("false");
		}

		public override void VisitSymbolVariable(SymbolVariable symbolVariable)
		{
			if (!symbolVariable.IsBound)
				sb.AppendFormat("new SymbolVariable(\"{0}\")", symbolVariable.Index);
			else
				sb.AppendFormat("pattern{0}.GetVariable(\"{1}\")", currentPatternIndex, symbolVariable.Index);
		}

		public override void VisitTermVariable(TermVariable termVariable)
		{
			if (!termVariable.IsBound)
				sb.AppendFormat("new TermVariable(\"{0}\")", termVariable.Index);
			else
				sb.AppendFormat("pattern{0}.GetVariable(\"{1}\")", currentPatternIndex, termVariable.Index);
		}

		public override void VisitExpressionVariable(ExpressionVariable expressionVariable)
		{
			if (!expressionVariable.IsBound)
				sb.AppendFormat("new ExpressionVariable(\"{0}\")", expressionVariable.Index);
			else
				sb.AppendFormat("pattern{0}.GetVariable(\"{1}\")", currentPatternIndex, expressionVariable.Index);
		}

		public override void VisitExpressionInParentheses(ExpressionInParentheses expressionInParentheses)
		{
			sb.Append("new OpeningBrace(), ");

			expressionInParentheses.Expression.Accept(this);

			if (!expressionInParentheses.Expression.IsEmpty) sb.Append(", ");
			sb.Append("new ClosingBrace()");
		}

		public override void VisitPatternInParentheses(PatternInParentheses patternInParentheses)
		{
			sb.Append("new OpeningBrace(), ");

			patternInParentheses.Pattern.Accept(this);

			if (!patternInParentheses.Pattern.IsEmpty) sb.Append(", ");
			sb.Append("new ClosingBrace()");
		}

		public override void VisitMacrodigit(Macrodigit macrodigit)
		{
			sb.AppendFormat("{0}", macrodigit.Value);
		}
	}
}
