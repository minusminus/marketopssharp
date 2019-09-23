using System.Collections.Generic;
using System.Linq;

namespace MarketOps.DataPump.Bossa
{
    /// <summary>
    /// Downloading files queue.
    /// All queue operations in critical section.
    /// </summary>
    internal class DownloadFilesQueue
    {
        private readonly object _criticalSection = new object();

        private readonly Dictionary<string, DownloadFileStage> _downloads = new Dictionary<string, DownloadFileStage>();

        public void AddToDownload(string downloadUrl)
        {
            lock (_criticalSection)
            {
                if(!_downloads.ContainsKey(downloadUrl))
                    _downloads.Add(downloadUrl, DownloadFileStage.ToDownload);
            }
        }

        public void SetStage(string downloadUrl, DownloadFileStage newStage)
        {
            lock (_criticalSection)
            {
                if (_downloads.ContainsKey(downloadUrl))
                    _downloads[downloadUrl] = newStage;
            }
        }

        public DownloadFileStage GetStage(string downloadUrl)
        {
            lock (_criticalSection)
            {
                DownloadFileStage stage = DownloadFileStage.Undefined;
                _downloads.TryGetValue(downloadUrl, out stage);
                return stage;
            }
        }

        //public string FristToDownload()
        //{
        //    lock (_criticalSection)
        //    {
        //        //if (_toDownload.Count == 0) return "";
        //        //return _toDownload[0];
        //    }
        //}
    }
}
