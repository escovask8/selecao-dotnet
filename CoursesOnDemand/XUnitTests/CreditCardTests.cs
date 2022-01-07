using CoursesOnDemand.Models;
using Xunit;

namespace CoursesOnDemand.XUnitTests
{
    public class CreditCardTests
    {
        [Fact]
        public void ValidEntity()
        {
            var ccard = new CreditCard()
            {
                Number = "9999156488131564"
            };
            Assert.False(string.IsNullOrEmpty(ccard.Number));
        }

        [Fact]
        public void InvalidEntity()
        {
            var ccard = new CreditCard();
            Assert.True(string.IsNullOrEmpty(ccard.Number));
        }
    }
}