using System;
using UsedCar.Backend.Domains.Users.AggregateRoots;
using UsedCar.Backend.Domains.Users.ValueObjects;
using Xunit;

namespace UsedCar.Backend.Domains.AggregateRoots
{
    public class UserTest
    {
        [Fact(DisplayName = "正常に値を取得できるこ")]
        public void User01()
        {
            // Arrange
            var expectedUserId = Guid.NewGuid();
            UserId userId = new(expectedUserId);

            var expectedFirstName = "fizz";
            var expectedLastName = "buzz";
            Name name = new(expectedFirstName, expectedLastName);

            var expectedDisplayName = "test";
            DisplayName displayName = new(expectedDisplayName);

            var expectedDateOfBirth = new DateTime(2000,01,01);
            DateOfBirth dateOfBirth = new(expectedDateOfBirth);

            var expectedPhoneNumber = "000-0000-0000";
            PhoneNumber phoneNumber = new(expectedPhoneNumber);

            var expectedMailAddress = "test@sample.com";
            MailAddress mailAddress = new(expectedMailAddress);

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

            Address address = new(
                zip,
                state,
                city,
                street1,
                street2);

            // Act
            User user = new(
                userId,
                name,
                displayName,
                dateOfBirth,
                phoneNumber,
                mailAddress,
                address
                );

            // Assert
            Assert.Equal(expectedUserId, user.UserId.Value);
            Assert.Equal(expectedFirstName, user.Name.FirstName);
            Assert.Equal(expectedLastName, user.Name.LastName);
            Assert.Equal(expectedDisplayName, user.DisplayName.Value);
            Assert.Equal(expectedDateOfBirth, user.DateOfBirth.Value);
            Assert.Equal(expectedMailAddress, user.MailAddress.Value);
            Assert.Equal(expectedZip, user.Address.Zip.Value);
            Assert.Equal(expectedState, user.Address.State.Value);
            Assert.Equal(expectedCity, user.Address.City.Value);
            Assert.Equal(expectedStreet1, user.Address.Street1.Value);
            Assert.Equal(expectedStreet2, user.Address.Street2.Value);
        }
    }
}
