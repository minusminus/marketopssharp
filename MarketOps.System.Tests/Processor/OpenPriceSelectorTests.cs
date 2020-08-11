﻿using MarketOps.StockData.Types;
using MarketOps.System.Processor;
using NUnit.Framework;
using Shouldly;
using System.Collections.Generic;
using System.Linq;

namespace MarketOps.System.Tests.Processor
{
    [TestFixture]
    public class OpenPriceSelectorTests
    {
        private StockPricesData CreatePricesData(float o, float h, float l, float c)
        {
            StockPricesData res = new StockPricesData(1);
            res.O[0] = o;
            res.H[0] = h;
            res.L[0] = l;
            res.C[0] = c;
            return res;
        }

        [Test]
        public void OnOpen__ReturnsOpenPrice()
        {
            OpenPriceSelector.OnOpen(new Signal(), CreatePricesData(10, 0, 0, 0), 0).ShouldBe(10);
        }

        [Test]
        public void OnClose__ReturnsClosePrice()
        {
            OpenPriceSelector.OnClose(new Signal(), CreatePricesData(0, 0, 0, 10), 0).ShouldBe(10);
        }

        [Test]
        public void OnPrice_LongSignalPriceInRange__ReturnsSignalPrice()
        {
            OpenPriceSelector.OnPrice(new Signal() { Direction = PositionDir.Long, Price = 50 }, CreatePricesData(10, 100, 5, 20), 0).ShouldBe(50);
        }

        [Test]
        public void OnPrice_LongSignalPriceBelowRange__ReturnsOpenPrice()
        {
            OpenPriceSelector.OnPrice(new Signal() { Direction = PositionDir.Long, Price = 5 }, CreatePricesData(10, 100, 5, 20), 0).ShouldBe(10);
        }

        [Test]
        public void OnPrice_ShortSignalPriceInRange__ReturnsSignalPrice()
        {
            OpenPriceSelector.OnPrice(new Signal() { Direction = PositionDir.Short, Price = 50 }, CreatePricesData(70, 100, 5, 20), 0).ShouldBe(50);
        }

        [Test]
        public void OnPrice_ShortSignalPriceBelowRange__ReturnsOpenPrice()
        {
            OpenPriceSelector.OnPrice(new Signal() { Direction = PositionDir.Short, Price = 50 }, CreatePricesData(10, 100, 5, 20), 0).ShouldBe(10);
        }
    }
}
