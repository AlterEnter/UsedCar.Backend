using System;
using UsedCar.Backend.Domains.Users.ValueObjects;
using Xunit;

namespace UsedCar.Backend.Domains.ValueObjects
{
    public class Street1Test
    {
        [Fact(DisplayName = "正常に値を取得できるこ")]
        public void Street101()
        {
            // Arrange
            var expectedStreet1 = "安芸郡新地１－１";

            // Act
            Street1 street1 = new(expectedStreet1);

            // Assert
            Assert.Equal(expectedStreet1, street1.Value);
        }

        [Theory(DisplayName = "ArgumentNullExceptionになること")]
        [InlineData("")]
        [InlineData(null)]
        public void ArgumentNullExceptionになること(string street1)
        {
            Assert.Throws<ArgumentNullException>(() => new Street1(street1));
        }
    }
}
