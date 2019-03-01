using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace employeeRecognition.Models
{
    public class UserAcct
    {
        public int id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string create_on { get; set; }
        public string signature { get; set; }
        public int role { get; set; }
    }
}
