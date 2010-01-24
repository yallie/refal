
/*-------------------------------------------------------------------------*/
/*                                                                         */
/*      CodeBuilder, helper class for building the AST                     */
/*      This file is a part of Refal5.NET project                          */
/*      Project license: http://www.gnu.org/licenses/lgpl.html             */
/*      Written by Y [11-06-06] <yallie@yandex.ru>                         */
/*                                                                         */
/*      Copyright (c) 2006-2007 Alexey Yakovlev                            */
/*      All Rights Reserved                                                */
/*                                                                         */
/*-------------------------------------------------------------------------*/

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
			program.AddFunction(fun);
		}

		// start new function definition
		public void BeginFunction()
		{
			currentFunction = new DefinedFunction();
		}

		// mark current function as public
		public void MarkFunctionAsPublic()
		{
			currentFunction.IsPublic = true;
		}

		public void SetFunctionName(string name)
		{
			currentFunction.Name = name;

			// check for duplicate function definition
			if (program.Functions.Contains(name))
				SemErr("Duplicate definition of function " + name);

			program.AddFunction(currentFunction);

			// function 'Go' is an entry point
			if (name.ToLower() == "go")
				program.EntryPoint = currentFunction;
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
