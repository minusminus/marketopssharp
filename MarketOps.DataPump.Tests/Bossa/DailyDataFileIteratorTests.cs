using System;
using NUnit.Framework;
using Shouldly;
using System.IO;
using MarketOps.DataPump.Bossa;

namespace MarketOps.DataPump.Tests.Bossa
{
    [TestFixture]
    public class DailyDataFileIteratorTests
    {
        private DailyDataFileIterator TestObj;

        private readonly string _testFilePath =
            Path.Combine(
                Path.GetDirectoryName(new Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).LocalPath),
                "TestFiles", "USDPLN.mst");

        [SetUp]
        public void SetUp()
        {
            TestObj = new DailyDataFileIterator();
        }

        [TearDown]
        public void TearDown()
        {
            TestObj.Close();
        }

        private bool CheckTestFileOpened()
        {
            try
            {
                using (FileStream fs = new FileStream(_testFilePath, FileMode.Open, FileAccess.Read, FileShare.None))
                    return false;
            }
            catch (IOException e)
            {
                return true;
            }
        }

        [Test]
        public void Open__OpensDataFile()
        {
            TestObj.Open(_testFilePath);
            CheckTestFileOpened().ShouldBeTrue();
            TestObj.PreviousLine().ShouldBeNullOrEmpty();
        }

        [Test]
        public void Close__ClosesDataFile()
        {
            DailyDataFileIterator obj = new DailyDataFileIterator();
            obj.Open(_testFilePath);
            CheckTestFileOpened().ShouldBeTrue();
            obj.Close();
            CheckTestFileOpened().ShouldBeFalse();
            TestObj.PreviousLine().ShouldBeNullOrEmpty();
        }

        [Test]
        public void Eof_InsideFile__ReturnsFalse()
        {
            TestObj.Open(_testFilePath);
            TestObj.Eof().ShouldBeFalse();
            for (int i = 0; i < 3; i++)
                TestObj.ReadLine();
            TestObj.Eof().ShouldBeFalse();
        }

        [Test]
        public void Eof_AfteLastLine__ReturnsTrue()
        {
            TestObj.Open(_testFilePath);
            while (!TestObj.Eof()) TestObj.ReadLine();
            TestObj.Eof().ShouldBeTrue();
        }

        [Test]
        public void ReadLine__ReturnsLine()
        {
            TestObj.Open(_testFilePath);
            while (!TestObj.Eof())
                TestObj.ReadLine().ShouldNotBeNullOrEmpty();
        }

        [Test]
        public void ReadLine_AfterLastLine__ReturnsNull()
        {
            ReadLine__ReturnsLine();
            TestObj.ReadLine().ShouldBeNull();
        }

        [Test]
        public void PreviousLine__ReturnsPreviousLine()
        {
            string prevLine = null;
            TestObj.Open(_testFilePath);
            while (!TestObj.Eof())
            {
                string currLine = TestObj.ReadLine();
                TestObj.PreviousLine().ShouldBe(prevLine);
                prevLine = currLine;
            }
        }

        [Test]
        public void PreviousLine_AfterLastLine__ReturnsOneBeforeLastLine()
        {
            ReadLine__ReturnsLine();
            TestObj.PreviousLine().ShouldBe("USDPLN,20191004,3.9478,3.9499,3.9299,3.9299,0");
        }

        [Test]
        public void SetOnLineAfterTS__SetsOnLine()
        {
            TestObj.Open(_testFilePath);
            TestObj.SetOnLineAfterTS(new DateTime(2019, 01, 01)).ShouldBeTrue();
            TestObj.ReadLine().ShouldBe("USDPLN,20190102,3.7352,3.7916,3.7244,3.7852,0");
        }

        [Test]
        public void SetOnLineAfterTS_TSAboveUpperBound__ReturnsFalse()
        {
            TestObj.Open(_testFilePath);
            TestObj.SetOnLineAfterTS(new DateTime(2100, 01, 01)).ShouldBeFalse();
        }
    }
}
