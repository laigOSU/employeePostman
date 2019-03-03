using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;

namespace employeeRecognition.Controllers
{

    [Route("api/[controller]")]
    // [Authorize(Roles = "User")] // <-- UNCOMMENT THIS WHEN LOGIN TOKENS WORK
    public class SampleDataController : Controller
    {

        string connectionString = @"Data Source = tcp:erraisqlserver.database.windows.net,1433;Initial Catalog=EmployeeDB;Persist Security Info=False;User ID=erraiadmin;Password=DBaccess3;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";
        [HttpGet("[action]")]
        public IEnumerable<Models.UserAcct> WeatherForecasts()
        {
            List<Models.UserAcct> list = new List<Models.UserAcct>();

            // runs stored procedure and returns data to main page
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                String sql = @"select * from userAcct";
                //con.ConnectionString = @"Server=comp1630.database.windows.net;Database=pubs;User Id=readonlylogin;Password=;";

                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand(sql, con);

                da.Fill(dt);

                foreach (DataRow row in dt.Rows)
                {
                    var user = new Models.UserAcct();
                    user.first_name = row["first_name"].ToString();
                    user.last_name = row["last_name"].ToString();
                    user.email = row["email"].ToString();
                    user.create_on = row["create_on"].ToString();
                    list.Add(user);
                }
            }

            return list;
        }

        [HttpGet("[action]")]
        public IEnumerable<Models.award> Nominated()
        {
            List<Models.award> list2 = new List<Models.award>();

            // runs stored procedure and returns data to main page
            using (SqlConnection con2 = new SqlConnection(connectionString))
            {
                String sql2 = @"SELECT * FROM award";
                //con.ConnectionString = @"Server=comp1630.database.windows.net;Database=pubs;User Id=readonlylogin;Password=;";

                DataTable dt2 = new DataTable();
                SqlDataAdapter da2 = new SqlDataAdapter();
                da2.SelectCommand = new SqlCommand(sql2, con2);

                da2.Fill(dt2);

                foreach (DataRow row in dt2.Rows)
                {
                    var aw = new Models.award();

                    aw.sender_user_id = row["sender_user_id"].ToString();
                    aw.recipient_user_id = row["recipient_user_id"].ToString();
                    aw.type = row["type"].ToString();
                    aw.time = row["time"].ToString();
                    aw.date = row["date"].ToString();
                    list2.Add(aw);
                }
            }
            return list2;
        }
    }
}
