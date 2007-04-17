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
		public void SetFunctionAsEntryPoint()
		{
			// check for duplicate entry point definition
			if (program.EntryPoint != null)
				SemErr("Only one entry point allowed, current entry point is " + program.EntryPoint.Name);

			program.EntryPoint = currentFunction;
		}

		public void SetFunctionName(string name)
		{
			currentFunction.Name = name;

			// check for duplicate function definition
			if (program.Functions.Contains(name))
				SemErr("Duplicate function definition: " + name);

			program.Functions[name] = currentFunction;
		}

		public void EndFunction(Block block)
		{
			currentFunction.Block = block;
			currentFunction = null;
		}

		public void SemErr(string msg)
		{
			Parser.SemErr(msg);
		}
	}
}
