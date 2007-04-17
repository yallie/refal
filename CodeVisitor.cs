using System;
using System.Collections;

namespace Refal
{
	public abstract class CodeVisitor
	{
		public abstract void VisitProgram(Program program);
		public abstract void VisitFunction(Function function);
		public abstract void VisitExternalFunction(ExternalFunction function);
		public abstract void VisitDefinedFunction(DefinedFunction function);
		public abstract void VisitBlock(Block block);
		public abstract void VisitSentence(Sentence sentence);
		public abstract void VisitConditions(Conditions conditions);
		public abstract void VisitPattern(Pattern pattern);
		public abstract void VisitExpression(Expression expression);
		public abstract void VisitTerm(Term term);
		public abstract void VisitSymbol(Symbol symbol);
		public abstract void VisitCharacter(Character character);
		public abstract void VisitCompoundSymbol(CompoundSymbol compoundSymbol);
		public abstract void VisitIdentifier(Identifier identifier);
		public abstract void VisitTrueIdentifier(TrueIdentifier identifier);
		public abstract void VisitFalseIdentifier(FalseIdentifier falseIdentifier);
		public abstract void VisitMacrodigit(Macrodigit macrodigit);
		public abstract void VisitVariable(Variable variable);
		public abstract void VisitSymbolVariable(SymbolVariable symbolVariable);
		public abstract void VisitTermVariable(TermVariable termVariable);
		public abstract void VisitExpressionVariable(ExpressionVariable expressionVariable);
		public abstract void VisitFunctionCall(FunctionCall functionCall);
		public abstract void VisitExpressionInParentheses(ExpressionInParentheses expressionInParentheses);
		public abstract void VisitPatternInParentheses(PatternInParentheses patternInParentheses);
	}

	public class BaseCodeVisitor : CodeVisitor
	{
		public override void VisitProgram(Program program)  { throw new NotImplementedException(); }
		public override void VisitFunction(Function function) { throw new NotImplementedException(); }
		public override void VisitExternalFunction(ExternalFunction function) { throw new NotImplementedException(); }
		public override void VisitDefinedFunction(DefinedFunction function) { throw new NotImplementedException(); }
		public override void VisitBlock(Block block) { throw new NotImplementedException(); }
		public override void VisitSentence(Sentence sentence) { throw new NotImplementedException(); }
		public override void VisitConditions(Conditions conditions) { throw new NotImplementedException(); }
		public override void VisitPattern(Pattern pattern) { throw new NotImplementedException(); }
		public override void VisitExpression(Expression expression) { throw new NotImplementedException(); }
		public override void VisitTerm(Term term) { throw new NotImplementedException(); }
		public override void VisitSymbol(Symbol symbol) { throw new NotImplementedException(); }
		public override void VisitCharacter(Character character) { throw new NotImplementedException(); }
		public override void VisitCompoundSymbol(CompoundSymbol compoundSymbol) { throw new NotImplementedException(); }
		public override void VisitIdentifier(Identifier identifier) { throw new NotImplementedException(); }
		public override void VisitTrueIdentifier(TrueIdentifier identifier) { throw new NotImplementedException(); }
		public override void VisitFalseIdentifier(FalseIdentifier falseIdentifier) { throw new NotImplementedException(); }
		public override void VisitMacrodigit(Macrodigit macrodigit) { throw new NotImplementedException(); }
		public override void VisitVariable(Variable variable) { throw new NotImplementedException(); }
		public override void VisitSymbolVariable(SymbolVariable symbolVariable) { throw new NotImplementedException(); }
		public override void VisitTermVariable(TermVariable termVariable) { throw new NotImplementedException(); }
		public override void VisitExpressionVariable(ExpressionVariable expressionVariable) { throw new NotImplementedException(); }
		public override void VisitFunctionCall(FunctionCall functionCall) { throw new NotImplementedException(); }
		public override void VisitExpressionInParentheses(ExpressionInParentheses expressionInParentheses) { throw new NotImplementedException(); }
		public override void VisitPatternInParentheses(PatternInParentheses patternInParentheses) { throw new NotImplementedException(); }
	}
}
