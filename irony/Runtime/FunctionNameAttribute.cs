using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace Refal.Runtime
{
	/// <summary>
	/// FunctionName attribute is used to specify Refal name for run-time library method written in C#
	/// </summary>
	[AttributeUsage(AttributeTargets.Method)]
	class FunctionNameAttribute : Attribute
	{
		public string Name { get; private set; }

		public FunctionNameAttribute(string name)
		{
			Name = name;
		}
	}
}
