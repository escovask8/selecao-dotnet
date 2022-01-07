using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoursesOnDemand.Models
{
    public class Course
    {
        public Nullable<int> Id { get; set; }
        public String Title { get; set; }
        public String Description { get; set; }
    }
}