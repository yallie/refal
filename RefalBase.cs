
using System;
using System.Text;
using System.Collections;

namespace Refal
{
	public class RefalBase
	{
		public static bool Match(PassiveExpression expression, object pattern)
		{
			if (expression == pattern)
				return true;

			return false;
		}

		public static PassiveExpression Prout(PassiveExpression expression)
		{
			if (expression == null)
				return null;

			StringBuilder sb = new StringBuilder();

			foreach (object value in expression)
			{
				sb.Append(value.ToString());

				if (!(value is char))
					sb.Append(' ');
			}

			Console.WriteLine("{0}", sb.ToString());

			return null;
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

