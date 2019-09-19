using System;

namespace MarketOps.DataPump
{
    /// <summary>
    /// File unzipping interface
    /// </summary>
    internal interface IFileUnzipper
    {
        void Unzip(string zipFilePath, string destDirectory);
    }
}
