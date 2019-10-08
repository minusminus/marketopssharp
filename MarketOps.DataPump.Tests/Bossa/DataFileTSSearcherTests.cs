using System;
using NUnit.Framework;
using Shouldly;
using System.IO;
using MarketOps.DataPump.Bossa;

namespace MarketOps.DataPump.Tests.Bossa
{
    [TestFixture]
    public class DataFileTSSearcherTests
    {
        private DataFileTSSearcher TestObj;

        private readonly string _testFilePath =
            Path.Combine(
                Path.GetDirectoryName(new Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).LocalPath),
                "TestFiles", "USDPLN.mst");
        private FileStream _fileStream;
        private StreamReader _fileReader;

        [SetUp]
        public void SetUp()
        {
            _fileStream = new FileStream(_testFilePath, FileMode.Open, FileAccess.Read);
            _fileReader = new StreamReader(_fileStream);
            TestObj = new DataFileTSSearcher(_fileReader);
        }

        [TearDown]
        public void TearDown()
        {
            _fileReader.Dispose();
            _fileStream.Dispose();
        }

        [Test]
        public void Find_InboundTSExistsInData__SetsToNextLine()
        {
            TestObj.Find(new DateTime(2019, 01, 04)).ShouldBeTrue();
            _fileReader.EndOfStream.ShouldBeFalse();
            _fileReader.ReadLine().ShouldBe("USDPLN,20190106,3.7587,3.7615,3.7558,3.7565,0");
        }

        [Test]
        public void Find_InboundTSNotExistsInData__SetsToNextLine()
        {
            TestObj.Find(new DateTime(2019, 01, 05)).ShouldBeTrue();
            _fileReader.EndOfStream.ShouldBeFalse();
            _fileReader.ReadLine().ShouldBe("USDPLN,20190106,3.7587,3.7615,3.7558,3.7565,0");
        }

        [Test]
        public void Find_InboundTSLastInFile__SetsToEOF()
        {
            TestObj.Find(new DateTime(2019, 10, 06)).ShouldBeTrue();
            _fileReader.EndOfStream.ShouldBeTrue();
        }

        [Test]
        public void Find_InboundTSFirstInFile__SetsToNextLine()
        {
            TestObj.Find(new DateTime(1999, 01, 04)).ShouldBeTrue();
            _fileReader.EndOfStream.ShouldBeFalse();
            _fileReader.ReadLine().ShouldBe("USDPLN,19990105,3.44,3.4754,3.4061,3.4199,0");
        }

        [Test]
        public void Find_TSBelowLowerBound__ReturnsFalse()
        {
            TestObj.Find(new DateTime(1990, 01, 01)).ShouldBeFalse();
        }

        [Test]
        public void Find_TSAboveUpperBound__ReturnsFalse()
        {
            TestObj.Find(new DateTime(2100, 01, 01)).ShouldBeFalse();
        }
    }
}
