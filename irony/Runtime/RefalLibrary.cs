// Refal5.NET runtime
// Written by Alexey Yakovlev <yallie@yandex.ru>
// http://refal.codeplex.com

using System;
using System.IO;
using System.Text;
using Irony.Ast;
using Irony.Interpreter;
using System.Reflection;
using System.Collections.Generic;

namespace Refal.Runtime
{
	/// <summary>
	/// Run-time library
	/// </summary>
	public class RefalLibrary
	{
		/// <summary>
		/// Irony interpreter evaluation context
		/// </summary>
		public EvaluationContext EvaluationContext { get; private set; }

		/// <summary>
		/// File I/O support: handle (expression) -> StreamReader/StreamWriter
		/// </summary>
		IDictionary<string, object> OpenFiles { get; set; }

		/// <summary>
		/// Bury/Dig functions expression storage
		/// </summary>
		IDictionary<string, PassiveExpression> BuriedKeys { get; set; }

		IDictionary<string, PassiveExpression> BuriedValues { get; set; }

		/// <summary>
		/// Command line arguments
		/// </summary>
		protected string[] CommandLineArguments { get; set; }

		public RefalLibrary(EvaluationContext ctx)
		{
			EvaluationContext = ctx;
			OpenFiles = new Dictionary<string, object>();
			BuriedKeys = new Dictionary<string, PassiveExpression>();
			BuriedValues = new Dictionary<string, PassiveExpression>();
			CommandLineArguments = null;
		}

		// Standard RTL routines

		public PassiveExpression Print(PassiveExpression expression)
		{
			if (expression == null)
				return null;

			//Console.WriteLine("{0}", expression.ToStringBuilder(0));
			EvaluationContext.Write(expression.ToStringBuilder(0).ToString() + Environment.NewLine);

			return expression;
		}

		public PassiveExpression Prout(PassiveExpression expression)
		{
			if (expression == null)
				return null;

			//Console.WriteLine("{0}", expression.ToStringBuilder(0));
			EvaluationContext.Write(expression.ToStringBuilder(0).ToString() + Environment.NewLine);

			return null;
		}

		public PassiveExpression Card(PassiveExpression expression)
		{
			throw new NotSupportedException();

			/*string s = Console.ReadLine();

			if (s != null)
				return PassiveExpression.Build(s.ToCharArray());
			else
				return PassiveExpression.Build(0);*/
		}

		public PassiveExpression Open(PassiveExpression expression)
		{
			// <Open s.Mode s.D e.File-name>
			if (expression == null || expression.Count < 1)
				throw new ArgumentNullException("s.Mode");
			else if (expression.Count < 2)
				throw new ArgumentNullException("s.D");

			string mode = expression[0].ToString().ToUpper();
			string handle = expression[1].ToString();
			string fileName = string.Format("refal{0}.dat", handle);

			// fileName can be omitted
			if (expression.Count > 2)
			{
				fileName = expression.ToStringBuilder(2).ToString();
			}

			// R - read, W - write, A - append
			if (mode.StartsWith("R"))
			{
				OpenFiles[handle] = new StreamReader(File.OpenRead(fileName));
			}
			else if (mode.StartsWith("W"))
			{
				OpenFiles[handle] = new StreamWriter(File.Create(fileName));
			}
			else if (mode.StartsWith("A"))
			{
				OpenFiles[handle] = File.AppendText(fileName);
			}
			else
			{
				throw new NotSupportedException("Bad file open mode: " + mode + " (R, W, or A expected)");
			}

			// AFAIK, Open don't return anything
			return null;
		}

		public PassiveExpression Get(PassiveExpression expression)
		{
			if (expression == null || expression.IsEmpty)
				return Card(expression);

			string handle = expression[0].ToString();
			StreamReader sr = OpenFiles[handle] as StreamReader;

			if (sr == null)
				return Card(expression);

			string s = sr.ReadLine();
			if (s != null)
				return PassiveExpression.Build(s.ToCharArray());
			else
				return PassiveExpression.Build(0);
		}

		public PassiveExpression Put(PassiveExpression expression)
		{
			if (expression == null || expression.IsEmpty)
				return Prout(expression);

			string handle = expression[0].ToString();
			StreamWriter sw = OpenFiles[handle] as StreamWriter;

			if (sw == null)
				return Prout(expression);

			sw.WriteLine("{0}", expression.ToStringBuilder(1));

			PassiveExpression result = PassiveExpression.Build(expression);
			result.Remove(result[0]);
			return result;
		}

		protected void CloseFiles()
		{
			foreach (object o in OpenFiles.Values)
			{
				if (o is StreamWriter)
					(o as StreamWriter).Close();
				else if (o is StreamReader)
					(o as StreamReader).Close();
			}
		}

		public PassiveExpression Arg(PassiveExpression expression)
		{
			if (expression == null || expression.IsEmpty || CommandLineArguments == null)
				return new PassiveExpression();

			int index = Convert.ToInt32(expression[0]) - 1; // in Refal, index is 1-based

			if (index >= CommandLineArguments.Length)
			{
				return new PassiveExpression();
			}

			return PassiveExpression.Build(CommandLineArguments[index].ToCharArray());
		}

		public PassiveExpression Br(PassiveExpression expression)
		{
			// <Br e.N '=' e.Expr>, where e.N is expression which does not
			// include '=' on the upper level of the bracket's structure
			Pattern pattern = new Pattern(new ExpressionVariable("Key"), '=', new ExpressionVariable("Value"));
			if (pattern.Match(expression))
			{
				PassiveExpression key = (PassiveExpression)pattern.GetVariable("Key");
				PassiveExpression value = (PassiveExpression)pattern.GetVariable("Value");
				string strKey = key.ToString();

				BuriedKeys[strKey] = key;
				BuriedValues[strKey] = value;
			}

			throw new RecognitionImpossibleException("<Br e.N '=' e.Expr>: unexpected arguments");
		}

		public PassiveExpression Dg(PassiveExpression expression)
		{
			// <Dg e.N>
			string strKey = expression.ToString();
			PassiveExpression result = PassiveExpression.Build(BuriedValues[strKey] as PassiveExpression);

			BuriedValues[strKey] = null;
			BuriedKeys[strKey] = null;

			return result;
		}

		public PassiveExpression Dgall(PassiveExpression expression)
		{
			List<object> result = new List<object>();
			foreach (string strKey in BuriedKeys.Keys)
			{
				result.AddRange(new object[] {new OpeningBrace(), BuriedKeys[strKey], '=', BuriedValues[strKey], new ClosingBrace()});
				BuriedKeys[strKey] = null;
				BuriedValues[strKey] = null;
			}

			return PassiveExpression.Build(result.ToArray());
		}

		// extract arguments specified as <Function t.1 e.2>
		void GetArguments(PassiveExpression expression, out object arg1, out object arg2)
		{
			var p = new Pattern(new TermVariable("t.1"), new ExpressionVariable("e.2"));
			if (p.Match(expression))
			{
				arg1 = p.GetVariable("t.1");
				arg2 = p.GetVariable("e.2");
				return;
			}

			// can't find match
			throw new RecognitionImpossibleException();
		}

		int ToInt32(object expression)
		{
			string s = expression.ToString().Trim(" \t\r\n()".ToCharArray());
			return Convert.ToInt32(s);
		}

		public PassiveExpression Add(PassiveExpression expression)
		{
			object op1, op2;
			GetArguments(expression, out op1, out op2);

			return PassiveExpression.Build(ToInt32(op1) + ToInt32(op2));
		}

		public PassiveExpression Sub(PassiveExpression expression)
		{
			object op1, op2;
			GetArguments(expression, out op1, out op2);

			return PassiveExpression.Build(ToInt32(op1) - ToInt32(op2));
		}

		public PassiveExpression Mul(PassiveExpression expression)
		{
			object op1, op2;
			GetArguments(expression, out op1, out op2);

			return PassiveExpression.Build(ToInt32(op1) * ToInt32(op2));
		}

		public PassiveExpression Div(PassiveExpression expression)
		{
			object op1, op2;
			GetArguments(expression, out op1, out op2);

			return PassiveExpression.Build(ToInt32(op1) / ToInt32(op2));
		}

		public PassiveExpression Cp(PassiveExpression expression)
		{
			throw new NotImplementedException();
		}

		public PassiveExpression Rp(PassiveExpression expression)
		{
			throw new NotImplementedException();
		}

		public PassiveExpression Type(PassiveExpression expression)
		{
			if (expression == null || expression.IsEmpty)
				return PassiveExpression.Build('*', 0, expression);

			object o = expression[0];

			if (o is OpeningBrace)
				return PassiveExpression.Build('B', 0, expression);

			if (o is char)
			{
				char c = (char)o;
				char subtype = Char.IsUpper(c) ? 'u' : 'l';

				if ((c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z'))
					return PassiveExpression.Build('L', subtype, expression);
				
				if (c >= '0' && c <= '9')
					return PassiveExpression.Build('D', 0, expression);

				if (Char.IsControl(c))
					return PassiveExpression.Build('O', subtype, expression);

				// printable
				return PassiveExpression.Build('P', subtype, expression);
			}

			if (o is int || o is long)
				return PassiveExpression.Build('N', 0, expression);

			if (o is string)
			{
				string s = o as string;
				char subtype = 'i';

				if (s.Length == 0 || !(Char.IsLetter(s[0])) || s.IndexOf(" ") >= 0)
					subtype = 'q';

				return PassiveExpression.Build('W', subtype, expression);
			}

			return PassiveExpression.Build('P', 'l', expression);
		}

		public PassiveExpression Mu(PassiveExpression expression)
		{
			throw new NotImplementedException();
		}

		public PassiveExpression Implode_Ext(PassiveExpression expression)
		{
			throw new NotImplementedException();
		}

		public PassiveExpression Implode(PassiveExpression expression)
		{
			string s = expression.ToString().Trim();

			int index = 0;
			while (index < s.Length && (char.IsLetterOrDigit(s[index]) || "-_".IndexOf(s[index]) >= 0))
			{
				index++;
			}

			return PassiveExpression.Build(s.Substring(0, index));
		}

		public PassiveExpression Explode(PassiveExpression expression)
		{
			throw new NotImplementedException();
		}

		public PassiveExpression Numb(PassiveExpression expression)
		{
			if (expression == null || expression.IsEmpty)
				return new PassiveExpression();
			
		   return PassiveExpression.Build(Convert.ToInt32(expression.ToString()));
		}

		public PassiveExpression Symb(PassiveExpression expression)
		{
			throw new NotImplementedException();
		}

		public PassiveExpression Chr(PassiveExpression expression)
		{
			var args = new List<object>();

			foreach (object o in expression)
			{
				var v = (o is int) ? (char)Convert.ToByte(o) : o;
				args.Add(v);
			}

			return PassiveExpression.Build(args.ToArray());
		}

		public PassiveExpression Ord(PassiveExpression expression)
		{
			var args = new List<object>();

			foreach (object o in expression)
			{
				var v = (o is char) ? Convert.ToInt32(o) : o;
				args.Add(v);
			}

			return PassiveExpression.Build(args.ToArray());
		}

		public PassiveExpression Divmod(PassiveExpression expression)
		{
			throw new NotImplementedException();
		}

		public PassiveExpression First(PassiveExpression expression)
		{
			throw new NotImplementedException();
		}

		public PassiveExpression Putout(PassiveExpression expression)
		{
			throw new NotImplementedException();
		}
	}
}
