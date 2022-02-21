using Xunit;

namespace UsedCar.Backend.Domains.Users.ValueObjects
{
    public class Street2Test
    {
        [Fact(DisplayName = "正常に値を取得できるこ")]
        public void Street201()
        {
            // Arrange
            var expectedStreet2 = "Street2テスト";

            // Act
            Street2 street2 = new(expectedStreet2);

            // Assert
            Assert.Equal(expectedStreet2, street2.Value);
        }
    }
}
