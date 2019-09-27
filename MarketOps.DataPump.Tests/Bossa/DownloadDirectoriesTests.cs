using System;
using NUnit.Framework;
using Shouldly;
using System.IO;
using MarketOps.DataPump.Bossa;
using MarketOps.StockData.Types;
using MarketOps.DataPump.Types;

namespace MarketOps.DataPump.Tests.Bossa
{
    [TestFixture]
    public class DownloadDirectoriesTests
    {
        private DownloadDirectories TestObj;

        private readonly string _rootPath = Path.Combine(Path.GetDirectoryName(new Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).LocalPath), "BossaDownloadDirectories");

        [SetUp]
        public void SetUp()
        {
            DirectoryUtils.ClearDir(_rootPath, true);
            TestObj = new DownloadDirectories(_rootPath);
        }

        [TearDown]
        public void TearDown()
        {
            DirectoryUtils.ClearDir(_rootPath, false);
        }

        [Test]
        public void PreparePath_Std__PreparesPath()
        {
            TestObj.PreparePath(StockType.Index, DataPumpDownloadRange.Daily);
            Directory.Exists(Path.Combine(_rootPath, StockType.Index.ToString())).ShouldBeTrue();
        }

        [Test]
        public void PreparePath_Intra__PreparesPath()
        {
            TestObj.PreparePath(StockType.Index, DataPumpDownloadRange.Ticks);
            Directory.Exists(Path.Combine(_rootPath, StockType.Index.ToString() + "_intra")).ShouldBeTrue();
        }

        [Test]
        public void ClearAll__LeavesEmptyDir()
        {
            Directory.CreateDirectory(Path.Combine(_rootPath, "dir1"));
            Directory.CreateDirectory(Path.Combine(_rootPath, "dir2"));
            Directory.CreateDirectory(Path.Combine(_rootPath, "dir2", "dir21"));
            using (StreamWriter file = new StreamWriter(Path.Combine(_rootPath, "test.txt")))
                file.WriteLine("test text");

            TestObj.ClearAll();

            DirectoryInfo di = new DirectoryInfo(_rootPath);
            di.GetFiles().Length.ShouldBe(0);
            di.GetDirectories().Length.ShouldBe(0);
        }
    }
}
