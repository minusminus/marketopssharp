using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms.DataVisualization.Charting;

namespace MarketOps.Controls.PriceChart
{
    /// <summary>
    /// Manages positions of StockStat stickers on chart areas.
    /// </summary>
    internal class StockStatStickersPositioner
    {
        private readonly List<StockStatSticker> _stickers = new List<StockStatSticker>();
        private readonly PriceVolumeChart _chart;

        public StockStatStickersPositioner(PriceVolumeChart chart)
        {
            _chart = chart;
        }

        public void Add(StockStatSticker sticker)
        {
            _stickers.Add(sticker);
            sticker.Parent = _chart;
            sticker.BringToFront();
            RepositionStickers();
        }

        public void Remove(StockStatSticker sticker)
        {
            sticker.Parent = null;
            _stickers.Remove(sticker);
            RepositionStickers();
        }

        public void RepositionStickers()
        {
            const int stickerSpace = 2;
            int[] nextStickerPos = Enumerable.Repeat(stickerSpace, _chart.ChartAreas.Count).ToArray();
            foreach (StockStatSticker sticker in _stickers)
            {
                ChartArea area = _chart.ChartAreas[sticker.Stat.ChartArea];
                int areaIndex = _chart.ChartAreas.IndexOf(area);
                sticker.Left = nextStickerPos[areaIndex];
                nextStickerPos[areaIndex] += stickerSpace + sticker.Width;
                sticker.Top = (int) ((float) _chart.Height*area.Position.Y/100F) + stickerSpace;
            }
        }
    }
}
