using MarketOps.StockData.Types;
using MarketOps.Stats.Calculators;
using MarketOps.StockData.Extensions;
using System.Drawing;

namespace MarketOps.Stats.Stats
{
    /// <summary>
    /// ATR stock stat
    /// </summary>
    public class StatATR : StockStat
    {
        public StatATR(string chartArea) : base(chartArea) { }

        public override void Calculate(StockPricesData data)
        {
            _data[StatATRData.ATR] = (new ATR()).Calculate(data.H, data.L, data.C, _statParams.Get(StatATRParams.Period).As<int>());
        }

        protected override int GetBackBufferLength() => 
            _statParams.Get(StatATRParams.Period).As<int>();

        protected override void InitializeData()
        {
            _name = "ATR";
            CreateDataStructures(1);
            _dataColors[StatATRData.ATR] = Color.IndianRed;
            _dataNames[StatATRData.ATR] = "Indicator";
        }

        protected override void InitializeStatParams()
        {
            _statParams.Set(StatATRParams.Period, 20);
        }
    }
}
