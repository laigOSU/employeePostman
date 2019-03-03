using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using employeeRecognition.Extensions;
using employeeRecognition.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace employeeRecognition.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private DataTable dt { get; set; }

        private DbConnection sqlConnection = new DbConnection();

        //[Authorize(Roles = "Admin")]
        [HttpGet("[action]")]
        public IEnumerable<UserAcct> Index()
        {
            List<UserAcct> list = new List<UserAcct>();

            String sql = @"SELECT * FROM userAcct";

            dt = sqlConnection.Connection(sql);

            foreach (DataRow row in dt.Rows)
            {
                var user = new UserAcct();
                user.id = (int)row["id"];
                user.first_name = row["first_name"].ToString();
                user.last_name = row["last_name"].ToString();
                user.email = row["email"].ToString();
                list.Add(user);
            }

            Console.WriteLine("LIST 12345: " + list);

            return list;
        }

        [HttpGet("{id}")]
        public ActionResult<string> GetUser()
        {
            return "Only admins see this: value";
        }


        [HttpPost("[action]")]
        public IActionResult Create([FromBody]UserAcct User)
        {
            if (ModelState.IsValid)
            {
                List<UserAcct> list = new List<UserAcct>();

                String query = $"INSERT INTO userAcct(first_name, last_name, password, email, role) VALUES" +
                    $"('{User.first_name}', '{User.last_name}', '{User.password}', '{User.email}', {User.role})" +
                    " SELECT id FROM userAcct WHERE id = SCOPE_IDENTITY()";

                String sql = @query;

                dt = sqlConnection.Connection(sql);

                foreach (DataRow row in dt.Rows)
                {
                    var user = new UserAcct();
                    user.id = (int)row["id"];
                    list.Add(user);
                }

                Console.WriteLine("LIST: " + list[0].id);
                var userId = new { list[0].id }; // create the object to return

                return new ObjectResult(new { Id = list[0].id }) { StatusCode = 201 };
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("[action]")]
        public IActionResult UploadSignature(int id, IList<IFormFile> files)
        {
            Console.WriteLine("USER: " + id);
            Console.WriteLine("Files: " + files);

            if (ModelState.IsValid)
            {
                String query = $"UPDATE userAcct set signature='{files}' WHERE userAcct.id={id}";

                String sql = @query;

                Console.WriteLine("QUERY: " + sql);

                dt = sqlConnection.Connection(sql);

                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }


        [HttpDelete("[action]")]
        public void Delete(int id)
        {
            String query = $"DELETE FROM userAcct WHERE userAcct.id = {id}";

            String sql = @query;

            dt = sqlConnection.Connection(sql);
        }


        [HttpPut("[action]")]
        public IActionResult Edit(int id, [FromBody]UserAcct User)
        {
            if (ModelState.IsValid)
            {
                String query = $"Update userAcct set first_name='{User.first_name}', last_name='{User.last_name}', password='{User.password}', email='{User.email}', role={User.role}, signature='{User.signature}' WHERE userAcct.id={id}";

                String sql = @query;

                Console.WriteLine("QUERY: " + sql);

                dt = sqlConnection.Connection(sql);

                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
