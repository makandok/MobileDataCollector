using System;
using System.Data.SqlClient;

namespace ServerCollector.store
{
    public class OutDb: LocalDB3
    {
        public OutDb():base()
        {
        }
    }

    public class LocalDB3
    {
        public string defaultConnectionString = Properties.Settings.Default.TestDbConnString;
        public LocalDB3(string connString)
        {
            defaultConnectionString = connString;
        }
        public LocalDB3()
        {
        }
        public SqlConnection DB
        {
            get
            {
                return new SqlConnection(defaultConnectionString);
            }
        }

        public string newId()
        {
            return Guid.NewGuid().ToString("N");
        }
    }
}