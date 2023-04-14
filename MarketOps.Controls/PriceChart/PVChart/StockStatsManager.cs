using MarketOps.Controls.Extensions;
using MarketOps.StockData.Types;
using System.Collections.Generic;

namespace MarketOps.Controls.PriceChart.PVChart
{
    internal delegate void StockStatAdded(StockStat stat);
    internal delegate void StockStatUpdated(StockStat stat, int index);
    internal delegate void StockStatRemoved(StockStat stat, int index);

    /// <summary>
    /// Manages stats displayed on price volume chart.
    /// </summary>
    internal class StockStatsManager
    {
        public readonly List<StockStat> PricesStats = new List<StockStat>();
        public readonly List<StockStat> AdditionalStats = new List<StockStat>();

        public event StockStatAdded OnPriceStatAdded;
        public event StockStatAdded OnAdditionalStatAdded;
        public event StockStatUpdated OnPriceStatUpdated;
        public event StockStatUpdated OnAdditionalStatUpdated;
        public event StockStatRemoved OnPriceStatRemoved;
        public event StockStatRemoved OnAdditionalStatRemoved;

        public void Add(StockStat stat)
        {
            if (stat.IsPricesStat())
                AddStatToList(PricesStats, OnPriceStatAdded);
            else
                AddStatToList(AdditionalStats, OnAdditionalStatAdded);

            void AddStatToList(List<StockStat> statsList, StockStatAdded addEvent)
            {
                statsList.Add(stat);
                addEvent?.Invoke(stat);
            }
        }

        public void Update(StockStat stat)
        {
            if (stat.IsPricesStat())
                FindStatOnList(PricesStats, OnPriceStatUpdated);
            else
                FindStatOnList(AdditionalStats, OnAdditionalStatUpdated);

            void FindStatOnList(List<StockStat> statsList, StockStatUpdated updateEvent)
            {
                int index = statsList.IndexOf(stat);
                if (index > -1)
                    updateEvent?.Invoke(stat, index);
            }
        }

        public void Remove(StockStat stat)
        {
            if (stat.IsPricesStat())
                RemoveStatFromList(PricesStats, OnPriceStatRemoved);
            else
                RemoveStatFromList(AdditionalStats, OnAdditionalStatRemoved);

            void RemoveStatFromList(List<StockStat> statsList, StockStatRemoved removeEvent)
            {
                int index = statsList.IndexOf(stat);
                if (index == -1) return;
                statsList.RemoveAt(index);
                removeEvent?.Invoke(stat, index);
            }
        }

        public void Clear()
        {
            PricesStats.Clear();
            AdditionalStats.Clear();
        }
    }
}
