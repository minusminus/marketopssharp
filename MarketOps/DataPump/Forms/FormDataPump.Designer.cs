namespace MarketOps.DataPump.Forms
{
    partial class FormDataPump
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tcDataPumpParams = new System.Windows.Forms.TabControl();
            this.tabDaily = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbDPDailyTypeForex = new System.Windows.Forms.CheckBox();
            this.cbDPDailyTypeNBPCurrency = new System.Windows.Forms.CheckBox();
            this.cbDPDailyTypeInvestmentFund = new System.Windows.Forms.CheckBox();
            this.cbDPDailyTypeFuture = new System.Windows.Forms.CheckBox();
            this.cbDPDailyTypeIndex = new System.Windows.Forms.CheckBox();
            this.cbDPDailyTypeStock = new System.Windows.Forms.CheckBox();
            this.tabIntra = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnImport = new System.Windows.Forms.Button();
            this.pnlImportProgress = new System.Windows.Forms.Panel();
            this.lblImportProgressType = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblImportProgressProg = new System.Windows.Forms.Label();
            this.lblImportProgressStock = new System.Windows.Forms.Label();
            this.lblImportProgressCaption2 = new System.Windows.Forms.Label();
            this.lblImportProgressCaption1 = new System.Windows.Forms.Label();
            this.tabLog = new System.Windows.Forms.TabPage();
            this.edtLog = new System.Windows.Forms.TextBox();
            this.tcDataPumpParams.SuspendLayout();
            this.tabDaily.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabIntra.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.pnlImportProgress.SuspendLayout();
            this.tabLog.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcDataPumpParams
            // 
            this.tcDataPumpParams.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tcDataPumpParams.Controls.Add(this.tabDaily);
            this.tcDataPumpParams.Controls.Add(this.tabIntra);
            this.tcDataPumpParams.Controls.Add(this.tabLog);
            this.tcDataPumpParams.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcDataPumpParams.Location = new System.Drawing.Point(5, 18);
            this.tcDataPumpParams.Name = "tcDataPumpParams";
            this.tcDataPumpParams.SelectedIndex = 0;
            this.tcDataPumpParams.Size = new System.Drawing.Size(260, 193);
            this.tcDataPumpParams.TabIndex = 0;
            this.tcDataPumpParams.TabStop = false;
            // 
            // tabDaily
            // 
            this.tabDaily.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tabDaily.Controls.Add(this.groupBox2);
            this.tabDaily.Location = new System.Drawing.Point(4, 25);
            this.tabDaily.Name = "tabDaily";
            this.tabDaily.Padding = new System.Windows.Forms.Padding(3);
            this.tabDaily.Size = new System.Drawing.Size(252, 164);
            this.tabDaily.TabIndex = 0;
            this.tabDaily.Text = "Daily";
            this.tabDaily.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbDPDailyTypeForex);
            this.groupBox2.Controls.Add(this.cbDPDailyTypeNBPCurrency);
            this.groupBox2.Controls.Add(this.cbDPDailyTypeInvestmentFund);
            this.groupBox2.Controls.Add(this.cbDPDailyTypeFuture);
            this.groupBox2.Controls.Add(this.cbDPDailyTypeIndex);
            this.groupBox2.Controls.Add(this.cbDPDailyTypeStock);
            this.groupBox2.Location = new System.Drawing.Point(6, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(130, 148);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Data type";
            // 
            // cbDPDailyTypeForex
            // 
            this.cbDPDailyTypeForex.AutoSize = true;
            this.cbDPDailyTypeForex.Location = new System.Drawing.Point(7, 120);
            this.cbDPDailyTypeForex.Name = "cbDPDailyTypeForex";
            this.cbDPDailyTypeForex.Size = new System.Drawing.Size(52, 17);
            this.cbDPDailyTypeForex.TabIndex = 5;
            this.cbDPDailyTypeForex.Text = "Forex";
            this.cbDPDailyTypeForex.UseVisualStyleBackColor = true;
            // 
            // cbDPDailyTypeNBPCurrency
            // 
            this.cbDPDailyTypeNBPCurrency.AutoSize = true;
            this.cbDPDailyTypeNBPCurrency.Location = new System.Drawing.Point(7, 100);
            this.cbDPDailyTypeNBPCurrency.Name = "cbDPDailyTypeNBPCurrency";
            this.cbDPDailyTypeNBPCurrency.Size = new System.Drawing.Size(100, 17);
            this.cbDPDailyTypeNBPCurrency.TabIndex = 4;
            this.cbDPDailyTypeNBPCurrency.Text = "NBP currencies";
            this.cbDPDailyTypeNBPCurrency.UseVisualStyleBackColor = true;
            // 
            // cbDPDailyTypeInvestmentFund
            // 
            this.cbDPDailyTypeInvestmentFund.AutoSize = true;
            this.cbDPDailyTypeInvestmentFund.Location = new System.Drawing.Point(7, 80);
            this.cbDPDailyTypeInvestmentFund.Name = "cbDPDailyTypeInvestmentFund";
            this.cbDPDailyTypeInvestmentFund.Size = new System.Drawing.Size(107, 17);
            this.cbDPDailyTypeInvestmentFund.TabIndex = 3;
            this.cbDPDailyTypeInvestmentFund.Text = "Investment funds";
            this.cbDPDailyTypeInvestmentFund.UseVisualStyleBackColor = true;
            // 
            // cbDPDailyTypeFuture
            // 
            this.cbDPDailyTypeFuture.AutoSize = true;
            this.cbDPDailyTypeFuture.Location = new System.Drawing.Point(7, 60);
            this.cbDPDailyTypeFuture.Name = "cbDPDailyTypeFuture";
            this.cbDPDailyTypeFuture.Size = new System.Drawing.Size(61, 17);
            this.cbDPDailyTypeFuture.TabIndex = 2;
            this.cbDPDailyTypeFuture.Text = "Futures";
            this.cbDPDailyTypeFuture.UseVisualStyleBackColor = true;
            // 
            // cbDPDailyTypeIndex
            // 
            this.cbDPDailyTypeIndex.AutoSize = true;
            this.cbDPDailyTypeIndex.Location = new System.Drawing.Point(7, 40);
            this.cbDPDailyTypeIndex.Name = "cbDPDailyTypeIndex";
            this.cbDPDailyTypeIndex.Size = new System.Drawing.Size(63, 17);
            this.cbDPDailyTypeIndex.TabIndex = 1;
            this.cbDPDailyTypeIndex.Text = "Indexes";
            this.cbDPDailyTypeIndex.UseVisualStyleBackColor = true;
            // 
            // cbDPDailyTypeStock
            // 
            this.cbDPDailyTypeStock.AutoSize = true;
            this.cbDPDailyTypeStock.Location = new System.Drawing.Point(7, 20);
            this.cbDPDailyTypeStock.Name = "cbDPDailyTypeStock";
            this.cbDPDailyTypeStock.Size = new System.Drawing.Size(59, 17);
            this.cbDPDailyTypeStock.TabIndex = 0;
            this.cbDPDailyTypeStock.Text = "Stocks";
            this.cbDPDailyTypeStock.UseVisualStyleBackColor = true;
            // 
            // tabIntra
            // 
            this.tabIntra.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tabIntra.Controls.Add(this.label1);
            this.tabIntra.Location = new System.Drawing.Point(4, 25);
            this.tabIntra.Name = "tabIntra";
            this.tabIntra.Padding = new System.Windows.Forms.Padding(3);
            this.tabIntra.Size = new System.Drawing.Size(252, 164);
            this.tabIntra.TabIndex = 1;
            this.tabIntra.Text = "Intra";
            this.tabIntra.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(6, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(238, 22);
            this.label1.TabIndex = 0;
            this.label1.Text = "Intraday data pump is not supported";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.tcDataPumpParams);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(5);
            this.groupBox1.Size = new System.Drawing.Size(270, 216);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Options";
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(21, 243);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(252, 23);
            this.btnImport.TabIndex = 2;
            this.btnImport.Text = "Download and import";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // pnlImportProgress
            // 
            this.pnlImportProgress.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlImportProgress.Controls.Add(this.lblImportProgressType);
            this.pnlImportProgress.Controls.Add(this.label3);
            this.pnlImportProgress.Controls.Add(this.lblImportProgressProg);
            this.pnlImportProgress.Controls.Add(this.lblImportProgressStock);
            this.pnlImportProgress.Controls.Add(this.lblImportProgressCaption2);
            this.pnlImportProgress.Controls.Add(this.lblImportProgressCaption1);
            this.pnlImportProgress.Location = new System.Drawing.Point(12, 272);
            this.pnlImportProgress.Name = "pnlImportProgress";
            this.pnlImportProgress.Size = new System.Drawing.Size(270, 63);
            this.pnlImportProgress.TabIndex = 3;
            // 
            // lblImportProgressType
            // 
            this.lblImportProgressType.AutoSize = true;
            this.lblImportProgressType.Location = new System.Drawing.Point(78, 10);
            this.lblImportProgressType.Name = "lblImportProgressType";
            this.lblImportProgressType.Size = new System.Drawing.Size(111, 13);
            this.lblImportProgressType.TabIndex = 5;
            this.lblImportProgressType.Text = "lblImportProgressType";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Type:";
            // 
            // lblImportProgressProg
            // 
            this.lblImportProgressProg.AutoSize = true;
            this.lblImportProgressProg.Location = new System.Drawing.Point(78, 40);
            this.lblImportProgressProg.Name = "lblImportProgressProg";
            this.lblImportProgressProg.Size = new System.Drawing.Size(109, 13);
            this.lblImportProgressProg.TabIndex = 3;
            this.lblImportProgressProg.Text = "lblImportProgressProg";
            // 
            // lblImportProgressStock
            // 
            this.lblImportProgressStock.AutoSize = true;
            this.lblImportProgressStock.Location = new System.Drawing.Point(78, 25);
            this.lblImportProgressStock.Name = "lblImportProgressStock";
            this.lblImportProgressStock.Size = new System.Drawing.Size(115, 13);
            this.lblImportProgressStock.TabIndex = 2;
            this.lblImportProgressStock.Text = "lblImportProgressStock";
            // 
            // lblImportProgressCaption2
            // 
            this.lblImportProgressCaption2.AutoSize = true;
            this.lblImportProgressCaption2.Location = new System.Drawing.Point(10, 40);
            this.lblImportProgressCaption2.Name = "lblImportProgressCaption2";
            this.lblImportProgressCaption2.Size = new System.Drawing.Size(51, 13);
            this.lblImportProgressCaption2.TabIndex = 1;
            this.lblImportProgressCaption2.Text = "Progress:";
            // 
            // lblImportProgressCaption1
            // 
            this.lblImportProgressCaption1.AutoSize = true;
            this.lblImportProgressCaption1.Location = new System.Drawing.Point(10, 25);
            this.lblImportProgressCaption1.Name = "lblImportProgressCaption1";
            this.lblImportProgressCaption1.Size = new System.Drawing.Size(38, 13);
            this.lblImportProgressCaption1.TabIndex = 0;
            this.lblImportProgressCaption1.Text = "Stock:";
            // 
            // tabLog
            // 
            this.tabLog.Controls.Add(this.edtLog);
            this.tabLog.Location = new System.Drawing.Point(4, 25);
            this.tabLog.Name = "tabLog";
            this.tabLog.Padding = new System.Windows.Forms.Padding(3);
            this.tabLog.Size = new System.Drawing.Size(252, 164);
            this.tabLog.TabIndex = 2;
            this.tabLog.Text = "Log";
            this.tabLog.UseVisualStyleBackColor = true;
            // 
            // edtLog
            // 
            this.edtLog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.edtLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.edtLog.Location = new System.Drawing.Point(3, 3);
            this.edtLog.Multiline = true;
            this.edtLog.Name = "edtLog";
            this.edtLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.edtLog.Size = new System.Drawing.Size(246, 158);
            this.edtLog.TabIndex = 0;
            this.edtLog.WordWrap = false;
            // 
            // FormDataPump
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(294, 347);
            this.Controls.Add(this.pnlImportProgress);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormDataPump";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Data Pump";
            this.tcDataPumpParams.ResumeLayout(false);
            this.tabDaily.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabIntra.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.pnlImportProgress.ResumeLayout(false);
            this.pnlImportProgress.PerformLayout();
            this.tabLog.ResumeLayout(false);
            this.tabLog.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tcDataPumpParams;
        private System.Windows.Forms.TabPage tabDaily;
        private System.Windows.Forms.TabPage tabIntra;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox cbDPDailyTypeFuture;
        private System.Windows.Forms.CheckBox cbDPDailyTypeIndex;
        private System.Windows.Forms.CheckBox cbDPDailyTypeStock;
        private System.Windows.Forms.CheckBox cbDPDailyTypeForex;
        private System.Windows.Forms.CheckBox cbDPDailyTypeNBPCurrency;
        private System.Windows.Forms.CheckBox cbDPDailyTypeInvestmentFund;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Panel pnlImportProgress;
        private System.Windows.Forms.Label lblImportProgressProg;
        private System.Windows.Forms.Label lblImportProgressStock;
        private System.Windows.Forms.Label lblImportProgressCaption2;
        private System.Windows.Forms.Label lblImportProgressCaption1;
        private System.Windows.Forms.Label lblImportProgressType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TabPage tabLog;
        private System.Windows.Forms.TextBox edtLog;
    }
}