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
        private const string ParamPeriod = "Period";
        private const string ParamSigmaWidth = "SigmaWidth";
        public StatBB(string chartArea) : base(chartArea) { }

        protected override void InitializeData()
        {
            _name = "BB";
            CreateDataStructures(3);
            _dataColors[0] = Color.SaddleBrown;
            _dataColors[1] = Color.DarkRed;
            _dataColors[2] = Color.SaddleBrown;
        }

        protected override void InitializeStatParams()
        {
            _statParams.Set(ParamPeriod, new StockStatParamInt() { Name = ParamPeriod, Value = 20 });
            _statParams.Set(ParamSigmaWidth, new StockStatParamFloat() { Name = ParamSigmaWidth, Value = 2.0f });
        }

        public override void Calculate(StockPricesData data)
        {
            BBData res = (new BB()).Calculate(data.C, _statParams.Get(ParamPeriod).As<int>(), _statParams.Get(ParamSigmaWidth).As<float>());
            _data[0] = res.BBL;
            _data[1] = res.SMA;
            _data[2] = res.BBH;
        }
    }
}
