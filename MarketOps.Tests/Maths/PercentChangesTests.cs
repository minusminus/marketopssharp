using NUnit.Framework;
using Shouldly;
using MarketOps.Maths;
using System.Collections.Generic;

namespace MarketOps.Tests.Maths
{
    [TestFixture]
    public class PercentChangesTests
    {
        public class WholeTableTest
        {
            public float[] data;
            public float[] expected;
        }
        public class PartOfTableTest
        {
            public float[] data;
            public int startIndex;
            public int length;
            public float[] expected;
        }

        private static WholeTableTest[] WholeTableTests =
        {
            new WholeTableTest()
            {
                data = new float[] {1, 1, 1, 1},
                expected = new float[] {0, 0, 0}
            },
            new WholeTableTest()
            {
                data = new float[] { 1, 2, 3, 4 },
                expected = new float[] { 1, 0.5f, 0.3333f }
            },
            new WholeTableTest()
            {
                data = new float[] { 4, 3, 2, 1 },
                expected = new float[] { -0.25f, -0.3333f, -0.5f }
            }
        };

        private static PartOfTableTest[] PartOfTableTests =
        {
            new PartOfTableTest()
            {
                data = new float[] { 1, 1, 1, 1 },
                startIndex = 0,
                length = 4,
                expected = new float[] { 0, 0, 0 }
            },
            new PartOfTableTest()
            {
                data = new float[] { 1, 1, 1, 1 },
                startIndex = 1,
                length = 2,
                expected = new float[] { 0 }
            },
            new PartOfTableTest()
            {
                data = new float[] { 1, 2, 3, 4 },
                startIndex = 0,
                length = 4,
                expected = new float[] { 1, 0.5f, 0.3333f }
            },
            new PartOfTableTest()
            {
                data = new float[] { 1, 2, 3, 4 },
                startIndex = 1,
                length = 2,
                expected = new float[] { 0.5f }
            },
            new PartOfTableTest(){
                data = new float[] { 4, 3, 2, 1 },
                startIndex = 0,
                length = 4,
                expected = new float[] { -0.25f, -0.3333f, -0.5f }
            },
            new PartOfTableTest(){
                data = new float[] { 4, 3, 2, 1 },
                startIndex = 1,
                length = 2,
                expected = new float[] { -0.3333f }
            }
        };

        [TestCaseSource(nameof(WholeTableTests))]
        public void Calculate_Floats_WholeTable__CalculatesCorrectly(WholeTableTest testCase)
        {
            PercentChanges.Calculate(testCase.data).ShouldBe(testCase.expected, 0.0001f);
        }

        [Test]
        public void Calculate_Floats_WholeTable_ZeroLength__ReturnsEmptyTable()
        {
            PercentChanges.Calculate(new float[0]).ShouldBeEmpty();
        }

        [TestCaseSource(nameof(PartOfTableTests))]
        public void Calculate_Floats_PartOfTable__CalculatesCorrectly(PartOfTableTest testCase)
        {
            PercentChanges.Calculate(testCase.data, testCase.startIndex, testCase.length).ShouldBe(testCase.expected, 0.0001f);
        }

        [Test]
        public void Calculate_Floats_PartOfTable_TableZeroLength__ReturnsEmptyTable()
        {
            PercentChanges.Calculate(new float[0], 0, 1).ShouldBeEmpty();
        }

        [Test]
        public void Calculate_Floats_PartOfTable_NegativeStartIndex__ReturnsEmptyTable()
        {
            PercentChanges.Calculate(new float[] { 1, 2 }, -1, 1).ShouldBeEmpty();
        }

        [Test]
        public void Calculate_Floats_PartOfTable_NotPositiveLength__ReturnsEmptyTable([Values(0, -1)] int length)
        {
            PercentChanges.Calculate(new float[] { 1, 2 }, 0, length).ShouldBeEmpty();
        }

        [TestCase(1, 10)]
        [TestCase(10, 1)]
        [TestCase(10, 10)]
        public void Calculate_Floats_PartOfTable_OutOfRangeIndexAndCalculationRange__ReturnsEmptyTable(int startIndex, int length)
        {
            PercentChanges.Calculate(new float[] { 1, 2 }, startIndex, length).ShouldBeEmpty();
        }

        [TestCaseSource(nameof(WholeTableTests))]
        public void Calculate_List_WholeTable__CalculatesCorrectly(WholeTableTest testCase)
        {
            PercentChanges.Calculate<float>(new List<float>(testCase.data), TestValueGetter).ShouldBe(testCase.expected, 0.0001f);
        }

        [Test]
        public void Calculate_List_WholeTable_ZeroLength__ReturnsEmptyTable()
        {
            PercentChanges.Calculate<float>(new List<float>(), TestValueGetter).ShouldBeEmpty();
        }

        [TestCaseSource(nameof(PartOfTableTests))]
        public void Calculate_List_PartOfTable__CalculatesCorrectly(PartOfTableTest testCase)
        {
            PercentChanges.Calculate<float>(new List<float>(testCase.data), testCase.startIndex, testCase.length, TestValueGetter).ShouldBe(testCase.expected, 0.0001f);
        }

        [Test]
        public void Calculate_List_PartOfTable_TableZeroLength__ReturnsEmptyTable()
        {
            PercentChanges.Calculate<float>(new List<float>(), 0, 1, TestValueGetter).ShouldBeEmpty();
        }

        [Test]
        public void Calculate_List_PartOfTable_NegativeStartIndex__ReturnsEmptyTable()
        {
            PercentChanges.Calculate<float>(new List<float>() { 1, 2 }, -1, 1, TestValueGetter).ShouldBeEmpty();
        }

        [Test]
        public void Calculate_List_PartOfTable_NotPositiveLength__ReturnsEmptyTable([Values(0, -1)] int length)
        {
            PercentChanges.Calculate<float>(new List<float>() { 1, 2 }, 0, length, TestValueGetter).ShouldBeEmpty();
        }

        [TestCase(1, 10)]
        [TestCase(10, 1)]
        [TestCase(10, 10)]
        public void Calculate_List_PartOfTable_OutOfRangeIndexAndCalculationRange__ReturnsEmptyTable(int startIndex, int length)
        {
            PercentChanges.Calculate<float>(new List<float>() { 1, 2 }, startIndex, length, TestValueGetter).ShouldBeEmpty();
        }

        private float TestValueGetter(float f) => f;
    }
}
