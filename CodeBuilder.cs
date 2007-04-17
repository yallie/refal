using System;
using System.Collections;

namespace Refal
{
	public class CodeBuilder
	{
		Program program = new Program();
		DefinedFunction currentFunction = null;
		Block currentBlock = null;
		Stack blocks = new Stack();
		Sentence currentSentence = null;
		Pattern currentPattern = null;
		Expression currentExpression = null;

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

		public Block BeginBlock()
		{
			// if block is nested, save parent block
			if (currentBlock != null)
				blocks.Push(currentBlock);

			currentBlock = new Block();
			return currentBlock;
		}

		public void EndBlock()
		{
			// if block is nested, restore parent block
			if (blocks.Count == 0)
				currentBlock = null;
			else
				currentBlock = (Block)blocks.Pop();
		}

		public void BeginSentence()
		{
			currentSentence = new Sentence();
			currentBlock.Sentences.Add(currentSentence);
		}

		public void EndSentence()
		{
			currentSentence = null;
		}

		public Pattern BeginPattern()
		{
			currentPattern = new Pattern();
			return currentPattern;
		}

		public void EndPattern()
		{
			currentPattern = null;
		}

		public Expression BeginExpression()
		{
			currentExpression = new Expression();
			return currentExpression;
		}

		public void EndExpression()
		{
			currentExpression = null;
		}
	}
}
