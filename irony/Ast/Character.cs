using System;
using Irony.Ast;
using Irony.Parsing;
using System.Collections.Generic;
using Irony.Interpreter;
using Refal.Runtime;

namespace Refal
{
	public class Character : Symb
	{
		// character is enclosed in single quotes: 'sample'
		// it is treated as a sequence of symbols
		string charValue;

		public Character(string value)
		{
			charValue = value;
		}

		public string Value
		{
			get { return charValue; }
			set { charValue = value; }
		}

		public override string ToString()
		{
			return string.Format("'{0}'", charValue);
		}

		public override void Evaluate(EvaluationContext context, AstMode mode)
		{
			context.Data.Push(Value.ToCharArray());
		}
	}
}
