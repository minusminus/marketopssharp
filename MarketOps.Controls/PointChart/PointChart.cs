using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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
            //chartPoints.DataSource = data;
            //chartPoints.Series["seriesPoints"].ToolTip = seriesTooltip;

            //plotPoints.Plot.Title(string.Empty);
            plotPoints.Plot.ManualDataArea(new ScottPlot.PixelPadding(20, 5, 20, 5));
            //plotPoints.Plot.ManualDataArea(new ScottPlot.PixelPadding(0, 5, 0, 5));
            //plotPoints.Plot.Style(axisLabel: Color.LightGray);
            plotPoints.Plot.XAxis.Color(Color.LightGray);
            plotPoints.Plot.XAxis.TickLabelStyle(fontSize: 8, color: Color.DarkGray);
            plotPoints.Plot.YAxis.Color(Color.LightGray);
            plotPoints.Plot.YAxis.TickLabelStyle(fontSize: 8, color: Color.DarkGray);
            plotPoints.Plot.XAxis2.Hide();
            plotPoints.Plot.YAxis2.Hide();
            //plotPoints.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            //plotPoints.Plot.XAxis.SetSizeLimit(max: 5);

            double[] x = data.Select(p => (double)p.X).ToArray();
            double[] y = data.Select(p => (double)p.Y).ToArray();
            plotPoints.Plot.AddScatter(x, y, lineWidth: 0);
            plotPoints.Refresh();
        }
    }
}
