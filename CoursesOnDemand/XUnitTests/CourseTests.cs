using CoursesOnDemand.Models;
using Xunit;

namespace CoursesOnDemand.XUnitTests
{
    public class CourseTests
    {
        [Fact]
        public void Validy()
        {
            var course = new Course()
            {
                Title = "xUnit Tests"
            };
            Assert.False(string.IsNullOrEmpty(course.Title));

        }

        [Fact]
        public void Invalid()
        {
            var course = new Course();
            Assert.True(true, course.Title);
        }
    }
}