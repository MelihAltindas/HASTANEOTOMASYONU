using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class VeriTabani
    {
        private static string GetDatabasePath()
        {

            string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string dbPath = Path.Combine(appDirectory, "Database1.accdb");
            return dbPath;
        }

        public OleDbConnection ConnectionOpen()
        {
            string connectionString = $@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={GetDatabasePath()}";
            OleDbConnection connection = new OleDbConnection(connectionString);
            connection.Open();
            return connection;

        }
    }
}
