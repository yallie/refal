// Guids.cs
// MUST match guids.h
using System;

namespace yallie.RefalProject
{
    static class GuidList
    {
		public const string guidRefalProjectFactory = "1E7CF41F-F040-49e5-96B9-E52592F09569";

		public const string guidRefalProjectPkgString = "140f69b3-02ba-43af-8912-dc237332e4a8";
        public const string guidRefalProjectCmdSetString = "7faf7b0d-6367-4220-9ad8-1b45b980d57e";

        public static readonly Guid guidRefalProjectPkg = new Guid(guidRefalProjectPkgString);
        public static readonly Guid guidRefalProjectCmdSet = new Guid(guidRefalProjectCmdSetString);
    };
}