using MarketOps.StockData.Interfaces;
using MarketOps.StockData.Types;
using MarketOps.SystemData.Interfaces;
using MarketOps.SystemData.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketOps.SystemDefs.BBTrendFunds
{
    /// <summary>
    /// Signals for multi funds bb trend.
    /// </summary>
    internal class SignalsBBTrendFunds : ISystemDataDefinitionProvider, ISignalGeneratorOnClose
    {
        private readonly List<StockDefinition> _stocks;

        public SignalsBBTrendFunds(ISystemDataLoader dataLoader, IStockDataProvider dataProvider)
        {
            _stocks = new List<StockDefinition>()
            {
                dataProvider.GetStockDefinition("PKO021"),  //akcji plus
                dataProvider.GetStockDefinition("PKO909"),  //rynku zlota
            };
        }

        public SystemDataDefinition GetDataDefinition() => new SystemDataDefinition()
        {
            //stocks = new List<SystemStockDataDefinition>() {
            //        new SystemStockDataDefinition()
            //        {
            //            stock = _stock,
            //            dataRange = StockDataRange.Monthly,
            //            stats = new List<StockStat>()
            //        }
            //    }
            stocks = _stocks.Select(def => new SystemStockDataDefinition()
            {
                stock = def,
                dataRange = StockDataRange.Monthly,
                stats = new List<StockStat>()
            }).ToList()
        };

        public List<Signal> GenerateOnClose(DateTime ts, int leadingIndex, SystemState systemState)
        {
            return new List<Signal>();
        }
    }
}
