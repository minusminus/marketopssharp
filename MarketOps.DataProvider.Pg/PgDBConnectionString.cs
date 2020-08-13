using System.Configuration;

namespace MarketOps.DataProvider.Pg
{
    internal static class PgDBConnectionString
    {
        public static string ConnectionString => ConfigurationManager.ConnectionStrings["AT"].ConnectionString;
    }
}
