using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Shouldly;
using NSubstitute;
using MarketOps.System.Extensions;
using MarketOps.System.Interfaces;

namespace MarketOps.System.Tests.Extensions
{
    [TestFixture]
    public class SystemValueCalculatorTests
    {
        private SystemValueCalculator _testObj;
        private IDataLoader _dataLoader;

        [SetUp]
        private void SetUp()
        {
            _testObj = new SystemValueCalculator();
            _dataLoader = Substitute.For<IDataLoader>();
        }
    }
}
