using System;
using Xunit;

namespace UsedCar.Backend.Domains.Users.ValueObjects
{
    public class ZipTest
    {
        [Fact(DisplayName = "正常に値を取得できるこ")]
        public void Zip01()
        {
            // Arrange
            var expectedZip = "000-0000";

            // Act
            Zip zip = new(expectedZip);

            // Assert
            Assert.Equal(expectedZip, zip.Value);
        }

        [Theory(DisplayName = "ArgumentNullExceptionになること")]
        [InlineData("")]
        [InlineData(null)]
        public void ArgumentNullExceptionになること(string zip)
        {
            Assert.Throws<ArgumentNullException>(() => new Zip(zip));
        }
    }
}
