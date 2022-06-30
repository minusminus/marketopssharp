
namespace MarketOps.Controls.MonteCarlo
{
    partial class MonteCarloDataChart
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chartData = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.chartData)).BeginInit();
            this.SuspendLayout();
            // 
            // chartData
            // 
            chartArea2.AxisX.IsLabelAutoFit = false;
            chartArea2.AxisX.LabelStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            chartArea2.AxisX.LabelStyle.ForeColor = System.Drawing.Color.DarkGray;
            chartArea2.AxisX.LabelStyle.Format = "F2";
            chartArea2.AxisX.LineColor = System.Drawing.Color.DarkGray;
            chartArea2.AxisX.MajorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea2.AxisX.MajorTickMark.LineColor = System.Drawing.Color.DarkGray;
            chartArea2.AxisY.IsLabelAutoFit = false;
            chartArea2.AxisY.LabelStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            chartArea2.AxisY.LabelStyle.ForeColor = System.Drawing.Color.DarkGray;
            chartArea2.AxisY.LineColor = System.Drawing.Color.DarkGray;
            chartArea2.AxisY.MajorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea2.AxisY.MajorTickMark.LineColor = System.Drawing.Color.DarkGray;
            chartArea2.Name = "areaData";
            this.chartData.ChartAreas.Add(chartArea2);
            this.chartData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartData.Location = new System.Drawing.Point(0, 0);
            this.chartData.Name = "chartData";
            series2.ChartArea = "areaData";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.IsXValueIndexed = true;
            series2.Name = "Series1";
            series2.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;
            series2.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Single;
            this.chartData.Series.Add(series2);
            this.chartData.Size = new System.Drawing.Size(420, 236);
            this.chartData.TabIndex = 0;
            this.chartData.Text = "chart1";
            // 
            // MonteCarloDataChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chartData);
            this.Name = "MonteCarloDataChart";
            this.Size = new System.Drawing.Size(420, 236);
            ((System.ComponentModel.ISupportInitialize)(this.chartData)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chartData;
    }
}
