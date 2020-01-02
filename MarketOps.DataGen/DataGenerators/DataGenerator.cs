using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketOps.DataGen.DataGenerators
{
    /// <summary>
    /// Aggregate stock data generator.
    /// </summary>
    internal class DataGenerator
    {
        /*
        wzor zapytania mieiseczne/tygodniowe:

        select data.grouper as "ts", (min(array[data.id, data.open]))[2] as "open", max(data.high) as "high", min(data.low) as "low", (max(array[data.id, data.close]))[2] as "close", sum(data.volume) as "volume"
        from 
        (
        select *, date_trunc('month', ts) as "grouper" --, date_trunc('week', ts)
        from at_dzienne1
        where fk_id_spolki = 289
        and ts >= date_trunc('month', date '2019-01-01')
        ) data
        --where data.grouper = date_trunc('month', date '2019-01-01')
        group by data.grouper
        order by data.grouper
        */
    }
}
