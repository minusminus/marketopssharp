
namespace MarketOps.Controls.ColumnChart
{
    partial class ColumnChart
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chartColumns = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.srcColumns = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.chartColumns)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.srcColumns)).BeginInit();
            this.SuspendLayout();
            // 
            // chartColumns
            // 
            chartArea1.AxisX.IsLabelAutoFit = false;
            chartArea1.AxisX.LabelStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            chartArea1.AxisX.LabelStyle.ForeColor = System.Drawing.Color.DarkGray;
            chartArea1.AxisX.LabelStyle.Format = "F2";
            chartArea1.AxisX.LineColor = System.Drawing.Color.DarkGray;
            chartArea1.AxisX.MajorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea1.AxisX.MajorTickMark.LineColor = System.Drawing.Color.DarkGray;
            chartArea1.AxisY.IsLabelAutoFit = false;
            chartArea1.AxisY.LabelStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            chartArea1.AxisY.LabelStyle.ForeColor = System.Drawing.Color.DarkGray;
            chartArea1.AxisY.LineColor = System.Drawing.Color.DarkGray;
            chartArea1.AxisY.MajorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea1.AxisY.MajorTickMark.LineColor = System.Drawing.Color.DarkGray;
            chartArea1.Name = "areaColumns";
            this.chartColumns.ChartAreas.Add(chartArea1);
            this.chartColumns.DataSource = this.srcColumns;
            this.chartColumns.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartColumns.Location = new System.Drawing.Point(0, 0);
            this.chartColumns.Name = "chartColumns";
            series1.ChartArea = "areaColumns";
            series1.Name = "seriesColumns";
            series1.XValueMember = "X";
            series1.YValueMembers = "Y";
            this.chartColumns.Series.Add(series1);
            this.chartColumns.Size = new System.Drawing.Size(442, 287);
            this.chartColumns.TabIndex = 1;
            this.chartColumns.Text = "chart1";
            // 
            // ColumnChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chartColumns);
            this.Name = "ColumnChart";
            this.Size = new System.Drawing.Size(442, 287);
            ((System.ComponentModel.ISupportInitialize)(this.chartColumns)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.srcColumns)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chartColumns;
        private System.Windows.Forms.BindingSource srcColumns;
    }
}
