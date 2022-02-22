using System;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Threading.Tasks;
using UsedCar.Backend.Domains.Users.ValueObjects;
using Xunit;

namespace UsedCar.Backend.Infrastructures.EntityFrameworkCore.Users
{
    public class IdaasRepositoryTest
    {
        [Fact(DisplayName = "正常にDBに登録されること")]
        public async Task CreateAsync01()
        {
            // Arrange
            Domains.Users.AggregateRoots.IdaasInfo expectedIdaasInfo = new(
                new IdaasId("idaasId test"),
                new DisplayName("displayName test"),
                new MailAddress("test@sample.com")
                );

            await using var dbContext = DbContextFactory.Create();

            // Act
            await new IdaasRepository(dbContext).CreateAsync(expectedIdaasInfo);
            var actual = await dbContext.IdaasInfos.FirstOrDefaultAsync();

            // Assert
            Assert.Equal(expectedIdaasInfo.IdaasId.Value, actual?.IdpUserId);
            Assert.Equal(expectedIdaasInfo.DisplayName.Value, actual?.DisplayName);
            Assert.Equal(expectedIdaasInfo.MailAddress.Value, actual?.MailAddress);
        }

        [Fact(DisplayName = "正常に取得できること")]
        public async Task FindAsync01()
        {
            // Arrange
            Domains.Users.AggregateRoots.IdaasInfo expectedIdaasInfo = new(
                new IdaasId("idaasId test"),
                new DisplayName("displayName test"),
                new MailAddress("test@sample.com")
            );

            await using var dbContext = DbContextFactory.Create();

            // Act
            var sut = new IdaasRepository(dbContext);
            await sut.CreateAsync(expectedIdaasInfo);
            var actual = await sut.FindAsync(expectedIdaasInfo.IdaasId);

            // Assert
            Assert.Equal(expectedIdaasInfo.IdaasId, actual?.IdaasId);
            Assert.Equal(expectedIdaasInfo.DisplayName, actual?.DisplayName);
            Assert.Equal(expectedIdaasInfo.MailAddress, actual?.MailAddress);
        }

        [Fact(DisplayName = "登録されておらず、null取得できること")]
        public async Task FindAsync02()
        {
            // Arrange
            await using var dbContext = DbContextFactory.Create();

            // Act
            var sut = new IdaasRepository(dbContext);
            var actual = await sut.FindAsync(new IdaasId(Guid.NewGuid().ToString()));

            // Assert
            Assert.Null(actual);
        }
    }
}