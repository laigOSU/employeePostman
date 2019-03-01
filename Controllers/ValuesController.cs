using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using employeeRecognition.Extensions;
using employeeRecognition.Models;

namespace employeesWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin, User")]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        //[Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            if (User.IsInRole("Admin")) 
            { 
                return new string[] {"Admin sees this"};
            }

            else if (User.IsInRole("User"))
            {
                return new string[] { "User sees this" };
            }

            else 
            //return new string[] { "Only admins see this:", "value1", "value2" };
                return new string[] { "Both Admin and User see this:", "This is Get:", "value1", "value2" };
        }
       

        //// GET api/values
        //[Authorize(Roles = "Admin")]
        //[HttpGet("{id}")]
        //public ActionResult<string> GetUser()
        //{
        //    return "Only admins see this: value";
        //}




        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }



}
