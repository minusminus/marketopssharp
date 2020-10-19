using System;

namespace MarketOps.SystemExecutor
{
    /// <summary>
    /// System configuration data.
    /// </summary>
    internal class SystemConfiguration
    {
        public DateTime tsFrom;
        public DateTime tsTo;
        public SystemDataDefinition dataDefinition;
    }
}
