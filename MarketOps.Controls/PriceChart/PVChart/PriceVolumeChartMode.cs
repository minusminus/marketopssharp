namespace MarketOps.Controls.PriceChart.PVChart
{
    /// <summary>
    /// PriceVolume chart display mode.
    /// 
    /// Lines and Candles are mutually exclusive.
    /// </summary>
    public class PriceVolumeChartMode
    {
        private bool _lines;
        private bool _candles;

        public bool Lines { get => _lines; set => SetLinesCanldes(value, false); }
        public bool Candles { get => _candles; set => SetLinesCanldes(false, value); }
        public bool HeikinAshi { get; set; }

        private void SetLinesCanldes(bool lines, bool candles)
        {
            _lines = lines;
            _candles = candles;
        }
    }
}
