using MarketOps.StockData.Extensions;
using MarketOps.StockData.Interfaces;
using MarketOps.StockData.Types;
using MarketOps.SystemData.Interfaces;
using MarketOps.SystemData.Types;
using MarketOps.SystemDefs.BBTrendRecognizer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MarketOps.SystemDefs.BBTrendFunds
{
    /// <summary>
    /// Signals for multi funds bb trend.
    /// 
    /// First fund in list is safe fund.
    /// </summary>
    internal class SignalsBBTrendMultiFunds : ISystemDataDefinitionProvider, ISignalGeneratorOnClose
    {
        private const int BBPeriod = 10;
        private const float BBSigmaWidth = 2f;
        private const int RebalanceInterval = 3;
        private const int ProfitBackDataLength = 2;
        private const int NumberOfAggressiveFundsTaken = 3;

        //obl dlugoterm, akcji plus, mis spolek, rynku zlota, akcji amer, akcji jap, pap dl usd
        private readonly string[] _fundsNames = { "PKO014", "PKO021", "PKO015", "PKO909", "PKO918", "PKO919", "PKO910" };
        private readonly bool[] _aggressiveFunds = { false, true, true, true, true, true, true };

        private readonly ISystemDataLoader _dataLoader;
        private readonly ISystemExecutionLogger _systemExecutionLogger;
        private readonly StockDataRange _dataRange;
        private BBTrendFundsData _fundsData;
        //private readonly ModNCounter _rebalanceSignal;

        public SignalsBBTrendMultiFunds(ISystemDataLoader dataLoader, IStockDataProvider dataProvider, ISystemExecutionLogger systemExecutionLogger)
        {
            if (_fundsNames.Length != _aggressiveFunds.Length)
                throw new Exception("_fundsNames != _aggressiveFunds");

            _dataLoader = dataLoader;
            _systemExecutionLogger = systemExecutionLogger;
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

            var sortedFunds = OrderStocksByProfit(ts, ProfitBackDataLength);

            //ResetRebalanceCountersIfNeeded();
            //if (ExecuteRebalance())
            float[] balance = CalculateBalance(sortedFunds, NumberOfAggressiveFundsTaken);
            result.Add(BBTrendFundsSignalFactory.CreateSignal(balance, _dataRange, _fundsData));
            //IncrementRebalanceCounters();

            LogData(ts, sortedFunds, balance);
            return result;
        }

        private float[] CalculateBalance(List<Tuple<StockDefinition, int, float>> sortedFunds, int topN)
        {
            //BBTrendExpectation[] acceptedExpectations = { BBTrendExpectation.UpAndRaising, BBTrendExpectation.UpButPossibleChange, BBTrendExpectation.DownButPossibleChange };
            BBTrendExpectation[] acceptedExpectations = { BBTrendExpectation.UpAndRaising, BBTrendExpectation.UpButPossibleChange };

            float[] balance = new float[_fundsNames.Length];

            var filteredTop = sortedFunds
                .Where(x => (x.Item3 > 0) && acceptedExpectations.Contains(_fundsData.CurrentExpectations[x.Item2]))
                .Take(topN)
                .ToList();
            //float singleMaxBalance = 1f / topN;
            float singleMaxBalance = filteredTop.Count > 0 ? 1f / filteredTop.Count : 0;
            foreach (var item in filteredTop)
                balance[item.Item2] = singleMaxBalance * CalculateBalanceForExpectation(_fundsData.CurrentExpectations[item.Item2]);
            balance[0] = 1f - balance.Skip(1).Sum();

            return balance;
        }

        private float CalculateBalanceForExpectation(BBTrendExpectation expectation)
        {
            switch (expectation)
            {
                case BBTrendExpectation.UpAndRaising: return 0.8f;
                case BBTrendExpectation.UpButPossibleChange: return 0.2f;
                case BBTrendExpectation.DownButPossibleChange: return 0.2f;
                case BBTrendExpectation.DownAndFalling: return 0f;
                case BBTrendExpectation.Unknown: return 0f;
                default: return 0f;
            }
        }

        private List<Tuple<StockDefinition, int, float>> OrderStocksByProfit(DateTime ts, int n)
        {
            return _fundsData.Stocks
                .Select((stockDef, i) => new Tuple<StockDefinition, int>(stockDef, i))
                .Where(x => _aggressiveFunds[x.Item2])
                .Select(x => new Tuple<StockDefinition, int, float>(x.Item1, x.Item2, PcntProfitFromNTicks(x.Item1.Name, n, ts)))
                .OrderByDescending(x => x.Item3)
                .ToList();
        }

        private float PcntProfitFromNTicks(string fundName, int n, DateTime ts)
        {
            StockPricesData spData = _dataLoader.Get(fundName, _dataRange, 0, ts, ts);
            int dataIndex = spData.FindByTS(ts);
            if (dataIndex < n) return float.MinValue;
            return (spData.C[dataIndex] - spData.C[dataIndex - n]) / spData.C[dataIndex - n];
        }

        private float AvgPcntProfitFromNTicks(string fundName, int n, DateTime ts)
        {
            StockPricesData spData = _dataLoader.Get(fundName, _dataRange, 0, ts, ts);
            int dataIndex = spData.FindByTS(ts);
            if (dataIndex < n) return float.MinValue;
            float sum = 0;
            for (int i = 0; i < n; i++)
                sum += (spData.C[dataIndex - i] - spData.C[dataIndex - i - 1]) / spData.C[dataIndex - i - 1];
            return sum / n;
        }

        private void LogData(DateTime ts, List<Tuple<StockDefinition, int, float>> sortedFunds, float[] balance)
        {
            string SortValue(float value) => value > float.MinValue ? (100f * value).ToString("F2") : "--";

            _systemExecutionLogger.Add(
                $"{ts.Date.ToString("yyyy-MM-dd")}:" + Environment.NewLine
                + "trends: " + string.Join(", ", _fundsData.Stocks.Select((def, i) => $"{def.StockName}({_fundsData.CurrentTrends[i]}, {_fundsData.CurrentExpectations[i]})").ToArray()) + Environment.NewLine
                + "sorted: " + string.Join(", ", sortedFunds.Select(x => $"{x.Item1.StockName} = {SortValue(x.Item3)}")) + Environment.NewLine
                + "balance: " + string.Join(", ", balance.Select((b, i) => $"{_fundsData.Stocks[i].StockName} = {b.ToString("F2")}").ToArray())
                );
        }
    }
}
