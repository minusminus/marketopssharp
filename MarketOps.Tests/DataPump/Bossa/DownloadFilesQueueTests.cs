using NUnit.Framework;
using Shouldly;
using MarketOps.DataPump.Bossa;

namespace MarketOps.Tests.DataPump.Bossa
{
    [TestFixture]
    public class DownloadFilesQueueTests
    {
        private DownloadFilesQueue TestObj;

        private const string DL1 = @"https://bossa.pl/pub/metastock/mstock/mstall.lst";
        private const string DL2 = @"http://bossa.pl/pub/metastock/waluty/mstnbp.lst";

        [SetUp]
        public void SetUp()
        {
            TestObj = new DownloadFilesQueue();
        }

        [Test]
        public void AddToDownload__AddsNewDownload()
        {
            TestObj.AddToDownload(DL1);
        }

        [Test]
        public void Next__GetsAddedElement()
        {
            TestObj.AddToDownload(DL1);
            TestObj.Next().ShouldBe(DL1);
        }

        [Test]
        public void Next_TwoElements__GetsElementsInAdditionOrder()
        {
            TestObj.AddToDownload(DL1);
            TestObj.AddToDownload(DL2);
            TestObj.Next().ShouldBe(DL1);
            TestObj.Next().ShouldBe(DL2);
        }

        [Test]
        public void Next_Empty__ReturnsEmptyString()
        {
            TestObj.Next().ShouldBeEmpty();
        }

        [Test]
        public void Next_AfterAllAdded__ReturnsEmptyString()
        {
            TestObj.AddToDownload(DL1);
            TestObj.AddToDownload(DL2);
            TestObj.Next().ShouldBe(DL1);
            TestObj.Next().ShouldBe(DL2);
            TestObj.Next().ShouldBeEmpty();
        }

        [Test]
        public void SetStage_Empty__DoesNothing()
        {
            TestObj.SetStage(DL1, DownloadFileStage.Download);
        }

        [Test]
        public void SetStage_ExistingElement__SetsStage()
        {
            TestObj.AddToDownload(DL1);
            TestObj.Next();
            TestObj.SetStage(DL1, DownloadFileStage.Done);
        }

        [Test]
        public void GetStage__ReturnsElementStage()
        {
            TestObj.AddToDownload(DL1);
            TestObj.Next();
            TestObj.SetStage(DL1, DownloadFileStage.Done);
            TestObj.GetStage(DL1).ShouldBe(DownloadFileStage.Done);
        }

        [Test]
        public void GetStage_Empty__ReturnsUndefined()
        {
            TestObj.GetStage(DL1).ShouldBe(DownloadFileStage.Undefined);
        }

        [Test]
        public void GetStage_NonExistingElement__ReturnsUndefined()
        {
            TestObj.AddToDownload(DL1);
            TestObj.Next();
            TestObj.GetStage(DL2).ShouldBe(DownloadFileStage.Undefined);
        }
    }
}
