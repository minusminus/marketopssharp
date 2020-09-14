namespace MarketOps.System.Extensions
{
    /// <summary>
    /// Extensions to PositionDir enum.
    /// </summary>
    public static class PositionDirExtensions
    {
        public static float DirectionMultiplier(this PositionDir postionDir)
        {
            return postionDir == PositionDir.Long ? 1 : -1;
        }

        public static PositionDir ReverseDirection(this PositionDir postionDir)
        {
            return postionDir == PositionDir.Long ? PositionDir.Short : PositionDir.Long;
        }
    }
}
