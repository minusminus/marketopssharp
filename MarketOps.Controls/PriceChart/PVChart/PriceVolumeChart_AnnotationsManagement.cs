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
        public void AddPositionsAnnotations(IReadOnlyList<Position> positions)
        {
            foreach (var position in positions)
                AddPositionAnnotation(position);
        }

        private void AddPositionAnnotation(Position position)
        {
            AddAnnotation(_currentData.FindByTS(position.TSOpen), position.Open, true, position.Direction);
            AddAnnotation(_currentData.FindByTS(position.TSClose), position.Close, false, position.Direction);
        }

        private void AddAnnotation(int index, float price, bool openAnotation, PositionDir dir)
        {
            chartPrices.Plot.AddMarker(
                x: index,
                y: GetAnnotationY(index, openAnotation, dir),
                shape: GetAnnotationShape(openAnotation, dir),
                size: PlotConsts.PositionAnnotationSize,
                color: GetAnnotationColor(openAnotation));

            chartPrices.Plot.AddLine(
                (double)index - PlotConsts.PositionAnnotationPriceLevelLineMargin, 
                price, 
                (double)index + PlotConsts.PositionAnnotationPriceLevelLineMargin, 
                price, 
                GetAnnotationColor(openAnotation), 
                PlotConsts.PositionAnnotationPriceLevelLineWidth);
        }

        private double GetAnnotationY(int index, bool openAnotation, PositionDir dir)
        {
            if (dir == PositionDir.Long)
                return openAnotation
                    ? _currentData.L[index] - PlotConsts.PositionAnnotationMargin
                    : _currentData.H[index] + PlotConsts.PositionAnnotationMargin;
            else
                return openAnotation
                    ? _currentData.H[index] + PlotConsts.PositionAnnotationMargin
                    : _currentData.L[index] - PlotConsts.PositionAnnotationMargin;
        }

        private static ScottPlot.MarkerShape GetAnnotationShape(bool openAnotation, PositionDir dir)
        {
            if (dir == PositionDir.Long)
                return openAnotation
                    ? ScottPlot.MarkerShape.filledTriangleUp
                    : ScottPlot.MarkerShape.filledTriangleDown;
            else
                return openAnotation
                    ? ScottPlot.MarkerShape.filledTriangleDown
                    : ScottPlot.MarkerShape.filledTriangleUp;
        }

        private static Color GetAnnotationColor(bool openAnotation) =>
            openAnotation ? PlotConsts.PositionAnnotationOpenColor : PlotConsts.PositionAnnotationCloseColor;
    }
}
