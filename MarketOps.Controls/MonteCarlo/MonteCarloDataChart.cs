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
            chartData.Series.Clear();
            DrawDataSeries(data.Data);
            DrawAverageDataSerie(data.AverageData);
        }

        private void DrawDataSeries(float[][] data)
        {
            for (int i = 0; i < data.Length; i++)
                AddDataRow(CreateDataSeries(i), data[i]);
        }

        private void DrawAverageDataSerie(float[] row) => 
            AddDataRow(CreateAverageDataSeries(), row);

        private Series CreateDataSeries(int rowIndex) =>
            CreateSeries($"series{rowIndex}", Color.LightSteelBlue, 1);

        private Series CreateAverageDataSeries() => 
            CreateSeries("seriesAverageData", Color.LightCoral, 2);

        private void AddDataRow(Series series, float[] row)
        {
            for (int i = 0; i < row.Length; i++)
                series.Points.AddXY(i, row[i]);
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
