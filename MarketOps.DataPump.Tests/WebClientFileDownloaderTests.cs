using System;
using NUnit.Framework;
using Shouldly;
using System.IO;

namespace MarketOps.DataPump.Tests
{
    [TestFixture]
    public class WebClientFileDownloaderTests
    {
        private readonly WebClientFileDownloader TestObj = new WebClientFileDownloader();

        private readonly string DownloadPath = Path.Combine(Path.GetDirectoryName(new Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).LocalPath), "WebClientFileDownloaderTests");
        private readonly string DownloadUrl = @"https://info.bossa.pl/pub/metastock/mstock/mstall.lst";
        private string _downloadFileName;

        [SetUp]
        public void SetUp()
        {
            _downloadFileName = Path.Combine(DownloadPath, "mstall.lst");
            DirectoryUtils.ClearDir(DownloadPath, true);
        }

        [TearDown]
        public void TearDown()
        {
            DirectoryUtils.ClearDir(DownloadPath, false);
        }

        [Test]
        public void Download__FileDownloaded()
        {
            TestObj.Download(DownloadUrl, _downloadFileName);
            File.Exists(_downloadFileName).ShouldBeTrue();
        }

        [Test]
        public void Download_EmptyUrl__Throws()
        {
            Should.Throw<Exception>(() => TestObj.Download("", _downloadFileName));
        }

        [Test]
        public void Download_EmptyFilePath__Throws()
        {
            Should.Throw<Exception>(() => TestObj.Download(DownloadUrl, ""));
        }

        [Test]
        public void Download_NotExistingDownloadDir__Throws()
        {
            Should.Throw<Exception>(() => TestObj.Download(DownloadUrl, Path.Combine(Path.Combine(DownloadPath, "notexistingdir"), "mstall.lst")));
        }
    }
}
