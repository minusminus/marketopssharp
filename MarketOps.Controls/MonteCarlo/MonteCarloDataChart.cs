using MarketOps.Controls.ChartsUtils;
using MarketOps.SystemAnalysis.MonteCarlo;
using System.Drawing;
using System.Windows.Forms;

namespace MarketOps.Controls.MonteCarlo
{
    public partial class MonteCarloDataChart : UserControl
    {
        public MonteCarloDataChart()
        {
            InitializeComponent();
            plotData.Plot.SetUpPlotArea();
        }

        public void LoadData(MonteCarloResult data)
        {
            plotData.Plot.Clear();
            DrawSeries(data.Data);
            DrawAverageSerie(data.AverageData);

            plotData.Refresh();
        }

        private void DrawSeries(double[][] data)
        {
            for (int i = 0; i < data.Length; i++)
                AddSerie(data[i], 1, Color.LightSteelBlue);
        }

        private void DrawAverageSerie(double[] row) =>
            AddSerie(row, 2, Color.LightCoral);

        private void AddSerie(double[] data, double lineWidth, Color color)
        {
            var signal = plotData.Plot.AddSignal(data);
            signal.Color = color;
            signal.LineWidth = lineWidth;
        }
    }
}
