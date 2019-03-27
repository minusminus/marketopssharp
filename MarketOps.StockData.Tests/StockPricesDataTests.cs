using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Shouldly;
using MarketOps.StockData.Types;

namespace MarketOps.StockData.Tests
{
    [TestFixture]
    public class StockPricesDataTests
    {
        private void CheckCreatedObject(StockPricesData obj, int expectedLength)
        {
            obj.O.Length.ShouldBe(expectedLength);
            obj.H.Length.ShouldBe(expectedLength);
            obj.L.Length.ShouldBe(expectedLength);
            obj.C.Length.ShouldBe(expectedLength);
            obj.V.Length.ShouldBe(expectedLength);
            obj.TS.Length.ShouldBe(expectedLength);
            obj.Length.ShouldBe(expectedLength);
        }

        [Test]
        public void CreatedEmpty_Length0()
        {
            CheckCreatedObject(new StockPricesData(0), 0);
        }

        [Test]
        public void CreatedWithLength_EqualsLength()
        {
            CheckCreatedObject(new StockPricesData(10), 10);
        }
    }
}
