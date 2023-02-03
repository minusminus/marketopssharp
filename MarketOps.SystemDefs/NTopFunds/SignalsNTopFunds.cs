using MarketOps.Maths.PositionsBalancing;
using MarketOps.StockData.Extensions;
using MarketOps.StockData.Interfaces;
using MarketOps.StockData.Types;
using MarketOps.SystemData.Extensions;
using MarketOps.SystemData.Interfaces;
using MarketOps.SystemData.Types;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MarketOps.SystemDefs.NTopFunds
{
    /// <summary>
    /// Signals for N top funds.
    /// </summary>
    internal class SignalsNTopFunds : ISystemDataDefinitionProvider, ISignalGeneratorOnClose
    {
        private readonly int _n;
        private int _avgProfitRange;
        private int _avgChangeRange;
        private float _aggressivePartSize;

        private readonly ISystemDataLoader _dataLoader;
        private readonly ISystemExecutionLogger _systemExecutionLogger;
        private readonly StockDataRange _dataRange;
        private readonly NTopFundsData _fundsData;

        private readonly string[] _fundsNames = { "PKO014",
            "PKO009", "PKO010", "PKO013", "PKO015", "PKO018", "PKO019", "PKO021",
            "PKO025", "PKO026", "PKO027", "PKO028", "PKO029", "PKO057",
            "PKO072", "PKO073", "PKO074", "PKO097", "PKO098", "PKO909",
            "PKO910", "PKO913", "PKO918", "PKO919", "PKO925"};


        public SignalsNTopFunds(ISystemDataLoader dataLoader, IStockDataProvider dataProvider, ISystemExecutionLogger systemExecutionLogger, MOParams systemParams)
        {
            _n = systemParams.Get(NTopFundsParams.N).As<int>();
            _avgProfitRange = systemParams.Get(NTopFundsParams.AvgProfitRange).As<int>();
            _avgChangeRange = systemParams.Get(NTopFundsParams.AvgChangeRange).As<int>();
            _aggressivePartSize = 0.6f;

            _dataLoader = dataLoader;
            _systemExecutionLogger = systemExecutionLogger;
            _dataRange = StockDataRange.Monthly;
            _fundsData = new NTopFundsData(_fundsNames.Length);

            NTopFundsDataCalculator.Initialize(_fundsData, _fundsNames, dataProvider);
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

            NTopFundsDataCalculator.Calculate(_fundsData, ts, _avgProfitRange, _avgChangeRange, _dataRange, _dataLoader);

            float portfolioValue = SystemValueCalculator.Calc(systemState, ts, _dataLoader);
            int[] selectedTop = GetTopN();
            float[] balance = new float[0];
            if (selectedTop.Length > 0)
                balance = CalculateBalance(selectedTop, _aggressivePartSize, portfolioValue, _fundsData);
            AddPassiveItemToBalance();
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

        private int[] GetTopN() =>
            _fundsNames
                .Select((_, i) => i)
                .Where(i => (i > 0) && _fundsData.Active[i])
                .Where(i => _fundsData.Profit[i] > 0)
                .OrderByDescending(i => _fundsData.Profit[i])
                .Take(_n)
                .ToArray();

        private float[] CalculateBalance(int[] selectedTop, float aggressivePartSize, float equityValue, NTopFundsData fundsData)
        {
            float[] newBalance = EqualRiskPositionsBalancer.Calculate(GetExpectedRisks(), GetPrices(), equityValue);
            ConvertPositionSizeToBalanceInPercent(newBalance);
            return newBalance;

            float[] GetExpectedRisks() =>
                selectedTop
                .Select(i => (float)fundsData.Risk[i])
                .ToArray();

            float[] GetPrices() =>
                selectedTop
                .Select(i => (float)fundsData.Prices[i])
                .ToArray();

            void ConvertPositionSizeToBalanceInPercent(float[] balance)
            {
                for (int i = 0; i < balance.Length; i++)
                    balance[i] = (aggressivePartSize * balance[i] * (float)fundsData.Prices[selectedTop[i]] / equityValue).TruncateTo2ndPlace();
            }
        }

        private Signal CreateSignal(int[] selectedTop, float[] newBalance, StockDataRange dataRange, NTopFundsData fundsData)
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
                 selectedTop.Select((i, index) => FormatBalance(_fundsNames[i], balance[index]));

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
