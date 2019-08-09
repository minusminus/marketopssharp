using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MarketOps.Controls.Types;
using MarketOps.StockData.Types;

namespace MarketOps.Controls
{
    public partial class FormEditStockStatParams : Form
    {
        public FormEditStockStatParams()
        {
            InitializeComponent();
        }

        public bool Execute(StockStatParams statParams)
        {
            LoadStatParams(statParams);
            return (ShowDialog() == DialogResult.OK);
        }

        private void LoadStatParams(StockStatParams statParams)
        {
            foreach (var param in statParams)
                srcParams.Add(new StockStatParamEditMapper(param));
        }
    }
}
