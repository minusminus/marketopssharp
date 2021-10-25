using System;
using System.Reflection;

namespace MarketOps.Config
{
    /// <summary>
    /// Class type loader.
    /// </summary>
    internal static class ClassesLoader
    {
        public static Type FindType(string libraryName, string className)
        {
            Type res = Assembly.LoadFrom(libraryName).GetType(className);
            if (res == null)
                throw new Exception($"Class {className} not found in {libraryName}");
            return res;
        }
    }
}
