using MarketOps.Stats.Calculators;
using MarketOps.StockData.Extensions;
using MarketOps.StockData.Types;
using System.Drawing;

namespace MarketOps.Stats.Stats
{
    /// <summary>
    /// High-Low channel stock stat.
    /// </summary>
    public class StatHLChannel : StockStat
    {
        public StatHLChannel(string chartArea) : base(chartArea) { }

        public override void Calculate(StockPricesData data)
        {
            HLChannelData res = HLChannel.Calculate(data.H, data.L, _statParams.Get(StatHLChannelParams.Period).As<int>());
            _data[StatHLChannelData.H] = res.H;
            _data[StatHLChannelData.L] = res.L;
        }

        protected override int GetBackBufferLength() => 
            _statParams.Get(StatHLChannelParams.Period).As<int>();

        protected override void InitializeData()
        {
            _name = "HLChannel";
            CreateDataStructures(2);
            _dataColors[StatHLChannelData.H] = Color.CornflowerBlue;
            _dataColors[StatHLChannelData.L] = Color.CornflowerBlue;
            _dataNames[StatHLChannelData.H] = "High band";
            _dataNames[StatHLChannelData.L] = "Low band";
        }

        protected override void InitializeStatParams()
        {
            _statParams.Set(StatHLChannelParams.Period, 10);
        }
    }
}
