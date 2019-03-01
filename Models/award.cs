using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace employeeRecognition.Models
{
    public class award
    {
        public string id { get; set; }
        public string sender_user_id { get; set; }
        public string recipient_user_id { get; set; }
        public string type { get; set; }
        public string time { get; set; }
        public string date { get; set; }
    }
}
