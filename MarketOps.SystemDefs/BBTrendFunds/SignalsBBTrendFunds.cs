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
        private const int RebalanceInterval = 3;

        //private readonly string[] _fundsNames = { "PKO021", "PKO909" }; //akcji plus, rynku zlota
        private readonly string[] _fundsNames = { "PKO014", "PKO021" }; //obl dlugoterm, akcji plus
        //private readonly string[] _fundsNames = { "PKO014", "PKO909" }; //obl dlugoterm, rynku zlota
        //private readonly string[] _fundsNames = { "PKO014", "PKO918" }; //obl dlugoterm, akcji amer

        private readonly ISystemDataLoader _dataLoader;
        private readonly StockDataRange _dataRange;
        private BBTrendFundsData _fundsData;
        private readonly ModNCounter _rebalanceSignal;

        public SignalsBBTrendFunds(ISystemDataLoader dataLoader, IStockDataProvider dataProvider)
        {
            _dataLoader = dataLoader;
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
            //LogData(ts);
            if (ExecuteRebalance())
                result.Add(CreateSignal(CalculateBalance()));
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

        private Signal CreateSignal(float[] newBalance)
        {
            return new Signal()
            {
                DataRange = StockDataRange.Monthly,
                IntradayInterval = 0,
                Type = SignalType.EnterOnOpen,
                Direction = PositionDir.Long,
                Rebalance = true,
                NewBalance = new List<(StockDefinition stockDef, float balance)>()
                {
                    (_fundsData.Stocks[0], newBalance[0]),
                    (_fundsData.Stocks[1], newBalance[1])
                }
            };
        }

        private float[] CalculateBalance()
        {
            float[] balance = new float[2] { 0, 0 };

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

        private void LogData(DateTime ts)
        {
            string filePath = Path.Combine(Path.GetDirectoryName(new Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).LocalPath), "log.txt");

            string text = ts.Date.ToString() + ": "
                + string.Join(", ", _fundsData.Stocks.Select((def, i) => $"{def.StockName} {def.Name}({_fundsData.CurrentTrends[i]}, {_fundsData.CurrentExpectations[i]}, {_fundsData.ExpectationChanged[i]})").ToArray())
                + "\n";

            File.AppendAllText(filePath, text);
        }
    }
}
