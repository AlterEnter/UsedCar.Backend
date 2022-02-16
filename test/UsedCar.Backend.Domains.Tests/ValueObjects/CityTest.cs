using System;
using UsedCar.Backend.Domains.Users.ValueObjects;
using Xunit;

namespace UsedCar.Backend.Domains.ValueObjects
{
    public class CityTest
    {
        [Fact(DisplayName = "問題がなく値を取得することができること")]
        public void City01()
        { 
            // arrange 準備
            var expectedCity = "広島市";

            // act 実行
            City city = new(expectedCity);

            // assert 検証
            Assert.Equal(expectedCity, city.Value);
        }

        [Theory(DisplayName = "ArgumentNullExceptionになること")]
        [InlineData("")]
        [InlineData(null)]
        public void ArgumentNullExceptionになること(string city)
        {
            // assert 検証
            _ = Assert.Throws<ArgumentNullException>(() =>
            {
                _ = new City(city);
            });
        }
    }
}
