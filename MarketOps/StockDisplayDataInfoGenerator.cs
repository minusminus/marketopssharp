using MarketOps.Controls.Types;
using MarketOps.StockData.Extensions;

namespace MarketOps
{
    /// <summary>
    /// generates label information of selected point in stock data
    /// </summary>
    internal class StockDisplayDataInfoGenerator : IStockInfoGenerator
    {
        public string GetStockInfo(StockDisplayData data)
        {
            string nodatainfo = (data.Prices.Length == 0) ? " - no data" : string.Empty;
            return $"{data.Stock.FullName} {data.Prices.DataRangeToString()} [{data.TsFrom.ToString(data.Prices.DataRangeFormatString())} - {data.TsTo.ToString(data.Prices.DataRangeFormatString())}{nodatainfo}]";
        }

        public string GetStockSelectedInfo(StockDisplayData data, int selectedIndex) =>
            $"{data.Prices.TS[selectedIndex].ToString(data.Prices.DataRangeFormatString())} OHLC(" +
            $"{data.Stock.FormatPrice(data.Prices.O[selectedIndex])}, " +
            $"{data.Stock.FormatPrice(data.Prices.H[selectedIndex])}, " +
            $"{data.Stock.FormatPrice(data.Prices.L[selectedIndex])}, " +
            $"{data.Stock.FormatPrice(data.Prices.C[selectedIndex])}) " +
            $"V={data.Prices.V[selectedIndex]}";

        public string GetAxisXToolTip(StockDisplayData data, int selectedIndex) => $"{data.Prices.TS[selectedIndex].ToString(data.Prices.DataRangeFormatString())}";

        public string GetAxisYToolTip(StockDisplayData data, double selectedValue) => data.Stock.FormatPrice(selectedValue);
    }
}
