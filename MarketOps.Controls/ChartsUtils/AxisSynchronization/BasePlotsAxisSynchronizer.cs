using ScottPlot;
using System;
using System.Collections.Generic;

namespace MarketOps.Controls.ChartsUtils.AxisSynchronization
{
    /// <summary>
    /// Base mechanism for plots axis synchronization.
    /// </summary>
    internal abstract class BasePlotsAxisSynchronizer
    {
        private readonly List<FormsPlot> _formsPlots = new List<FormsPlot>();

        public BasePlotsAxisSynchronizer(params FormsPlot[] formsPlots)
        {
            for (int i = 0; i < formsPlots.Length; i++)
                Add(formsPlots[i]);
        }

        public void Add(FormsPlot formsPlot)
        {
            _formsPlots.Add(formsPlot);
            formsPlot.AxesChanged += OnAxesChanged;
        }

        public void Remove(FormsPlot formsPlot)
        {
            if (!_formsPlots.Remove(formsPlot)) return;
            formsPlot.AxesChanged -= OnAxesChanged;
        }

        protected abstract void SetLimits(FormsPlot formsPlot, AxisLimits newAxisLimits);

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
