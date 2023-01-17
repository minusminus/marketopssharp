using MarketOps.StockData.Extensions;
using MarketOps.StockData.Interfaces;
using MarketOps.StockData.Types;
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

            return result;
        }
    }
}
