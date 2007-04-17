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
					Scanner.Init(args[0]);
					Parser.Parse();
//					GenerateCode(Parser.Code);
				}
				catch (Exception ex)
				{
					Console.WriteLine("Exception: {0}\nMessage: {1}\nStack trace:\n{2}", 
						ex.GetType(), ex.Message, ex.StackTrace);
				}
				
			}
		}

		static void GenerateCode(CodeCompileUnit code)
		{
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