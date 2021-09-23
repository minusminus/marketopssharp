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
            FillBindingSource(data);
            BindChartData();
            chartPoints.Series["seriesPoints"].ToolTip = seriesTooltip;
        }

        private void FillBindingSource(List<PointChartData> data)
        {
            srcPoints.Clear();
            data.ForEach(v => srcPoints.Add(v));
        }

        private void BindChartData()
        {
            chartPoints.DataBind();
        }
    }
}
