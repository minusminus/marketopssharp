using MarketOps.Stats.Calculators;
using MarketOps.StockData.Extensions;
using MarketOps.StockData.Types;
using System.Drawing;

namespace MarketOps.Stats.Stats
{
    /// <summary>
    /// Avg to StdDev of stock value percent changes in specified range.
    /// </summary>
    public class StatAvgToStdDevPercent : StockStat
    {
        public StatAvgToStdDevPercent(string chartArea) : base(chartArea) { }

        public override void Calculate(StockPricesData data)
        {
            _data[StatAvgToStdDevPercentData.RangeStdDev] = AvgToStdDevPercent.Calculate(data.C, _statParams.Get(StatAvgToStdDevPercentParams.Range).As<int>());
        }

        protected override int GetBackBufferLength() =>
            _statParams.Get(StatAvgToStdDevPercentParams.Range).As<int>() + 1;

        protected override void InitializeData()
        {
            _name = "AvgToStdDevPcnt";
            CreateDataStructures(1);
            _dataColors[StatAvgToStdDevPercentData.RangeStdDev] = Color.IndianRed;
            _dataNames[StatAvgToStdDevPercentData.RangeStdDev] = "Indicator";
        }

        protected override void InitializeStatParams()
        {
            _statParams.Set(StatAvgToStdDevPercentParams.Range, 10);
        }
    }
}
