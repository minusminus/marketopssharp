using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MarketOps.StockData.Types;

namespace MarketOps.DataPump.Forms
{
    public partial class FormDataPump : Form
    {
        private readonly DataPumper _dataPumper;

        public FormDataPump(DataPumper dataPumper)
        {
            _dataPumper = dataPumper;
            InitializeComponent();
        }

        public void Execute()
        {
            UpdateControls(false);
            ClearPumpProgress();
            ShowDialog();
        }

        private void UpdateControls(bool stateLocked)
        {
            tcDataPumpParams.Enabled = !stateLocked;
            btnImport.Enabled = !stateLocked;
            pnlImportProgress.Enabled = stateLocked;
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            UpdateControls(true);
            try
            {
                ExecuteDataPumping();
            }
            finally
            {
                UpdateControls(false);
            }
            ClearPumpProgress();
        }

        private void ClearPumpProgress()
        {
            lblImportProgressType.Text = "";
            lblImportProgressStock.Text = "";
            lblImportProgressProg.Text = "";
        }

        private void OnStockStartProcessing(DataPumperDailyProcessingInfo info)
        {
            lblImportProgressStock.Text = $"{info.Stock.Name} [{info.CurrentPosition} / {info.TotalCount}]";
            Application.DoEvents();
        }

        private void ExecuteDataPumping()
        {
            List<Tuple<CheckBox, StockType>> defs = new List<Tuple<CheckBox, StockType>>()
            {
                new Tuple<CheckBox, StockType>(cbDPDailyTypeStock, StockType.Stock),
                new Tuple<CheckBox, StockType>(cbDPDailyTypeIndex, StockType.Index),
                new Tuple<CheckBox, StockType>(cbDPDailyTypeFuture, StockType.Future),
                new Tuple<CheckBox, StockType>(cbDPDailyTypeInvestmentFund, StockType.InvestmentFund),
                new Tuple<CheckBox, StockType>(cbDPDailyTypeNBPCurrency, StockType.NBPCurrency),
                new Tuple<CheckBox, StockType>(cbDPDailyTypeForex, StockType.Forex),
            };

            _dataPumper.StartSession(OnStockStartProcessing);
            try
            {
                foreach (var tuple in defs)
                    PumpIfChecked(tuple.Item1, tuple.Item2);
            }
            finally
            {
                _dataPumper.EndSession();
            }
        }

        private void PumpIfChecked(CheckBox cb, StockType stockType)
        {
            if (!cb.Checked) return;
            lblImportProgressType.Text = stockType.ToString();
            _dataPumper.PumpDaily(stockType);
        }
    }
}
