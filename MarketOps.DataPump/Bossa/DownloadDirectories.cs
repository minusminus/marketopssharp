using System;
using System.IO;

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

        public string GetDownloadPath(string downloadFileName)
        {
            return Path.Combine(_rootPath, downloadFileName);
        }

        public string GetUnzipPath(string downloadFileName)
        {
            string path = Path.Combine(_rootPath, Path.GetFileNameWithoutExtension(downloadFileName));
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
