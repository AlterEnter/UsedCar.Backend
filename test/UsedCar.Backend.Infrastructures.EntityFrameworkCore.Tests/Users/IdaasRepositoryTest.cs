using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using UsedCar.Backend.Domains.Users.ValueObjects;
using Xunit;

namespace UsedCar.Backend.Infrastructures.EntityFrameworkCore.Users
{
    public class IdaasRepositoryTest
    {
        [Fact(DisplayName = "ê≥èÌÇ…DBÇ…ìoò^Ç≥ÇÍÇÈÇ±Ç∆")]
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
        }
    }
}