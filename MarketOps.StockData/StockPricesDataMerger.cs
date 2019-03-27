using System;
using MarketOps.StockData.Types;

namespace MarketOps.StockData
{
    /// <summary>
    /// merger of data prices objects
    /// adds one object to another, resulting data is still sorted in ascending order on TS
    /// doesn't merge overlapping data
    /// </summary>
    public class StockPricesDataMerger
    {
        private void SwapIfLeftAfterRight(ref StockPricesData dataLeft, ref StockPricesData dataRight)
        {
            if (dataRight.TS[0] > dataLeft.TS[dataLeft.Length - 1]) return;
            StockPricesData t = dataLeft;
            dataLeft = dataRight;
            dataRight = t;
        }

        private void ThrowIfOverlappingData(StockPricesData dataLeft, StockPricesData dataRight)
        {
            if (dataRight.TS[0] <= dataLeft.TS[dataLeft.Length - 1]) 
                throw new Exception("Overlapping data");
        }

        private void ResizeAndCopyArray<T>(ref T[] arrLeft, ref T[] arrRight)
        {
            int rightstart = arrLeft.Length;
            Array.Resize<T>(ref arrLeft, arrLeft.Length + arrRight.Length);
            Array.Copy(arrRight, 0, arrLeft, rightstart, arrRight.Length);
        }

        private void AddRightToLeft(StockPricesData dataLeft, StockPricesData dataRight)
        {
            ResizeAndCopyArray<float>(ref dataLeft.O, ref dataRight.O);
            ResizeAndCopyArray<float>(ref dataLeft.H, ref dataRight.H);
            ResizeAndCopyArray<float>(ref dataLeft.L, ref dataRight.L);
            ResizeAndCopyArray<float>(ref dataLeft.C, ref dataRight.C);
            ResizeAndCopyArray<Int64>(ref dataLeft.V, ref dataRight.V);
            ResizeAndCopyArray<DateTime>(ref dataLeft.TS, ref dataRight.TS);
        }

        public StockPricesData Merge(StockPricesData data1, StockPricesData data2)
        {
            if ((data1.Length == 0) && (data1.Length == data2.Length)) return data1;
            if ((data1.Length == 0) && (data2.Length > 0)) return data2;
            if ((data2.Length == 0) && (data1.Length > 0)) return data1;

            SwapIfLeftAfterRight(ref data1, ref data2);
            ThrowIfOverlappingData(data1, data2);
            AddRightToLeft(data1, data2);
            return data1;
        }
    }
}
