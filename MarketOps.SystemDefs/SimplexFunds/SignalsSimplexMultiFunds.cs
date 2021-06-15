using MarketOps.StockData.Extensions;
using MarketOps.StockData.Interfaces;
using MarketOps.StockData.Types;
using MarketOps.SystemData.Interfaces;
using MarketOps.SystemData.Types;
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
        private const double AcceptableRisk = 0.1;
        private const double RiskSigmaMultiplier = 2;
        private const double MaxSinglePositionSize = 0.8;

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

            SimplexFundsDataCalculator.Calculate(_fundsData, ts, AvgProfitRange, AvgChangeRange, _dataRange, _dataLoader);

            float[] balance = SimplexExecutor.Execute(_fundsNames, _fundsData, systemState.Equity.Count > 0 ? systemState.Equity.Last().Value : systemState.Cash, AcceptableRisk, RiskSigmaMultiplier, MaxSinglePositionSize);
            result.Add(CreateSignal(balance, _dataRange, _fundsData));

            LogData(ts, balance);
            return result;
        }

        private Signal CreateSignal(float[] newBalance, StockDataRange dataRange, SimplexFundsData fundsData) =>
            new Signal()
            {
                DataRange = dataRange,
                IntradayInterval = 0,
                Type = SignalType.EnterOnOpen,
                Direction = PositionDir.Long,
                Rebalance = true,
                NewBalance = fundsData.Stocks.Select((def, i) => (def, newBalance[i])).ToList()
            };


        private void LogData(DateTime ts, float[] balance)
        {
            _systemExecutionLogger.Add(
                $"{ts.Date:yyyy-MM-dd}:" + Environment.NewLine
                + string.Join(", ", _fundsNames.Select((name, i) => $"{name}[{_fundsData.Active[i]}, {100f * _fundsData.AvgProfit[i]:F2}, {100f * _fundsData.AvgChange[i]:F2}, {100f * _fundsData.AvgChangeSigma[i]:F2}]")) + Environment.NewLine
                + "balance: " + string.Join(", ", _fundsNames.Select((name, i) => $"{name}[{100f * balance[i]:F2}]")) + Environment.NewLine
                + "selected: " + string.Join(", ", _fundsNames.Select((name, i) => (name, i)).Where(x => balance[x.i]>0).Select(x => $"{x.name}[{100f * balance[x.i]:F2}]")) + Environment.NewLine
                );
        }
    }
}
