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
        [Fact(DisplayName = "�����DB�ɓo�^����邱��")]
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

        [Fact(DisplayName = "����Ɏ擾�ł��邱��")]
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

        [Fact(DisplayName = "�o�^����Ă��炸�Anull�擾�ł��邱��")]
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

        [Fact(DisplayName = "�o�^����Ă��炸�AArgumentNullException�ɂȂ邱��")]
        public async Task DeleteAsync01()
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

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => sut.DeleteAsync(expectedIdaasInfo));
        }

        [Fact(DisplayName = "����ɍ폜����邱��")]
        public async Task DeleteAsync02()
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
            Assert.NotNull(await dbContext.IdaasInfos.FirstOrDefaultAsync());

            await sut.DeleteAsync(expectedIdaasInfo);

            // Assert
            Assert.Null(await dbContext.IdaasInfos.FirstOrDefaultAsync());
        }

        [Fact(DisplayName = "�o�^����Ă��炸�AArgumentNullException�ɂȂ邱��")]
        public async Task UpdateAsync01()
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

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => sut.UpdateAsync(expectedIdaasInfo));
        }

        [Fact(DisplayName = "����ɍX�V����邱��")]
        public async Task UpdateAsync02()
        {
            // Arrange
            Domains.Users.AggregateRoots.IdaasInfo oldIdaasInfo = new(
                new IdaasId("idaasId test"),
                new DisplayName("displayName test"),
                new MailAddress("test@sample.com")
            );
            await using var dbContext = DbContextFactory.Create();

            // Act
            var sut = new IdaasRepository(dbContext);
            await sut.CreateAsync(oldIdaasInfo);
            Assert.NotNull(await dbContext.IdaasInfos.FirstOrDefaultAsync());

            Domains.Users.AggregateRoots.IdaasInfo expectedIdaasInfo = new(
                new IdaasId("idaasId test"),
                new DisplayName("displayName expected"),
                new MailAddress("expected@sample.com")
            );

            await sut.UpdateAsync(expectedIdaasInfo);

            var actual = await dbContext.IdaasInfos.FirstOrDefaultAsync();

            // Assert
            Assert.Equal(expectedIdaasInfo.IdaasId.Value, actual?.IdpUserId);
            Assert.Equal(expectedIdaasInfo.DisplayName.Value, actual?.DisplayName);
            Assert.Equal(expectedIdaasInfo.MailAddress.Value, actual?.MailAddress);
        }

    }
}