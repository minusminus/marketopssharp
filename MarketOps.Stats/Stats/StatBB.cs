using MarketOps.StockData.Types;
using MarketOps.Stats.Calculators;
using MarketOps.StockData.Extensions;
using System.Drawing;

namespace MarketOps.Stats.Stats
{
    /// <summary>
    /// Bollinger Band stock stat
    /// </summary>
    public class StatBB : StockStat
    {
        public StatBB(string chartArea) : base(chartArea) { }

        public override void Calculate(StockPricesData data)
        {
            BBData res = (new BB()).Calculate(data.C, _statParams.Get(StatBBParams.Period).As<int>(), _statParams.Get(StatBBParams.SigmaWidth).As<float>());
            _data[StatBBData.BBH] = res.BBH;
            _data[StatBBData.SMA] = res.SMA;
            _data[StatBBData.BBL] = res.BBL;
        }

        protected override int GetBackBufferLength() => 
            _statParams.Get(StatBBParams.Period).As<int>();

        protected override void InitializeData()
        {
            _name = "BB";
            CreateDataStructures(3);
            _dataColors[StatBBData.BBH] = Color.SaddleBrown;
            _dataColors[StatBBData.SMA] = Color.DarkRed;
            _dataColors[StatBBData.BBL] = Color.SaddleBrown;
            _dataNames[StatBBData.BBH] = "High band";
            _dataNames[StatBBData.SMA] = "SMA band";
            _dataNames[StatBBData.BBL] = "Low band";
        }

        protected override void InitializeStatParams()
        {
            _statParams.Set(StatBBParams.Period, 20);
            _statParams.Set(StatBBParams.SigmaWidth, 2.0f);
        }
    }
}
