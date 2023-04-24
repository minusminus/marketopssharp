using MarketOps.Controls.ChartsUtils;
using MarketOps.StockData.Extensions;
using MarketOps.SystemData.Types;
using ScottPlot.Plottable;
using System.Collections.Generic;
using System.Linq;

namespace MarketOps.Controls.PriceChart.PVChart
{
    /// <summary>
    /// Trailing stops manipulation part of PriceVolumeChart.
    /// </summary>
    public partial class PriceVolumeChart
    {
        private double[] _trailingStopsData;
        public double[] TrailingStopsData => _trailingStopsData;

        public void AddPositionsTrailingStops(List<Position> positions)
        {
            _trailingStopsData = CreateEmptyTrailingStopSerie();
            foreach (var position in positions)
                AddPositionTrailingStopData(_trailingStopsData, position);
            AddPosiotnsTrailingStopsLine(_trailingStopsData);
        }

        private void ClearTrailingStopsData()
        {
            _trailingStopsData = null;
        }

        private double[] CreateEmptyTrailingStopSerie() =>
            Enumerable.Repeat(double.NaN, _currentData.Length).ToArray();

        private void AddPositionTrailingStopData(in double[] trailingStopsData, in Position position)
        {
            if (position.TrailingStop.Count == 0) return;

            int startIndex = _currentData.FindByTS(position.TrailingStop[0].TS);
            for (int i = 0; i < position.TrailingStop.Count; i++)
                trailingStopsData[startIndex + i] = position.TrailingStop[i].Value;
            if (startIndex + position.TrailingStop.Count < _currentData.Length)
                trailingStopsData[startIndex + position.TrailingStop.Count] = position.TrailingStop[position.TrailingStop.Count - 1].Value;
        }

        private void AddPosiotnsTrailingStopsLine(in double[] trailingStopsData)
        {
            var trailingStopsLine = chartPrices.Plot.AddScatterLines(_statsXs, trailingStopsData, PlotConsts.PositionsTrailingStopsColor);
            trailingStopsLine.OnNaN = ScatterPlot.NanBehavior.Gap;
            trailingStopsLine.StepDisplay = true;
            //trailingStopsLine.StepDisplayRight = true;
        }
    }
}
