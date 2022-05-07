namespace MarketOps.Maths
{
    /// <summary>
    /// Calculates change current to previous value in percent of previous value.
    /// </summary>
    public static class ChangeInPercent
    {
        public static float Calculate(float current, float previous) =>
                (previous != 0) ? (current - previous) / previous : 0;

        public static double Calculate(double current, double previous) =>
                (previous != 0) ? (current - previous) / previous : 0;
    }
}
