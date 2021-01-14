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

        public void LoadData(List<PointChartData> data)
        {
            FillBindingSource(data);
            BindChartData();
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
