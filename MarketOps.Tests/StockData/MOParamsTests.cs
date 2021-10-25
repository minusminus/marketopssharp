using System;
using NUnit.Framework;
using Shouldly;
using MarketOps.StockData.Types;
using MarketOps.StockData.Extensions;

namespace MarketOps.Tests.StockData
{
    [TestFixture]
    public class MOParamsTests
    {
        [Test]
        public void Set_Int__GetsCorrectValue()
        {
            MOParams testObj = new MOParams();
            const int testValue = 123;
            testObj.Set("test", new MOParamInt() { Value = testValue });
            testObj.Get("test").As<int>().ShouldBe(testValue);
        }

        [Test]
        public void Set_Float__GetsCorrectValue()
        {
            MOParams testObj = new MOParams();
            const float testValue = 123.123f;
            testObj.Set("test", new MOParamFloat() { Value = testValue });
            testObj.Get("test").As<float>().ShouldBe(testValue);
        }

        [Test]
        public void Set_Double__GetsCorrectValue()
        {
            MOParams testObj = new MOParams();
            const double testValue = 123.123;
            testObj.Set("test", new MOParamDouble() { Value = testValue });
            testObj.Get("test").As<double>().ShouldBe(testValue);
        }

        [Test]
        public void Set_String__GetsCorrectValue()
        {
            MOParams testObj = new MOParams();
            const string testValue = "abcdefkgofdsa dfgsdfg";
            testObj.Set("test", new MOParamString() { Value = testValue });
            testObj.Get("test").As<string>().ShouldBe(testValue);
        }

        [Test]
        public void Set_TwoDifferentParams__GetsCorrectValues()
        {
            MOParams testObj = new MOParams();
            const int val1 = 123;
            const int val2 = 987;
            testObj.Set("param1", new MOParamInt() { Value = val1 });
            testObj.Set("param2", new MOParamInt() { Value = val2 });
            testObj.Get("param1").As<int>().ShouldBe(val1);
            testObj.Get("param2").As<int>().ShouldBe(val2);
        }

        [Test]
        public void UpdateValue__GetsCorrectValue()
        {
            MOParams testObj = new MOParams();
            const int val1 = 123;
            const int val2 = 987;
            testObj.Set("test", new MOParamInt() { Value = val1 });
            testObj.Get("test").As<int>().ShouldBe(val1);
            testObj.Set("test", new MOParamInt() { Value = val2 });
            testObj.Get("test").As<int>().ShouldBe(val2);
        }

        [Test]
        public void Get_NotExistingParam__Throws()
        {
            MOParams testObj = new MOParams();
            Should.Throw<Exception>(() => testObj.Get("notexistingparam"));
        }
    }
}
