namespace MarketOps.System.Extensions
{
    /// <summary>
    /// Extensions to Position class.
    /// </summary>
    public static class PositionExtensions
    {
        public static float Value(this Position pos)
        {
            return (pos.Direction == PositionDir.Long ? pos.Close - pos.Open : pos.Open - pos.Close) * pos.Volume;
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
