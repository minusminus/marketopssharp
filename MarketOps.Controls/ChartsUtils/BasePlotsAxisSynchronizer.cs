using ScottPlot;
using System;

namespace MarketOps.Controls.ChartsUtils
{
    /// <summary>
    /// Base mechanism for plots axis synchronization.
    /// </summary>
    internal abstract class BasePlotsAxisSynchronizer
    {
        private readonly FormsPlot[] _formsPlots;

        public BasePlotsAxisSynchronizer(params FormsPlot[] formsPlots)
        {
            _formsPlots = formsPlots;
            SetPlotsEventHandlers();
        }

        protected abstract void SetLimits(FormsPlot formsPlot, AxisLimits newAxisLimits);

        private void SetPlotsEventHandlers()
        {
            foreach (var formsPlot in _formsPlots)
                formsPlot.AxesChanged += OnAxesChanged;
        }

        private void OnAxesChanged(object sender, EventArgs e)
        {
            FormsPlot changedPlot = (FormsPlot)sender;
            var newAxisLimits = changedPlot.Plot.GetAxisLimits();

            foreach (var formsPlot in _formsPlots)
            {
                if (formsPlot == changedPlot) continue;
                SetLimitsOnPlot(formsPlot);
            }

            void SetLimitsOnPlot(FormsPlot formsPlot)
            {
                formsPlot.Configuration.AxesChangedEventEnabled = false;
                try
                {
                    SetLimits(formsPlot, newAxisLimits);
                    formsPlot.Render();
                }
                finally
                {
                    formsPlot.Configuration.AxesChangedEventEnabled = true;
                }
            }
        }
    }
}
