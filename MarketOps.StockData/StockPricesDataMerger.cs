using System;
using MarketOps.StockData.Types;

namespace MarketOps.StockData
{
    /// <summary>
    /// merger of data prices objects
    /// adds one object to another, returns new object
    /// resulting data is still sorted in ascending order on TS
    /// doesn't merge overlapping data
    /// </summary>
    public static class StockPricesDataMerger
    {
        public static StockPricesData Merge(StockPricesData data1, StockPricesData data2)
        {
            if ((data1.Length == 0) && (data1.Length == data2.Length)) return data1;
            if ((data1.Length == 0) && (data2.Length > 0)) return data2;
            if ((data2.Length == 0) && (data1.Length > 0)) return data1;

            SwapIfLeftAfterRight(ref data1, ref data2);
            ThrowIfOverlappingData(data1, data2);
            StockPricesData res = new StockPricesData(data1, data1.Length + data2.Length);
            AddRightToLeft(res, data1, data2);
            return res;
        }

        private static void AddRightToLeft(StockPricesData dataResult, StockPricesData dataLeft, StockPricesData dataRight)
        {
            CopyBothToResult<float>(ref dataResult.O, ref dataLeft.O, ref dataRight.O);
            CopyBothToResult<float>(ref dataResult.H, ref dataLeft.H, ref dataRight.H);
            CopyBothToResult<float>(ref dataResult.L, ref dataLeft.L, ref dataRight.L);
            CopyBothToResult<float>(ref dataResult.C, ref dataLeft.C, ref dataRight.C);
            CopyBothToResult<Int64>(ref dataResult.V, ref dataLeft.V, ref dataRight.V);
            CopyBothToResult<DateTime>(ref dataResult.TS, ref dataLeft.TS, ref dataRight.TS);
        }

        private static void CopyBothToResult<T>(ref T[] arrResult, ref T[] arrLeft, ref T[] arrRight)
        {
            Array.Copy(arrLeft, arrResult, arrLeft.Length);
            Array.Copy(arrRight, 0, arrResult, arrLeft.Length, arrRight.Length);
        }

        private static void SwapIfLeftAfterRight(ref StockPricesData dataLeft, ref StockPricesData dataRight)
        {
            if (dataRight.TS[0] > dataLeft.TS[dataLeft.Length - 1]) return;
            StockPricesData t = dataLeft;
            dataLeft = dataRight;
            dataRight = t;
        }

        private static void ThrowIfOverlappingData(StockPricesData dataLeft, StockPricesData dataRight)
        {
            if (dataRight.TS[0] <= dataLeft.TS[dataLeft.Length - 1])
                throw new Exception("Overlapping data");
        }
    }
}
