using System;
using UsedCar.Backend.Domains.Users.ValueObjects;
using Xunit;

namespace UsedCar.Backend.Domains.ValueObjects
{
    public class UserIdTest
    {
        [Fact(DisplayName = "正常に値を取得できるこ")]
        public void UserId01()
        {
            // Arrange
            var expectedUserId = Guid.NewGuid();

            // Act
            UserId userId = new(expectedUserId);

            // Assert
            Assert.Equal(expectedUserId, userId.Value);
        }
    }
}
