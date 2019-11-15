using System.IO;

namespace MarketOps
{
    internal class DirectoryUtils
    {
        public static void ClearDir(string dirPath, bool recreateDir)
        {
            if (Directory.Exists(dirPath))
                Directory.Delete(dirPath, true);
            if (recreateDir)
                Directory.CreateDirectory(dirPath);
        }
    }
}
