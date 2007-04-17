using System;
using System.Collections;

namespace Refal
{
	// syntax summary taken from: http://shura.botik.ru/refal/book/html/ref_b.html

	public class Program
	{
		IDictionary functions = new Hashtable();

		public Program()
		{
		}

		public IDictionary Functions
		{
			get { return functions; }
		}
	}

	public abstract class Function
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
			set { name = value; }
		}
	}

	public class ExternalFunction : Function
	{
		public ExternalFunction(string name) : base(name)
		{
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
	}

	public class Block
	{
		ArrayList sentences = new ArrayList();

		public Block()
		{
		}

		public ArrayList Sentences
		{
			get { return sentences; }
		}
	}

	public class Sentence
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
	}

   public class Conditions
	{
		Expression expression;
		Pattern pattern;
		Block block;
		Conditions moreConditions;

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

		public Block Block
		{
			get { return block; }
			set { block = value; }
		}
	}

   public class Pattern
	{
		ArrayList terms = new ArrayList();

		public Pattern()
		{
		}

		public ArrayList Terms
		{
			get { return terms; }
		}
	}

	public class Expression
	{
		ArrayList terms = new ArrayList();

		public Expression()
		{
		}

		public ArrayList Terms
		{
			get { return terms; }
		}
	}

	public abstract class Term
	{
	}

	public abstract class Symbol : Term
	{
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
	}

	public class Identifier : CompoundSymbol
	{
		public Identifier(string value) : base(value)
		{
		}
	}

	public class TrueIdentifier : Identifier
	{
		public TrueIdentifier(string value) : base(value)
		{
		}
	}

	public class FalseIdentifier : Identifier
	{
		public FalseIdentifier(string value) : base(value)
		{
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
	}

	public abstract class Variable : Term
	{
		string index;

		public Variable()
		{
		}

		public Variable(string index)
		{
			this.index = index;
		}

		public string Index
		{
			get { return index; }
			set { index = value; }
		}
	}

	public class SymbolVariable : Variable
	{
	}

	public class TermVariable : Variable
	{
	}

	public class ExpressionVariable : Variable
	{
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
			set { functionName = value; }
		}

		public Expression Expression
		{
			get { return expression; }
			set { expression = value; }
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
	}
}
