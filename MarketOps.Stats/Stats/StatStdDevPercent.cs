using MarketOps.Stats.Calculators;
using MarketOps.StockData.Extensions;
using MarketOps.StockData.Types;
using System.Drawing;

namespace MarketOps.Stats.Stats
{
    /// <summary>
    /// StdDev of stock value percent change in specified range.
    /// </summary>
    public class StatStdDevPercent : StockStat
    {
        public StatStdDevPercent(string chartArea) : base(chartArea) { }

        public override void Calculate(StockPricesData data)
        {
            _data[StatStdDevPercentData.Range] = StdDevPercent.Calculate(data.C, _statParams.Get(StatRangeChangePcntParams.Range).As<int>());
        }

        protected override int GetBackBufferLength() =>
            _statParams.Get(StatStdDevPercentParams.Range).As<int>() + 1;

        protected override void InitializeData()
        {
            _name = "StdDevPcnt";
            CreateDataStructures(1);
            _dataColors[StatStdDevPercentData.Range] = Color.IndianRed;
            _dataNames[StatStdDevPercentData.Range] = "Indicator";
        }

        protected override void InitializeStatParams()
        {
            _statParams.Set(StatStdDevPercentParams.Range, 10);
        }
    }
}
