
/*-------------------------------------------------------------------------*/
/*                                                                         */
/*      Refal console driver, startup class of the application             */
/*      This file is a part of Refal5.NET project                          */
/*      Project license: http://www.gnu.org/licenses/lgpl.html             */
/*      Written by Y [11-06-06] <yallie@yandex.ru>                         */
/*                                                                         */
/*      Copyright (c) 2006-2007 Alexey Yakovlev                            */
/*      All Rights Reserved                                                */
/*                                                                         */
/*-------------------------------------------------------------------------*/

using System;
using System.IO;
using System.CodeDom;
using System.CodeDom.Compiler;
using Microsoft.CSharp;

namespace Refal
{
	class RefalCompiler
	{
		static void Main(string[] args)
		{
			if (args.Length < 1)
				Console.WriteLine("Syntax: {0} filename", "refal.exe");
			else
			{
				try
				{
					string fileName = args[0];

					Scanner.Init(fileName);
					Parser.Parse();

					if (Errors.count == 0)
					{
						GenerateCode(Parser.Program, fileName);
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine("Exception: {0}\nMessage: {1}\nStack trace:\n{2}", 
						ex.GetType(), ex.Message, ex.StackTrace);
				}
				
			}
		}

		static void GenerateCode(Program program, string fileName)
		{
			//FormatCodeVisitor visitor = new FormatCodeVisitor();
			CSharpCodeVisitor visitor = new CSharpCodeVisitor();
			program.Name = fileName;
			program.Accept(visitor);
			Console.WriteLine(visitor.Text);

			/*/ Framework 1.1 version
			CodeDomProvider provider = new CSharpCodeProvider();
			ICodeGenerator gen = provider.CreateGenerator();
			using (IndentedTextWriter tw = new IndentedTextWriter(new StreamWriter("_output_.cs", false), "\t"))
			{
				gen.GenerateCodeFromCompileUnit(code, tw, new CodeGeneratorOptions());
				tw.Close();
			}//*/

			/* Framework 2.0 BETA version
			CodeDomProvider provider = new CSharpCodeProvider();
			using (IndentedTextWriter tw = new IndentedTextWriter(new StreamWriter("_output_.cs", false), "\t"))
			{
				provider.GenerateCodeFromCompileUnit(code, tw, new CodeGeneratorOptions());
				tw.Close();
			}//*/

			/* Framework 2.0 BETA + MSIL provider
			CodeDomProvider provider = new Microsoft.Msil.MsilCodeProvider();
			using (IndentedTextWriter tw = new IndentedTextWriter(new StreamWriter("_output_.il", false), "\t"))
			{
				provider.GenerateCodeFromCompileUnit(code, tw, new CodeGeneratorOptions());
				tw.Close();
			}//*/
		}
	}
}
