using MarketOps.Stats.Stats;
using MarketOps.StockData.Extensions;
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
    internal class SignalsBBTrendFunds : ISystemDataDefinitionProvider, ISignalGeneratorOnClose
    {
        //private readonly string[] _fundsNames = { "PKO021", "PKO909" }; //akcji plus, rynku zlota
        private readonly string[] _fundsNames = { "PKO014", "PKO021" }; //obl dlugoterm, akcji plus

        private readonly ISystemDataLoader _dataLoader;
        private readonly StockDataRange _dataRange;
        private readonly List<StockDefinition> _stocks = new List<StockDefinition>();
        private readonly List<StatBB> _statsBB = new List<StatBB>();
        private readonly BBTrendType[] _currentTrends;
        private readonly BBTrendExpectation[] _currentExpectations;

        public SignalsBBTrendFunds(ISystemDataLoader dataLoader, IStockDataProvider dataProvider)
        {
            _dataLoader = dataLoader;
            _dataRange = StockDataRange.Monthly;
            _currentTrends = new BBTrendType[_fundsNames.Length];
            _currentExpectations = new BBTrendExpectation[_fundsNames.Length];

            for (int i = 0; i < _fundsNames.Length; i++)
            {
                _stocks.Add(dataProvider.GetStockDefinition(_fundsNames[i]));
                StockStat statBB = new StatBB("")
                    .SetParam(StatBBParams.Period, new MOParamInt() { Value = 10 })
                    .SetParam(StatBBParams.SigmaWidth, new MOParamFloat() { Value = 2f });
                _statsBB.Add((StatBB)statBB);
                _currentTrends[i] = BBTrendType.Unknown;
                _currentExpectations[i] = BBTrendExpectation.Unknown;
            }
        }

        public SystemDataDefinition GetDataDefinition() => new SystemDataDefinition()
        {
            stocks = _stocks
                .Select((def, i) =>
                {
                    return new SystemStockDataDefinition()
                    {
                        stock = def,
                        dataRange = _dataRange,
                        stats = new List<StockStat>() { _statsBB[i] }
                    };
                })
                .ToList()
        };

        public List<Signal> GenerateOnClose(DateTime ts, int leadingIndex, SystemState systemState)
        {
            CalculateTrendsAndExpectations(ts);
            //LogData(ts);
            return new List<Signal>()
            {
                CreateSignal(CalculateBalance())
            };
        }

        private void CalculateTrendsAndExpectations(DateTime ts)
        {
            for (int i = 0; i < _stocks.Count; i++)
            {
                StockPricesData data = _dataLoader.Get(_stocks[i].Name, _dataRange, 0, ts, ts);
                int dataIndex = data.FindByTS(ts);
                if (dataIndex < _statsBB[i].BackBufferLength) continue;
                _currentTrends[i] = BBTrendRecognizer.BBTrendRecognizer.RecognizeTrend(data, _statsBB[i], dataIndex, _currentTrends[i]);
                _currentExpectations[i] = BBTrendRecognizer.BBTrendRecognizer.GetExpectation(data, _statsBB[i], dataIndex, _currentTrends[i]);
            }
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
                    (_stocks[0], newBalance[0]),
                    (_stocks[1], newBalance[1])
                }
            };
        }

        private float[] CalculateBalance()
        {
            float[] balance = new float[2] { 0, 0 };

            switch (_currentExpectations[1])
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
                + string.Join(", ", _stocks.Select((def, i) => $"{def.Name}({_currentTrends[i]}, {_currentExpectations[i]})").ToArray())
                + "\n";

            File.AppendAllText(filePath, text);
        }
    }
}
