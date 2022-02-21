using System;
using UsedCar.Backend.Domains.Users.ValueObjects;
using Xunit;

namespace UsedCar.Backend.Domains.Users.AggregateRoots
{
    public class IdaasInfoTest
    {
        [Fact(DisplayName = "正常に値を取得できるこ")]
        public void IdaasInfo01()
        {
            // Arrange
            var expectedIDassId = Guid.NewGuid().ToString();
            IdaasId idaasId = new(expectedIDassId);

            var expectedDisplayName = "test";
            DisplayName displayName = new(expectedDisplayName);

            var expectedMailAddress = "test@sample.com";
            MailAddress mailAddress = new(expectedMailAddress);

            // Act
            IdaasInfo idaasInfo = new(
                idaasId,
                displayName,
                mailAddress
            );

            // Assert
            Assert.Equal(expectedIDassId, idaasInfo.IdaasId.Value);
            Assert.Equal(expectedDisplayName, idaasInfo.DisplayName.Value);
            Assert.Equal(expectedMailAddress, idaasInfo.MailAddress.Value);
        }
    }
}
