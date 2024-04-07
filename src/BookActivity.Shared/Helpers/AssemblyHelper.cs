using System;
using System.IO;
using System.Reflection;

namespace BookActivity.Shared.Helpers
{
    public static class AssemblyHelper
    {
        public static string CurrentAssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().Location;
                return Path.GetDirectoryName(codeBase);
            }
        }
    }
}
