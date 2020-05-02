using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketOps.DataProvider.Pg
{
    internal static class PgDataExtensions
    {
        public static string ToTimestampQueryValue(this DateTime dt)
        {
            return $"timestamp '{dt.ToString("yyyy-MM-dd HH:mm")}'";
        }
    }
}
