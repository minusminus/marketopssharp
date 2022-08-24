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

        public static float FloorDivideWithAccuracy(this float dividend, float divider, float accuracy)
        {
            float result = dividend / divider;
            float ceil = (float)System.Math.Ceiling(result);
            if (ceil - result < accuracy) return ceil;
            return (float)System.Math.Floor(result);
        }

        public static float CeilingDivideWithAccuracy(this float dividend, float divider, float accuracy)
        {
            float result = dividend / divider;
            float floor = (float)System.Math.Floor(result);
            if (result - floor < accuracy) return floor;
            return (float)System.Math.Ceiling(result);
        }
    }
}
