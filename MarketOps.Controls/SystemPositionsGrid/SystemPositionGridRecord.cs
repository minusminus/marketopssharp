using MarketOps.SystemData.Types;
using MarketOps.SystemData.Extensions;
using System;

namespace MarketOps.Controls.SystemPositionsGrid
{
    /// <summary>
    /// Record for system position grid.
    /// </summary>
    internal class SystemPositionGridRecord
    {
        public SystemPositionGridRecord(int lp, Position position)
        {
            LP = lp;
            StockName = position.Stock.Name;
            Dir = position.Direction;
            TSOpen = position.TSOpen;
            Open = position.Open;
            OpenCommission = position.OpenCommission;
            TSClose = position.TSClose;
            Close = position.Close;
            CloseCommission = position.CloseCommission;
            Volume = position.Volume;
            Ticks = position.TicksActive;
            Profit = position.Value();
        }

        public int LP { get; }
        public string StockName { get; }
        public PositionDir Dir { get; }
        public DateTime TSOpen { get; }
        public float Open { get; }
        public float OpenCommission { get; }
        public DateTime TSClose { get; }
        public float Close { get; }
        public float CloseCommission { get; }
        public float Volume { get; }
        public int Ticks { get; }
        public float Profit { get; }
    }
}
