using System;
using System.Collections.Generic;
using System.Linq;
using MarketOps.StockData.Interfaces;
using MarketOps.StockData.Types;

namespace MarketOps.DataPump
{
    /// <summary>
    /// Manages data pumping.
    /// </summary>
    public class DataPumper
    {
        private readonly IDataPumpProvider _dataPumpProvider;
        private readonly IDataPump _dataPump;

        private List<StockDefinition> _stocks;
        private Action<DataPumperDailyProcessingInfo> _onStockStartProcessing;
        private DataPumperDailyProcessingInfo _processingInfo;

        public DataPumper(IDataPumpProvider dataPumpProvider, IDataPump dataPump)
        {
            _dataPumpProvider = dataPumpProvider;
            _dataPump = dataPump;
        }

        public void StartSession(Action<DataPumperDailyProcessingInfo> onStockStartProcessing)
        {
            _onStockStartProcessing = onStockStartProcessing;
            _stocks = _dataPumpProvider.GetAllStockDefinitions();
            _processingInfo = new DataPumperDailyProcessingInfo();
        }

        public void EndSession()
        {
            _processingInfo = null;
            _stocks = null;
            _onStockStartProcessing = null;
        }

        public void PumpDaily(StockType stockType)
        {
            var stocksToPump = _stocks.Where(x => (x.Type == stockType) && (x.Enabled))
                                      .OrderBy(x => x.ID);

            _processingInfo.CurrentPosition = 0;
            _processingInfo.TotalCount = stocksToPump.Count();
            foreach (var stockDefinition in stocksToPump)
                PumpDailySingleStock(stockDefinition);
            _dataPump.UpdateStartTS(stockType);
        }

        private void PumpDailySingleStock(StockDefinition stockDefinition)
        {
            _processingInfo.Stock = stockDefinition;
            _processingInfo.CurrentPosition++;
            _onStockStartProcessing(_processingInfo);
            _dataPump.PumpDaily(stockDefinition);
        }
    }
}
