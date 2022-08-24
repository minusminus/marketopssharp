using MarketOps.SystemData.Types;

namespace MarketOps.SystemData.Extensions
{
    /// <summary>
    /// Extensions to Position class.
    /// </summary>
    public static class PositionExtensions
    {
        public static float DirectionMultiplier(this Position pos) => 
            pos.Direction.DirectionMultiplier();

        public static PositionDir ReversedDirection(this Position pos) => 
            pos.Direction.ReverseDirection();

        public static float Value(this Position pos) => 
            pos.DirectionMultiplier() * (pos.Close - pos.Open) * pos.Volume;

        public static float OpenValue(this Position pos) => 
            pos.Open * pos.Volume;

        public static float CloseValue(this Position pos) => 
            pos.Close * pos.Volume;

        public static float RValue(this Position pos) => 
            ((pos.CloseMode == PositionCloseMode.OnStopHit) && (pos.EntrySignal != null))
                ? pos.DirectionMultiplier() * (pos.Open - pos.EntrySignal.InitialStopValue)
                : 0;

        public static float CalculateRProfit(this Position pos)
        {
            float r = pos.RValue();
            return (r != 0)
                ? pos.DirectionMultiplier() * (pos.Close - pos.Open) / r
                : 0;
        }
    }
}
