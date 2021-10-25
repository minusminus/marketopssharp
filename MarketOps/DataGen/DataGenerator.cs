using System.Collections.Generic;
using MarketOps.StockData.Interfaces;
using MarketOps.StockData.Types;

namespace MarketOps.DataGen
{
    /// <summary>
    /// Manages data generation.
    /// </summary>
    public class DataGenerator : DataOp
    {
        private readonly IDataGenProvider _dataGenProvider;
        private readonly IDataGen _dataGen;

        public DataGenerator(IDataGenProvider dataGenProvider, IDataGen dataGen)
        {
            _dataGenProvider = dataGenProvider;
            _dataGen = dataGen;
        }

        protected override List<StockDefinition> GetAllStocksDefinitions()
        {
            return _dataGenProvider.GetAllStockDefinitions();
        }

        public void GenerateWeekly(StockType stockType)
        {
            ProcessStocks(stockType, _dataGen.GenerateWeekly);
        }

        public void GenerateMonthly(StockType stockType)
        {
            ProcessStocks(stockType, _dataGen.GenerateMonthly);
        }
    }
}
