using System;
using System.Collections;

namespace Refal
{
	public class CodeBuilder
	{
		Program program = new Program();
		DefinedFunction currentFunction = null;

		public CodeBuilder()
		{
		}

		public Program Program
		{
			get { return program; }
		}

		public void AddExternalFunction(string name)
		{
			Function fun = new ExternalFunction(name);
			program.Functions[name] = fun;
		}

		// start new function definition
		public void BeginFunction()
		{
			currentFunction = new DefinedFunction();
		}

		// mark current function as program entry point
		public void SetEntryPoint()
		{
			// TODO: add check: if entryPoint != null, then more than one entry point found
			program.EntryPoint = currentFunction;
		}

		public void SetFunctionName(string name)
		{
			currentFunction.Name = name;
			// TODO: add check: if program.Function.Contains(name) then duplicate definition of name
			program.Functions[name] = currentFunction;
		}

		public void EndFunction(Block block)
		{
			currentFunction.Block = block;
			currentFunction = null;
		}
	}
}
