using System;
using UsedCar.Backend.Domains.Users.ValueObjects;
using Xunit;

namespace UsedCar.Backend.Domains.ValueObjects
{
    public class DateOfBirthTest
    {
        [Fact(DisplayName = "問題がなく値を取得することができること")]
        public void DateOfBirth01()
        {
            // arrange 準備
            DateTime expectedDate = new(2000, 01, 01);

            // act 実行
            DateOfBirth actualDate = new(expectedDate);

            // assert 検証
            Assert.Equal(expectedDate, actualDate.Value);
        }

        [Theory(DisplayName = "ArgumentOutOfRangeExceptionになること")]
        [InlineData("1909/12/31 00:00:00")]
        [InlineData("2122/12/31 00:00:00")]
        public void ArgumentOutOfRangeExceptionになること(string dateTime)
        {
            // arrange 準備
            DateTime actualDate = DateTime.Parse(dateTime);

            // assert 検証
            _ = Assert.Throws<ArgumentOutOfRangeException>(() => new DateOfBirth(actualDate));
        }
    }
}
