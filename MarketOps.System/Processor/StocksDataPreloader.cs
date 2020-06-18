using MarketOps.StockData.Interfaces;
using MarketOps.StockData.Types;
using MarketOps.System.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketOps.System.Processor
{
    /// <summary>
    /// Preloads stocks data and calculates defined stats.
    /// </summary>
    internal class StocksDataPreloader
    {
        private readonly IStockDataProvider _dataProvider;
        private readonly IDataLoader _dataLoader;

        public StocksDataPreloader(IStockDataProvider dataProvider, IDataLoader dataLoader)
        {
            _dataProvider = dataProvider;
            _dataLoader = dataLoader;
        }

        public void PreloadAndPrecalc(DateTime tsFrom, DateTime tsTo, Dictionary<SystemStockDataDefinition, int> backBufferInfo, SystemDataDefinition systemDataDefinition)
        {
            foreach (var backBuf in backBufferInfo)
            {
                DateTime tsMovedBack = _dataProvider.GetNearestTickGETicksBefore(_dataProvider.GetStockDefinition(backBuf.Key.name), backBuf.Key.dataRange, 0, tsFrom, backBuf.Value);
                StockPricesData stockPricesData = _dataLoader.Get(backBuf.Key.name, backBuf.Key.dataRange, 0, tsMovedBack, tsTo);
                foreach (var stat in systemDataDefinition.statsForStocks[backBuf.Key])
                    stat.Calculate(stockPricesData);
            }                  
        }
    }
}
