using CoursesOnDemand.Models;
using Xunit;

namespace CoursesOnDemand.XUnitTests
{
    public class StudentTests
    {
        [Fact]
        public void ValidEntity()
        {
            var student = new Student()
            {
                FirstName = "Victor"
            };
            Assert.False(string.IsNullOrEmpty(student.FirstName));

        }

        [Fact]
        public void InvalidEntity()
        {
            var student = new Student();
            Assert.True(string.IsNullOrEmpty(student.FirstName));
        }

        [Theory]
        [InlineData("Victor")]
        [InlineData("")]
        public void ChangeStudentName(string firstname)
        {
            Assert.False(string.IsNullOrEmpty(firstname));
        }
    }
}
