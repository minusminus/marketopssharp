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
        public void Map_CorrectData_NoPrevLine__MapsCorrectly()
        {
            const string line = "KGHM,20191004,74.0200,74.9400,73.6200,74.9000,439632";

            TestObj.Map(line, null, _mappedData);
            _mappedData.O.ShouldBe("74.0200");
            _mappedData.H.ShouldBe("74.9400");
            _mappedData.L.ShouldBe("73.6200");
            _mappedData.C.ShouldBe("74.9000");
            _mappedData.RefCourse.ShouldBe("0");
            _mappedData.V.ShouldBe("439632");
            _mappedData.TS.ShouldBe(new DateTime(2019, 10, 04));
        }

        [Test]
        public void Map_CorrectData_WithPrevLine__MapsCorrectly()
        {
            const string line = "KGHM,20191004,74.0200,74.9400,73.6200,74.9000,439632";
            const string prevLine = "KGHM,20191003,74.0200,74.9400,73.6200,75.8000,439632";

            TestObj.Map(line, prevLine, _mappedData);
            _mappedData.O.ShouldBe("74.0200");
            _mappedData.H.ShouldBe("74.9400");
            _mappedData.L.ShouldBe("73.6200");
            _mappedData.C.ShouldBe("74.9000");
            _mappedData.RefCourse.ShouldBe("75.8000");
            _mappedData.V.ShouldBe("439632");
            _mappedData.TS.ShouldBe(new DateTime(2019, 10, 04));
        }

        [Test]
        public void Map_CorrectDataLength8_NoPrevLine__MapsCorrectly()
        {
            const string line = "FW20WS,20191217,2121,2143,2115,2134,26070,43566";

            TestObj.Map(line, null, _mappedData);
            _mappedData.O.ShouldBe("2121");
            _mappedData.H.ShouldBe("2143");
            _mappedData.L.ShouldBe("2115");
            _mappedData.C.ShouldBe("2134");
            _mappedData.RefCourse.ShouldBe("0");
            _mappedData.V.ShouldBe("26070");
            _mappedData.TS.ShouldBe(new DateTime(2019, 12, 17));
        }

        [Test]
        public void Map_CorrectDataLength8_WithPrevLine__MapsCorrectly()
        {
            const string line = "FW20WS,20191217,2121,2143,2115,2134,26070,43566";
            const string prevLine = "FW20WS,20191216,2114,2123,2107,2120,16770,48210";

            TestObj.Map(line, prevLine, _mappedData);
            _mappedData.O.ShouldBe("2121");
            _mappedData.H.ShouldBe("2143");
            _mappedData.L.ShouldBe("2115");
            _mappedData.C.ShouldBe("2134");
            _mappedData.RefCourse.ShouldBe("2120");
            _mappedData.V.ShouldBe("26070");
            _mappedData.TS.ShouldBe(new DateTime(2019, 12, 17));
        }

        [Test]
        public void Map_NotEnoughColumns__Throws()
        {
            const string line = "KGHM,20191004,74.0200,74.9400";
            Should.Throw<Exception>(() => TestObj.Map(line, null, _mappedData));
        }

        [Test]
        public void Map_TooMuchColumns__Throws()
        {
            const string line = "KGHM,20191004,74.0200,74.9400,73.6200,74.9000,439632,12314,5678";
            Should.Throw<Exception>(() => TestObj.Map(line, null, _mappedData));
        }

        [Test]
        public void Map_IncorrectFloatSeparator__Throws()
        {
            const string line = "KGHM,20191004,74_0200,74.9400,73.6200,74.9000,439632";
            Should.Throw<Exception>(() => TestObj.Map(line, null, _mappedData));
        }

        [Test]
        public void Map_NonNumericValue__Throws()
        {
            const string line = "KGHM,20191004,abcdefgh,74.9400,73.6200,74.9000,439632";
            Should.Throw<Exception>(() => TestObj.Map(line, null, _mappedData));
        }

        [Test]
        public void Map_IncorrectDateValue__Throws()
        {
            const string line = "KGHM,201910041,74.0200,74.9400,73.6200,74.9000,439632";
            Should.Throw<Exception>(() => TestObj.Map(line, null, _mappedData));
        }
    }
}
