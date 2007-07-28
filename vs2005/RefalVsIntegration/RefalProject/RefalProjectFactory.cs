using System;
using System.Text;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Package;

namespace yallie.RefalProject
{
	[Guid(GuidList.guidRefalProjectFactory)]
	public class RefalProjectFactory : ProjectFactory
	{
		public RefalProjectFactory(RefalProjectPackage package) : base(package)
        {
        }

		protected override ProjectNode CreateProject()
		{
			RefalProjectNode project = new RefalProjectNode(this.Package as RefalProjectPackage);
//?			project.SetSite((IOleServiceProvider)((IServiceProvider)this.Package).GetService(typeof(IOleServiceProvider)));
			return project;
		}
	}
}
