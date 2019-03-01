using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace employeeRecognition.Extensions
{
    public class DbConnection
    {
        private string connectionString = @"Data Source = tcp:erraisqlserver.database.windows.net,1433;Initial Catalog=EmployeeDB;Persist Security Info=False;User ID=erraiadmin;Password=DBaccess3;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";

        private DataTable dt { get; set; }

        // runs stored procedure and returns data to main page
        public DataTable Connection(String sql)
        {
            SqlConnection con = new SqlConnection(connectionString);

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand(sql, con);

            da.Fill(dt);

            return dt;
        }
    }
}
