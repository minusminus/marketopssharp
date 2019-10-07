using System;
using NUnit.Framework;
using Shouldly;
using MarketOps.DataPump.Bossa;
using MarketOps.DataPump.Types;

namespace MarketOps.DataPump.Tests.Bossa
{
    [TestFixture]
    public class DailyDataFileLineToStockDataTests
    {
        private readonly DailyDataFileLineToStockData TestObj = new DailyDataFileLineToStockData();
        private readonly DataPumpStockData _mappedData = new DataPumpStockData();

        [Test]
        public void Map_CorrectData__MapsCorrectly()
        {
            const string line = "KGHM,20191004,74.0200,74.9400,73.6200,74.9000,439632";

            TestObj.Map(line, _mappedData);
            _mappedData.O.ShouldBe("74.0200");
            _mappedData.H.ShouldBe("74.9400");
            _mappedData.L.ShouldBe("73.6200");
            _mappedData.C.ShouldBe("74.9000");
            _mappedData.V.ShouldBe("439632");
            _mappedData.TS.ShouldBe(new DateTime(2019, 10, 04));
        }

        [Test]
        public void Map_NotEnoughColumns__Throws()
        {
            const string line = "KGHM,20191004,74.0200,74.9400";
            Should.Throw<Exception>(() => TestObj.Map(line, _mappedData));
        }

        [Test]
        public void Map_TooMuchColumns__Throws()
        {
            const string line = "KGHM,20191004,74.0200,74.9400,73.6200,74.9000,439632,12314";
            Should.Throw<Exception>(() => TestObj.Map(line, _mappedData));
        }

        [Test]
        public void Map_IncorrectFloatSeparator__Throws()
        {
            const string line = "KGHM,20191004,74_0200,74.9400,73.6200,74.9000,439632";
            Should.Throw<Exception>(() => TestObj.Map(line, _mappedData));
        }

        [Test]
        public void Map_NonNumericValue__Throws()
        {
            const string line = "KGHM,20191004,abcdefgh,74.9400,73.6200,74.9000,439632";
            Should.Throw<Exception>(() => TestObj.Map(line, _mappedData));
        }

        [Test]
        public void Map_IncorrectDateValue__Throws()
        {
            const string line = "KGHM,201910041,74.0200,74.9400,73.6200,74.9000,439632";
            Should.Throw<Exception>(() => TestObj.Map(line, _mappedData));
        }
    }
}
