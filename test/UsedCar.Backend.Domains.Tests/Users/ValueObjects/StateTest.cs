using System;
using Xunit;

namespace UsedCar.Backend.Domains.Users.ValueObjects
{
    public class StateTest
    {
        [Fact(DisplayName = "正常に値を取得できるこ")]
        public void State01()
        {
            // Arrange
            var expectedState = "広島県";

            // Act
            State state = new(expectedState);

            // Assert
            Assert.Equal(expectedState, state.Value);
        }

        [Theory(DisplayName = "ArgumentNullExceptionになること")]
        [InlineData("")]
        [InlineData(null)]
        public void ArgumentNullExceptionになること(string state)
        {
            Assert.Throws<ArgumentNullException>(() => new State(state));
        }
    }
}
