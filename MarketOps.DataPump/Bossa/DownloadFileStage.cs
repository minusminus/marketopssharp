using System;

namespace MarketOps.DataPump.Bossa
{
    /// <summary>
    /// Download stages of files from bossa.
    /// </summary>
    internal enum DownloadFileStage
    {
        Undefined,
        ToDownload,
        ToUnzip,
        Done
    }
}
