// Guids.cs
// MUST match guids.h
using System;

namespace yallie.RefalLangService
{
    static class GuidList
    {
        public const string guidRefalLangServicePkgString = "4bc150f9-c2fd-4062-93bd-e57d5fc652fb";
        public const string guidRefalLangServiceCmdSetString = "8c0af5c8-60a2-4120-b53f-c526f6a0edd1";

        public static readonly Guid guidRefalLangServicePkg = new Guid(guidRefalLangServicePkgString);
        public static readonly Guid guidRefalLangServiceCmdSet = new Guid(guidRefalLangServiceCmdSetString);
    };
}