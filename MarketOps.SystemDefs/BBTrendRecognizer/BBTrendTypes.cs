namespace MarketOps.SystemDefs.BBTrendRecognizer
{
    /// <summary>
    /// Types of trend
    /// </summary>
    public enum BBTrendType
    {
        Unknown,
        Up,
        Down
    }

    /// <summary>
    /// Expectation of future trend.
    /// </summary>
    public enum BBTrendExpectation
    {
        Unknown,
        UpAndRaising,
        UpButPossibleChange,
        DownButPossibleChange,
        DownAndFalling
    }
}
