using System;
using MarketOps.StockData.Types;
using System.IO;
using MarketOps.DataPump.Types;

namespace MarketOps.DataPump.Bossa
{
    /// <summary>
    /// Download directories manager
    /// </summary>
    internal class DownloadDirectories
    {
        private readonly string _rootPath;

        public DownloadDirectories(string rootPath)
        {
            _rootPath = rootPath;
        }

        public string PreparePath(StockType stockType, DataPumpDownloadRange downloadRange)
        {
            string path = Path.Combine(_rootPath,
                stockType.ToString() + (downloadRange == DataPumpDownloadRange.Ticks ? "_intra" : ""));
            Directory.CreateDirectory(path);
            return path;
        }

        public void ClearAll()
        {
            DirectoryInfo di = new DirectoryInfo(_rootPath);
            foreach (var file in di.EnumerateFiles())
                file.Delete();
            foreach (var dir in di.EnumerateDirectories())
                dir.Delete(true);
        }
    }
}
