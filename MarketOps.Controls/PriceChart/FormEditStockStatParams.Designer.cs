﻿namespace MarketOps.Controls.PriceChart
{
    partial class FormEditStockStatParams
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
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.dlgColor = new System.Windows.Forms.ColorDialog();
            this.pnlTblLayoutColors = new System.Windows.Forms.TableLayoutPanel();
            this.dgvParams = new MarketOps.Controls.StockData.MOParamsEditor();
            this.pnlButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlButtons
            // 
            this.pnlButtons.Controls.Add(this.btnCancel);
            this.pnlButtons.Controls.Add(this.btnOk);
            this.pnlButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlButtons.Location = new System.Drawing.Point(0, 237);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Size = new System.Drawing.Size(424, 35);
            this.pnlButtons.TabIndex = 1;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(340, 5);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(259, 5);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 0;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // dlgColor
            // 
            this.dlgColor.AnyColor = true;
            this.dlgColor.FullOpen = true;
            // 
            // pnlTblLayoutColors
            // 
            this.pnlTblLayoutColors.ColumnCount = 1;
            this.pnlTblLayoutColors.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.pnlTblLayoutColors.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.pnlTblLayoutColors.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.pnlTblLayoutColors.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlTblLayoutColors.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.pnlTblLayoutColors.Location = new System.Drawing.Point(0, 199);
            this.pnlTblLayoutColors.Name = "pnlTblLayoutColors";
            this.pnlTblLayoutColors.RowCount = 1;
            this.pnlTblLayoutColors.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.pnlTblLayoutColors.Size = new System.Drawing.Size(424, 38);
            this.pnlTblLayoutColors.TabIndex = 3;
            // 
            // dgvParams
            // 
            this.dgvParams.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvParams.Location = new System.Drawing.Point(0, 0);
            this.dgvParams.Name = "dgvParams";
            this.dgvParams.Size = new System.Drawing.Size(424, 199);
            this.dgvParams.TabIndex = 4;
            // 
            // FormEditStockStatParams
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(424, 272);
            this.Controls.Add(this.dgvParams);
            this.Controls.Add(this.pnlTblLayoutColors);
            this.Controls.Add(this.pnlButtons);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormEditStockStatParams";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edit stat parameters";
            this.pnlButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel pnlButtons;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.ColorDialog dlgColor;
        private System.Windows.Forms.TableLayoutPanel pnlTblLayoutColors;
        private StockData.MOParamsEditor dgvParams;
    }
}