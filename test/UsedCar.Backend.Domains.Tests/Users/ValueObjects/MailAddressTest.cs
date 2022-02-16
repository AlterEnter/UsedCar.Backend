using System;
using Xunit;

namespace UsedCar.Backend.Domains.Users.ValueObjects
{
    public class MailAddressTest
    {
        [Fact(DisplayName = "問題がなく値を取得することができること")]
        public void MailAddress01()
        { 
            // arrange 準備
            var expectedMailAddress = "test@example.com";

            // act 実行
            MailAddress mailAddress = new(expectedMailAddress);

            // assert 検証
            Assert.Equal(expectedMailAddress, mailAddress.Value);
        }

        [Fact(DisplayName = "ArgumentOutOfRangeExceptionになること")]
        public void MailAddress02()
        { 
            // arrange 準備
            var mailAddress = "testtesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttest255@example.com";

            // assert 検証
            _ = Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                _ = new MailAddress(mailAddress);
            });
        }

        [Theory(DisplayName = "ArgumentNullExceptionになること")]
        [InlineData("")]
        [InlineData(null)]
        public void ArgumentNullExceptionになること(string mailAddress)
        {
            // assert 検証
            _ = Assert.Throws<ArgumentNullException>(() =>
            {
                _ = new MailAddress(mailAddress);
            });
        }

        [Theory(DisplayName = "ArgumentExceptionになること")]
        [InlineData("@example.com")]
        [InlineData("test.com")]
        [InlineData("test@@sample.com")]
        [InlineData("test@sample .com")]
        public void ArgumentExceptionになること(string mailAddress)
        {
            // assert 検証
            _ = Assert.Throws<ArgumentException>(() =>
            {
                _ = new MailAddress(mailAddress);
            });
        }
    }
}
