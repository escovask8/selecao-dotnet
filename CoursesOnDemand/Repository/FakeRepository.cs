using CoursesOnDemand.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoursesOnDemand.Repository
{
    public static class FakeRepository
    {
        public static List<Student> Students { get; set; }
        public static List<Course> Courses { get; set; }
    }
}