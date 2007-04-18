
using System;
using System.IO;
using System.Text;
using System.Collections;

namespace Refal.Runtime
{
	public class RefalBase
	{
		// Refal file I/O support hash: handle -> StreamReader/StreamWriter
		private static Hashtable openFiles = new Hashtable();

		// bury/dig functions global expression storage
		protected static Hashtable buriedExpressions = new Hashtable();

		// command line arguments
		protected static string[] commandLineArguments = null;

		public static bool Match(PassiveExpression expression, Pattern pattern)
		{
			if (expression == null || expression.IsEmpty)
			{
				// empty expression matches empty pattern
				if (pattern == null || pattern.IsEmpty)
					return true;

				// any expression matches the pattern consisting of a
				// single expression variable (like e.1)
				if (pattern.Count == 1 && pattern[0] is ExpressionVariable)
				{
					// bind free variable to an expression
					ExpressionVariable var = (ExpressionVariable)pattern[0];
					var.Expression = expression;
					return true;
				}

				// expression is empty, pattern isn't
				return false;
			}

			// pattern is empty while expression isn't
			if (pattern == null || pattern.IsEmpty)
				return false;

			// handle simple cases
			if (pattern.Count == 1)
			{
				if (pattern[0] is ExpressionVariable)
				{
					// bind free variable to an expression
					ExpressionVariable var = (ExpressionVariable)pattern[0];
					var.Expression = expression;
					return true;
				}

				// check for single symbol or single term
				if (expression.Count == 1)
				{
					// symbol matches symbol
					if (pattern[0].Equals(expression[0]) && !(pattern[0] is Variable))
						return true;

					// term ::= symbol | (expression)
					if (expression[0] is PassiveExpression && pattern[0] is TermVariable)
					{
						// bind free variable to an expression
						TermVariable var = (TermVariable)pattern[0];
						PassiveExpression expr = (PassiveExpression)expression[0];
						var.Expression = expr;
						return true;
					}

					// either term or symbol
					if (pattern[0] is Variable && !(expression[0] is PassiveExpression))
					{
						Variable var = (Variable)pattern[0];
						var.Value = expression[0];
						return true;
					}
				}
			}

			// both pattern and expression are not empty, perform non-trivial matching
			return PatternMatchHelper.Match(expression, pattern);
		}

		// Standard RTL routines

		public static PassiveExpression _Print(PassiveExpression expression)
		{
			if (expression == null)
				return null;

			Console.WriteLine("{0}", ExpressionToString(expression, 0));

			return expression;
		}

		public static PassiveExpression _Prout(PassiveExpression expression)
		{
			if (expression == null)
				return null;

			Console.WriteLine("{0}", ExpressionToString(expression, 0));

			return null;
		}

		private static string ExpressionToString(PassiveExpression expression, int startIndex)
		{
			StringBuilder sb = new StringBuilder();

			for (int i = startIndex; i < expression.Count; i++)
			{
				object value = expression[i];
				sb.Append(value.ToString());

				if (!(value is char))
					sb.Append(' ');
			}

			return sb.ToString();
		}

		public static PassiveExpression _Card(PassiveExpression expression)
		{
			string s = Console.ReadLine();

			if (s != null)
				return PassiveExpression.Build(s.ToCharArray());
			else
				return PassiveExpression.Build(0);
		}

		public static PassiveExpression _Open(PassiveExpression expression)
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
				fileName = ExpressionToString(expression, 2);
			}

			// R - read, W - write, A - append
			if (mode.StartsWith("R"))
				openFiles[handle] = new StreamReader(File.OpenRead(fileName));
			else if (mode.StartsWith("W"))
				openFiles[handle] = new StreamWriter(File.Create(fileName));
			else if (mode.StartsWith("A"))
			{
				openFiles[handle] = File.AppendText(fileName);
			}
			else
			{
				throw new NotSupportedException("Bad file open mode: " + mode + " (R, W, or A expected)");
			}

			// AFAIK, Open don't return anything
			return null;
		}

		public static PassiveExpression _Get(PassiveExpression expression)
		{
			if (expression == null || expression.IsEmpty)
				return _Card(expression);

			string handle = expression[0].ToString();
			StreamReader sr = openFiles[handle] as StreamReader;

			if (sr == null)
				return _Card(expression);

			string s = sr.ReadLine();
			if (s != null)
				return PassiveExpression.Build(s.ToCharArray());
			else
				return PassiveExpression.Build(0);
		}

		public static PassiveExpression _Put(PassiveExpression expression)
		{
//			return Prout(expression);

			if (expression == null || expression.IsEmpty)
				return _Prout(expression);

			string handle = expression[0].ToString();
			StreamWriter sw = openFiles[handle] as StreamWriter;

			if (sw == null)
				return _Prout(expression);

			sw.WriteLine(ExpressionToString(expression, 1)); // sw Console

			PassiveExpression result = PassiveExpression.Build(expression);
			result.Remove(result[0]);
			return result;
		}

		protected static void CloseFiles()
		{
			foreach (object o in openFiles.Values)
			{
				if (o is StreamWriter)
					(o as StreamWriter).Close();
				else if (o is StreamReader)
					(o as StreamReader).Close();
			}
		}

		protected static string[] CommandLineArguments
		{
			get { return commandLineArguments; }
			set { commandLineArguments = value; }
		}

		public static PassiveExpression _Arg(PassiveExpression expression)
		{
			if (expression == null || expression.IsEmpty || commandLineArguments == null)
				return new PassiveExpression();

			int index = Convert.ToInt32(expression[0]) - 1; // in Refal, index is 1-based

			if (index >= commandLineArguments.Length)
			{
				return new PassiveExpression();
			}

			return PassiveExpression.Build(commandLineArguments[index].ToCharArray());
		}

		public static PassiveExpression _Br(PassiveExpression expression)
		{
			// <Br N '=' Expr>, where N is expression which does not
			// include '=' on the upper level of the bracket's structure
			// my restriction: N is a single value
			throw new NotImplementedException();
		}

		public static PassiveExpression _Dg(PassiveExpression expression)
		{
			// <Dg N>, where N is expression
			// my restriction: N is a single value
			throw new NotImplementedException();
		}

		public static PassiveExpression _Dgall(PassiveExpression expression)
		{
			throw new NotImplementedException();
		}

		public static PassiveExpression _Cp(PassiveExpression expression)
		{
			throw new NotImplementedException();
		}

		public static PassiveExpression _Rp(PassiveExpression expression)
		{
			throw new NotImplementedException();
		}

		public static PassiveExpression _Type(PassiveExpression expression)
		{
			throw new NotImplementedException();
		}

		public static PassiveExpression _Mu(PassiveExpression expression)
		{
			throw new NotImplementedException();
		}

		public static PassiveExpression _Implode_Ext(PassiveExpression expression)
		{
			throw new NotImplementedException();
		}

		public static PassiveExpression _Implode(PassiveExpression expression)
		{
			throw new NotImplementedException();
		}

		public static PassiveExpression _Explode(PassiveExpression expression)
		{
			throw new NotImplementedException();
		}

		public static PassiveExpression _Add(PassiveExpression expression)
		{
			throw new NotImplementedException();
		}

		public static PassiveExpression _Mul(PassiveExpression expression)
		{
			throw new NotImplementedException();
		}

		public static PassiveExpression _Numb(PassiveExpression expression)
		{
			throw new NotImplementedException();
		}

		public static PassiveExpression _Symb(PassiveExpression expression)
		{
			throw new NotImplementedException();
		}

		public static PassiveExpression _Chr(PassiveExpression expression)
		{
			throw new NotImplementedException();
		}

		public static PassiveExpression _Ord(PassiveExpression expression)
		{
			throw new NotImplementedException();
		}

		public static PassiveExpression _Divmod(PassiveExpression expression)
		{
			throw new NotImplementedException();
		}

		public static PassiveExpression _First(PassiveExpression expression)
		{
			throw new NotImplementedException();
		}

		public static PassiveExpression _Putout(PassiveExpression expression)
		{
			throw new NotImplementedException();
		}
	}

	public class RecognitionImpossibleException : Exception
	{
		public RecognitionImpossibleException() : base()
		{
		}

		public RecognitionImpossibleException(string msg) : base(msg)
		{
		}
	}
}

