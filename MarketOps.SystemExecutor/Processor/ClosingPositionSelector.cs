﻿using MarketOps.StockData.Types;
using MarketOps.SystemData.Types;

namespace MarketOps.SystemExecutor.Processor
{
    /// <summary>
    /// Closing positions selector for system processor.
    /// Accepts specified positions.
    /// </summary>
    internal static class ClosingPositionSelector
    {
        public static bool OnOpen(Position position, StockPricesData pricesData, int priceIndex) =>
            position.CloseMode == PositionCloseMode.OnOpen;

        public static bool OnClose(Position position, StockPricesData pricesData, int priceIndex) =>
            position.CloseMode == PositionCloseMode.OnClose;

        public static bool OnStopHit(Position position, StockPricesData pricesData, int priceIndex) =>
            (position.CloseMode == PositionCloseMode.OnStopHit)
            && (
                ((position.Direction == PositionDir.Long) && (position.CloseModePrice >= pricesData.L[priceIndex]))
                || ((position.Direction == PositionDir.Short) && (position.CloseModePrice <= pricesData.H[priceIndex]))
            );

        public static bool OnStopHitInFirstTick(Position position, StockPricesData pricesData, int priceIndex) =>
            (position.TicksActive == 1)
            && OnStopHit(position, pricesData, priceIndex);
    }
}
