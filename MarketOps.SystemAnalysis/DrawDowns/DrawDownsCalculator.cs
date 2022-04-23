namespace MarketOps.SystemAnalysis.DrawDowns
{
    /// <summary>
    /// Calculates drawdown with flooding algorithm.
    /// Drawdown is active until next value is higher then its top.
    /// Callsback on finished drawdown.
    /// </summary>
    internal static class DrawDownsCalculator
    {
        public delegate void OnDrawDown(int startIndex, int lastIndex, int bottomIndex, float topValue, float bottomValue);

        private class DrawDownInfo
        {
            public int StartIndex;
            public int LastIndex;
            public int BottomIndex;
            public float TopValue;
            public float BottomValue;
        }

        public static void Calculate(float[] data, OnDrawDown onDrawDown)
        {
            if (data.Length == 0) return;

            DrawDownInfo ddInfo = new DrawDownInfo();
            StartNewDrawDown(ddInfo, 0, data[0]);
            ProcessData(data, onDrawDown, ddInfo);
            CallOnFinishedDrawDown(ddInfo, onDrawDown);
        }

        private static void ProcessData(float[] data, OnDrawDown onDrawDown, DrawDownInfo ddInfo)
        {
            for (int i = 1; i < data.Length; i++)
                ProcessValue(i, data[i], ddInfo, onDrawDown);
        }

        private static void ProcessValue(int currentIndex, float currentValue, DrawDownInfo ddInfo, OnDrawDown onDrawDown)
        {
            if (ddInfo.TopValue > currentValue)
                UpdateCurrentDrawDown(ddInfo, currentIndex, currentValue);
            else
            {
                CallOnFinishedDrawDown(ddInfo, onDrawDown);
                StartNewDrawDown(ddInfo, currentIndex, currentValue);
            }
        }

        private static void StartNewDrawDown(DrawDownInfo ddInfo, int startIndex, float value)
        {
            ddInfo.StartIndex = startIndex;
            ddInfo.BottomIndex = startIndex;
            ddInfo.LastIndex = -1;
            ddInfo.TopValue = value;
            ddInfo.BottomValue = value;
        }

        private static void UpdateCurrentDrawDown(DrawDownInfo ddInfo, int currentIndex, float currentValue)
        {
            ddInfo.LastIndex = currentIndex;
            if (ddInfo.BottomValue > currentValue)
            {
                ddInfo.BottomIndex = currentIndex;
                ddInfo.BottomValue = currentValue;
            }
        }

        private static void CallOnFinishedDrawDown(DrawDownInfo ddInfo, OnDrawDown onDrawDown)
        {
            if (ddInfo.LastIndex == -1) return;
            onDrawDown?.Invoke(ddInfo.StartIndex, ddInfo.LastIndex, ddInfo.BottomIndex, ddInfo.TopValue, ddInfo.BottomValue);
        }
    }
}
