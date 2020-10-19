namespace MarketOps.SystemExecutor.Extensions
{
    /// <summary>
    /// Extensions to Position class.
    /// </summary>
    public static class PositionExtensions
    {
        public static float DirectionMultiplier(this Position pos)
        {
            return pos.Direction.DirectionMultiplier();
        }

        public static PositionDir ReverseDirection(this Position pos)
        {
            return pos.Direction.ReverseDirection();
        }

        public static float Value(this Position pos)
        {
            return pos.DirectionMultiplier() * (pos.Close - pos.Open) * pos.Volume;
        }

        public static float OpenValue(this Position pos)
        {
            return pos.Open * pos.Volume;
        }

        public static float CloseValue(this Position pos)
        {
            return pos.Close * pos.Volume;
        }
    }
}
