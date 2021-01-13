using System.Collections.Generic;
using System.Windows.Forms;
using MarketOps.SystemAnalysis;

namespace MarketOps.Controls.DrawDowns
{
    public partial class DrawDowns2DChart : UserControl
    {
        public DrawDowns2DChart()
        {
            InitializeComponent();
        }

        public void LoadData(List<SystemDrawDown> data)
        {
            FillBindingSource(data);
            BindChartData();
        }

        private void FillBindingSource(List<SystemDrawDown> data)
        {
            srcDD2D.Clear();
            data.ForEach(v => srcDD2D.Add(new SystemDrawDownMapper(v)));
        }

        private void BindChartData()
        {
            chartDD2D.DataBind();
        }
    }
}
