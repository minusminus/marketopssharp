﻿using MarketOps.Stats.Calculators;
using MarketOps.StockData.Extensions;
using MarketOps.StockData.Types;
using System.Drawing;

namespace MarketOps.Stats.Stats
{
    /// <summary>
    /// Stock value change in specified range.
    /// </summary>
    public class StatRangeChangePcnt : StockStat
    {
        public StatRangeChangePcnt(string chartArea) : base(chartArea) { }

        public override void Calculate(StockPricesData data)
        {
            _data[StatRangeChangePcntData.RangeChange] = RangeChangePcnt.Calculate(data.C, _statParams.Get(StatRangeChangePcntParams.Range).As<int>());
        }

        protected override int GetBackBufferLength() => 
            _statParams.Get(StatRangeChangePcntParams.Range).As<int>();

        protected override void InitializeData()
        {
            _name = "RangeChangePcnt";
            CreateDataStructures(1);
            _dataColors[StatRangeChangePcntData.RangeChange] = Color.IndianRed;
            _dataNames[StatRangeChangePcntData.RangeChange] = "Indicator";
        }

        protected override void InitializeStatParams()
        {
            _statParams.Set(StatRangeChangePcntParams.Range, 12);
        }
    }
}
