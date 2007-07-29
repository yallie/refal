using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.Package;

namespace yallie.RefalProject
{
	class RefalProjectNode : ProjectNode
	{
		RefalProjectPackage package;

		public RefalProjectNode(RefalProjectPackage package) : base()
		{
			this.package = package;
		}

		public override Guid ProjectGuid
		{
			get { return typeof(RefalProjectFactory).GUID; }
		}

		public override string ProjectType
		{
			get { return "Refal5 Project"; }
		}
	}
}
