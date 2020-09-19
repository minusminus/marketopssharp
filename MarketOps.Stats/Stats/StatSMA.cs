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
            _dataColors[0] = Color.Red;
            _dataNames[0] = "Indicator";
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
            _data[0] = (new SMA()).Calculate(data.C, _statParams.Get(StatSMAParams.Period).As<int>());
        }
    }
}
