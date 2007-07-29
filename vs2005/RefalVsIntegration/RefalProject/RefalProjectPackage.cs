// VsPkg.cs : Implementation of RefalProject
//

using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.ComponentModel.Design;
using Microsoft.Win32;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Package;

namespace yallie.RefalProject
{
	// In order be loaded inside Visual Studio in a machine that has not the VS SDK installed, 
	// package needs to have a valid load key (it can be requested at 
	// http://msdn.microsoft.com/vstudio/extend/). This attributes tells the shell that this 
	// package has a load key embedded in its resources.
	[ProvideLoadKey("Standard", "1.0", "Visual Studio Integration of Refal5 Project System", "yallie", 1)]
	[DefaultRegistryRoot("Software\\Microsoft\\VisualStudio\\8.0Exp")]
	[PackageRegistration(UseManagedResourcesOnly = true)]
	[InstalledProductRegistration(true, "#110", "#112", "1.0", IconResourceID = 400)]
	[ProvideProjectFactory(typeof(RefalProjectFactory), "Refal5", "Refal5 Project Files (*.refproj);*.refproj", "refproj", "refproj", "Refal5", LanguageVsTemplate = "Refal5")]
//	[ProvideObject(typeof(GeneralPropertyPage))]
	[Guid(GuidList.guidRefalProjectPkgString)]
	public sealed class RefalProjectPackage : ProjectPackage
	{
		/// <summary>
		/// Default constructor of the package.
		/// Inside this method you can place any initialization code that does not require 
		/// any Visual Studio service because at this point the package object is created but 
		/// not sited yet inside Visual Studio environment. The place to do all the other 
		/// initialization is the Initialize method.
		/// </summary>
		public RefalProjectPackage()
		{
		}

		/// <summary>
		/// Initialization of the package; this method is called right after the package is sited, so this is the place
		/// where you can put all the initilaization code that rely on services provided by VisualStudio.
		/// </summary>
		protected override void Initialize()
		{
			base.Initialize();
			this.RegisterProjectFactory(new RefalProjectFactory(this));
		}
	}
}