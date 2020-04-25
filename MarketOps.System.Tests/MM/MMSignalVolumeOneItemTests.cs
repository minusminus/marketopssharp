using System;
using MarketOps.System.MM;
using NUnit.Framework;
using Shouldly;
using System.Linq;

namespace MarketOps.System.Tests.MM
{
    [TestFixture]
    public class MMSignalVolumeOneItemTests
    {
        private readonly MMSignalVolumeOneItem _testObj = new MMSignalVolumeOneItem();

        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(10)]
        [TestCase(12345678)]
        public void GetSignalVolume__ReturnsOne(int initialVolume)
        {
            Signal sig = new Signal() { Volume = initialVolume };
            _testObj.GetSignalVolume(sig).ShouldBe(1);
        }

        [Test]
        public void GetSignalVolume_RandomValues__ReturnsOne()
        {
            Random r = new Random();
            Enumerable.Range(1, 10).ToList()
                .ForEach(_ =>
                {
                    int v = r.Next(1000);
                    _testObj.GetSignalVolume(new Signal() { Volume = v }).ShouldBe(1, $"{v}");
                });
        }
    }
}
