using System;
using Xunit;

namespace UsedCar.Backend.Domains.Users.ValueObjects
{
    public class DisplayNameTest
    {
        [Fact(DisplayName = "問題がなく値を取得することができること")]
        public void DisplayName01()
        { 
            // arrange 準備
            var expectedDisplayName = "buzz";

            // act 実行
            DisplayName displayName = new(expectedDisplayName);

            // assert 検証
            Assert.Equal(expectedDisplayName, displayName.Value);
        }

        [Fact(DisplayName = "ArgumentOutOfRangeExceptionになること")]
        public void DisplayName02()
        { 
            // arrange 準備
            var expectedDisplayName = "buzzbuzzbuzzbuzzbuzzbuzzbuzzbu32";

            // assert 検証
            _ = Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                _ = new DisplayName(expectedDisplayName);
            });
        }

        [Theory(DisplayName = "ArgumentNullExceptionになること")]
        [InlineData("")]
        [InlineData(null)]
        public void ArgumentNullExceptionになること(string displayName)
        {
            // assert 検証
            _ = Assert.Throws<ArgumentNullException>(() =>
            {
                _ = new DisplayName(displayName);
            });
        }
    }
}
