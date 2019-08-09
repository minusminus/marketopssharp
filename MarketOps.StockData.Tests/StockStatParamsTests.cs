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
            testObj.Set("test", new StockStatParamInt() { Value = testValue });
            testObj.Get("test").As<int>().ShouldBe(testValue);
        }

        [Test]
        public void Set_Float__GetsCorrectValue()
        {
            StockStatParams testObj = new StockStatParams();
            const float testValue = 123.123f;
            testObj.Set("test", new StockStatParamFloat() { Value = testValue });
            testObj.Get("test").As<float>().ShouldBe(testValue);
        }

        [Test]
        public void Set_String__GetsCorrectValue()
        {
            StockStatParams testObj = new StockStatParams();
            const string testValue = "abcdefkgofdsa dfgsdfg";
            testObj.Set("test", new StockStatParamString() { Value = testValue });
            testObj.Get("test").As<string>().ShouldBe(testValue);
        }

        [Test]
        public void Set_TwoDifferentParams__GetsCorrectValues()
        {
            StockStatParams testObj = new StockStatParams();
            const int val1 = 123;
            const int val2 = 987;
            testObj.Set("param1", new StockStatParamInt() { Value = val1 });
            testObj.Set("param2", new StockStatParamInt() { Value = val2 });
            testObj.Get("param1").As<int>().ShouldBe(val1);
            testObj.Get("param2").As<int>().ShouldBe(val2);
        }

        [Test]
        public void UpdateValue__GetsCorrectValue()
        {
            StockStatParams testObj = new StockStatParams();
            const int val1 = 123;
            const int val2 = 987;
            testObj.Set("test", new StockStatParamInt() { Value = val1 });
            testObj.Get("test").As<int>().ShouldBe(val1);
            testObj.Set("test", new StockStatParamInt() { Value = val2 });
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
