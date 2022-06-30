using System.Collections.Generic;
using System.Windows.Forms;

namespace MarketOps.Controls.PointChart
{
    public partial class PointChart : UserControl
    {
        public PointChart()
        {
            InitializeComponent();
        }

        public void LoadData(List<PointChartData> data, string seriesTooltip)
        {
            chartPoints.DataSource = data;
            chartPoints.Series["seriesPoints"].ToolTip = seriesTooltip;
        }
    }
}
