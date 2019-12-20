using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        private void RepositionStickers()
        {
            
        }
    }
}
