using MarketOps.StockData.Extensions;
using MarketOps.StockData.Interfaces;
using MarketOps.StockData.Types;
using MarketOps.SystemData.Interfaces;
using MarketOps.SystemData.Types;
using MarketOps.SystemDefs.BBTrendRecognizer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MarketOps.SystemDefs.SimplexFunds
{
    /// <summary>
    /// Signals for multi funds with simplex balance calculation.
    /// 
    /// First fund in list is safe fund.
    /// 
    /// --== Original version ==--
    /// acceptable_risk = 0.1
    /// sigma_multiplier = 2
    /// max_single_pos_size = 0.8
    /// 
    /// calculations:
    /// average change close - close of 3 and 6 periods
    /// stddev for avgchange(6)
    /// 
    /// simplex solver:
    /// goal: max avg 3 periods profit
    /// constaints for each decision:
    /// - decision >= 0
    /// - avgchange(6) + stddev * sigma_multiplier <= portoflio_value * acceptable_risk
    /// - price * decision <= portoflio_value * max_single_pos_size
    /// </summary>
    internal class SignalsSimplexMultiFunds : ISystemDataDefinitionProvider, ISignalGeneratorOnClose
    {
        private const int AvgProfitRange = 3;
        private const int AvgChangeRange = 6;

        private readonly string[] _fundsNames = { "PKO014",
            "PKO008", "PKO009", "PKO010", "PKO013", "PKO015", "PKO018", "PKO019", "PKO020", "PKO021",
            "PKO025", "PKO026", "PKO027", "PKO028", "PKO029", "PKO057", "PKO097", "PKO098", "PKO909", 
            "PKO910", "PKO913", "PKO918", "PKO919", "PKO925"};
        private readonly bool[] _aggressiveFunds;

        private readonly ISystemDataLoader _dataLoader;
        private readonly ISystemExecutionLogger _systemExecutionLogger;
        private readonly StockDataRange _dataRange;
        private SimplexFundsData _fundsData;

        public SignalsSimplexMultiFunds(ISystemDataLoader dataLoader, IStockDataProvider dataProvider, ISystemExecutionLogger systemExecutionLogger)
        {
            _aggressiveFunds = _fundsNames.Select((_, i) => i > 0).ToArray();
            if (_fundsNames.Length != _aggressiveFunds.Length)
                throw new Exception("_fundsNames != _aggressiveFunds");

            _dataLoader = dataLoader;
            _systemExecutionLogger = systemExecutionLogger;
            _dataRange = StockDataRange.Monthly;
            _fundsData = new SimplexFundsData(_fundsNames.Length);
            SimplexFundsDataCalculator.Initialize(_fundsData, _fundsNames, dataProvider);
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
                        stats = new List<StockStat>() { }
                    };
                })
                .ToList()
        };

        public List<Signal> GenerateOnClose(DateTime ts, int leadingIndex, SystemState systemState)
        {
            List<Signal> result = new List<Signal>();

            SimplexFundsDataCalculator.SetCurrentPrices(_fundsData, ts, _dataRange, _dataLoader);
            SimplexFundsDataCalculator.CalculateAvgProfit(_fundsData, AvgProfitRange, ts, _dataRange, _dataLoader);
            SimplexFundsDataCalculator.CalculateAvgChange(_fundsData, AvgChangeRange, ts, _dataRange, _dataLoader);

            SimplexExecutor.Execute(_fundsNames, _fundsData, systemState.Equity.Count > 0 ? systemState.Equity.Last().Value : systemState.Cash, 0.1, 2, 0.8);

            LogData(ts);
            return result;
        }

        private void LogData(DateTime ts)
        {
            _systemExecutionLogger.Add(
                $"{ts.Date:yyyy-MM-dd}:" + Environment.NewLine
                + string.Join(", ", _fundsNames.Select((name, i) => $"{name}[{_fundsData.AvgProfit[i]:F2}, {_fundsData.AvgChange[i]:F2}, {_fundsData.AvgChangeSigma[i]:F2}]"))
                );
        }
    }
}
