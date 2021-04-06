using MarketOps.StockData.Interfaces;
using MarketOps.StockData.Types;
using MarketOps.SystemData.Interfaces;
using MarketOps.SystemData.Types;
using MarketOps.SystemDefs.BBTrendRecognizer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MarketOps.SystemDefs.BBTrendFunds
{
    /// <summary>
    /// Signals for multi funds bb trend.
    /// </summary>
    internal class SignalsBBTrendMultiFunds : ISystemDataDefinitionProvider, ISignalGeneratorOnClose
    {
        private const int BBPeriod = 10;
        private const float BBSigmaWidth = 2f;
        private const int RebalanceInterval = 3;

        //private readonly string[] _fundsNames = { "PKO021", "PKO909" }; //akcji plus, rynku zlota
        private readonly string[] _fundsNames = { "PKO014", "PKO021" }; //obl dlugoterm, akcji plus
        //private readonly string[] _fundsNames = { "PKO014", "PKO909" }; //obl dlugoterm, rynku zlota
        //private readonly string[] _fundsNames = { "PKO014", "PKO918" }; //obl dlugoterm, akcji amer

        private readonly ISystemDataLoader _dataLoader;
        private readonly StockDataRange _dataRange;
        private BBTrendFundsData _fundsData;
        //private readonly ModNCounter _rebalanceSignal;

        public SignalsBBTrendMultiFunds(ISystemDataLoader dataLoader, IStockDataProvider dataProvider)
        {
            _dataLoader = dataLoader;
            _dataRange = StockDataRange.Monthly;
            _fundsData = new BBTrendFundsData(_fundsNames.Length);
            BBTrendFundsDataCalculator.Initialize(_fundsData, _fundsNames, BBPeriod, BBSigmaWidth, dataProvider);
            //_rebalanceSignal = new ModNCounter(RebalanceInterval);
        }

        public SystemDataDefinition GetDataDefinition() => new SystemDataDefinition()
        {
            stocks = _fundsData.Stocks
                .Select((def, i) =>
                {
                    return new SystemStockDataDefinition()
                    {
                        stock = def,
                        dataRange = _dataRange,
                        stats = new List<StockStat>() { _fundsData.StatsBB[i] }
                    };
                })
                .ToList()
        };

        public List<Signal> GenerateOnClose(DateTime ts, int leadingIndex, SystemState systemState)
        {
            List<Signal> result = new List<Signal>();

            BBTrendFundsDataCalculator.CalculateTrendsAndExpectations(_fundsData, ts, _dataRange, _dataLoader);
            //ResetRebalanceCountersIfNeeded();
            //LogData(ts);
            //if (ExecuteRebalance())
            result.Add(BBTrendFundsSignalFactory.CreateSignal(CalculateBalance(), _dataRange, _fundsData));
            //IncrementRebalanceCounters();

            return result;
        }

        private float[] CalculateBalance()
        {
            float[] balance = new float[_fundsNames.Length];

            switch (_fundsData.CurrentExpectations[1])
            {
                case BBTrendExpectation.UpAndRaising: balance[0] = 0.2f; balance[1] = 0.8f; break;
                case BBTrendExpectation.UpButPossibleChange: balance[0] = 0.8f; balance[1] = 0.2f; break;
                case BBTrendExpectation.DownButPossibleChange: balance[0] = 0.8f; balance[1] = 0.2f; break;
                case BBTrendExpectation.DownAndFalling: balance[0] = 1f; balance[1] = 0f; break;
                case BBTrendExpectation.Unknown: balance[0] = 1f; balance[1] = 0f; break;
            }

            return balance;
        }
    }
}
