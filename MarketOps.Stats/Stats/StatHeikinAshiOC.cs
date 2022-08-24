using MarketOps.StockData.Types;
using MarketOps.Stats.Calculators;
using MarketOps.StockData.Extensions;
using System.Drawing;

namespace MarketOps.Stats.Stats
{
    /// <summary>
    /// Heikin-Ashi Open/Close stock stat.
    /// 
    /// Calculates bands for heikin-ashi open and close.
    /// </summary>
    public class StatHeikinAshiOC : StockStat
    {
        public StatHeikinAshiOC(string chartArea) : base(chartArea) { }

        public override void Calculate(StockPricesData data)
        {
            HeikinAshiOCData res = HeikinAshiOC.Calculate(data.O, data.H, data.L, data.C);
            _data[StatHeikinAshiOCData.O] = res.O;
            _data[StatHeikinAshiOCData.C] = res.C;
        }

        protected override int GetBackBufferLength() => 1;

        protected override void InitializeData()
        {
            _name = "HeikinAshiOC";
            CreateDataStructures(2);
            _dataColors[StatHeikinAshiOCData.O] = Color.CornflowerBlue;
            _dataColors[StatHeikinAshiOCData.C] = Color.IndianRed;
            _dataNames[StatHeikinAshiOCData.O] = "Open band";
            _dataNames[StatHeikinAshiOCData.C] = "Close band";
        }

        protected override void InitializeStatParams() { }
    }
}
