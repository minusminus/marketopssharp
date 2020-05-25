using System;
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

        protected override void InitializeData()
        {
            _name = "BB";
            CreateDataStructures(3);
            _dataColors[0] = Color.SaddleBrown;
            _dataColors[1] = Color.DarkRed;
            _dataColors[2] = Color.SaddleBrown;
            _dataNames[0] = "High band";
            _dataNames[1] = "SMA band";
            _dataNames[2] = "Low band";
        }

        protected override void InitializeStatParams()
        {
            _statParams.Set(StatBBParams.Period, new StockStatParamInt() { Name = StatBBParams.Period, Value = 20 });
            _statParams.Set(StatBBParams.SigmaWidth, new StockStatParamFloat() { Name = StatBBParams.SigmaWidth, Value = 2.0f });
        }

        protected override int GetBackBufferLength()
        {
            return _statParams.Get(StatBBParams.Period).As<int>();
        }

        public override void Calculate(StockPricesData data)
        {
            BBData res = (new BB()).Calculate(data.C, _statParams.Get(StatBBParams.Period).As<int>(), _statParams.Get(StatBBParams.SigmaWidth).As<float>());
            _data[0] = res.BBL;
            _data[1] = res.SMA;
            _data[2] = res.BBH;
        }
    }
}
