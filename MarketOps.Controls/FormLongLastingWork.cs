using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MarketOps.Controls
{
    public partial class FormLongLastingWork : Form
    {
        public FormLongLastingWork()
        {
            InitializeComponent();
        }

        public async Task<bool> Execute(string caption, string exceptionCaption, Action operation)
        {
            bool result = true;
            lblInfo.Text = caption;
            var msgDisplay = new MsgDisplay(this, exceptionCaption);
            using (var taskForm = this.ShowDialogAsync())
            {
                try
                {
                    await Task.Run(operation);
                }
                catch (Exception e)
                {
                    msgDisplay.Error(e.ToString());
                    result = false;
                }

                Close();
                await taskForm;
            }

            return result;
        }
    }
}
