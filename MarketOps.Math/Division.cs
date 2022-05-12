namespace MarketOps.Maths
{
    /// <summary>
    /// Division extensions
    /// </summary>
    public static class Division
    {
        public static float DivideByZeroToZero(this float dividend, float divider) =>
            (divider != 0)
            ? dividend / divider
            : 0;
    }
}
