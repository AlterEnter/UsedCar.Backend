using System;
using UsedCar.Backend.Domains.Users.ValueObjects;
using Xunit;

namespace UsedCar.Backend.Domains.ValueObjects
{
    public class PhoneNumberTest
    {
        [Fact(DisplayName = "正常に値を取得できるこ")]
        public void PhoneNumber01()
        {
            // Arrange
            var expectedMobilePhoneNumber = "000-0000-0000";

            // Act
            PhoneNumber phoneNumber = new(expectedMobilePhoneNumber);

            // Assert
            Assert.Equal(expectedMobilePhoneNumber, phoneNumber.Value);
        }

        [Theory(DisplayName = "ArgumentNullExceptionになること")]
        [InlineData("")]
        [InlineData(null)]
        public void ArgumentNullExceptionになること(string mobilePhoneNumber)
        {
            Assert.Throws<ArgumentNullException>(() => new PhoneNumber(mobilePhoneNumber));
        }

        [Fact(DisplayName = "ArgumentOutOfRangeExceptionになること")]
        public void PhoneNumber02()
        {
            // Arrange
            var mobilePhoneNumber = "123456789012345A";
            Assert.Throws<ArgumentOutOfRangeException>(() => new PhoneNumber(mobilePhoneNumber));
        }
    }
}
