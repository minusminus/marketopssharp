using System;
using MarketOps.StockData.Types;
using MarketOps.DataPump.Types;
using MarketOps.StockData.Interfaces;
using MarketOps.DataPump.DBWriters;
using System.IO;

namespace MarketOps.DataPump.Bossa
{
    /// <summary>
    /// Bossa data pump mechanism
    /// </summary>
    internal class DataPump : IDataPump
    {
        private readonly IDataPumpProvider _dataPumpProvider;
        private readonly IDataFileIterator _dataFileIterator;
        private readonly IDataPumpStockDataToDBWriter _stockDataToDBWriter;
        private readonly IDataFileLineToStockData _lineToStockData;
        private readonly IDataFileDownloader _dataFileDownloader;
        private readonly DownloadDirectories _downloadDirectories;

        public DataPump(IDataPumpProvider dataPumpProvider, IDataFileIterator dataFileIterator, 
            IDataPumpStockDataToDBWriter stockDataToDBWriter, IDataFileLineToStockData lineToStockData,
            IDataFileDownloader dataFileDownloader, DownloadDirectories downloadDirectories)
        {
            _dataPumpProvider = dataPumpProvider;
            _dataFileIterator = dataFileIterator;
            _stockDataToDBWriter = stockDataToDBWriter;
            _lineToStockData = lineToStockData;
            _dataFileDownloader = dataFileDownloader;
            _downloadDirectories = downloadDirectories;
        }

        public void UpdateStartTS(StockType stockType)
        {
            new StartTSUpdater(_dataPumpProvider).Update(stockType);
        }

        public void PumpDaily(StockDefinition stockDefinition)
        {
            ProcessFileDaily(GetDataFilePath(DownloadFileDaily(stockDefinition), stockDefinition), stockDefinition);
        }

        private string DownloadFileDaily(StockDefinition stockDefinition)
        {
            string res = _dataFileDownloader.InitializeDownload(stockDefinition, DataPumpDownloadRange.Daily);
            _dataFileDownloader.WaitForDownload(res);
            return res;
        }

        private string GetDataFilePath(string zipFilePath, StockDefinition stockDefinition)
        {
            return Path.Combine(_downloadDirectories.GetUnzipPath(zipFilePath), stockDefinition.Name + ".mst");
        }

        private void ProcessFileDaily(string fileName, StockDefinition stockDefinition)
        {
            DataPumpStockData data = new DataPumpStockData();
            _stockDataToDBWriter.StartSession();
            try
            {
                _dataFileIterator.Open(fileName);
                try
                {
                    _dataFileIterator.SetOnLineAfterTS(_dataPumpProvider.GetMaxTS(stockDefinition, StockDataRange.Day, 0));
                    while (!_dataFileIterator.Eof())
                    {
                        string prevLine = _dataFileIterator.PreviousLine();
                        _lineToStockData.Map(_dataFileIterator.ReadLine(), prevLine, data);
                        _stockDataToDBWriter.WriteDaily(data, stockDefinition);
                    }
                }
                finally
                {
                    _dataFileIterator.Close();
                }
            } finally
            {
                _stockDataToDBWriter.EndSession();
            }
        }
    }
}
