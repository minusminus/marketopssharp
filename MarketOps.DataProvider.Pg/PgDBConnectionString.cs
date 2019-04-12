using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace MarketOps.DataProvider.Pg
{
    internal class PgDBConnectionString
    {
        public static string ConnectionString => ConfigurationManager.ConnectionStrings["AT"].ConnectionString;
    }
}
