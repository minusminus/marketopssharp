using System.Collections.Generic;

namespace MarketOps.DataPump.Bossa
{
    /// <summary>
    /// Downloading files queue.
    /// 
    /// Queue works as fifo.
    /// All queue operations in critical section.
    /// </summary>
    internal class DownloadFilesQueue
    {
        private readonly object _criticalSection = new object();

        private readonly List<string> _toGet = new List<string>();
        private readonly Dictionary<string, DownloadFileStage> _downloads = new Dictionary<string, DownloadFileStage>();

        public void AddToDownload(string downloadUrl)
        {
            lock (_criticalSection)
            {
                if ((!_toGet.Contains(downloadUrl)) && (!_downloads.ContainsKey(downloadUrl)))
                    _toGet.Add(downloadUrl);
            }
        }

        public string Next()
        {
            lock (_criticalSection)
            {
                if (_toGet.Count == 0) return "";
                string res = _toGet[0];
                _downloads.Add(res, DownloadFileStage.Undefined);
                _toGet.RemoveAt(0);
                return res;
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
    }
}
