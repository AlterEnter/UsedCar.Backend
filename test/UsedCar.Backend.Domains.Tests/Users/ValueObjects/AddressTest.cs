using Xunit;

namespace UsedCar.Backend.Domains.Users.ValueObjects
{
    public class UserTest
    {
        [Fact(DisplayName = "正常に値を取得できるこ")]
        public void Address01()
        {
            // Arrange
            var expectedZip = "000-0000";
            Zip zip = new(expectedZip);

            var expectedState = "広島県";
            State state = new(expectedState);

            var expectedCity = "広島市";
            City city = new(expectedCity);

            var expectedStreet1 = "安芸郡";
            Street1 street1 = new(expectedStreet1);

            var expectedStreet2 = "新地１－１";
            Street2 street2 = new(expectedStreet2);

            // Act
            Address address = new(
                zip,
                state,
                city,
                street1,
                street2);

            // Assert
            Assert.Equal(expectedZip, address.Zip.Value);
            Assert.Equal(expectedState, address.State.Value);
            Assert.Equal(expectedCity, address.City.Value);
            Assert.Equal(expectedStreet1, address.Street1.Value);
            Assert.Equal(expectedStreet2, address.Street2.Value);
        }
    }
}
