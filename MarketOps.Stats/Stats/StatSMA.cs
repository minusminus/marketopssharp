using MarketOps.StockData.Types;
using MarketOps.Stats.Calculators;
using MarketOps.StockData.Extensions;
using System.Drawing;

namespace MarketOps.Stats.Stats
{
    /// <summary>
    /// SMA stock stat
    /// </summary>
    public class StatSMA : StockStat
    {
        public StatSMA(string chartArea) : base(chartArea) { }

        protected override void InitializeData()
        {
            _name = "SMA";
            CreateDataStructures(1);
            _dataColors[StatSMAData.SMA] = Color.IndianRed;
            _dataNames[StatSMAData.SMA] = "Indicator";
        }

        protected override void InitializeStatParams()
        {
            _statParams.Set(StatSMAParams.Period, 20);
        }

        protected override int GetBackBufferLength()
        {
            return _statParams.Get(StatSMAParams.Period).As<int>();
        }

        public override void Calculate(StockPricesData data)
        {
            _data[StatSMAData.SMA] = (new SMA()).Calculate(data.C, _statParams.Get(StatSMAParams.Period).As<int>());
        }
    }
}
