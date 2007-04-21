
/*-------------------------------------------------------------------------*/
/*                                                                         */
/*      RefalLanguageService, implements Refal5.NET language service       */
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
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Package;

namespace yallie.RefalLangService
{
	/// <summary>
	/// Implements language service that provides syntax highlighting
	/// for refal source files.
	/// </summary>
	[ComVisible(true)]
	[Guid(GuidList.guidRefalLangServiceClassString)]
	class RefalLanguageService : LanguageService
	{
		private RefalScanner scanner = null;
		private LanguagePreferences preferences = null;

		public override LanguagePreferences GetLanguagePreferences()
		{
			if (preferences == null)
				preferences = new LanguagePreferences(this.Site, typeof(RefalLanguageService).GUID, Name);

			return preferences;
		}

		public override IScanner GetScanner(Microsoft.VisualStudio.TextManager.Interop.IVsTextLines buffer)
		{
			if (scanner == null)
				scanner = new RefalScanner();

			return scanner;
		}

		public override string Name
		{
			get { return "Refal5 Language Service"; }
		}

		public override AuthoringScope ParseSource(ParseRequest req)
		{
			throw new NotImplementedException("Source parsing is not supported");
		}
	}
}
