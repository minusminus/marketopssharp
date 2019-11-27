using System;
using System.Collections.Generic;
using MarketOps.StockData.Types;

namespace MarketOps.DataProvider.Pg
{
    /// <summary>
    /// gets table name for specified data range
    /// </summary>
    public class DataTableSelector
    {
        private void CheckInput(StockType stockType, StockDataRange dataRange, int intradayInterval)
        {
            if (stockType == StockType.Undefined)
                throw new Exception("Undefined stock type");
            if (dataRange == StockDataRange.Undefined)
                throw new Exception("Undefined data range");
            if ((dataRange == StockDataRange.Intraday) && (intradayInterval == 0))
                throw new Exception("Undefined intraday interval");
        }

        private readonly Dictionary<StockDataRange, string> _tableNameByDataRange = new Dictionary<StockDataRange, string>()
        {
            { StockDataRange.Daily, "at_dzienne" },
            { StockDataRange.Weekly, "at_tyg" },
            { StockDataRange.Monthly, "at_mies" },
            { StockDataRange.Tick, "at_ciagle" }
        };

        private string GetStdTableName(StockType stockType, StockDataRange dataRange)
        {
            return _tableNameByDataRange[dataRange] + ((int)stockType).ToString();
        }

        private string GetIntradayTableName(StockType stockType, int intradayInterval)
        {
            return $"at_intra{intradayInterval}m{(int)stockType}";
        }

        public string GetTableName(StockType stockType, StockDataRange dataRange, int intradayInterval)
        {
            CheckInput(stockType, dataRange, intradayInterval);
            if (dataRange == StockDataRange.Intraday)
                return GetIntradayTableName(stockType, intradayInterval);
            return GetStdTableName(stockType, dataRange);
        }
    }
}
