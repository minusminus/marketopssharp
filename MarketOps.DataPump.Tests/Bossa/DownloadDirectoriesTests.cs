using System;
using NUnit.Framework;
using Shouldly;
using System.IO;
using MarketOps.DataPump.Bossa;
using MarketOps.StockData.Types;

namespace MarketOps.DataPump.Tests.Bossa
{
    [TestFixture]
    public class DownloadDirectoriesTests
    {
        private DownloadDirectories TestObj;

        private readonly string RootPath = Path.Combine(Path.GetDirectoryName(new Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).LocalPath), "BossaDownloadDirectories");

        [SetUp]
        public void SetUp()
        {
            DirectoryUtils.ClearDir(RootPath, true);
            TestObj = new DownloadDirectories(RootPath);
        }

        [TearDown]
        public void TearDown()
        {
            DirectoryUtils.ClearDir(RootPath, false);
        }

        [Test]
        public void PreparePath_Std__PreparesPath()
        {
            TestObj.PreparePath(StockType.Index, false);
            Directory.Exists(Path.Combine(RootPath, StockType.Index.ToString())).ShouldBeTrue();
        }

        [Test]
        public void PreparePath_Intra__PreparesPath()
        {
            TestObj.PreparePath(StockType.Index, true);
            Directory.Exists(Path.Combine(RootPath, StockType.Index.ToString() + "_intra")).ShouldBeTrue();
        }

        [Test]
        public void ClearAll__LeavesEmptyDir()
        {
            Directory.CreateDirectory(Path.Combine(RootPath, "dir1"));
            Directory.CreateDirectory(Path.Combine(RootPath, "dir2"));
            Directory.CreateDirectory(Path.Combine(RootPath, "dir2", "dir21"));
            using (StreamWriter file = new StreamWriter(Path.Combine(RootPath, "test.txt")))
                file.WriteLine("test text");

            TestObj.ClearAll();

            DirectoryInfo di = new DirectoryInfo(RootPath);
            di.GetFiles().Length.ShouldBe(0);
            di.GetDirectories().Length.ShouldBe(0);
        }
    }
}
