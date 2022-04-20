using MarketOps.SystemAnalysis.MonteCarlo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace MarketOps.Controls.MonteCarlo
{
    public partial class MonteCarloDataChart : UserControl
    {
        public MonteCarloDataChart()
        {
            InitializeComponent();
        }

        public void LoadData(MonteCarloResult data)
        {
            int rowsCount = data.Data.GetLength(0);
            int rowLength = data.Data.GetLength(1);
            chartData.Series.Clear();
            DrawDataSeries(data.Data, rowsCount, rowLength);
            DrawAverageDataSerie(data.AverageData, rowLength);
        }

        private void DrawDataSeries(float[,] data, int rowsCount, int rowLength)
        {
            for (int i = 0; i < rowsCount; i++)
                AddDataRow(CreateDataSeries(i), data, i, rowLength);
        }

        private void DrawAverageDataSerie(float[] data, int rowLength) => 
            AddAverageDataRow(CreateAverageDataSeries(), data, rowLength);

        private Series CreateDataSeries(int rowIndex) =>
            CreateSeries($"series{rowIndex}", Color.LightSteelBlue, 1);

        private Series CreateAverageDataSeries() => 
            CreateSeries("seriesAverageData", Color.LightCoral, 2);

        private void AddDataRow(Series series, float[,] data, int rowIndex, int rowLength)
        {
            for (int i = 0; i < rowLength; i++)
                series.Points.AddXY(i, data[rowIndex, i]);
        }

        private void AddAverageDataRow(Series series, float[] data, int rowLength)
        {
            for (int i = 0; i < rowLength; i++)
                series.Points.AddXY(i, data[i]);
        }

        private Series CreateSeries(string name, Color color, int lineWidth)
        {
            Series series = chartData.Series.Add(name);
            series.ChartArea = "areaData";
            series.ChartType = SeriesChartType.Line;
            series.Color = color;
            series.BorderWidth = lineWidth;
            series.IsXValueIndexed = true;
            series.XValueType = ChartValueType.Int32;
            series.YValueType = ChartValueType.Single;
            return series;
        }
    }
}
