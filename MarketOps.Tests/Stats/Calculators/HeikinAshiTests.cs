using NUnit.Framework;
using Shouldly;
using MarketOps.Stats.Calculators;
using MarketOps.StockData.Types;
using System;

namespace MarketOps.Tests.Stats.Calculators
{
    [TestFixture]
    public class HeikinAshiTests
    {
        [Test]
        public void Calculate__ReturnsCorrectly()
        {
            var dt = DateTime.Now.Date;
            var data = new StockPricesData(5);
            FillDataArray(data.O, 10, 20, 30, 40, 50);
            FillDataArray(data.H, 20, 30, 40, 50, 60);
            FillDataArray(data.L, 5, 15, 25, 35, 45);
            FillDataArray(data.C, 15, 25, 35, 45, 55);
            FillDataArray(data.TS, dt);

            var result = HeikinAshi.Calculate(data);

            CheckLength(result, data.Length - 1);
            result.O.ShouldBe(new float[] { 12.5f, 22.5f, 32.5f, 42.5f });
            result.H.ShouldBe(new float[] { 30, 40, 50, 60 });
            result.L.ShouldBe(new float[] { 12.5f, 22.5f, 32.5f, 42.5f });
            result.C.ShouldBe(new float[] { 22.5f, 32.5f, 42.5f, 52.5f });
            result.TS.ShouldBe(new DateTime[] { dt.AddDays(1), dt.AddDays(2), dt.AddDays(3), dt.AddDays(4) });
        }

        [Test]
        public void Calculate_TooShortData__ReturnsEmpty([Values(0, 1)] int length)
        {
            var data = new StockPricesData(length);

            var result = HeikinAshi.Calculate(data);

            CheckLength(result, 0);
        }

        private static void CheckLength(HeikinAshiData result, int expectedLength)
        {
            result.O.Length.ShouldBe(expectedLength);
            result.H.Length.ShouldBe(expectedLength);
            result.L.Length.ShouldBe(expectedLength);
            result.C.Length.ShouldBe(expectedLength);
            result.TS.Length.ShouldBe(expectedLength);
        }

        private static void FillDataArray(in float[] data, params float[] values)
        {
            values.Length.ShouldBe(data.Length);
            for (int i = 0; i < data.Length; i++)
                data[i] = values[i];
        }

        private static void FillDataArray(in DateTime[] data, DateTime startDt)
        {
            for (int i = 0; i < data.Length; i++)
                data[i] = startDt.AddDays(i);
        }
    }
}
