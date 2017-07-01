using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class DBConnection
    {
        public static SqlConnection GetDBConnection()
        {
            var connString = @"Data Source=localhost;Initial Catalog=SignInFormDB;Integrated Security=True";
            var conn = new SqlConnection(connString);
            return conn;
        }
    }
}
