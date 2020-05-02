using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketOps.System;
using MarketOps.System.Interfaces;
using MarketOps.StockData.Types;
using MarketOps.StockData.Extensions;

namespace MarketOps.SignalGenerators
{
    /// <summary>
    /// Signal on price crossing sma up or down.
    /// </summary>
    public class PriceCrossingSMA : ISignalGeneratorOnClose
    {
        private readonly string _stockName;
        private readonly StockDataRange _dataRange;
        private readonly int _smaPeriod;
        private readonly IDataLoader _dataLoader;
        private readonly ITickAligner _tickAligner;

        public PriceCrossingSMA(string stockName, StockDataRange dataRange, int smaPeriod, IDataLoader dataLoader, ITickAligner tickAligner)
        {
            _stockName = stockName;
            _dataRange = dataRange;
            _smaPeriod = smaPeriod;
            _dataLoader = dataLoader;
            _tickAligner = tickAligner;
        }

        public void Initialize(DateTime tsStart)
        {

        }

        public List<Signal> GenerateOnClose(DateTime ts)
        {
            List<Signal> res = new List<Signal>();

            StockPricesData data = _dataLoader.Get(_stockName, _dataRange, 0, ts, ts);
            int ix = data.FindByTS(ts);

            return res;
        }
    }
}
