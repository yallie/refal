
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
		string source = null;
		int offset = 0;

		public bool ScanTokenAndProvideInfoAboutIt(TokenInfo tokenInfo, ref int state)
		{
			if (source == null || source.Length == 0)
				return false;

			if (tokenInfo != null)
			{
				tokenInfo.Type = TokenType.Comment;
				tokenInfo.Color = TokenColor.Comment;
				tokenInfo.StartIndex = offset;
				tokenInfo.EndIndex = ++offset;
			}

			source = source.Substring(1);
			return true;
		}

		public void SetSource(string source, int offset)
		{
			System.Windows.Forms.MessageBox.Show("Offset = " + offset + ", text:\r\n\r\n" + source);
			this.source = source;
			this.offset = offset;
		}
	}
}
