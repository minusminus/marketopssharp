using MarketOps.Controls.Extensions;
using MarketOps.StockData.Types;
using ScottPlot;

namespace MarketOps.Controls.PriceChart.PVChart
{
    /// <summary>
    /// Stats manipulation part of PriceVolumeChart.
    /// </summary>
    public partial class PriceVolumeChart
    {
        public void AddStockStat(StockStat stat) =>
            _stockStatsManager.Add(stat);

        public void UpdateStockStat(StockStat stat) =>
            _stockStatsManager.Update(stat);

        public void RemoveStockStat(StockStat stat) =>
            _stockStatsManager.Remove(stat);

        public (int index, FormsPlot chart) FindChartForStat(StockStat stat)
        {
            if (stat.IsPricesStat())
                return (0, chartPrices);

            int index = _stockStatsManager.AdditionalStats.IndexOf(stat);
            return (index, _additionalChartsManager.Charts[index]);
        }

        private void OnPriceStatAdded(StockStat stat)
        {
            var statCharts = _priceStatsManager.Add(stat);
            chartPrices.Plot.DrawStat(stat, _statsXs, statCharts.ChartSeries);
            chartPrices.Refresh();
        }

        private void OnAdditionalStatAdded(StockStat stat)
        {
            FormsPlot chart = _additionalChartsManager.Add();
            chart.SetUpAdditionalFormsPlot();
            chart.DisplayOnControlsBottom(pnlCharts, chartVolume.Height);
            chart.Plot.DrawStat(stat, _statsXs);
            _crosshairManager.Add(chart, false);
            chart.Refresh();
        }

        private void OnPriceStatUpdated(StockStat stat, int index)
        {
            var statCharts = _priceStatsManager.Charts[index];
            chartPrices.Plot.RemoveStats(statCharts.ChartSeries);
            chartPrices.Plot.DrawStat(stat, _statsXs, statCharts.ChartSeries);
            chartPrices.Refresh();
        }

        private void OnAdditionalStatUpdated(StockStat stat, int index)
        {
            FormsPlot chart = _additionalChartsManager.Charts[index];
            chart.Plot.Clear();
            chart.Plot.DrawStat(stat, _statsXs);
            chart.Refresh();
        }

        private void OnPriceStatRemoved(StockStat stat, int index)
        {
            chartPrices.Plot.RemoveStats(_priceStatsManager.Charts[index].ChartSeries);
            _priceStatsManager.Remove(index);
            chartPrices.Refresh();
        }

        private void OnAdditionalStatRemoved(StockStat stat, int index)
        {
            _crosshairManager.Remove(_additionalChartsManager.Charts[index]);
            _additionalChartsManager.Remove(index);
        }
    }
}
