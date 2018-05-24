using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;


namespace DataAccessLayer
{
    internal static class DBConnection
    {
        public static SqlConnection GetDBConnection()
        {
            // this should be the only place in your application
            // where your connection string appears, and this
            // should be the only method any class uses to create
            // a database connection object

            var connString = @"Data Source=localhost;Initial Catalog=kirkwoodLibrary;Integrated Security=True";
            var conn = new SqlConnection(connString);
            return conn;
        }
    }
}