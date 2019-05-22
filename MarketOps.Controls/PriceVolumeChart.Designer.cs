namespace MarketOps.Controls
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.PVChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tooltipAxisX = new MarketOps.Controls.ToolTipExtended();
            this.tooltipAxisY = new MarketOps.Controls.ToolTipExtended();
            ((System.ComponentModel.ISupportInitialize)(this.PVChart)).BeginInit();
            this.SuspendLayout();
            // 
            // PVChart
            // 
            chartArea3.AxisX.IsLabelAutoFit = false;
            chartArea3.AxisX.IsStartedFromZero = false;
            chartArea3.AxisX.LabelStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            chartArea3.AxisX.LabelStyle.ForeColor = System.Drawing.Color.DarkGray;
            chartArea3.AxisX.LineColor = System.Drawing.Color.DarkGray;
            chartArea3.AxisX.MajorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea3.AxisX.MajorTickMark.LineColor = System.Drawing.Color.DarkGray;
            chartArea3.AxisY.IsLabelAutoFit = false;
            chartArea3.AxisY.IsStartedFromZero = false;
            chartArea3.AxisY.LabelStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            chartArea3.AxisY.LabelStyle.ForeColor = System.Drawing.Color.DarkGray;
            chartArea3.AxisY.LineColor = System.Drawing.Color.DarkGray;
            chartArea3.AxisY.MajorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea3.AxisY.MajorTickMark.LineColor = System.Drawing.Color.DarkGray;
            chartArea3.CursorX.IsUserEnabled = true;
            chartArea3.CursorX.IsUserSelectionEnabled = true;
            chartArea3.CursorX.LineColor = System.Drawing.Color.Gray;
            chartArea3.CursorY.IsUserEnabled = true;
            chartArea3.CursorY.LineColor = System.Drawing.Color.Gray;
            chartArea3.Name = "areaPrices";
            chartArea3.Position.Auto = false;
            chartArea3.Position.Height = 80F;
            chartArea3.Position.Width = 100F;
            chartArea4.AlignWithChartArea = "areaPrices";
            chartArea4.AxisX.IsLabelAutoFit = false;
            chartArea4.AxisX.IsStartedFromZero = false;
            chartArea4.AxisX.LabelStyle.Enabled = false;
            chartArea4.AxisX.LabelStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            chartArea4.AxisX.LabelStyle.ForeColor = System.Drawing.Color.DarkGray;
            chartArea4.AxisX.LineColor = System.Drawing.Color.DarkGray;
            chartArea4.AxisX.MajorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea4.AxisX.MajorTickMark.Enabled = false;
            chartArea4.AxisX.MajorTickMark.LineColor = System.Drawing.Color.DarkGray;
            chartArea4.AxisY.IsLabelAutoFit = false;
            chartArea4.AxisY.LabelStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            chartArea4.AxisY.LabelStyle.ForeColor = System.Drawing.Color.DarkGray;
            chartArea4.AxisY.LineColor = System.Drawing.Color.DarkGray;
            chartArea4.AxisY.MajorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea4.AxisY.MajorTickMark.LineColor = System.Drawing.Color.DarkGray;
            chartArea4.CursorX.IsUserEnabled = true;
            chartArea4.CursorX.LineColor = System.Drawing.Color.Gray;
            chartArea4.CursorY.LineColor = System.Drawing.Color.Gray;
            chartArea4.Name = "areaVolume";
            chartArea4.Position.Auto = false;
            chartArea4.Position.Height = 20F;
            chartArea4.Position.Width = 100F;
            chartArea4.Position.Y = 80F;
            this.PVChart.ChartAreas.Add(chartArea3);
            this.PVChart.ChartAreas.Add(chartArea4);
            this.PVChart.Cursor = System.Windows.Forms.Cursors.Cross;
            this.PVChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PVChart.Location = new System.Drawing.Point(0, 0);
            this.PVChart.Name = "PVChart";
            series4.BorderColor = System.Drawing.Color.Black;
            series4.ChartArea = "areaPrices";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Candlestick;
            series4.Color = System.Drawing.Color.Black;
            series4.CustomProperties = "PriceDownColor=Black, PriceUpColor=White";
            series4.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            series4.IsXValueIndexed = true;
            series4.Name = "dataPricesCandles";
            series4.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Date;
            series4.YValuesPerPoint = 4;
            series4.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Single;
            series5.ChartArea = "areaPrices";
            series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series5.Color = System.Drawing.Color.Black;
            series5.Enabled = false;
            series5.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            series5.IsXValueIndexed = true;
            series5.Name = "dataPricesLine";
            series5.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Single;
            series6.ChartArea = "areaVolume";
            series6.Color = System.Drawing.Color.RoyalBlue;
            series6.IsXValueIndexed = true;
            series6.Name = "dataVolume";
            series6.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Date;
            series6.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int64;
            this.PVChart.Series.Add(series4);
            this.PVChart.Series.Add(series5);
            this.PVChart.Series.Add(series6);
            this.PVChart.Size = new System.Drawing.Size(430, 302);
            this.PVChart.TabIndex = 0;
            this.PVChart.Text = "chart1";
            this.PVChart.AxisViewChanged += new System.EventHandler<System.Windows.Forms.DataVisualization.Charting.ViewEventArgs>(this.PVChart_AxisViewChanged);
            this.PVChart.MouseEnter += new System.EventHandler(this.PVChart_MouseEnter);
            this.PVChart.MouseLeave += new System.EventHandler(this.PVChart_MouseLeave);
            this.PVChart.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PVChart_MouseMove);
            // 
            // tooltipAxisX
            // 
            this.tooltipAxisX.OwnerDraw = true;
            this.tooltipAxisX.TooltipBackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.tooltipAxisX.TooltipFont = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tooltipAxisX.TooltipFontColor = System.Drawing.Color.DimGray;
            // 
            // tooltipAxisY
            // 
            this.tooltipAxisY.OwnerDraw = true;
            this.tooltipAxisY.TooltipBackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.tooltipAxisY.TooltipFont = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tooltipAxisY.TooltipFontColor = System.Drawing.Color.DimGray;
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
        private ToolTipExtended tooltipAxisX;
        private ToolTipExtended tooltipAxisY;
    }
}
