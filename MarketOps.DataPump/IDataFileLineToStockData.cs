using System;
using MarketOps.DataPump.Types;

namespace MarketOps.DataPump
{
    /// <summary>
    /// Interface for data file lines mapping to DataPumpStockData.
    /// 
    /// Map return data constraints:
    /// floats - with dot as separator
    /// ints - with no separator
    /// timestamp - as correct datetime
    /// </summary>
    internal interface IDataFileLineToStockData
    {
        void Map(string dataFileLine, DataPumpStockData stockData);
    }
}
