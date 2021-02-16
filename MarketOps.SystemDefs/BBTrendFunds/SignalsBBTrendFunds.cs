using MarketOps.Stats.Stats;
using MarketOps.StockData.Extensions;
using MarketOps.StockData.Interfaces;
using MarketOps.StockData.Types;
using MarketOps.SystemData.Interfaces;
using MarketOps.SystemData.Types;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MarketOps.SystemDefs.BBTrendFunds
{
    /// <summary>
    /// Signals for multi funds bb trend.
    /// </summary>
    internal class SignalsBBTrendFunds : ISystemDataDefinitionProvider, ISignalGeneratorOnClose
    {
        private readonly string[] _fundsNames = { "PKO021", "PKO909" }; //akcji plus, rynku zlota

        private readonly List<StockDefinition> _stocks = new List<StockDefinition>();
        private readonly List<StockStat> _statsBB = new List<StockStat>();

        public SignalsBBTrendFunds(ISystemDataLoader dataLoader, IStockDataProvider dataProvider)
        {
            foreach (string fundName in _fundsNames)
            {
                _stocks.Add(dataProvider.GetStockDefinition(fundName));
                StockStat statBB = new StatBB("")
                    .SetParam(StatBBParams.Period, new MOParamInt() { Value = 10 })
                    .SetParam(StatBBParams.SigmaWidth, new MOParamFloat() { Value = 2f });
                _statsBB.Add(statBB);
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
                        dataRange = StockDataRange.Monthly,
                        stats = new List<StockStat>() { _statsBB[i] }
                    };
                })
                .ToList()
        };

        public List<Signal> GenerateOnClose(DateTime ts, int leadingIndex, SystemState systemState)
        {
            return new List<Signal>();
        }
    }
}
