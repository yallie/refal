using System;
using System.Collections.Generic;
using System.Text;
using Irony.Parsing;
using Irony.Ast;
using Refal;

namespace Irony.Samples
{
	/// <summary>
	/// Refal-5 Grammar
	/// Written by Alexey Yakovlev <yallie@yandex.ru>
	/// </summary>
	[Language("Refal5", "0.0 alpha", "Refal-5.NET language interpreter")]
	public class RefalGrammar : Grammar
	{
		public RefalGrammar() : base(true) // case sensitive
		{
			GrammarComments = "Refal-5 language interpreter based on Irony toolkit.";

			// Terminals

			var Number = new NumberLiteral("Number");
			Number.DefaultIntTypes = new TypeCode[] { TypeCode.Int32, TypeCode.Int64 };
			Number.AddPrefix("0x", NumberOptions.Hex);
			Number.AddSuffix("u", TypeCode.UInt32);
			Number.AddSuffix("l", TypeCode.Int64);
			Number.AddSuffix("ul", TypeCode.UInt64);
			Number.Options |= NumberOptions.AllowSign;

			var CharLiteral = new StringLiteral("Char", "'", StringOptions.AllowsAllEscapes);
			var StringLiteral = new StringLiteral("String", "\"", StringOptions.AllowsAllEscapes);
			var Identifier = new IdentifierTerminal("Identifier", "_-", "");

			var LineComment = new CommentTerminal("LineComment", "*", "\n", "\r") { Priority = -1000 };
			var BlockComment = new CommentTerminal("BlockComment", "/*", "*/");
			NonGrammarTerminals.Add(LineComment);
			NonGrammarTerminals.Add(BlockComment);

			// Non-terminals

			var Program = new NonTerminal("Program", typeof(Program));
			var Definition = new NonTerminal("Definition");
			var Function = new NonTerminal("Function", typeof(DefinedFunction));
			var External = new NonTerminal("External", typeof(ExternalFunctionList));
			var IdentifierList = new NonTerminal("Identifier+", typeof(IdentifierList));
			var Block = new NonTerminal("Block", typeof(Block));
			var Sentence = new NonTerminal("Sentence", typeof(Sentence));
			var RSentence = new NonTerminal("RSentence", typeof(RSentence));
			var SentenceList = new NonTerminal("Sentence+", typeof(SentenceList));
			var Pattern = new NonTerminal("Pattern", typeof(Pattern));
			var PatternItem = new NonTerminal("PatternItem");
			var PatternInParentheses = new NonTerminal("(Pattern)", typeof(PatternInParentheses));
			var Expression = new NonTerminal("Expression", typeof(Expression));
			var ExpressionItem = new NonTerminal("ExpressionItem");
			var ExpressionInParentheses = new NonTerminal("(Expression)", typeof(ExpressionInParentheses));
			var Var = new NonTerminal("Variable", Variable.CreateVariableNode);
			var Symbol = new NonTerminal("Symbol", Symb.CreateSymbolNode);
			var Call = new NonTerminal("Call", typeof(FunctionCall));
			var FunctionName = new NonTerminal("FunctionName", typeof(FunctionName));
			var WhereOrWithClause = new NonTerminal("WhereOrWithClause", typeof(Conditions));
			var RWhereOrWithClause = new NonTerminal("RWhereOrWithClause", typeof(RConditions));

			var SemicolonOpt = new NonTerminal("[;]", Empty | ";");
			var EntryOpt = new NonTerminal("[ENTRY]", Empty | "$ENTRY");
			var Extern = new NonTerminal("Extern", ToTerm("$EXTRN") | "$EXTERN" | "$EXTERNAL");

			// Rules

			Root = Program;

			Program.Rule = MakePlusRule(Program, Definition);
			Definition.Rule = Function | External;
			External.Rule = Extern + IdentifierList + ";";
			IdentifierList.Rule = MakePlusRule(IdentifierList, ToTerm(","), Identifier);

			Function.Rule = EntryOpt + Identifier + Block + SemicolonOpt;
			Block.Rule = "{" + SentenceList + SemicolonOpt + "}";
			SentenceList.Rule = MakePlusRule(SentenceList, ToTerm(";"), Sentence);

			Sentence.Rule = Pattern + RSentence;
			RSentence.Rule = ToTerm("=") + Expression | WhereOrWithClause;
			Pattern.Rule = MakeStarRule(Pattern, PatternItem);
			PatternItem.Rule = Var | Symbol | PatternInParentheses;
			PatternInParentheses.Rule = "(" + Pattern + ")";
			Expression.Rule = MakeStarRule(Expression, ExpressionItem);
			ExpressionItem.Rule = Call | Var | Symbol | ExpressionInParentheses;
			ExpressionInParentheses.Rule = "(" + Expression + ")";

			Var.Rule = (ToTerm("e") | "s" | "t") + "." + (Number | Identifier);
			Symbol.Rule = StringLiteral | CharLiteral | Number | "True" | "False" | Identifier;
			Call.Rule = ToTerm("<") + FunctionName + Expression + ">";
			FunctionName.Rule = Identifier | "+" | "-" | "*" | "/";

			WhereOrWithClause.Rule = (ToTerm(",") | "&") + Expression + ":" + RWhereOrWithClause;
			RWhereOrWithClause.Rule = Block // with-clause
				| Pattern + // where-clause
				(ToTerm("=") + Expression | WhereOrWithClause);

			// Punctuation, braces, transient terms, options

			RegisterPunctuation("(", ")");
			RegisterPunctuation("{", "}");

			RegisterBracePair("(", ")");
			RegisterBracePair("<", ">");
			RegisterBracePair("{", "}");

			MarkTransient(Definition, PatternItem, ExpressionItem, SemicolonOpt, EntryOpt);
			LanguageFlags = LanguageFlags.CreateAst | LanguageFlags.CanRunSample | LanguageFlags.TailRecursive;
		}
	}
}
