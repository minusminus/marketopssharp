using MarketOps.StockData.Types;
using MarketOps.StockData.Interfaces;

namespace MarketOps.DataPump.DBWriters
{
    /// <summary>
    /// Updates StartTS in stock table.
    /// </summary>
    internal class StartTSUpdater
    {
        private readonly IDataPumpProvider _dataPumpProvider;

        public StartTSUpdater(IDataPumpProvider dataPumpProvider)
        {
            _dataPumpProvider = dataPumpProvider;
        }

        public void Update(StockType stockType)
        {
            string updateQuery =
                $"update at_spolki " +
                $"set startts=t.ts " +
                $"from " +
                $"( " +
                $"select fk_id_spolki, min(ts) as \"ts\" " +
                $"from {_dataPumpProvider.GetTableName(stockType, StockDataRange.Daily, 0)} " +
                $"where fk_id_spolki in (select id from at_spolki where startts is null) " +
                $"group by fk_id_spolki " +
                $") t " +
                $"where id=t.fk_id_spolki";

            _dataPumpProvider.ExecuteSQL(updateQuery);
        }
    }
}
