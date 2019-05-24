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
        private int _lastX = -1;
        private int _lastY = -1;

        public bool SetAndCheckChange(int x, int y)
        {
            if ((x == _lastX) && (y == _lastY)) return false;
            _lastX = x;
            _lastY = y;
            return true;
        }
    }
}
