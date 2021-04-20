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
    /// Signals for two funds bb trend.
    /// </summary>
    internal class SignalsBBTrendFunds : ISystemDataDefinitionProvider, ISignalGeneratorOnClose
    {
        private const int BBPeriod = 10;
        private const float BBSigmaWidth = 2f;
        private const int RebalanceInterval = 1;

        //private readonly string[] _fundsNames = { "PKO021", "PKO909" }; //akcji plus, rynku zlota
        //private readonly string[] _fundsNames = { "PKO014", "PKO021" }; //obl dlugoterm, akcji plus
        //private readonly string[] _fundsNames = { "PKO014", "PKO909" }; //obl dlugoterm, rynku zlota
        //private readonly string[] _fundsNames = { "PKO014", "PKO918" }; //obl dlugoterm, akcji amer
        private readonly string[] _fundsNames = { "PKO014", "PKO019" };

        private readonly ISystemDataLoader _dataLoader;
        private readonly ISystemExecutionLogger _systemExecutionLogger;
        private readonly StockDataRange _dataRange;
        private BBTrendFundsData _fundsData;
        private readonly ModNCounter _rebalanceSignal;

        public SignalsBBTrendFunds(ISystemDataLoader dataLoader, IStockDataProvider dataProvider, ISystemExecutionLogger systemExecutionLogger)
        {
            _dataLoader = dataLoader;
            _systemExecutionLogger = systemExecutionLogger;
            _dataRange = StockDataRange.Monthly;
            _fundsData = new BBTrendFundsData(_fundsNames.Length);
            BBTrendFundsDataCalculator.Initialize(_fundsData, _fundsNames, BBPeriod, BBSigmaWidth, dataProvider);
            _rebalanceSignal = new ModNCounter(RebalanceInterval);
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
            ResetRebalanceCountersIfNeeded();
            //if (ExecuteRebalance())
            {
                var balance = CalculateBalance();
                result.Add(BBTrendFundsSignalFactory.CreateSignal(balance, _dataRange, _fundsData));
                LogData(ts, balance);
            }
            IncrementRebalanceCounters();

            return result;
        }

        private void ResetRebalanceCountersIfNeeded()
        {
            if (_fundsData.ExpectationChanged[1])
                _rebalanceSignal.Reset();
        }

        private void IncrementRebalanceCounters()
        {
            _rebalanceSignal.Next();
        }

        private bool ExecuteRebalance()
        {
            //return true;
            return (_fundsData.CurrentExpectations[1] == BBTrendExpectation.Unknown)
                || _rebalanceSignal.IsZero;
        }

        private float[] CalculateBalance()
        {
            float[] balance = new float[2] { 0, 0 };

            switch (_fundsData.CurrentExpectations[1])
            {
                case BBTrendExpectation.UpAndRaising: balance[0] = 0.2f; balance[1] = 0.8f; break;
                case BBTrendExpectation.UpButPossibleChange: balance[0] = 0.8f; balance[1] = 0.2f; break;
                //case BBTrendExpectation.UpButPossibleChange: balance[0] = 1f; balance[1] = 0f; break;
                //case BBTrendExpectation.DownButPossibleChange: balance[0] = 0.8f; balance[1] = 0.2f; break;
                case BBTrendExpectation.DownButPossibleChange: balance[0] = 1f; balance[1] = 0f; break;
                case BBTrendExpectation.DownAndFalling: balance[0] = 1f; balance[1] = 0f; break;
                case BBTrendExpectation.Unknown: balance[0] = 1f; balance[1] = 0f; break;
            }

            return balance;
        }

        private void LogData(DateTime ts, float[] balance)
        {
            //string filePath = Path.Combine(Path.GetDirectoryName(new Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).LocalPath), "log.txt");

            //string text = ts.Date.ToString() + ": "
            //    + string.Join(", ", _fundsData.Stocks.Select((def, i) => $"{def.StockName} {def.Name}({_fundsData.CurrentTrends[i]}, {_fundsData.CurrentExpectations[i]}, {_fundsData.ExpectationChanged[i]})").ToArray())
            //    + "\n";

            //File.AppendAllText(filePath, text);

            _systemExecutionLogger.Add(
                $"{ts.Date.ToString("yyyy-MM-dd")}:" + Environment.NewLine
                + "trends: " + string.Join(", ", _fundsData.Stocks.Select((def, i) => $"{def.StockName} {def.Name}({_fundsData.CurrentTrends[i]}, {_fundsData.CurrentExpectations[i]}, {_fundsData.ExpectationChanged[i]})").ToArray()) + Environment.NewLine
                + "balance: " + string.Join(", ", balance.Select((b, i) => $"{_fundsData.Stocks[i].StockName} = {b.ToString("F2")}").ToArray())
                );
        }
    }
}
