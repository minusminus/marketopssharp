using System;
using MarketOps.DataPump.Types;
using MarketOps.StockData.Interfaces;
using MarketOps.StockData.Types;
using MarketOps.DataPump.DBWriters;
using MarketOps.DataPump.Bossa;
using System.Collections.Generic;

namespace MarketOps.DataPump
{
    /// <summary>
    /// Factory creating specified data pump mechanism
    /// </summary>
    public class DataPumpFactory
    {
        public static IDataPump Get(DataPumpType dataPumpType, IDataPumpProvider dataPumpProvider, string downloadPath)
        {
            Dictionary<StockType, DataPumpDownloadDefinition> downloadDefinitions = dataPumpProvider.GetDownloadDefinitions();

            DownloadDirectories downloadDirectories = new DownloadDirectories(downloadPath);
            DownloadUrlPrepator downloadUrlPrepator = new DownloadUrlPrepator(downloadDefinitions);
            DownloadFilePathPreparator downloadFilePathPreparator = new DownloadFilePathPreparator(downloadDefinitions, downloadDirectories);
            DownloadUnzipPathPreparator downloadUnzipPathPreparator = new DownloadUnzipPathPreparator(downloadDirectories);
            DownloadFilesQueue downloadFilesQueue = new DownloadFilesQueue();
            DownloadPipe downloadPipe = new DownloadPipe(new WebClientFileDownloader(), new SystemFileUnzipper(), downloadFilesQueue);
            IDataFileDownloader dataFileDownloader = new DataFileDownloader(downloadPipe, downloadFilesQueue, downloadUrlPrepator, downloadFilePathPreparator, downloadUnzipPathPreparator);
            IDataPumpStockDataToDBWriter stockDataToDBWriter = new DataPumpStockDataToDBWriter(dataPumpProvider, new InsertCommandGenerator(dataPumpProvider));
            IDataFileIterator dataFileIterator = new DailyDataFileIterator();
            IDataFileLineToStockData lineToStockData = new DailyDataFileLineToStockData();

            return new Bossa.DataPump(dataPumpProvider, dataFileIterator, stockDataToDBWriter, lineToStockData, dataFileDownloader, downloadDirectories);
        }
    }
}
