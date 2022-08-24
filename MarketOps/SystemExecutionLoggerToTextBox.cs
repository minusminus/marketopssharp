using MarketOps.SystemData.Interfaces;
using System;
using System.Windows.Forms;

namespace MarketOps
{
    /// <summary>
    /// Logs system execution messages to TextBox control.
    /// </summary>
    internal class SystemExecutionLoggerToTextBox : ISystemExecutionLogger
    {
        private readonly TextBox _logControl;

        public SystemExecutionLoggerToTextBox(TextBox logControl)
        {
            _logControl = logControl;
        }

        public void Add(string message) => 
            _logControl.AppendText(message + Environment.NewLine);
    }
}
