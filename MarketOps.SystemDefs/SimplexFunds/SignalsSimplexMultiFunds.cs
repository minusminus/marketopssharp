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
        private readonly string[] _fundsNames = { "PKO014",
            "PKO008", "PKO009", "PKO009", "PKO010", "PKO013", "PKO015", "PKO018", "PKO019", "PKO020", "PKO021",
            "PKO025", "PKO026", "PKO027", "PKO028", "PKO029", "PKO057", "PKO097", "PKO098", "PKO909", "PKO910",
            "PKO913", "PKO918", "PKO919", "PKO925"};
        private readonly bool[] _aggressiveFunds;

        private readonly ISystemDataLoader _dataLoader;
        private readonly ISystemExecutionLogger _systemExecutionLogger;
        private readonly StockDataRange _dataRange;

        public SignalsSimplexMultiFunds(ISystemDataLoader dataLoader, IStockDataProvider dataProvider, ISystemExecutionLogger systemExecutionLogger)
        {
            _aggressiveFunds = _fundsNames.Select((_, i) => i > 0).ToArray();
            if (_fundsNames.Length != _aggressiveFunds.Length)
                throw new Exception("_fundsNames != _aggressiveFunds");

            _dataLoader = dataLoader;
            _systemExecutionLogger = systemExecutionLogger;
            _dataRange = StockDataRange.Monthly;
            //_fundsData = new BBTrendFundsData(_fundsNames.Length);
            //BBTrendFundsDataCalculator.Initialize(_fundsData, _fundsNames, BBPeriod, BBSigmaWidth, HLPeriod, dataProvider);
        }

        public SystemDataDefinition GetDataDefinition()
        {
            throw new NotImplementedException();
        }

        public List<Signal> GenerateOnClose(DateTime ts, int leadingIndex, SystemState systemState)
        {
            throw new NotImplementedException();
        }
    }
}
