using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoursesOnDemand.Models
{
    public class User
    {
        public Nullable<long> Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
    }
}