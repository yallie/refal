using System;
using System.Collections;
using Irony.Ast;
using Irony.Parsing;
using System.Collections.Generic;
using Irony.Interpreter;
using Refal.Runtime;

namespace Refal
{
	public class CompoundSymbol : Symb
	{
		// compound symbol is enclosed in double quotes: "a+b"
		// it is treated as a single symbol
		string symValue;

		public CompoundSymbol(string value)
		{
			symValue = value;
		}

		public string Value
		{
			get { return symValue; }
			set { symValue = value; }
		}

		public override string ToString()
		{
			return string.Format("\"{0}\"", symValue);
		}

		public override void  Evaluate(EvaluationContext context, AstMode mode)
		{
			context.Data.Push(Value);
		}
	}
}
