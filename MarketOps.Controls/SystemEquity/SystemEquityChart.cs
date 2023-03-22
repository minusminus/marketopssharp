using MarketOps.Controls.ChartsUtils;
using MarketOps.SystemData.Types;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace MarketOps.Controls.SystemEquity
{
    public partial class SystemEquityChart : UserControl
    {
        private readonly PlotsAxisXSynchronizer _axisSynchronizer;

        public SystemEquityChart()
        {
            InitializeComponent();
            _axisSynchronizer = new PlotsAxisXSynchronizer(plotEquity, plotCapitalUsage);

            plotEquity.Plot.SetUpPlotArea();
            plotEquity.Plot.XAxis.DateTimeFormat(true);

            plotCapitalUsage.Plot.SetUpPlotArea();
            plotCapitalUsage.Plot.SetAxisLimitsY(0, 100);
            plotCapitalUsage.Plot.SetUpBottomPlotXAxis();
            plotCapitalUsage.Configuration.LockVerticalAxis = true;
            plotCapitalUsage.Plot.Title("Capital usage [%]", false, PlotConsts.AxisTextColor, 10);
            //plotCapitalUsage.Visible = false;
        }

        public void LoadData(List<SystemValue> equity, List<SystemValue> equityCapitalUsage)
        {
            plotEquity.Plot.Clear();
            plotCapitalUsage.Plot.Clear();

            AddEquity(equity);
            if (equityCapitalUsage != null)
            {
                plotCapitalUsage.Visible = true;
                AddCapitalUsage(equityCapitalUsage);
            }

            plotEquity.Refresh();
            plotCapitalUsage.Refresh();
        }

        private void AddEquity(List<SystemValue> equity)
        {
            double[] x = equity.Select(sv => sv.TS.ToOADate()).ToArray();
            double[] y = equity.Select(sv => (double)sv.Value).ToArray();

            plotEquity.Plot.AddScatterLines(x, y, PlotConsts.FirstLineColor);
        }

        private void AddCapitalUsage(List<SystemValue> equityCapitalUsage)
        {
            double[] x = equityCapitalUsage.Select(cu => cu.TS.ToOADate()).ToArray();
            double[] y = equityCapitalUsage.Select(cu => 100.0 * cu.Value).ToArray();

            plotCapitalUsage.Plot.AddScatterLines(x, y, PlotConsts.SecondLineColor);
        }
    }
}
