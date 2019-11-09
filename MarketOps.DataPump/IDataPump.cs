using System;
using MarketOps.StockData.Types;

namespace MarketOps.DataPump
{
    /// <summary>
    /// Interface for data pump mechanism
    /// </summary>
    public interface IDataPump
    {
        void Cleanup();
        void UpdateStartTS(StockType stockType);
        void PumpDaily(StockDefinition stockDefinition);
    }
}
