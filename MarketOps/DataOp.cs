using System;
using System.Collections.Generic;
using System.Linq;
using MarketOps.StockData.Types;

namespace MarketOps
{
    /// <summary>
    /// Base class for stock data processing.
    /// </summary>
    public abstract class DataOp
    {
        private List<StockDefinition> _stocks;
        private Action<DataOpProcessingInfo> _onStockStartProcessing;
        private Action<DataOpProcessingInfo, Exception> _onStockProcessingException;
        private Action<string, Exception> _onProcessingException;
        private DataOpProcessingInfo _processingInfo;

        public void StartSession(Action<DataOpProcessingInfo> onStockStartProcessing,
            Action<DataOpProcessingInfo, Exception> onStockProcessingException,
            Action<string, Exception> onProcessingException)
        {
            _onStockStartProcessing = onStockStartProcessing;
            _onStockProcessingException = onStockProcessingException;
            _onProcessingException = onProcessingException;
            _stocks = GetAllStocksDefinitions();
            _processingInfo = new DataOpProcessingInfo();
        }

        public void EndSession()
        {
            _processingInfo = null;
            _stocks = null;
            _onProcessingException = null;
            _onStockProcessingException = null;
            _onStockStartProcessing = null;
        }

        protected abstract List<StockDefinition> GetAllStocksDefinitions();

        protected void ProcessStocks(StockType stockType, Action<StockDefinition> generateOp)
        {
            var stocksToProcess = _stocks.Where(x => (x.Type == stockType) && (x.Enabled))
                                      .OrderBy(x => x.ID);

            _processingInfo.CurrentPosition = 0;
            _processingInfo.TotalCount = stocksToProcess.Count();
            try
            {
                foreach (var stockDefinition in stocksToProcess)
                    ProcessSingleStock(stockDefinition, generateOp);
            }
            catch (Exception e)
            {
               ExecuteProcessingException("ProcessStocks", e);
            }
        }

        private void ProcessSingleStock(StockDefinition stockDefinition, Action<StockDefinition> generateOp)
        {
            _processingInfo.Stock = stockDefinition;
            _processingInfo.CurrentPosition++;
            _onStockStartProcessing?.Invoke(_processingInfo);
            try
            {
                generateOp(stockDefinition);
            }
            catch (Exception e)
            {
                ExecuteStockProcessingException(e);
            }
        }

        protected void ExecuteProcessingException(string caption, Exception e)
        {
            _onProcessingException?.Invoke(caption, e);
        }

        protected void ExecuteStockProcessingException(Exception e)
        {
            _onStockProcessingException?.Invoke(_processingInfo, e);
        }
    }
}
