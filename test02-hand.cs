
/* test02 - translated to C# by hand */

namespace Refal
{
	class Test02
	{
		static void Main()
		{
			Go(null);
		}

		public static object Go(object expression)
		{
			if (Sys.Match(expression, null))
				return Sys.Prout("Hello");

			throw new RecognitionImpossibleException("Recognition impossible in Test02.Go()");
		}
	}

	class Sys
	{
		public static bool Match(object expression, object pattern)
		{
			if (expression == pattern)
				return true;

			return false;
		}

		public static object Prout(object expression)
		{
			System.Console.WriteLine("{0}", expression);

			return null;
		}
	}

	public class RecognitionImpossibleException : System.Exception
	{
		public RecognitionImpossibleException(string msg) : base(msg)
		{
		}
	}
}