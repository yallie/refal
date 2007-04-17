using System;
using System.Text;
using System.Collections;

namespace Refal
{
	public class CSharpCodeVisitor : BaseCodeVisitor
	{
		StringBuilder sb = new StringBuilder();
		int indentLevel = 2;
//		int currentPatternIndex = 1;

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
			if (program.EntryPoint == null)
			{
				Parser.SemErr("No entry point defined");
				return;
			}

			sb.AppendFormat(@"
using System;
using System.Collections;

namespace Refal.Runtime
{{
	public class Program : RefalBase
	{{
		static void Main()
		{{
			{0}(null);
		}}" + "\r\n\r\n", program.EntryPoint.Name);

			foreach (Function function in program.FunctionList)
			{
				function.Accept(this);
				sb.Append("\r\n\r\n");
			}

			sb.Append("\t}\r\n}\r\n");
		}

		public override void VisitExternalFunction(ExternalFunction function)
		{
			// do nothing
//			sb.AppendFormat("$EXTRN {0};\r\n", function.Name);
		}

		public override void VisitDefinedFunction(DefinedFunction function)
		{
			Indent(indentLevel);
			sb.AppendFormat("{0} static PassiveExpression {1}(PassiveExpression expression)\r\n",
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
			sb.Append("throw new RecognitionImpossibleException(\"Recognition impossible\");\r\n");

			indentLevel--;
			Indent(indentLevel);
			sb.Append("}");
		}

		public override void VisitSentence(Sentence sentence)
		{
			Indent(indentLevel);

			sb.Append("if (RefalBase.Match(expression, ");
			sentence.Pattern.Accept(this);

			// TODO
//			if (sentence.Conditions != null)
//				sentence.Conditions.Accept(this);

			sb.Append("))\r\n");

			if (sentence.Expression != null)
			{
				Indent(indentLevel + 1);

				sb.Append("return ");
				sentence.Expression.Accept(this);
			}
		}

		public override void VisitPattern(Pattern pattern)
		{
			sb.Append("new Pattern(");
			for (int i = 0; i < pattern.Terms.Count; i++)
			{
				Term term = pattern.Terms[i] as Term;
				term.Accept(this);

				if (i < pattern.Terms.Count - 1)
					sb.Append(", ");
			}
			sb.Append(")");
		}

		public override void VisitExpression(Expression expression)
		{
			sb.Append("new PassiveExpression(");
			for (int i = 0; i < expression.Terms.Count; i++)
			{
				Term term = expression.Terms[i] as Term;
				term.Accept(this);

				if (i < expression.Terms.Count - 1)
					sb.Append(", ");
			}
			sb.Append(")");
		}

		public override void VisitFunctionCall(FunctionCall functionCall)
		{
			sb.AppendFormat("{0}(", functionCall.FunctionName);
			functionCall.Expression.Accept(this);
			sb.Append(")");
		}

		public override void VisitCharacter(Character character)
		{
			string charValue = character.Value;

			if (charValue.StartsWith("'"))
				charValue = charValue.Substring(1);

			if (charValue.EndsWith("'"))
				charValue = charValue.Substring(0, charValue.Length - 1);
				
			sb.AppendFormat("\"{0}\".ToCharArray()", charValue);
		}

		public override void VisitCompoundSymbol(CompoundSymbol compoundSymbol)
		{
			sb.Append(compoundSymbol.Value);
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
		}

		public override void VisitTermVariable(TermVariable termVariable)
		{
			if (!termVariable.IsBound)
				sb.AppendFormat("new TermVariable(\"{0}\")", termVariable.Index);
		}

		public override void VisitExpressionVariable(ExpressionVariable expressionVariable)
		{
			if (!expressionVariable.IsBound)
				sb.AppendFormat("new ExpressionVariable(\"{0}\")", expressionVariable.Index);
		}

		public override void VisitExpressionInParentheses(ExpressionInParentheses expressionInParentheses)
		{
			expressionInParentheses.Expression.Accept(this);
		}

		public override void VisitPatternInParentheses(PatternInParentheses patternInParentheses)
		{
			patternInParentheses.Pattern.Accept(this);
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
