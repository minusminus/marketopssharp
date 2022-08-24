using MarketOps.StockData.Types;
using MarketOps.SystemData.Types;
using System;

namespace MarketOps.SystemDefs.StrongBBTrendStocks
{
    /// <summary>
    /// Position manager for StrongBBTrend
    /// </summary>
    internal class PositionManager
    {
        public void Manage(Position position, StockPricesData data, int currentIndex)
        {
            MoveStopOnOpenIfLAboveOpen(position, data, currentIndex);
        }

        private void MoveStopOnOpenIfLAboveOpen(Position position, StockPricesData data, int currentIndex)
        {
            if (position.Open >= data.L[currentIndex]) return;
            position.CloseModePrice = Math.Max(position.CloseModePrice, position.Open);
        }
    }
}
