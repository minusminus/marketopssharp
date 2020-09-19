using System;
using NUnit.Framework;
using Shouldly;
using MarketOps.StockData.Types;
using MarketOps.StockData.Extensions;

namespace MarketOps.StockData.Tests
{
    [TestFixture]
    public class MOParamsExtensionsTests
    {
        private const string ParamName = "TestParameter";

        private MOParams testObj;

        [SetUp]
        public void SetUp()
        {
            testObj = new MOParams();
        }

        [Test]
        public void Set_Int__SetsCorrectValue()
        {
            const int testValue = 123;
            testObj.Set(ParamName, testValue);
            testObj.Get(ParamName).As<int>().ShouldBe(testValue);
        }

        [Test]
        public void Set_Float__SetsCorrectValue()
        {
            const float testValue = 123.123f;
            testObj.Set(ParamName, testValue);
            testObj.Get(ParamName).As<float>().ShouldBe(testValue);
        }

        [Test]
        public void Set_String__SetsCorrectValue()
        {
            const string testValue = "abcdefkgofdsa dfgsdfg";
            testObj.Set(ParamName, testValue);
            testObj.Get(ParamName).As<string>().ShouldBe(testValue);
        }
    }
}
