using MarketOps.Controls.ChartsUtils;
using MarketOps.StockData.Extensions;
using MarketOps.SystemData.Types;
using System.Collections.Generic;
using System.Drawing;

namespace MarketOps.Controls.PriceChart.PVChart
{
    /// <summary>
    /// Annotations manipulation part of PriceVolumeChart.
    /// </summary>
    public partial class PriceVolumeChart
    {
        public void AddPositionsAnnotations(List<Position> positions)
        {
            foreach (var position in positions)
                AddPositionAnnotation(position);
            chartPrices.Refresh();
        }

        private void AddPositionAnnotation(Position position)
        {
            AddAnnotation(_currentData.FindByTS(position.TSOpen), true, position.Direction, PlotConsts.PositionAnnotationOpenColor);
            AddAnnotation(_currentData.FindByTS(position.TSClose), false, position.Direction, PlotConsts.PositionAnnotationCloseColor);
        }

        private void AddAnnotation(int index, bool openAnotation, PositionDir dir, Color color)
        {
            const float margin = 5;

            chartPrices.Plot.AddMarker(
                x: index,
                y: GetY(),
                shape: GetShape(),
                size: 10,
                color: color
                );

            double GetY()
            {
                if (dir == PositionDir.Long)
                {
                    return openAnotation
                        ? _currentData.L[index] - margin
                        : _currentData.H[index] + margin;
                }
                else
                {
                    return openAnotation
                        ? _currentData.H[index] + margin
                        : _currentData.L[index] - margin;
                }
            }

            ScottPlot.MarkerShape GetShape()
            {
                return (dir == PositionDir.Long)
                    ? ScottPlot.MarkerShape.filledTriangleUp
                    : ScottPlot.MarkerShape.filledTriangleDown;
            }
        }
    }
}
