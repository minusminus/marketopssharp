namespace MarketOps.SystemDefs.BBTrendRecognizer
{
    /// <summary>
    /// Types of trend
    /// </summary>
    internal enum BBTrendType
    {
        Unknown,
        Up,
        Down
    }

    /// <summary>
    /// Expectation of future trend.
    /// </summary>
    internal enum BBTrendExpectation
    {
        Unknown,
        UpAndRaising,
        UpButPossibleChange,
        DownButPossibleChange,
        DownAndFalling
    }
}
