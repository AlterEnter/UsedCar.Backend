using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using UsedCar.Backend.Domains.Users.AggregateRoots;
using UsedCar.Backend.Domains.Users.ValueObjects;
using Xunit;

namespace UsedCar.Backend.Infrastructures.EntityFrameworkCore.Users
{
    public class UserRepositoryTest
    {
        [Fact(DisplayName = "正常にユーザー詳細情報を登録できること")]
        public async Task CreateAsync01()
        {
            //Arrange

            var existIdaasInfo = new IdaasInfo(
                new IdaasId("idaasId test"),
                new DisplayName("displayName test"),
                new MailAddress("test@sample.com")
                );
            var expectedUser = new User(
                UserId.Create(), 
                existIdaasInfo.IdaasId,
                new Name("Arnold", "Schwarzenegger"),
                new DateOfBirth(DateTime.Now.Date.AddYears(-20)),
                new PhoneNumber("09099999999"),
                new Address(new Zip("518-8888"), new State("California"), new City("Los Angels"), new Street1("Best Street"), new Street2("Best House")));

            await using var dbContext = DbContextFactory.Create();

            //Act
            await new IdaasRepository(dbContext).CreateAsync(existIdaasInfo);
            await new UserRepository(dbContext).CreateAsync(expectedUser);
 
            var actual = await dbContext.Users.FirstOrDefaultAsync();

            //Assert
            Assert.Equal(expectedUser.UserId.Value, actual?.UserId);
            Assert.Equal(expectedUser.IdaasId.Value, actual?.IdpUserId);
            Assert.Equal(expectedUser.Name.FirstName, actual?.FirstName);
            Assert.Equal(expectedUser.Name.LastName, actual?.LastName);
            Assert.Equal(expectedUser.DateOfBirth.Value, actual?.DateOfBirth);
            Assert.Equal(expectedUser.Address.Zip.Value, actual?.Zip);
            Assert.Equal(expectedUser.Address.State.Value, actual?.State);
            Assert.Equal(expectedUser.Address.City.Value, actual?.City);
            Assert.Equal(expectedUser.Address.Street1.Value, actual?.Street1);
            Assert.Equal(expectedUser.Address.Street2.Value, actual?.Street2);
        }

        [Fact(DisplayName = "正常にユーザーを取得できること")]
        public async Task FindAsync01()
        {
            //Arrange
            var existIdaasInfo = new IdaasInfo(
                new IdaasId("idaasId test"),
                new DisplayName("displayName test"),
                new MailAddress("test@sample.com")
            );
            var expectedUser = new User(
                UserId.Create(),
                existIdaasInfo.IdaasId,
                new Name("Arnold", "Schwarzenegger"),
                new DateOfBirth(DateTime.Now.Date.AddYears(-20)),
                new PhoneNumber("09099999999"),
                new Address(new Zip("518-8888"), new State("California"), new City("Los Angels"), new Street1("Best Street"), new Street2("Best House")));

            await using var dbContext = DbContextFactory.Create();

            //Act
            await new IdaasRepository(dbContext).CreateAsync(existIdaasInfo);
            var sut =  new UserRepository(dbContext);
            await sut.CreateAsync(expectedUser);
            var actual = sut.FindAsync(existIdaasInfo.IdaasId).Result;

            //Assert
            Assert.Equal(expectedUser.UserId.Value, actual?.UserId.Value);
            Assert.Equal(expectedUser.IdaasId.Value, actual?.IdaasId.Value);
            Assert.Equal(expectedUser.Name.FirstName, actual?.Name.FirstName);
            Assert.Equal(expectedUser.Name.LastName, actual?.Name.LastName);
            Assert.Equal(expectedUser.DateOfBirth.Value, actual?.DateOfBirth.Value);
            Assert.Equal(expectedUser.Address.Zip.Value, actual?.Address.Zip.Value);
            Assert.Equal(expectedUser.Address.State.Value, actual?.Address.State.Value);
            Assert.Equal(expectedUser.Address.City.Value, actual?.Address.City.Value);
            Assert.Equal(expectedUser.Address.Street1.Value, actual?.Address.Street1.Value);
            Assert.Equal(expectedUser.Address.Street2.Value, actual?.Address.Street2.Value);

        }

        [Fact(DisplayName = "ユーザーが登録されておらず、nullがかえすこと")]
        public async Task FindAsync02()
        {
            //Arrange
            var existIdaasInfo = new IdaasInfo(
                new IdaasId("idaasId test"),
                new DisplayName("displayName test"),
                new MailAddress("test@sample.com")
            );
            var expectedUser = new User(
                UserId.Create(),
                existIdaasInfo.IdaasId,
                new Name("Arnold", "Schwarzenegger"),
                new DateOfBirth(DateTime.Now.Date.AddYears(-20)),
                new PhoneNumber("09099999999"),
                new Address(new Zip("518-8888"), new State("California"), new City("Los Angels"), new Street1("Best Street"), new Street2("Best House")));

            await using var dbContext = DbContextFactory.Create();

            //Act
            await new IdaasRepository(dbContext).CreateAsync(existIdaasInfo);
            var sut = new UserRepository(dbContext);
            await sut.CreateAsync(expectedUser);
            var actual = sut.FindAsync(new IdaasId(Guid.NewGuid().ToString()));

            //Assert
            Assert.Null(actual.Result);
        }

        [Fact(DisplayName = "ユーザーが登録されておらず、ArgumentNullExceptionになること")]
        public async Task UpdateAsync01()
        {
            //Arrange
            var existIdaasInfo = new IdaasInfo(
                new IdaasId("idaasId test"),
                new DisplayName("displayName test"),
                new MailAddress("test@sample.com")
            );
            var expectedUser = new User(
                UserId.Create(),
                existIdaasInfo.IdaasId,
                new Name("Arnold", "Schwarzenegger"),
                new DateOfBirth(DateTime.Now.Date.AddYears(-20)),
                new PhoneNumber("09099999999"),
                new Address(new Zip("518-8888"), new State("California"), new City("Los Angels"), new Street1("Best Street"), new Street2("Best House")));

            var updateUser = new User(
                UserId.Create(),
                expectedUser.IdaasId,
                new Name("Harry", "Potter"),
                new DateOfBirth(DateTime.Now.Date.AddYears(-30)),
                new PhoneNumber("77777777777"),
                new Address(new Zip("519-9999"), new State("Scotland"), new City("Scottish Highlands"), new Street1("Hogwarts"), new Street2("Gryffindor")));

            await using var dbContext = DbContextFactory.Create();

            //Act・Assert
            await new IdaasRepository(dbContext).CreateAsync(existIdaasInfo);
            var sut = new UserRepository(dbContext);
            await sut.CreateAsync(expectedUser);
            await Assert.ThrowsAsync<ArgumentNullException>(() => sut.UpdateAsync(updateUser));

        }

        [Fact(DisplayName = "正常にユーザー詳細情報をアップデートできること")]
        public async Task UpdateAsync02()
        {
            //Arrange
            var existIdaasInfo = new IdaasInfo(
                new IdaasId("idaasId test"),
                new DisplayName("displayName test"),
                new MailAddress("test@sample.com")
            );
            var expectedUser = new User(
                UserId.Create(),
                existIdaasInfo.IdaasId,
                new Name("Arnold", "Schwarzenegger"),
                new DateOfBirth(DateTime.Now.Date.AddYears(-20)),
                new PhoneNumber("09099999999"),
                new Address(new Zip("518-8888"), new State("California"), new City("Los Angels"), new Street1("Best Street"), new Street2("Best House")));

            var updateUser = new User(
                expectedUser.UserId,
                expectedUser.IdaasId,
                new Name("Harry", "Potter"),
                new DateOfBirth(DateTime.Now.Date.AddYears(-30)),
                new PhoneNumber("77777777777"),
                new Address(new Zip("519-9999"), new State("Scotland"), new City("Scottish Highlands"), new Street1("Hogwarts"), new Street2("Gryffindor")));

            await using var dbContext = DbContextFactory.Create();

            //Act
            await new IdaasRepository(dbContext).CreateAsync(existIdaasInfo);
            var sut = new UserRepository(dbContext);
            await sut.CreateAsync(expectedUser);
            await sut.UpdateAsync(updateUser);
            var actual = dbContext.Users.FirstOrDefaultAsync().Result;

            //Assert
            Assert.Equal(updateUser.UserId.Value, actual?.UserId);
            Assert.Equal(updateUser.IdaasId.Value, actual?.IdpUserId);
            Assert.Equal(updateUser.Name.FirstName, actual?.FirstName);
            Assert.Equal(updateUser.Name.LastName, actual?.LastName);
            Assert.Equal(updateUser.DateOfBirth.Value, actual?.DateOfBirth);
            Assert.Equal(updateUser.Address.Zip.Value, actual?.Zip);
            Assert.Equal(updateUser.Address.State.Value, actual?.State);
            Assert.Equal(updateUser.Address.City.Value, actual?.City);
            Assert.Equal(updateUser.Address.Street1.Value, actual?.Street1);
            Assert.Equal(updateUser.Address.Street2.Value, actual?.Street2);


        }


        [Fact(DisplayName = "ユーザーが登録されておらず、ArgumentNullExceptionになること")]
        public async Task DeleteAsync01()
        {
            //Arrange
            var existIdaasInfo = new IdaasInfo(
                new IdaasId("idaasId test"),
                new DisplayName("displayName test"),
                new MailAddress("test@sample.com")
            );
            var expectedUser = new User(
                UserId.Create(),
                existIdaasInfo.IdaasId,
                new Name("Arnold", "Schwarzenegger"),
                new DateOfBirth(DateTime.Now.Date.AddYears(-20)),
                new PhoneNumber("09099999999"),
                new Address(new Zip("518-8888"), new State("California"), new City("Los Angels"), new Street1("Best Street"), new Street2("Best House")));

            await using var dbContext = DbContextFactory.Create();

            //Act・Assert
            await new IdaasRepository(dbContext).CreateAsync(existIdaasInfo);
            var sut = new UserRepository(dbContext);
            await sut.CreateAsync(expectedUser);
            await Assert.ThrowsAsync<ArgumentNullException>(() => sut.DeleteAsync(new UserId(Guid.NewGuid())));

        }


        [Fact(DisplayName = "正常にユーザーを削除できること")]
        public async Task DeleteAsync02()
        {
            //Arrange
            var existIdaasInfo = new IdaasInfo(
                new IdaasId("idaasId test"),
                new DisplayName("displayName test"),
                new MailAddress("test@sample.com")
            );
            var expectedUser = new User(
                UserId.Create(),
                existIdaasInfo.IdaasId,
                new Name("Arnold", "Schwarzenegger"),
                new DateOfBirth(DateTime.Now.Date.AddYears(-20)),
                new PhoneNumber("09099999999"),
                new Address(new Zip("518-8888"), new State("California"), new City("Los Angels"), new Street1("Best Street"), new Street2("Best House")));

            await using var dbContext = DbContextFactory.Create();

            //Act
            await new IdaasRepository(dbContext).CreateAsync(existIdaasInfo);
            var sut = new UserRepository(dbContext);
            await sut.CreateAsync(expectedUser);
            await sut.DeleteAsync(expectedUser.UserId);

            //Assert
            Assert.Null(dbContext.Users.FirstOrDefaultAsync().Result);
        }
    }
}
