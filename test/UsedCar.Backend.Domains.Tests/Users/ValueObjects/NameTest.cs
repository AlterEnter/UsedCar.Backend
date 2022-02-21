using System;
using Xunit;

namespace UsedCar.Backend.Domains.Users.ValueObjects
{
    public class NameTest
    {
        [Fact(DisplayName = "正常に値を取得できるこ")]
        public void Name01()
        {
            string expectedFirstName = "Red";
            string expectedLastName = "Blue";

            Name name = new(expectedFirstName, expectedLastName);

            Assert.Equal(expectedFirstName, name.FirstName);
            Assert.Equal(expectedLastName, name.LastName);
        }

        [Theory(DisplayName = "ArgumentNullExceptionになること")]
        [InlineData("", "")]
        [InlineData(null, null)]
        [InlineData("", "red")]
        [InlineData("blue", null)]
        public void ArgumentNullExceptionになること(string firstName, string lastName)
        {
            Assert.Throws<ArgumentNullException>(() => new Name(firstName, lastName));
        }

        [Theory(DisplayName = "ArgumentOutOfRangeExceptionになること")]
        [InlineData("123451234512345A", "123451234512345B")]
        [InlineData("123451234512345", "123451234512345A")]
        [InlineData("123451234512345A", "123451234512345")]
        public void ArgumentOutOfRangeExceptionになること(string firstName, string lastName)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Name(firstName, lastName));
        }
    }
}
