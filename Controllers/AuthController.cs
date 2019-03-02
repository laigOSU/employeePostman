using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using employeeRecognition.Extensions;
using employeeRecognition.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data.SqlClient;
using System.Data;

namespace employeeRecognition.Controllers
{
    [Route("api/[controller]")] // Original
    [ApiController] // ApiController returns data; Controller returns views
    //[Route("[controller]")]
    public class AuthController : ControllerBase  // So you get token at: http://locahost:50226/Home  , nothing after backslash
    {
        private DataTable dt { get; set; }

        private DbConnection sqlConnection = new DbConnection();

        [HttpPost("token")]   //CHANGED FROM /auth/token to ""; So to access: api/auth/token to access this endpoint of our api
        public ActionResult GetToken([FromBody]UserAcct User)
        {
            // pass in email/psw
            // query of all users and see if there is a match
            // if no match, error message 
            // if match, then send the security key

            //https://github.com/mcastillo40/employeeRecognition/blob/react-auth/employeeRecognition/Controllers/SampleDataController.cs
            List<UserAcct> list = new List<UserAcct>();

            String query = $"SELECT * FROM userAcct WHERE email='{User.email}' AND password='{User.password}'";

            String sql = @query;

            Console.WriteLine("Query: " + sql);
            dt = sqlConnection.Connection(sql);


            Console.WriteLine(sql);

            // If nothing returned in datatable, then wrong email + psw
            if (dt.Rows.Count == 0)   // https://stackoverflow.com/questions/12358950/checking-if-a-datatable-is-null
            {
                return BadRequest();
                }

            else {
                String roleQuery = $"SELECT role FROM userAcct WHERE email='{User.email}'";
                String checkRole = @roleQuery;

                Console.WriteLine("Query: " + checkRole);
                dt = sqlConnection.Connection(checkRole);

                // 1. Need security key, which we use to sign the token, s.t. we can validate it later
                // Just a string. Can save it anywhere in DB, config file, or envt. 
                // Let's put in envt s.t. we won't accidentally commit it to code version server, and it 
                // will always be secured. But later, put this key somewhere safe.
                string securityKey = "this_is_the_very_long_security_key_for_token_validation_keep_this_a_secret_somehow";

                // 2. Need symmetric security key
                // we pass our securityKey to it.
                var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));

                // 3. Need to create credentials for signing the token
                // Pass our symmetricSecurityKey. Then choose algo for generating, sign, and validate the token 
                var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);

                // 3a. Add claims (user type in JWT payload)
                var claims = new List<Claim>();
                //if (role == 0)
                //  then claims.Add = User
                // else 
                // then claims.Add = Admin

                // https://stackoverflow.com/questions/29046715/extract-values-from-datatable-with-single-row/29046726

                DataRow row = dt.Rows[0];
                int roleID = (int)row["role"];

                Console.WriteLine("roleID is: " + roleID);

                if (roleID == 1) {
                    claims.Add(new Claim(ClaimTypes.Role, "Admin"));  // Don't forget to assign this claim to the token body, in #4.    
                }

                else {
                    claims.Add(new Claim(ClaimTypes.Role, "User"));
                }

                //claims.Add(new Claim("Custom_Claim", "Custom_value"));

                // 4. Need to create the token
                var token = new JwtSecurityToken(
                    issuer: "theIssuer", //any string
                    audience: "readers",
                    expires: DateTime.Now.AddHours(1), // expires in 1 hr
                    signingCredentials: signingCredentials,
                    claims: claims
                    );

                // 5. Return token
                // We need the JWT SecurityTokenHandler to return, from this token, a string that can be used in request to our API
                // The WriteToken will return string version of token, which we return to the client
                return Ok(new JwtSecurityTokenHandler().WriteToken(token));
            }
        }
    }
}
