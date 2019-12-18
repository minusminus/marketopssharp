using System;
using System.Linq;
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
        public void Map(string currFileLine, string prevFileLine, DataPumpStockData stockData)
        {
            var currData = SplitLineToCols(currFileLine);
            var prevData = SplitLineToCols(prevFileLine);
            VerifyLineData(currData);
            FillStockData(stockData, currData, prevData);
        }

        private string[] SplitLineToCols(string dataFileLine)
        {
            if (String.IsNullOrEmpty(dataFileLine)) return null;
            return dataFileLine.Split(',');
        }

        private void VerifyLineData(string[] lineData)
        {
            if ((lineData.Length != 7) && (lineData.Length != 8))
                throw new Exception($"Incorrect line length: {lineData.Length} columns");
            for (int i = 1; i <= 6; i++)
            {
                if (!lineData[i].All(c => char.IsDigit(c) || (c == '.')))
                    throw new Exception($"Incorrect value format in column {i}: {lineData[i]}");
            }
        }

        private void FillStockData(DataPumpStockData stockData, string[] currLineData, string[] prevLineData)
        {
            stockData.O = currLineData[2];
            stockData.H = currLineData[3];
            stockData.L = currLineData[4];
            stockData.C = currLineData[5];
            stockData.V = currLineData[6];
            stockData.RefCourse = prevLineData != null ? prevLineData[5] : "0";
            stockData.TS = DateTime.ParseExact(currLineData[1], "yyyyMMdd", null);
        }
    }
}
