using System;
using System.Windows.Forms;
using MarketOps.Controls.Types;
using MarketOps.StockData.Types;

namespace MarketOps.Controls.PriceChart
{
    public delegate void StatStickerDoubleClick(StockStatSticker sticker, StockStat stat);
    public delegate void StatStickerMouseClick(StockStatSticker sticker, StockStat stat, MouseEventArgs e);

    public partial class StockStatSticker : UserControl
    {
        private readonly StockStat _stat;
        private readonly IStockStatsInfoGenerator _statsInfoGenerator;

        public StockStat Stat => _stat;

        public event StatStickerDoubleClick OnStickerDoubleClick;
        public event StatStickerMouseClick OnStickerMouseClick;

        public StockStatSticker(StockStat stat, IStockStatsInfoGenerator statsInfoGenerator)
        {
            _stat = stat;
            _statsInfoGenerator = statsInfoGenerator;
            InitializeComponent();
            UpdateStatInfo();
        }

        public void UpdateStatInfo()
        {
            lblInfo.Text = _statsInfoGenerator.GetStatHeader(_stat);
            BackColor = _stat.DataColor[0];
        }

        private void lblInfo_DoubleClick(object sender, EventArgs e) => 
            OnStickerDoubleClick?.Invoke(this, _stat);

        private void lblInfo_MouseClick(object sender, MouseEventArgs e) => 
            OnStickerMouseClick?.Invoke(this, _stat, e);
    }
}
