using System;

namespace MarketOps.SystemData.Types
{
    /// <summary>
    /// System configuration data.
    /// </summary>
    public class SystemConfiguration
    {
        public DateTime tsFrom;
        public DateTime tsTo;
        public SystemDataDefinition dataDefinition;
    }
}
