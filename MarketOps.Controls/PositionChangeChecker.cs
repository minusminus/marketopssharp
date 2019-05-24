using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MarketOps.Controls
{
    /// <summary>
    /// Checks if new position (x and y coordinates) differs from previous one
    /// </summary>
    internal class PositionChangeChecker
    {
        private int _lastX = Int32.MinValue;
        private int _lastY = Int32.MinValue;

        public bool SetAndCheckChange(int x, int y)
        {
            if ((x == _lastX) && (y == _lastY)) return false;
            _lastX = x;
            _lastY = y;
            return true;
        }
    }
}
