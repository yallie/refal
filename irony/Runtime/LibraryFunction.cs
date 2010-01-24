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
	public class LibraryFunction : ICallTarget
	{
		readonly string name;
		readonly LibraryDelegate function;

		delegate PassiveExpression LibraryDelegate(PassiveExpression value);

		private LibraryFunction(string n, LibraryDelegate fun)
		{
			name = n;
			function = fun;
		}

		public string Name
		{
			get { return name; }
		}

		public void Call(EvaluationContext context)
		{
			context.PushFrame(name, null, context.CurrentFrame);

			var ex = function(context.Data.Pop() as PassiveExpression);
			if (ex != null)
				context.Data.Push(ex);

			context.PopFrame();
		}

		public override string ToString()
		{
			return "refal function: " + Name;
		}

		public static LibraryFunction[] ExtractLibraryFunctions(object instance)
		{
			if (instance == null)
				return new LibraryFunction[0];

			var list = new List<LibraryFunction>();

			MethodInfo[] methods = instance.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
			foreach (MethodInfo method in methods)
			{
				LibraryDelegate fun = (LibraryDelegate)Delegate.CreateDelegate(typeof(LibraryDelegate), instance, method.Name, false, false);
				if (fun != null)
				{
					list.Add(new LibraryFunction(method.Name, fun));
				}
			}

			return list.ToArray();
		}
	}
}
