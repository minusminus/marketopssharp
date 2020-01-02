using System;
using System.Collections.Generic;
using MarketOps.StockData.Interfaces;
using MarketOps.StockData.Types;

namespace MarketOps.DataProvider.Pg.Bossa
{
    /// <summary>
    /// data pump database provider for AT database on postgres
    /// </summary>
    public class PgDataProvider : PgBaseProvider, IDataPumpProvider, IDataGenProvider
    {
        private readonly DataTableSelector _dataTableSelector;

        public PgDataProvider(DataTableSelector dataTableSelector)
        {
            _dataTableSelector = dataTableSelector;
        }

        public Dictionary<StockType, DataPumpDownloadDefinition> GetDownloadDefinitions()
        {
            Dictionary<StockType, DataPumpDownloadDefinition> res = new Dictionary<StockType, DataPumpDownloadDefinition>();

            string qry = $"select * from bossa_downloadurls order by typ";
            ProcessSelectQuery(qry, (reader) =>
            {
                if (!reader.HasRows) return;
                while (reader.Read())
                {
                    DataPumpDownloadDefinition def = new DataPumpDownloadDefinition();
                    PgDataToDownloadDefinitionConverter.ToDownloadDefinition(reader, def);
                    res.Add(def.Type, def);
                }
            });
            return res;
        }

        public string GetTableName(StockType stockType, StockDataRange dataRange, int intradayInterval)
        {
            return _dataTableSelector.GetTableName(stockType, dataRange, intradayInterval);
        }

        public DateTime GetMaxTS(StockDefinition stockDefinition, StockDataRange dataRange, int intradayInterval)
        {
            DateTime res = DateTime.MinValue;

            string qry = $"select max(ts) from {GetTableName(stockDefinition.Type, dataRange, intradayInterval)} where fk_id_spolki={stockDefinition.ID}";
            ProcessSelectQuery(qry, (reader) =>
            {
                if (!reader.HasRows) return;
                reader.Read();
                res = reader.GetFieldValue<DateTime>(0);
            });
            return res;
        }

        public void ExecuteSQL(string qry)
        {
            ExecuteQuery(qry);
        }
    }
}
