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

        public readonly Position Position;

        public SystemPositionGridRecord(int lp, Position pos)
        {
            Position = pos;

            LP = lp;
            StockName = $"({pos.Stock.Name}) {pos.Stock.FullName}";
            Dir = pos.Direction;
            TSOpen = pos.TSOpen;
            Open = pos.Open;
            OpenCommission = pos.OpenCommission;
            TSClose = pos.TSClose;
            Close = pos.Close;
            CloseCommission = pos.CloseCommission;
            Volume = pos.Volume;
            Ticks = pos.TicksActive;
            Profit = pos.Value();
        }
    }
}
