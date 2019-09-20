using System;
using System.Net;
using System.IO;

namespace MarketOps.DataPump
{
    /// <summary>
    /// Download file with System.Net.WebClient
    /// </summary>
    internal class WebClientFileDownloader : IFileDownloader
    {
        private void VerifyDownloadParams(string url, string destFilePath)
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new Exception("Empty url path for WebClientFileDownloader.Download");
            if (string.IsNullOrWhiteSpace(destFilePath))
                throw new Exception("Empty file download path for WebClientFileDownloader.Download");
            if (!Directory.Exists(Path.GetDirectoryName(destFilePath)))
                throw new Exception("Not existing file download path for WebClientFileDownloader.Download");
        }

        public void Download(string url, string destFilePath)
        {
            VerifyDownloadParams(url, destFilePath);
            using (var client = new WebClient())
            {
                client.DownloadFile(url, destFilePath);
            }
        }
    }
}
