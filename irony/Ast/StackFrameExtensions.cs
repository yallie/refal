using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Irony.Interpreter;

namespace Refal
{
	/// <summary>
	/// Refal stack frame should be able to store the last recognized patterns
	/// </summary>
	public static class StackFrameExtensions
	{
		private static IDictionary<StackFrame, Refal.Runtime.Pattern> LastPatterns { get; set; }

		static StackFrameExtensions()
		{
			LastPatterns = new Dictionary<StackFrame,Refal.Runtime.Pattern>();
		}

		public static Refal.Runtime.Pattern GetLastPattern(this StackFrame frame)
		{
			if (LastPatterns.ContainsKey(frame))
				return LastPatterns[frame];

			return null;
		}

		public static void SetLastPattern(this StackFrame frame, Refal.Runtime.Pattern pattern)
		{
			LastPatterns[frame] = pattern;
		}
	}
}
