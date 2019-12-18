﻿using System;
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
        private const string ParamPeriod = "Period";

        public StatATR(string chartArea) : base(chartArea) { }

        protected override void InitializeData()
        {
            _name = "ATR";
            CreateDataStructures(1);
            _dataColors[0] = Color.Red;
            _dataNames[0] = "Indicator";
        }

        protected override void InitializeStatParams()
        {
            _statParams.Set(ParamPeriod, new StockStatParamInt() { Name = ParamPeriod, Value = 20 });
        }

        public override void Calculate(StockPricesData data)
        {
            _data[0] = (new ATR()).Calculate(data.H, data.L, data.C, _statParams.Get(ParamPeriod).As<int>());
        }
    }
}
