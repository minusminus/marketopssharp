using System;
using MarketOps.SystemExecutor.MM;
using NUnit.Framework;
using Shouldly;
using System.Linq;
using MarketOps.SystemData.Types;
using MarketOps.StockData.Types;
using MarketOps.SystemData.Interfaces;
using NSubstitute;

namespace MarketOps.SystemExecutor.Tests.MM
{
    [TestFixture]
    public class MMSignalVolumeForAllCashTests
    {
        private ICommission _commission;
        private MMSignalVolumeForAllCash _testObj;

        [SetUp]
        public void SetUp()
        {
            _commission = Substitute.For<ICommission>();
            _testObj = new MMSignalVolumeForAllCash(_commission);
        }
    }
}
