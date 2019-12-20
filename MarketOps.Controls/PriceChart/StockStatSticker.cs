using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MarketOps.Controls.Types;
using MarketOps.StockData.Types;

namespace MarketOps.Controls.PriceChart
{
    public partial class StockStatSticker : UserControl
    {
        private readonly StockStat _stat;
        private readonly IStockStatsInfoGenerator _statsInfoGenerator;

        public StockStat Stat => _stat;

        public StockStatSticker(StockStat stat, IStockStatsInfoGenerator statsInfoGenerator)
        {
            _stat = stat;
            _statsInfoGenerator = statsInfoGenerator;
            InitializeComponent();
            UpdateStatInfo();
        }

        private void UpdateStatInfo()
        {
            lblInfo.Text = _statsInfoGenerator.GetStatHeader(_stat);
            BackColor = _stat.DataColor[0];
        }
    }
}
