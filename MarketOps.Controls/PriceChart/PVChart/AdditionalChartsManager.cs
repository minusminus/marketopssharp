using MarketOps.Controls.ChartsUtils;
using MarketOps.Controls.ChartsUtils.AxisSynchronization;
using ScottPlot;
using System.Collections.Generic;

namespace MarketOps.Controls.PriceChart.PVChart
{
    /// <summary>
    /// Manages additional FormsPlot objects.
    /// </summary>
    internal class AdditionalChartsManager
    {
        private readonly PlotsAxisXSynchronizer _axisXSynchronizer;

        public readonly List<FormsPlot> Charts = new List<FormsPlot>();

        public AdditionalChartsManager(PlotsAxisXSynchronizer axisXSynchronizer)
        {
            _axisXSynchronizer = axisXSynchronizer;
        }

        public FormsPlot Add()
        {
            var result = new FormsPlot();
            Charts.Add(result);
            _axisXSynchronizer.Add(result);
            return result;
        }

        public void Remove(int index)
        {
            var chart = Charts[index];
            Charts.RemoveAt(index);
            _axisXSynchronizer.Remove(chart);
            chart.Visible = false;
            chart.Dispose();
        }

        public void RefreshAll()
        {
            foreach (var chart in Charts)
                chart.Refresh();
        }

        public void Clear()
        {
            while (Charts.Count > 0)
                Remove(0);
        }
    }
}
