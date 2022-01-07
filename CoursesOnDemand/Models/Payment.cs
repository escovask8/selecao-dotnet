using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoursesOnDemand.Models
{
    public class Payment
    {
        public long Id { get; set; }
        public DateTime DataTime { get; set; }
        public float Valor { get; set; }
    }
}