using System;
using MarketOps.DataPump.Types;

namespace MarketOps.DataPump.Bossa
{
    /// <summary>
    /// Bossa daily data file line mapper to DataPumpStockData
    /// 
    /// example data lines:
    /// PKO909,20191002,144.3000,144.3000,144.3000,144.3000,0
    /// KGHM,20191004,74.0200,74.9400,73.6200,74.9400,439632
    /// </summary>
    internal class DailyDataFileLineToStockData : IDataFileLineToStockData
    {
        public void Map(string dataFileLine, DataPumpStockData stockData)
        {
            var cols = SplitLineToCols(dataFileLine);
            VerifyLineData(cols);
            FillStockData(stockData, cols);
        }

        private string[] SplitLineToCols(string dataFileLine)
        {
            return dataFileLine.Split(',');
        }

        private void VerifyLineData(string[] cols)
        {
            if (cols.Length != 7) throw new Exception($"Incorrect line length: {cols.Length} columns");
            for (int i = 1; i <= 6; i++)
            {
                Int64 t;
                if (!Int64.TryParse(cols[i].Replace(".", ""), out t))
                    throw new Exception($"Incorrect value format in column {i}: {cols[i]}");
            }
        }

        private void FillStockData(DataPumpStockData stockData, string[] cols)
        {
            stockData.O = cols[2];
            stockData.H = cols[3];
            stockData.L = cols[4];
            stockData.C = cols[5];
            stockData.V = cols[6];
            stockData.TS = DateTime.ParseExact(cols[1], "yyyyMMdd", null);
        }
    }
}
