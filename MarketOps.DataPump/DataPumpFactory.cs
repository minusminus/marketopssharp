using System;
using MarketOps.DataPump.Types;

namespace MarketOps.DataPump
{
    /// <summary>
    /// Factory creating specified data pump mechanism
    /// </summary>
    public class DataPumpFactory
    {
        public static IDataPump Get(DataPumpType dataPumpType)
        {
            return new Bossa.DataPump(null, null, null, null, null, null);
        }
    }
}
