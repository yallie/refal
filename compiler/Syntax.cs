
/*-------------------------------------------------------------------------*/
/*                                                                         */
/*      Abstract syntax tree classes                                       */
/*      This file is a part of Refal5.NET project                          */
/*      Project license: http://www.gnu.org/licenses/lgpl.html             */
/*      Written by Y [11-06-06] <yallie@yandex.ru>                         */
/*                                                                         */
/*      Copyright (c) 2006-2007 Alexey Yakovlev                            */
/*      All Rights Reserved                                                */
/*                                                                         */
/*-------------------------------------------------------------------------*/

using System;
using System.Collections;

namespace Refal
{
	// syntax summary taken from: http://shura.botik.ru/refal/book/html/ref_b.html

	public abstract class SyntaxNode
	{
		public SyntaxNode()
		{
		}

		public abstract void Accept(CodeVisitor visitor);
	}

	public class Program : SyntaxNode
	{
		IDictionary functions = new Hashtable();
		ArrayList functionList = new ArrayList();
		Function entryPoint = null;
		string name = "Program";

		public Program()
		{
		}

		public IDictionary Functions
		{
			get { return functions; }
		}

		public ArrayList FunctionList
		{
			get { return functionList; }
		}

		public Function EntryPoint
		{
			get { return entryPoint; }
			set { entryPoint = value; }
		}

		public void AddFunction(Function function)
		{
			functions[function.Name] = function;
			functionList.Add(function);
		}

		public string Name
		{
			get { return name; }
			set
			{
				if (value != null && value.IndexOf(".") >= 0)
					value = value.Substring(0, value.IndexOf("."));

				if (value != null && value.Length > 0)
					name = value;
			}
		}

		public override void Accept(CodeVisitor visitor)
		{
			visitor.VisitProgram(this);
		}
	}

	public abstract class Function : SyntaxNode
	{
		protected string name = "";

		public Function()
		{
		}

		public Function(string name)
		{
			this.name = name;
		}

		public string Name
		{
			get { return name; }
			set { name = value.Replace("-", "__"); }
		}

		public override void Accept(CodeVisitor visitor)
		{
			visitor.VisitFunction(this);
		}
	}

	public class ExternalFunction : Function
	{
		public ExternalFunction(string name) : base(name)
		{
		}

		public override void Accept(CodeVisitor visitor)
		{
			visitor.VisitExternalFunction(this);
		}
	}

	public class DefinedFunction : Function
	{
		Block block;
		bool isPublic = false;

		public DefinedFunction() : base()
		{
		}

		public DefinedFunction(string name) : base(name)
		{
		}

		public Block Block
		{
			get { return block; }
			set { block = value; }
		}

		public bool IsPublic
		{
			get { return isPublic; }
			set { isPublic = value; }
		}

		public override void Accept(CodeVisitor visitor)
		{
			visitor.VisitDefinedFunction(this);
		}
	}

	public class Block : SyntaxNode
	{
		ArrayList sentences = new ArrayList();

		public Block()
		{
		}

		public ArrayList Sentences
		{
			get { return sentences; }
		}

		public override void Accept(CodeVisitor visitor)
		{
			visitor.VisitBlock(this);
		}
	}

	public class Sentence : SyntaxNode
	{
		// pattern { conditions } = expression;
		// or pattern conditions block;
		Pattern pattern;
		Conditions conditions;
		Expression expression;

		public Sentence()
		{
		}

		public Pattern Pattern
		{
			get { return pattern; }
			set { pattern = value; }
		}

		public Conditions Conditions
		{
			get { return conditions; }
			set { conditions = value; }
		}

		public Expression Expression
		{
			get { return expression; }
			set { expression = value; }
		}

		public override void Accept(CodeVisitor visitor)
		{
			visitor.VisitSentence(this);
		}
	}

   public class Conditions : SyntaxNode
	{
		Expression expression;
		Pattern pattern;
		Block block;
		Conditions moreConditions;
		Expression resultExpression;

		public Conditions()
		{
		}

		public Expression Expression
		{
			get { return expression; }
			set { expression = value; }
		}

		public Pattern Pattern
		{
			get { return pattern; }
			set { pattern = value; }
		}

		public Conditions MoreConditions
		{
			get { return moreConditions; }
			set { moreConditions = value; }
		}

		public Expression ResultExpression
		{
			get { return resultExpression; }
			set { resultExpression = value; }
		}

		public Block Block
		{
			get { return block; }
			set { block = value; }
		}

		public override void Accept(CodeVisitor visitor)
		{
			visitor.VisitConditions(this);
		}
	}

   public class Pattern : SyntaxNode
	{
		ArrayList terms = new ArrayList();

		public Pattern()
		{
		}

		public ArrayList Terms
		{
			get { return terms; }
		}

		public bool IsEmpty
		{
			get { return terms.Count == 0; }
		}

		public override void Accept(CodeVisitor visitor)
		{
			visitor.VisitPattern(this);
		}
	}

	public class Expression : SyntaxNode
	{
		ArrayList terms = new ArrayList();

		public Expression()
		{
		}

		public ArrayList Terms
		{
			get { return terms; }
		}

		public bool IsEmpty
		{
			get { return terms.Count == 0; }
		}

		public override void Accept(CodeVisitor visitor)
		{
			visitor.VisitExpression(this);
		}
	}

	public abstract class Term : SyntaxNode
	{
		public override void Accept(CodeVisitor visitor)
		{
			visitor.VisitTerm(this);
		}
	}

	public abstract class Symbol : Term
	{
		public override void Accept(CodeVisitor visitor)
		{
			visitor.VisitSymbol(this);
		}
	}

	public class Character : Symbol
	{
		// character is enclosed in single quotes: 'sample'
		// it is treated as a sequence of symbols
		string charValue;

		public Character()
		{
		}

		public Character(string value)
		{
			charValue = value;
		}

		public string Value
		{
			get { return charValue; }
			set { charValue = value; }
		}

		public override void Accept(CodeVisitor visitor)
		{
			visitor.VisitCharacter(this);
		}
	}

	public class CompoundSymbol : Symbol
	{
		// compound symbol is enclosed in double quotes: "a+b"
		// it is treated as a single symbol
		string symValue;

		public CompoundSymbol()
		{
		}

		public CompoundSymbol(string value)
		{
			symValue = value;
		}

		public string Value
		{
			get { return symValue; }
			set { symValue = value; }
		}

		public override void Accept(CodeVisitor visitor)
		{
			visitor.VisitCompoundSymbol(this);
		}
	}

	public class Identifier : CompoundSymbol
	{
		public Identifier(string value) : base(value)
		{
		}

		public override void Accept(CodeVisitor visitor)
		{
			visitor.VisitIdentifier(this);
		}
	}

	public class TrueIdentifier : Identifier
	{
		public TrueIdentifier(string value) : base(value)
		{
		}

		public override void Accept(CodeVisitor visitor)
		{
			visitor.VisitTrueIdentifier(this);
		}
	}

	public class FalseIdentifier : Identifier
	{
		public FalseIdentifier(string value) : base(value)
		{
		}

		public override void Accept(CodeVisitor visitor)
		{
			visitor.VisitFalseIdentifier(this);
		}
	}

	public class Macrodigit : Symbol
	{
		int digitValue;

		public Macrodigit()
		{
		}

		public Macrodigit(int value)
		{
			digitValue = value;
		}

		public int Value
		{
			get { return digitValue; }
			set { digitValue = value; }
		}

		public override void Accept(CodeVisitor visitor)
		{
			visitor.VisitMacrodigit(this);
		}
	}

	public abstract class Variable : Term
	{
		string index;
		bool isBound = false;

		public Variable()
		{
		}

		public Variable(string index)
		{
			this.index = index;
		}

		public virtual string Index
		{
			get { return index; }
			set { index = value; }
		}

		public bool IsBound
		{
			get { return isBound; }
			set { isBound = value; }
		}

		public override void Accept(CodeVisitor visitor)
		{
			visitor.VisitVariable(this);
		}
	}

	public class SymbolVariable : Variable
	{
		public override string Index
		{
			get { return base.Index; }
			set { base.Index = "s." + value; }
		}

		public override void Accept(CodeVisitor visitor)
		{
			visitor.VisitSymbolVariable(this);
		}
	}

	public class TermVariable : Variable
	{
		public override string Index
		{
			get { return base.Index; }
			set { base.Index = "t." + value; }
		}

		public override void Accept(CodeVisitor visitor)
		{
			visitor.VisitTermVariable(this);
		}
	}

	public class ExpressionVariable : Variable
	{
		public override string Index
		{
			get { return base.Index; }
			set { base.Index = "e." + value; }
		}

		public override void Accept(CodeVisitor visitor)
		{
			visitor.VisitExpressionVariable(this);
		}
	}

	public class FunctionCall : Term
	{
		string functionName;
		Expression expression;

		public FunctionCall()
		{
		}

		public string FunctionName
		{
			get { return functionName; }
			set { functionName = value.Replace("-", "__"); }
		}

		public Expression Expression
		{
			get { return expression; }
			set { expression = value; }
		}

		public override void Accept(CodeVisitor visitor)
		{
			visitor.VisitFunctionCall(this);
		}
	}

	public class ExpressionInParentheses : Term
	{
		Expression expression;

		public ExpressionInParentheses()
		{
		}

		public ExpressionInParentheses(Expression expression)
		{
			this.expression = expression;
		}

		public Expression Expression
		{
			get { return expression; }
			set { expression = value; }
		}

		public override void Accept(CodeVisitor visitor)
		{
			visitor.VisitExpressionInParentheses(this);
		}
	}

	public class PatternInParentheses : Term
	{
		Pattern pattern;

		public PatternInParentheses()
		{
		}

		public PatternInParentheses(Pattern pattern)
		{
			this.pattern = pattern;
		}

		public Pattern Pattern
		{
			get { return pattern; }
			set { pattern = value; }
		}

		public override void Accept(CodeVisitor visitor)
		{
			visitor.VisitPatternInParentheses(this);
		}
	}
}
