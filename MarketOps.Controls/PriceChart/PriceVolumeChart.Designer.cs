namespace MarketOps.Controls.PriceChart
{
    partial class PriceVolumeChart
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod wygenerowany przez Projektanta składników

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować 
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.PVChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.PVChart)).BeginInit();
            this.SuspendLayout();
            // 
            // PVChart
            // 
            chartArea1.AxisX.IsLabelAutoFit = false;
            chartArea1.AxisX.IsStartedFromZero = false;
            chartArea1.AxisX.LabelStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            chartArea1.AxisX.LabelStyle.ForeColor = System.Drawing.Color.DarkGray;
            chartArea1.AxisX.LineColor = System.Drawing.Color.DarkGray;
            chartArea1.AxisX.MajorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea1.AxisX.MajorTickMark.LineColor = System.Drawing.Color.DarkGray;
            chartArea1.AxisY.IsLabelAutoFit = false;
            chartArea1.AxisY.IsStartedFromZero = false;
            chartArea1.AxisY.LabelStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            chartArea1.AxisY.LabelStyle.ForeColor = System.Drawing.Color.DarkGray;
            chartArea1.AxisY.LineColor = System.Drawing.Color.DarkGray;
            chartArea1.AxisY.MajorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea1.AxisY.MajorTickMark.LineColor = System.Drawing.Color.DarkGray;
            chartArea1.CursorX.IsUserEnabled = true;
            chartArea1.CursorX.IsUserSelectionEnabled = true;
            chartArea1.CursorX.LineColor = System.Drawing.Color.Gray;
            chartArea1.CursorY.IsUserEnabled = true;
            chartArea1.CursorY.LineColor = System.Drawing.Color.Gray;
            chartArea1.Name = "areaPrices";
            chartArea1.Position.Auto = false;
            chartArea1.Position.Height = 80F;
            chartArea1.Position.Width = 100F;
            chartArea2.AlignWithChartArea = "areaPrices";
            chartArea2.AxisX.IsLabelAutoFit = false;
            chartArea2.AxisX.IsStartedFromZero = false;
            chartArea2.AxisX.LabelStyle.Enabled = false;
            chartArea2.AxisX.LabelStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            chartArea2.AxisX.LabelStyle.ForeColor = System.Drawing.Color.DarkGray;
            chartArea2.AxisX.LineColor = System.Drawing.Color.DarkGray;
            chartArea2.AxisX.MajorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea2.AxisX.MajorTickMark.Enabled = false;
            chartArea2.AxisX.MajorTickMark.LineColor = System.Drawing.Color.DarkGray;
            chartArea2.AxisY.IsLabelAutoFit = false;
            chartArea2.AxisY.LabelStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            chartArea2.AxisY.LabelStyle.ForeColor = System.Drawing.Color.DarkGray;
            chartArea2.AxisY.LineColor = System.Drawing.Color.DarkGray;
            chartArea2.AxisY.MajorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea2.AxisY.MajorTickMark.LineColor = System.Drawing.Color.DarkGray;
            chartArea2.CursorX.IsUserEnabled = true;
            chartArea2.CursorX.LineColor = System.Drawing.Color.Gray;
            chartArea2.CursorY.LineColor = System.Drawing.Color.Gray;
            chartArea2.Name = "areaVolume";
            chartArea2.Position.Auto = false;
            chartArea2.Position.Height = 20F;
            chartArea2.Position.Width = 100F;
            chartArea2.Position.Y = 80F;
            this.PVChart.ChartAreas.Add(chartArea1);
            this.PVChart.ChartAreas.Add(chartArea2);
            this.PVChart.Cursor = System.Windows.Forms.Cursors.Cross;
            this.PVChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PVChart.Location = new System.Drawing.Point(0, 0);
            this.PVChart.Name = "PVChart";
            series1.BorderColor = System.Drawing.Color.Black;
            series1.ChartArea = "areaPrices";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Candlestick;
            series1.Color = System.Drawing.Color.Black;
            series1.CustomProperties = "PriceDownColor=Black, PriceUpColor=White";
            series1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            series1.IsXValueIndexed = true;
            series1.Name = "dataPricesCandles";
            series1.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Date;
            series1.YValuesPerPoint = 4;
            series1.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Single;
            series2.ChartArea = "areaPrices";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Color = System.Drawing.Color.Black;
            series2.Enabled = false;
            series2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            series2.IsXValueIndexed = true;
            series2.Name = "dataPricesLine";
            series2.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Single;
            series3.ChartArea = "areaVolume";
            series3.Color = System.Drawing.Color.RoyalBlue;
            series3.IsXValueIndexed = true;
            series3.Name = "dataVolume";
            series3.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Date;
            series3.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int64;
            this.PVChart.Series.Add(series1);
            this.PVChart.Series.Add(series2);
            this.PVChart.Series.Add(series3);
            this.PVChart.Size = new System.Drawing.Size(430, 302);
            this.PVChart.TabIndex = 0;
            this.PVChart.Text = "chart1";
            this.PVChart.AxisViewChanged += new System.EventHandler<System.Windows.Forms.DataVisualization.Charting.ViewEventArgs>(this.PVChart_AxisViewChanged);
            this.PVChart.DoubleClick += new System.EventHandler(this.PVChart_DoubleClick);
            this.PVChart.MouseEnter += new System.EventHandler(this.PVChart_MouseEnter);
            this.PVChart.MouseLeave += new System.EventHandler(this.PVChart_MouseLeave);
            this.PVChart.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PVChart_MouseMove);
            // 
            // PriceVolumeChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.PVChart);
            this.Name = "PriceVolumeChart";
            this.Size = new System.Drawing.Size(430, 302);
            ((System.ComponentModel.ISupportInitialize)(this.PVChart)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart PVChart;
    }
}
