using MarketOps.SystemData.Extensions;
using NUnit.Framework;
using Shouldly;
using System;

namespace MarketOps.Tests.SystemData.Extensions
{
    [TestFixture]
    public class DateTimeExtensionsTests
    {
        [TestCase(2022, 05, 23, false)]
        [TestCase(2022, 05, 31, true)]
        [TestCase(2022, 02, 28, true)]
        [TestCase(2020, 02, 28, false)]
        [TestCase(2020, 02, 29, true)]
        public void IsLastDayOfMonth__ReturnsCorrectly(int year, int month, int day, bool expected)
        {
            new DateTime(year, month, day).IsLastDayOfMonth().ShouldBe(expected);
        }

        [TestCase(2022, 05, 23, false)]
        [TestCase(2022, 05, 30, true)]
        [TestCase(2022, 07, 25, true)]
        [TestCase(2022, 10, 31, true)]
        public void MonthEndsInCurrentWeek__ReturnsCorrectly(int year, int month, int day, bool expected)
        {
            new DateTime(year, month, day).MonthEndsInCurrentWeek().ShouldBe(expected);
        }
    }
}
