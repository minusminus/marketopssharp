using System.Windows.Forms.DataVisualization.Charting;
using MarketOps.Controls.PriceChart.PVChart;
using System.Collections.Generic;
using MarketOps.SystemData.Types;
using System;
using MarketOps.Controls.PriceChart;

namespace MarketOps.Controls.Extensions
{
    /// <summary>
    /// extensions for position annotations on PriceVolumeChart
    /// </summary>
    public static class PriceVolumeChartPositionAnnotationsExtensions
    {
        public static void AddPositionsAnnotations(this PriceVolumeChart chart, List<Position> positions)
        {
            //chart.PVChartControl.Annotations.Clear();
            //foreach (var position in positions)
            //    AddPositionAnnotation(chart, position);
        }

        private static void AddPositionAnnotation(PriceVolumeChart chart, Position position)
        {
            //AddAnnotation(chart, PositionOpenCloseImages.IndexOpen, position.Direction, position.TSOpen);
            //AddAnnotation(chart, PositionOpenCloseImages.IndexClose, position.Direction, position.TSClose);
        }

        private static void AddAnnotation(PriceVolumeChart chart, int imageIndex, PositionDir dir, DateTime ts)
        {
            //DataPoint dataPoint = chart.PricesCandles.Points.FindByValue(ts.ToOADate(), "X");
            //if (dataPoint == null) return;

            //ImageAnnotation annotation = new ImageAnnotation()
            //{
            //    AnchorDataPoint = dataPoint,
            //    AnchorY = dataPoint.YValues[PositionOpenCloseImages.AnnotationAnchorYValueIndex[dir][imageIndex]],
            //    AnchorAlignment = PositionOpenCloseImages.AnnotationContentAlignment[dir][imageIndex],
            //    Image = PositionOpenCloseImages.ImageName[dir][imageIndex],
            //    AnchorOffsetY = 2
            //};

            //chart.PVChartControl.Annotations.Add(annotation);
        }
    }
}
