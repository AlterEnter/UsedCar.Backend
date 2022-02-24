using Moq;
using System;
using System.Threading.Tasks;
using UsedCar.Backend.Domains.Users;
using UsedCar.Backend.Domains.Users.AggregateRoots;
using UsedCar.Backend.Domains.Users.ValueObjects;
using UsedCar.Backend.UseCases.Exceptions;
using UsedCar.Backend.UseCases.Users;
using Xunit;

namespace UsedCar.Backend.UseCase.Users
{
    public class UserReadUseCaseTest
    {
        [Fact(DisplayName = "IdaasInfoのみ正常に取得すること")]
        public async Task ExecuteAsync01()
        {
            var idaasRepository = new Mock<IIdaasRepository>();

            IdaasInfo idaasInfo = new(
                new IdaasId(Guid.NewGuid().ToString()),
                new DisplayName("test"),
                new MailAddress("test@sample.com")
                );

            idaasRepository.Setup(_ => _.FindAsync(idaasInfo.IdaasId)).ReturnsAsync(idaasInfo);

            var userRepository = new Mock<IUserRepository>();

            userRepository.Setup(_ => _.FindAsync(idaasInfo.IdaasId)).ReturnsAsync(null as User);


            UserReadUseCase sut = new(idaasRepository.Object, userRepository.Object);

            var actualUserReadResponse = await sut.ExecuteAsync(idaasInfo.IdaasId.Value);

            Assert.Equal(idaasInfo.DisplayName.Value, actualUserReadResponse?.DisplayName);
            Assert.Equal(idaasInfo.MailAddress.Value, actualUserReadResponse?.MailAddress);
            Assert.Null(actualUserReadResponse?.UserId);
            Assert.Null(actualUserReadResponse?.FirstName);
            Assert.Null(actualUserReadResponse?.LastName);
            Assert.Null(actualUserReadResponse?.DateOfBirth);
            Assert.Null(actualUserReadResponse?.PhoneNumber);
            Assert.Null(actualUserReadResponse?.Zip);
            Assert.Null(actualUserReadResponse?.State);
            Assert.Null(actualUserReadResponse?.City);
            Assert.Null(actualUserReadResponse?.Street1);
            Assert.Null(actualUserReadResponse?.Street2);
        }

        [Fact(DisplayName = "IdaasInfoもUserも正常に取得すること")]
        public async Task ExecuteAsync02()
        {
            var idaasRepository = new Mock<IIdaasRepository>();

            IdaasInfo idaasInfo = new(
                new IdaasId(Guid.NewGuid().ToString()),
                new DisplayName("test"),
                new MailAddress("test@sample.com")
            );

            idaasRepository.Setup(_ => _.FindAsync(idaasInfo.IdaasId)).ReturnsAsync(idaasInfo);

            var userRepository = new Mock<IUserRepository>();

            User user = new(
                new UserId(Guid.NewGuid()),
                idaasInfo.IdaasId,
                new Name("first", "last"),
                new DateOfBirth(DateTime.UtcNow.AddYears(-20)),
                new PhoneNumber("000-0000-0000"),
                new Address(
                    new Zip("000-0000"),
                    new State("広島県"),
                    new City("広島市"),
                    new Street1("安芸郡"),
                    new Street2("新地1-1")
                    )
                );

            userRepository.Setup(_ => _.FindAsync(idaasInfo.IdaasId)).ReturnsAsync(user);


            UserReadUseCase sut = new(idaasRepository.Object, userRepository.Object);

            var actualUserReadResponse = await sut.ExecuteAsync(idaasInfo.IdaasId.Value);

            Assert.Equal(idaasInfo.DisplayName.Value, actualUserReadResponse?.DisplayName);
            Assert.Equal(idaasInfo.MailAddress.Value, actualUserReadResponse?.MailAddress);
            Assert.Equal(user.UserId.Value.ToString(), actualUserReadResponse?.UserId);
            Assert.Equal(user.Name.FirstName, actualUserReadResponse?.FirstName);
            Assert.Equal(user.Name.LastName, actualUserReadResponse?.LastName);
            Assert.Equal(user.DateOfBirth.Value, actualUserReadResponse?.DateOfBirth);
            Assert.Equal(user.PhoneNumber.Value, actualUserReadResponse?.PhoneNumber);
            Assert.Equal(user.Address.Zip.Value, actualUserReadResponse?.Zip);
            Assert.Equal(user.Address.State.Value, actualUserReadResponse?.State);
            Assert.Equal(user.Address.City.Value, actualUserReadResponse?.City);
            Assert.Equal(user.Address.Street1.Value, actualUserReadResponse?.Street1);
            Assert.Equal(user.Address.Street2.Value, actualUserReadResponse?.Street2);
        }

        [Fact(DisplayName = "IdaasInfo存在しない、IdaasNotFoundExceptionになること")]
        public async Task ExecuteAsync03()
        {
            var idaasRepository = new Mock<IIdaasRepository>();

            var userRepository = new Mock<IUserRepository>();

            UserReadUseCase sut = new(idaasRepository.Object, userRepository.Object);

            var idaasId = Guid.NewGuid().ToString();

            await Assert.ThrowsAsync<IdaasNotFoundException>(() => sut.ExecuteAsync(idaasId));
        }
    }
}
