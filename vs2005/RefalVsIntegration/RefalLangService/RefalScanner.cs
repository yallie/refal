
/*-------------------------------------------------------------------------*/
/*                                                                         */
/*      RefalScanner, implements IScanner for Refal5.NET language service  */
/*      This file is a part of Refal5.NET project                          */
/*      Project license: http://www.gnu.org/licenses/lgpl.html             */
/*      Written by Y [21-04-06] <yallie@yandex.ru>                         */
/*                                                                         */
/*      Copyright (c) 2006-2007 Alexey Yakovlev                            */
/*      All Rights Reserved                                                */
/*                                                                         */
/*-------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.Package;

namespace yallie.RefalLangService
{
	class RefalScanner : IScanner
	{
		// current line of the source code and scan position
		string source = null;
		int offset = 0;

		/// <summary>
		/// Correspondence table between regex patterns and token colors.
		/// First entries are matched first.
		/// </summary>
		static TokenTableEntry[] tokenTable = new TokenTableEntry[]
		{
			// comments
			new TokenTableEntry(ScanContext.Default, ScanContext.Default, @"\G\/\*.*?\*\/", TokenColor.Comment),
			new TokenTableEntry(ScanContext.Default, ScanContext.BlockComment, @"\G\/\*.*$", TokenColor.Comment),
			new TokenTableEntry(ScanContext.BlockComment, ScanContext.Default, @"\G.*?\*\/", TokenColor.Comment),
			new TokenTableEntry(ScanContext.BlockComment, ScanContext.BlockComment, @".+", TokenColor.Comment),
			new TokenTableEntry(ScanContext.Default, ScanContext.Default, @"^\s*\*.*$", TokenColor.Comment),

			// whitespace
			new TokenTableEntry(ScanContext.Default, ScanContext.Default, @"\G\s*", TokenColor.Text),

			// strings
			new TokenTableEntry(ScanContext.Default, ScanContext.Default, @"\G\""(\\.|[^""\\])*?\""", TokenColor.String),
			new TokenTableEntry(ScanContext.Default, ScanContext.Default, @"\G\'(\\.|[^'\\])*?\'", TokenColor.String),

			// free variables
			new TokenTableEntry(ScanContext.Default, ScanContext.Default, @"\G[ets]\.", TokenColor.Keyword),

			// keywords
			new TokenTableEntry(ScanContext.Default, ScanContext.Default, @"\G\$((ENTRY)|(EXTRN)|(EXTERN)|(EXTERNAL))", TokenColor.Keyword),
			new TokenTableEntry(ScanContext.Default, ScanContext.Default, @"\G((True)|(False))", TokenColor.Keyword),

			// numbers
			new TokenTableEntry(ScanContext.Default, ScanContext.Default, @"\G[\d]+(([Uu][Ll]?)|([Ll][Uu]?))?", TokenColor.Text),
			new TokenTableEntry(ScanContext.Default, ScanContext.Default, @"\G0[xX][\dA-Fa-f]+(([Uu][Ll]?)|([Ll][Uu]?))", TokenColor.Text),

			// identifiers
			new TokenTableEntry(ScanContext.Default, ScanContext.Default, @"\G[A-Za-z][A-Za-z\d_-]*", TokenColor.Text),

			// others
			new TokenTableEntry(ScanContext.Default, ScanContext.Default, @".", TokenColor.Text)
		};

		public bool ScanTokenAndProvideInfoAboutIt(TokenInfo tokenInfo, ref int state)
		{
			// is it the end?
			if (source == null || offset >= source.Length)
				return false;

			// find the match within the current scan context (text, comment, string, etc.)
			foreach (TokenTableEntry t in tokenTable)
			{
				if (t.InputContext != (ScanContext)state)
					continue;

				Match match = t.Regex.Match(source, offset);
				if (match.Success && match.Length > 0)
				{
					// set ouput context and token information
					state = (int)t.OutputContext;
					if (tokenInfo != null)
					{
						tokenInfo.Color = t.TokenColor;
						tokenInfo.Type = TokenType.Text;
						tokenInfo.StartIndex = offset;
						tokenInfo.EndIndex = offset + match.Length - 1;
					}

					// update offset
					offset += match.Length;
					return true;
				}
			}

			// no matches => skip one character
			if (tokenInfo != null)
			{
				tokenInfo.Color = TokenColor.Text;
				tokenInfo.Type = TokenType.Text;
				tokenInfo.StartIndex = offset;
				tokenInfo.EndIndex = offset + 1;
			}

			// increase current offset
			offset++;
			return true;
		}

		public void SetSource(string source, int offset)
		{
			this.source = source;
			this.offset = offset;
		}
	}

	/// <summary>
	/// Visual Studio scans input file line-by-line, so scanner can stop
	/// inside block comment, or character string.
	/// </summary>
	internal enum ScanContext
	{
		Default,
		BlockComment,
		String,
		Character,
		Error
	}

	/// <summary>
	/// This class stores information about patterns and token colors.
	/// </summary>
	internal class TokenTableEntry
	{
		readonly ScanContext inputContext, outputContext;
		readonly Regex regExpression;
		readonly TokenColor tokenColor;

		public TokenTableEntry(ScanContext inputContext, ScanContext outputContext, string pattern, TokenColor color)
		{
			this.inputContext = inputContext;
			this.outputContext = outputContext;
			this.regExpression = new Regex(/*"\\G" + */ pattern); // \G = "anchor to the current position"
			this.tokenColor = color;
		}

		public ScanContext InputContext
		{
			get { return inputContext; }
		}

		public ScanContext OutputContext
		{
			get { return outputContext; }
		}

		public Regex Regex
		{
			get { return regExpression; }
		}

		public TokenColor TokenColor
		{
			get { return tokenColor; }
		}
	}
}
