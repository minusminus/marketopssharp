using System.Collections.Generic;
using System.Windows.Forms;

namespace MarketOps.Controls.ColumnChart
{
    public partial class ColumnChart : UserControl
    {
        public ColumnChart()
        {
            InitializeComponent();
        }

        public void LoadData(List<ColumnChartData> data, string seriesTooltip)
        {
            chartColumns.DataSource = data;
            chartColumns.Series["seriesColumns"].ToolTip = seriesTooltip;
        }
    }
}
