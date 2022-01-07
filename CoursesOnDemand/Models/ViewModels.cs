

namespace CoursesOnDemand.Models
{
    public class StudentSetCourse
    {
        public long IdStudent { get; set; }
        public int IdCourse { get; set; }
    }

    public class UserLoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}