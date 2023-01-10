using MarketOps.StockData.Extensions;
using MarketOps.StockData.Interfaces;
using MarketOps.StockData.Types;
using MarketOps.SystemData.Interfaces;
using MarketOps.SystemData.Types;
using System;
using System.Collections.Generic;

namespace MarketOps.SystemDefs.NTopFunds
{
    /// <summary>
    /// Signals for N to funds.
    /// </summary>
    internal class SignalsNTopFunds : ISystemDataDefinitionProvider, ISignalGeneratorOnClose
    {
        private readonly int _n;

        private readonly ISystemDataLoader _dataLoader;
        private readonly ISystemExecutionLogger _systemExecutionLogger;
        private readonly StockDataRange _dataRange;

        public SignalsNTopFunds(ISystemDataLoader dataLoader, IStockDataProvider dataProvider, ISystemExecutionLogger systemExecutionLogger, MOParams systemParams)
        {
            _n = systemParams.Get(NTopFundsParams.N).As<int>();

            _dataLoader = dataLoader;
            _systemExecutionLogger = systemExecutionLogger;
            _dataRange = StockDataRange.Monthly;
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
