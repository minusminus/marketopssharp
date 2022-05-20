using System;
using System.Collections.Generic;
using MarketOps.SystemData.Interfaces;
using MarketOps.StockData.Types;
using MarketOps.StockData.Extensions;
using MarketOps.Stats.Stats;
using MarketOps.StockData.Interfaces;
using MarketOps.SystemData.Types;

namespace MarketOps.SystemDefs.StrongBBtrendStocks
{
    /// <summary>
    /// Signals for strong trends stocks.
    /// Long positions only.
    /// </summary>
    internal class SignalsStrongBBTrendStocksMW : ISystemDataDefinitionProvider, ISignalGeneratorOnClose
    {
        private const StockDataRange EntrySignalDataRange = StockDataRange.Monthly;
        private const StockDataRange PositionDataRange = StockDataRange.Weekly;


    }
}
