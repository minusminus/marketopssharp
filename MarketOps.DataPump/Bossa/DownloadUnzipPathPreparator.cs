using System.IO;

namespace MarketOps.DataPump.Bossa
{
    /// <summary>
    /// Bossa download unzip path preparator.
    /// 
    /// Prepared path: filename without extension in zip file path.
    /// </summary>
    internal class DownloadUnzipPathPreparator
    {
        private readonly DownloadDirectories _downloadDirectories;

        public DownloadUnzipPathPreparator(DownloadDirectories downloadDirectories)
        {
            _downloadDirectories = downloadDirectories;
        }

        public string Prepare(string downloadFilePath)
        {
            return _downloadDirectories.GetUnzipPath(Path.GetFileName(downloadFilePath));
        }
    }
}
