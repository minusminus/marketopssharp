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
            FillBindingSource(data);
            BindChartData();
            chartColumns.Series["seriesColumns"].ToolTip = seriesTooltip;
        }

        private void FillBindingSource(List<ColumnChartData> data)
        {
            srcColumns.Clear();
            data.ForEach(v => srcColumns.Add(v));
        }

        private void BindChartData()
        {
            chartColumns.DataBind();
        }
    }
}
