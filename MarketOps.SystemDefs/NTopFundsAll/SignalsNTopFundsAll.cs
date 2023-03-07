using MarketOps.Maths.PositionsBalancing;
using MarketOps.Stats.Stats;
using MarketOps.StockData.Extensions;
using MarketOps.StockData.Interfaces;
using MarketOps.StockData.Types;
using MarketOps.SystemData.Extensions;
using MarketOps.SystemData.Interfaces;
using MarketOps.SystemData.Types;
using System.Collections.Generic;
using System.Linq;
using System;

namespace MarketOps.SystemDefs.NTopFundsAll
{
    /// <summary>
    /// Signals for N top funds all.
    /// </summary>
    internal class SignalsNTopFundsAll : ISystemDataDefinitionProvider, ISignalGeneratorOnClose
    {
        private readonly int _n;
        private readonly int _avgProfitRange;
        private readonly int _avgChangeRange;
        private readonly float _aggressivePartSize;
        private readonly int _aggressiveSmaLength;

        private readonly ISystemDataLoader _dataLoader;
        private readonly ISystemExecutionLogger _systemExecutionLogger;
        private readonly StockDataRange _dataRange;
        private readonly NTopFundsAllData _fundsData;

        private readonly NTFADefinition _ntfaDefinition = new NTFADefinition();


        public SignalsNTopFundsAll(ISystemDataLoader dataLoader, IStockDataProvider dataProvider, ISystemExecutionLogger systemExecutionLogger, MOParams systemParams)
        {
            _n = systemParams.Get(NTopFundsAllParams.N).As<int>();
            _avgProfitRange = systemParams.Get(NTopFundsAllParams.AvgProfitRange).As<int>();
            _avgChangeRange = systemParams.Get(NTopFundsAllParams.AvgChangeRange).As<int>();
            _aggressivePartSize = 1f;
            _aggressiveSmaLength = 10;

            _dataLoader = dataLoader;
            _systemExecutionLogger = systemExecutionLogger;
            _dataRange = StockDataRange.Monthly;
            _fundsData = new NTopFundsAllData(_ntfaDefinition.Length);

            for (int i = 0; i < _ntfaDefinition.Length; i++)
                _fundsData.StatsSMA[i] = (StatSMA)new StatSMA("")
                    .SetParam(StatSMAParams.Period, new MOParamInt() { Value = _aggressiveSmaLength });

            NTopFundsAllDataCalculator.Initialize(_fundsData, _ntfaDefinition, dataProvider);
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
                        stats = new List<StockStat>() { _fundsData.StatsSMA[i] }
                    };
                })
                .ToList()
        };

        public List<Signal> GenerateOnClose(DateTime ts, int leadingIndex, SystemState systemState)
        {
            List<Signal> result = new List<Signal>();

            NTopFundsAllDataCalculator.Calculate(_fundsData, ts, _avgProfitRange, _avgChangeRange, _dataRange, _dataLoader);

            float portfolioValue = SystemValueCalculator.Calc(systemState, ts, _dataLoader);
            int[] selectedTop = GetTopN(ts);
            float[] balance = new float[0];
            if (selectedTop.Length > 0)
                balance = CalculateBalance(selectedTop, _aggressivePartSize, portfolioValue, _fundsData);
            //AddPassiveItemToBalance();
            result.Add(CreateSignal(selectedTop, balance, _dataRange, _fundsData));

            LogData(ts, selectedTop, balance, portfolioValue);
            return result;

            void AddPassiveItemToBalance()
            {
                float passiveBalance = (1.0f - balance.Sum()).TruncateTo2ndPlace();
                Array.Resize(ref selectedTop, selectedTop.Length + 1);
                Array.Resize(ref balance, balance.Length + 1);
                selectedTop[selectedTop.Length - 1] = 0;
                balance[balance.Length - 1] = passiveBalance;
            }
        }

        private int[] GetTopN(DateTime ts) =>
            _ntfaDefinition.BalancedStocks
                .Select((_, i) => i)
                //.Where(i => (i > 0) && _fundsData.Active[i])
                .Where(i => _fundsData.Active[i])
                .Where(i => _fundsData.Profit[i] > 0)
                //.Where(i => PriceOverSMA(i, ts))
                .OrderByDescending(i => _fundsData.Profit[i])
                .Take(_n)
                .ToArray();

        private bool PriceOverSMA(int stockIndex, DateTime ts)
        {
            if (!_dataLoader.GetWithIndex(_ntfaDefinition[stockIndex].Name, _dataRange, ts, _fundsData.StatsSMA[stockIndex].BackBufferLength, out var spData, out int dataIndex))
                return false;
            return (spData.C[dataIndex] > _fundsData.StatsSMA[stockIndex].Data(StatSMAData.SMA)[dataIndex - _fundsData.StatsSMA[stockIndex].BackBufferLength]);
        }

        private float[] CalculateBalance(int[] selectedTop, float aggressivePartSize, float equityValue, NTopFundsAllData fundsData)
        {
            float[] newBalance = EqualRiskPercentageBalancer.Calculate(GetExpectedRisks());
            ScaleBalanceToAggressivePart(newBalance);
            return newBalance;

            float[] GetExpectedRisks() =>
                selectedTop
                .Select(i => (float)(fundsData.Risk[i] * _ntfaDefinition[i].RiskMultiplier))
                .ToArray();

            void ScaleBalanceToAggressivePart(float[] balance)
            {
                for (int i = 0; i < balance.Length; i++)
                    balance[i] = (aggressivePartSize * balance[i]).TruncateTo2ndPlace();
            }
        }

        private Signal CreateSignal(int[] selectedTop, float[] newBalance, StockDataRange dataRange, NTopFundsAllData fundsData)
        {
            return new Signal()
            {
                DataRange = dataRange,
                IntradayInterval = 0,
                Type = SignalType.EnterOnOpen,
                Direction = PositionDir.Long,
                Rebalance = true,
                NewBalance = GetBalanceFromSelected()
            };

            List<(StockDefinition, float)> GetBalanceFromSelected() =>
                selectedTop.Select((i, index) => (fundsData.Stocks[i], newBalance[index])).ToList();
        }

        private void LogData(DateTime ts, int[] selectedTop, float[] balance, float equityValue)
        {
            _systemExecutionLogger.Add(
                $"{ts.Date:yyyy-MM-dd}:" + Environment.NewLine
                + $"selected: {string.Join(", ", BalancesToStrings())} | exp. risks: {string.Join(", ", ExpectedRisks())}" + Environment.NewLine
                );

            IEnumerable<string> BalancesToStrings() =>
                 selectedTop.Select((i, index) => FormatBalance(_ntfaDefinition[i].Name, balance[index]));

            string FormatBalance(string fundName, float fundBalance) =>
                $"{fundName}: {100f * fundBalance:F2}";

            IEnumerable<string> ExpectedRisks() =>
                selectedTop.Select((i, index) =>
                {
                    //double risk = (_fundsData.Risk[i] * _fundsData.Prices[i] * balance[index] * equityValue) / (_fundsData.Prices[i] * equityValue);
                    double risk = _fundsData.Risk[i] * balance[index];
                    return $"{100f * risk:F2}";
                });
        }
    }
}
