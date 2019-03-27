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
        private void SwapIfLeftAfterRight(ref StockPricesData DataLeft, ref StockPricesData DataRight)
        {
            if (DataRight.TS[0] > DataLeft.TS[DataLeft.Length - 1]) return;
            StockPricesData t = DataLeft;
            DataLeft = DataRight;
            DataRight = t;
        }

        private void ThrowIfOverlappingData(StockPricesData DataLeft, StockPricesData DataRight)
        {
            if (DataRight.TS[0] <= DataLeft.TS[DataLeft.Length - 1]) 
                throw new Exception("Overlapping data");
        }

        private void ResizeAndCopyArray<T>(ref T[] ArrLeft, ref T[] ArrRight)
        {
            int rightstart = ArrLeft.Length;
            Array.Resize<T>(ref ArrLeft, ArrLeft.Length + ArrRight.Length);
            Array.Copy(ArrRight, 0, ArrLeft, rightstart, ArrRight.Length);
        }

        private void AddRightToLeft(StockPricesData DataLeft, StockPricesData DataRight)
        {
            ResizeAndCopyArray<float>(ref DataLeft.O, ref DataRight.O);
            ResizeAndCopyArray<float>(ref DataLeft.H, ref DataRight.H);
            ResizeAndCopyArray<float>(ref DataLeft.L, ref DataRight.L);
            ResizeAndCopyArray<float>(ref DataLeft.C, ref DataRight.C);
            ResizeAndCopyArray<Int64>(ref DataLeft.V, ref DataRight.V);
            ResizeAndCopyArray<DateTime>(ref DataLeft.TS, ref DataRight.TS);
        }

        public StockPricesData Merge(StockPricesData Data1, StockPricesData Data2)
        {
            if ((Data1.Length == 0) && (Data1.Length == Data2.Length)) return Data1;
            if ((Data1.Length == 0) && (Data2.Length > 0)) return Data2;
            if ((Data2.Length == 0) && (Data1.Length > 0)) return Data1;

            SwapIfLeftAfterRight(ref Data1, ref Data2);
            ThrowIfOverlappingData(Data1, Data2);
            AddRightToLeft(Data1, Data2);
            return Data1;
        }
    }
}
