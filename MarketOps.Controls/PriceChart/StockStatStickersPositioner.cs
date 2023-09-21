using MarketOps.Controls.Extensions;
using MarketOps.Controls.PriceChart.PVChart;
using ScottPlot;
using System.Collections.Generic;
using System.Linq;

namespace MarketOps.Controls.PriceChart
{
    /// <summary>
    /// Manages positions of StockStat stickers on PriceVolumeChart.
    /// </summary>
    internal class StockStatStickersPositioner
    {
        private const int StickerSpace = 2;

        private readonly List<StockStatSticker> _stickers = new List<StockStatSticker>();
        private readonly PriceVolumeChart _chart;

        public StockStatStickersPositioner(PriceVolumeChart chart)
        {
            _chart = chart;
        }

        public void Add(in StockStatSticker sticker)
        {
            _stickers.Add(sticker);
            RepositionStickers();
        }

        public void Remove(in StockStatSticker sticker)
        {
            UnlinkSticker(sticker);
            _stickers.Remove(sticker);
            RepositionStickers();
        }

        public void RepositionStickers()
        {
            int nextStickerPosPrices = StickerSpace;
            int[] nextStickerPosAdditional = Enumerable.Repeat(StickerSpace, _chart.AdditionalChartsCount).ToArray();

            foreach (StockStatSticker sticker in _stickers)
            {
                var chart = _chart.FindChartForStat(sticker.Stat);
                if (sticker.Stat.IsPricesStat())
                    RepositionSticker(sticker, chart.chart, ref nextStickerPosPrices);
                else
                    RepositionSticker(sticker, chart.chart, ref nextStickerPosAdditional[chart.index]);
            }
        }

        public void UnlinkStickers() =>
            _stickers
                .ForEach(sticker => UnlinkSticker(sticker));

        private static void RepositionSticker(in StockStatSticker sticker, in FormsPlot parentChart, ref int nextLeftPos)
        {
            sticker.Parent = parentChart;
            sticker.BringToFront();
            sticker.Top = StickerSpace;
            sticker.Left = nextLeftPos;
            nextLeftPos += StickerSpace + sticker.Width;
        }

        private static void UnlinkSticker(in StockStatSticker sticker) =>
            sticker.Parent = null;
    }
}
