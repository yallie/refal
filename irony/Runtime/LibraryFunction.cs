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
	using IronySymbol = Irony.Parsing.Symbol;
	using Irony.Parsing;

	/// <summary>
	/// LibraryFunction is a function defined in the standard library and available to Refal program
	/// </summary>
	public class LibraryFunction : ICallTarget
	{
		public IronySymbol Name { get; private set; }

		private LibraryDelegate Function { get; set; }

		delegate PassiveExpression LibraryDelegate(PassiveExpression value);

		private LibraryFunction(IronySymbol n, LibraryDelegate fun)
		{
			Name = n;
			Function = fun;
		}

		public void Call(EvaluationContext context)
		{
			context.PushFrame(Name.Text, null, context.CurrentFrame);

			var ex = Function(context.Data.Pop() as PassiveExpression);
			if (ex != null)
				context.Data.Push(ex);

			context.PopFrame();
		}

		public static LibraryFunction[] ExtractLibraryFunctions(SymbolTable symbols, EvaluationContext context, object instance)
		{
			if (instance == null)
				return new LibraryFunction[0];

			var list = new List<LibraryFunction>();

			MethodInfo[] methods = instance.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
			foreach (MethodInfo method in methods)
			{
				var fun = method.CreateDelegate<LibraryDelegate>(instance, false);
				if (fun != null)
				{
					var fname = method.GetCustomAttribute<FunctionNameAttribute>();
					IronySymbol name = symbols.TextToSymbol(fname != null ? fname.Name : method.Name);
					name = context.LanguageCaseSensitive ? name : name.LowerSymbol;
					list.Add(new LibraryFunction(name, fun));
				}
			}

			return list.ToArray();
		}

		public override string ToString()
		{
			return "refal function: " + Name;
		}
	}
}
