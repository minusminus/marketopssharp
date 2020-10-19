using MarketOps.StockData.Interfaces;
using MarketOps.StockData.Types;
using MarketOps.SystemExecutor.Interfaces;
using System;
using System.Collections.Generic;

namespace MarketOps.SystemExecutor.Processor
{
    /// <summary>
    /// Preloads stocks data and calculates defined stats.
    /// </summary>
    internal class StocksDataPreloader
    {
        private readonly IStockDataProvider _dataProvider;
        private readonly ISystemDataLoader _dataLoader;

        public StocksDataPreloader(IStockDataProvider dataProvider, ISystemDataLoader dataLoader)
        {
            _dataProvider = dataProvider;
            _dataLoader = dataLoader;
        }

        public void PreloadDataAndPrecalcStats(DateTime tsFrom, DateTime tsTo, List<(SystemStockDataDefinition stock, int max)> backBufferInfo)
        {
            foreach (var backBuf in backBufferInfo)
            {
                DateTime tsMovedBack = _dataProvider.GetNearestTickGETicksBefore(_dataProvider.GetStockDefinition(backBuf.stock.stock.Name), backBuf.stock.dataRange, 0, tsFrom, backBuf.max);
                StockPricesData stockPricesData = _dataLoader.Get(backBuf.stock.stock.Name, backBuf.stock.dataRange, 0, tsMovedBack, tsTo);
                foreach (var stat in backBuf.stock.stats)
                    stat.Calculate(stockPricesData);
            }
        }
    }
}
