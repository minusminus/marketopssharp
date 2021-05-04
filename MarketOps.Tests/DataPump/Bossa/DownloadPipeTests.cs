using System;
using NUnit.Framework;
using Shouldly;
using NSubstitute;
using MarketOps.DataPump.Bossa;
using System.IO;
using MarketOps.DataPump;

namespace MarketOps.Tests.DataPump.Bossa
{
    [TestFixture]
    public class DownloadPipeTests
    {
        private DownloadPipe TestObj;

        private IFileDownloader _fileDownloader;
        private IFileUnzipper _fileUnzipper;
        private DownloadFilesQueue _downloadFilesQueue;

        private const string DL1 = @"https://bossa.pl/pub/metastock/mstock/mstall.lst";
        private readonly string DestPath = Path.Combine(Path.GetDirectoryName(new Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).LocalPath), "DownloadPipeTests", "test.zip");
        private readonly string UnzipPath = Path.Combine(Path.GetDirectoryName(new Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).LocalPath), "DownloadPipeTests", "test");

        [SetUp]
        public void SetUp()
        {
            _downloadFilesQueue = new DownloadFilesQueue();
            _fileDownloader = Substitute.For<IFileDownloader>();
            _fileUnzipper = Substitute.For<IFileUnzipper>();
            TestObj = new DownloadPipe(_fileDownloader, _fileUnzipper, _downloadFilesQueue);

            _downloadFilesQueue.AddToDownload(DL1);
            _downloadFilesQueue.Next();
        }

        [Test]
        public void Process__ProcessesFile()
        {
            TestObj.Process(DL1, DestPath, UnzipPath);
            _downloadFilesQueue.GetStage(DL1).ShouldBe(DownloadFileStage.Done);
            _fileDownloader.Received().Download(DL1, DestPath);
            _fileUnzipper.Received().Unzip(DestPath, UnzipPath);
        }
    }
}
