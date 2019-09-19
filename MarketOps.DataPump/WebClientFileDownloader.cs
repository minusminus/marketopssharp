using System;
using System.Net;

namespace MarketOps.DataPump
{
    /// <summary>
    /// Download file with System.Net.WebClient
    /// </summary>
    internal class WebClientFileDownloader : IFileDownloader
    {
        public void Download(string url, string destFilePath)
        {
            using (var client = new WebClient())
            {
                client.DownloadFile(url, destFilePath);
            }
        }
    }
}
