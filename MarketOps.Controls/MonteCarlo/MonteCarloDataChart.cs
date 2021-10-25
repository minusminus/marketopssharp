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
            CreateSeriesWithPoints(data);
        }

        private void CreateSeriesWithPoints(MonteCarloResult data)
        {
            int rowsCount = data.data.GetLength(0);
            for (int i = 0; i < rowsCount; i++)
            {
                CreateSeries(i);
                AddSingleRow(data, i);
            }
        }

        private void CreateSeries(int rowIndex)
        {
            Series series = chartData.Series.Add($"series{rowIndex}");
            series.ChartArea = "areaData";
            series.ChartType = SeriesChartType.Line;
            series.Color = Color.LightSteelBlue;
            series.IsXValueIndexed = true;
            series.XValueType = ChartValueType.Int32;
            series.YValueType = ChartValueType.Single;
        }

        private void AddSingleRow(MonteCarloResult data, int rowIndex)
        {
            int rowLength = data.data.GetLength(1);
            for (int i = 0; i < rowLength; i++)
                chartData.Series[rowIndex].Points.AddXY(i, data.data[rowIndex, i]);
        }
    }
}
