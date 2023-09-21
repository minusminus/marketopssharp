using MarketOps.Controls.ChartsUtils.AxisSynchronization;
using MarketOps.Controls.ChartsUtils;
using MarketOps.SystemData.Types;
using ScottPlot.Plottable;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System;

namespace MarketOps.Controls.SystemEquity
{
    public partial class SystemEquityChart : UserControl
    {
        private ScatterPlot _scatterEquity;
        private Tooltip _tooltip;
        private PlotTooltipMover _tooltipMover;
        private readonly PlotsAxisXSynchronizer _axisSynchronizer;

        public SystemEquityChart()
        {
            InitializeComponent();
            _axisSynchronizer = new PlotsAxisXSynchronizer(plotEquity, plotCapitalUsage);

            const float yAxisSizeLimit = 50;

            plotEquity.Plot.SetUpPlotArea();
            plotEquity.Plot.XAxis.DateTimeFormat(true);
            plotEquity.Plot.YAxis.SetSizeLimit(yAxisSizeLimit, yAxisSizeLimit);

            plotCapitalUsage.Plot.SetUpPlotArea();
            plotCapitalUsage.Plot.SetUpBottomPlotXAxis();
            plotCapitalUsage.Plot.SetAxisLimitsY(0, 100);
            plotCapitalUsage.Configuration.LockVerticalAxis = true;
            plotCapitalUsage.Plot.YAxis.SetSizeLimit(yAxisSizeLimit, yAxisSizeLimit);
            plotCapitalUsage.Plot.Title("Capital usage [%]", false, PlotConsts.AxisTextColor, 10);
        }

        public void LoadData(List<SystemValue> equity, List<SystemValue> equityCapitalUsage)
        {
            _tooltipMover?.UnlinkPlot();
            _tooltipMover = null;
            plotEquity.Plot.Clear();
            plotCapitalUsage.Plot.Clear();

            AddEquity(equity);
            plotCapitalUsage.Visible = (equityCapitalUsage != null);
            if (equityCapitalUsage != null)
                AddCapitalUsage(equityCapitalUsage);
            _tooltip = plotEquity.Plot.CreateTooltip();
            _tooltipMover = new PlotTooltipMover(plotEquity, _tooltip, GetNearestPoint, GetTooltipLabel);

            plotEquity.Refresh();
            plotCapitalUsage.Refresh();
        }

        private (double x, double y, int index) GetNearestPoint((double x, double y) mouseCoords, double xyRatio) =>
            _scatterEquity.GetPointNearest(mouseCoords.x, mouseCoords.y, xyRatio);

        private string GetTooltipLabel((double x, double y, int index) nearestPoint) =>
            $"{DateTime.FromOADate(_scatterEquity.Xs[nearestPoint.index])}{Environment.NewLine}value: {_scatterEquity.Ys[nearestPoint.index]}";

        private void AddEquity(List<SystemValue> equity)
        {
            double[] x = equity.Select(sv => sv.TS.ToOADate()).ToArray();
            double[] y = equity.Select(sv => (double)sv.Value).ToArray();

            _scatterEquity = plotEquity.Plot.AddScatterLines(x, y, PlotConsts.PrimaryLineColor);
        }

        private void AddCapitalUsage(List<SystemValue> equityCapitalUsage)
        {
            double[] x = equityCapitalUsage.Select(cu => cu.TS.ToOADate()).ToArray();
            double[] y = equityCapitalUsage.Select(cu => 100.0 * cu.Value).ToArray();

            plotCapitalUsage.Plot.AddScatterLines(x, y, PlotConsts.SecondaryLineColor);
        }
    }
}
