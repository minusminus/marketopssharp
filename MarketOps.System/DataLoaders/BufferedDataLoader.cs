using System;
using System.Collections.Generic;
using System.Linq;
using MarketOps.StockData.Interfaces;
using MarketOps.StockData.Types;
using MarketOps.System.Interfaces;

namespace MarketOps.System.DataLoaders
{
    /// <summary>
    /// Stock data loader with buffer.
    /// 
    /// Each get resizes buffered data to fit required boundaries.
    /// </summary>
    internal class BufferedDataLoader : IDataLoader
    {
        private readonly IStockDataProvider _dataProvider;
        private readonly Dictionary<string, StockPricesData> _buffer = new Dictionary<string, StockPricesData>();

        public BufferedDataLoader(IStockDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }

        public StockPricesData Get(string stockName, StockDataRange dataRange, int intradayInterval, DateTime tsFrom, DateTime tsTo)
        {
            string key = BufferKey(stockName, dataRange, intradayInterval);
            StockPricesData res;
            if (TryGetFromBuffer(key, tsFrom, tsTo, out res)) return res;
            if (res == null)
                res = LoadData(stockName, dataRange, intradayInterval, tsFrom, tsTo);
            else
                res = LoadData(stockName, dataRange, intradayInterval, tsFrom < res.TS.First() ? tsFrom : res.TS.First(), tsTo > res.TS.Last() ? tsTo : res.TS.Last());
            AddOrReplaceInBuffer(key, res);
            return res;
        }

        private string BufferKey(string stockName, StockDataRange dataRange, int intradayDataRange) => $"{stockName}_{dataRange}_{intradayDataRange}";

        private bool TryGetFromBuffer(string key, DateTime tsFrom, DateTime tsTo, out StockPricesData res)
        {
            if (!_buffer.TryGetValue(key, out res)) return false;
            if ((res.TS.First() > tsFrom) || (res.TS.Last() < tsTo)) return false;
            return true;
        }

        private void AddOrReplaceInBuffer(string key, StockPricesData data)
        {
            _buffer.Remove(key);
            _buffer.Add(key, data);
        }

        private StockPricesData LoadData(string stockName, StockDataRange dataRange, int intradayInterval, DateTime tsFrom, DateTime tsTo)
        {
            StockDefinition stock = _dataProvider.GetStockDefinition(stockName);
            return _dataProvider.GetPricesData(stock, dataRange, intradayInterval, tsFrom, tsTo);
        }
    }
}
