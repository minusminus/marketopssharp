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
        private Action<DataPumperDailyProcessingInfo, Exception> _onStockProcessingException;
        private Action<string, Exception> _onProcessingException;
        private DataPumperDailyProcessingInfo _processingInfo;

        public DataPumper(IDataPumpProvider dataPumpProvider, IDataPump dataPump)
        {
            _dataPumpProvider = dataPumpProvider;
            _dataPump = dataPump;
        }

        public void StartSession(Action<DataPumperDailyProcessingInfo> onStockStartProcessing,
            Action<DataPumperDailyProcessingInfo, Exception> onStockProcessingException,
            Action<string, Exception> onProcessingException)
        {
            _onStockStartProcessing = onStockStartProcessing;
            _onStockProcessingException = onStockProcessingException;
            _onProcessingException = onProcessingException;
            _stocks = _dataPumpProvider.GetAllStockDefinitions();
            _processingInfo = new DataPumperDailyProcessingInfo();
        }

        public void EndSession()
        {
            _processingInfo = null;
            _stocks = null;
            _onProcessingException = null;
            _onStockProcessingException = null;
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
            _onStockStartProcessing?.Invoke(_processingInfo);
            try
            {
                _dataPump.PumpDaily(stockDefinition);
            }
            catch (Exception e)
            {
                _onStockProcessingException?.Invoke(_processingInfo, e);
            }
        }

        private void PumpDailyUpdateStartTS(StockType stockType)
        {
            try
            {
                _dataPump.UpdateStartTS(stockType);
            }
            catch (Exception e)
            {
                _onProcessingException?.Invoke("UpdateStartTS", e);
            }
        }
    }
}
