using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketOps.System.Equity
{
    /// <summary>
    /// System positions data.
    /// </summary>
    internal class Positions
    {
        public readonly List<Position> Active = new List<Position>();
        public readonly List<Position> Closed = new List<Position>();
    }
}
