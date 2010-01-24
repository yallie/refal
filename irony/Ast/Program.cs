using System;
using Irony.Ast;
using Irony.Parsing;
using System.Collections.Generic;
using Irony.Interpreter;
using Refal.Runtime;

namespace Refal
{
	public class Program : SyntaxNode
	{
		IDictionary<string, Function> functions = new Dictionary<string, Function>();
		IList<Function> functionList = new List<Function>();
		Function entryPoint = null;
		string name = "Program";

		public override void Init(ParsingContext context, ParseTreeNode parseNode)
		{
			base.Init(context, parseNode);

			foreach (ParseTreeNode node in parseNode.ChildNodes)
			{
				if (node.AstNode is Function)
				{
					AddFunction(node.AstNode as Function);
				}
				else if (node.AstNode is ExternalFunctionList)
				{
					foreach (IdentifierNode id in (node.AstNode as ExternalFunctionList).Identifiers)
					{
						ExternalFunction ef = new ExternalFunction();
						ef.SetSpan(id.Span);
						ef.Name = id.Symbol;
						AddFunction(ef);
					}
				}
			}
		}

		public override System.Collections.IEnumerable GetChildNodes()
		{
			foreach (var fun in FunctionList)
				yield return fun;
		}

		public IDictionary<string, Function> Functions
		{
			get { return functions; }
		}

		public IList<Function> FunctionList
		{
			get { return functionList; }
		}

		public Function EntryPoint
		{
			get { return entryPoint; }
			set { entryPoint = value; }
		}

		public void AddFunction(Function function)
		{
			functions[function.Name] = function;
			functionList.Add(function);
			
			if (function.Name == "Go")
			{
				entryPoint = function;
			}
		}

		public string Name
		{
			get { return name; }
			set
			{
				if (value != null && value.IndexOf(".") >= 0)
					value = value.Substring(0, value.IndexOf("."));

				if (value != null && value.Length > 0)
					name = value;
			}
		}

		public override void Evaluate(EvaluationContext context, AstMode mode)
		{
			if (EntryPoint == null)
				context.ThrowError(this, "No entry point defined (entry point is a function named «Go»)");

			// load standard run-time library functions
			var libraryFunctions = LibraryFunction.ExtractLibraryFunctions(new RefalLibrary(context));
			foreach (LibraryFunction libFun in libraryFunctions)
			{
				context.SetValue(libFun.Name, libFun);
			}

			// define all functions
			foreach (Function fun in FunctionList)
			{
				fun.Evaluate(context, mode);
			}

			// call entry point with empty expression as an argument
			context.Data.Push(Runtime.PassiveExpression.Build());
			EntryPoint.Call(context);

			// discard execution results
			context.Data.Pop();
			context.ClearLastResult();
		}

		public override string ToString()
		{
			return "Refal-5 program";
		}
	}
}
