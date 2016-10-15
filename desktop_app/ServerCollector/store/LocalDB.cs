using System;
using System.Data.SqlClient;

namespace ServerCollector.store
{
    public class LocalDB
    {
        //public const string defaultDatabaseName = "JhpDefaultDB1.db3";
        public string ConnectionString { get; set; }
        public LocalDB()
        {
            var connString = Properties.
                Settings.Default.TestDbConnString;
            ConnectionString = connString;
        }

        public string newId()
        {
            return Guid.NewGuid().ToString("N");
        }
    }
}