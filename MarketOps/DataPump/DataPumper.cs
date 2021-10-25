using System;
using System.Collections.Generic;
using MarketOps.StockData.Interfaces;
using MarketOps.StockData.Types;

namespace MarketOps.DataPump
{
    /// <summary>
    /// Manages data pumping.
    /// </summary>
    public class DataPumper : DataOp
    {
        private readonly IDataPumpProvider _dataPumpProvider;
        private readonly IDataPump _dataPump;

        public DataPumper(IDataPumpProvider dataPumpProvider, IDataPump dataPump)
        {
            _dataPumpProvider = dataPumpProvider;
            _dataPump = dataPump;
        }

        protected override List<StockDefinition> GetAllStocksDefinitions()
        {
            return _dataPumpProvider.GetAllStockDefinitions();
        }

        public void PumpDaily(StockType stockType)
        {
            ProcessStocks(stockType, _dataPump.PumpDaily);
            PumpDailyUpdateStartTS(stockType);
        }

        private void PumpDailyUpdateStartTS(StockType stockType)
        {
            try
            {
                _dataPump.UpdateStartTS(stockType);
            }
            catch (Exception e)
            {
                ExecuteProcessingException("UpdateStartTS", e);
            }
        }
    }
}
