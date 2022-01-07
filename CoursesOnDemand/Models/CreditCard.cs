using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoursesOnDemand.Models
{
    public class CreditCard
    {
        public String Number { get; set; }
        public String ValidUntil { get; set; }
        public String NameOnCard { get; set; }
        public String SCode { get; set; }
    }
}