using System;
using NUnit.Framework;
using Shouldly;
using MarketOps.StockData.Types;
using MarketOps.StockData.Extensions;

namespace MarketOps.StockData.Tests
{
    [TestFixture]
    public class StockStatParamsTests
    {
        [Test]
        public void Set_Int__GetsCorrectValue()
        {
            StockStatParams testObj = new StockStatParams();
            const int testValue = 123;
            testObj.Set("test", new StockStatParam() { Value = testValue });
            testObj.Get("test").As<int>().ShouldBe(testValue);
        }

        [Test]
        public void Set_Double__GetsCorrectValue()
        {
            StockStatParams testObj = new StockStatParams();
            const double testValue = 123.123;
            testObj.Set("test", new StockStatParam() { Value = testValue });
            testObj.Get("test").As<double>().ShouldBe(testValue);
        }

        [Test]
        public void Set_String__GetsCorrectValue()
        {
            StockStatParams testObj = new StockStatParams();
            const string testValue = "abcdefkgofdsa dfgsdfg";
            testObj.Set("test", new StockStatParam() { Value = testValue });
            testObj.Get("test").As<string>().ShouldBe(testValue);
        }

        [Test]
        public void Set_TwoDifferentParams__GetsCorrectValues()
        {
            StockStatParams testObj = new StockStatParams();
            const int val1 = 123;
            const int val2 = 987;
            testObj.Set("param1", new StockStatParam() { Value = val1 });
            testObj.Set("param2", new StockStatParam() { Value = val2 });
            testObj.Get("param1").As<int>().ShouldBe(val1);
            testObj.Get("param2").As<int>().ShouldBe(val2);
        }

        [Test]
        public void UpdateValue__GetsCorrectValue()
        {
            StockStatParams testObj = new StockStatParams();
            const int val1 = 123;
            const int val2 = 987;
            testObj.Set("test", new StockStatParam() { Value = val1 });
            testObj.Get("test").As<int>().ShouldBe(val1);
            testObj.Set("test", new StockStatParam() { Value = val2 });
            testObj.Get("test").As<int>().ShouldBe(val2);
        }

        [Test]
        public void Get_NotExistingParam__Throws()
        {
            StockStatParams testObj = new StockStatParams();
            Should.Throw<Exception>(() => testObj.Get("notexistingparam"));
        }
    }
}
