using System;
using System.IO;
using MarketOps.DataPump.Bossa;
using NUnit.Framework;
using Shouldly;

namespace MarketOps.Tests.DataPump.Bossa
{
    [TestFixture]
    public class DownloadUnzipPathPreparatorTests
    {
        private DownloadUnzipPathPreparator TestObj;

        private readonly string _rootPath = Path.Combine(Path.GetDirectoryName(new Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).LocalPath), "DownloadUnzipPathPreparatorTests");
        private DownloadDirectories _downloadDirectories;

        [SetUp]
        public void SetUp()
        {
            DirectoryUtils.ClearDir(_rootPath, true);
            _downloadDirectories = new DownloadDirectories(_rootPath);
            TestObj = new DownloadUnzipPathPreparator(_downloadDirectories);
        }

        [TearDown]
        public void TearDown()
        {
            DirectoryUtils.ClearDir(_rootPath, false);
        }

        [Test]
        public void Prepare__PreparesPath()
        {
            string zipFileName = Path.Combine(_rootPath, "test.zip");
            TestObj.Prepare(zipFileName).ShouldBe(Path.Combine(_rootPath, "test"));
        }
    }
}
