using System;
using System.IO;
using System.IO.Compression;

namespace MarketOps.DataPump
{
    /// <summary>
    /// Unzipping using System.IO.Compression
    /// </summary>
    internal class SystemFileUnzipper : IFileUnzipper
    {
        private void VerifyUnzipParams(string zipFilePath, string destDirectory)
        {
            if (string.IsNullOrWhiteSpace(zipFilePath))
                throw new Exception("Empty zip file path for SystemFileUnzipper.Unzip");
            if (string.IsNullOrWhiteSpace(destDirectory))
                throw new Exception("Empty destination path for SystemFileUnzipper.Unzip");
            if (!Directory.Exists(destDirectory))
                throw new Exception("Destination path not exists for SystemFileUnzipper.Unzip");
        }

        public void Unzip(string zipFilePath, string destDirectory)
        {
            VerifyUnzipParams(zipFilePath, destDirectory);
            ZipFile.ExtractToDirectory(zipFilePath, destDirectory);
        }
    }
}
