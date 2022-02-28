using Moq;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using UsedCar.Backend.Domains.Users;
using UsedCar.Backend.Domains.Users.AggregateRoots;
using UsedCar.Backend.Domains.Users.ValueObjects;
using UsedCar.Backend.UseCases.Exceptions;
using UsedCar.Backend.UseCases.Users;
using UsedCar.Backend.UseCases.Users.Models;
using Xunit;

namespace UsedCar.Backend.UseCase.Users
{
    public class UserUpdateUseCaseTest
    {
        [Fact(DisplayName = "IdaasInfoのみの場合、User正常にCreateすること（DisplayName変更あり）")]
        public async Task ExecuteAsync01()
        {
            var idaasRepository = new Mock<IIdaasRepository>();

            var idaasId = Guid.NewGuid().ToString();

            IdaasInfo idaasInfo = new(
                new IdaasId(idaasId),
                new DisplayName("test"),
                new MailAddress("test@sample.com")
                );

            idaasRepository.Setup(_ => _.FindAsync(idaasInfo.IdaasId)).ReturnsAsync(idaasInfo);

            var userRepository = new Mock<IUserRepository>();

            userRepository.Setup(_ => _.FindAsync(idaasInfo.IdaasId)).ReturnsAsync(null as User);

            var idaasManagement = new Mock<IIdaasManagement>();

            var logger = new Mock<ILogger<UserUpdateUseCase>>();

            UserUpdateUseCase sut = new(idaasRepository.Object, userRepository.Object, idaasManagement.Object, logger.Object);

            var userId = Guid.NewGuid();

            User user = new(
                new UserId(userId),
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

            var userUpdateRequest = new UserUpdateRequest
            {
                IdaasId = idaasInfo.IdaasId.Value,
                DisplayName = "update displayName",//idaasInfo.DisplayName.Value,
                MailAddress = idaasInfo.MailAddress.Value,
                FirstName = user.Name.FirstName,
                LastName = user.Name.LastName,
                DateOfBirth = user.DateOfBirth.Value,
                PhoneNumber = user.PhoneNumber.Value,
                Zip = user.Address.Zip.Value,
                State = user.Address.State.Value,
                City = user.Address.City.Value,
                Street1 = user.Address.Street1.Value,
                Street2 = user.Address.Street2.Value
            };

            await sut.ExecuteAsync(userUpdateRequest);

            IdaasInfo idaasInfoUpdate = new(
                new IdaasId(userUpdateRequest.IdaasId),
                new DisplayName(userUpdateRequest.DisplayName),
                new MailAddress(userUpdateRequest.MailAddress)
            );

            idaasManagement.Verify(_ => _.UserUpdateAsync(idaasInfoUpdate), Times.Once());
            idaasRepository.Verify(_ => _.UpdateAsync(idaasInfoUpdate), Times.Once());
            userRepository.Verify(_ => _.CreateAsync(It.IsAny<User>()), Times.Once());
            userRepository.Verify(_ => _.UpdateAsync(It.IsAny<User>()), Times.Never());

        }

        [Fact(DisplayName = "IdaasInfoのみの場合、User正常にCreateすること（mailAddress変更あり）")]
        public async Task ExecuteAsync02()
        {
            var idaasRepository = new Mock<IIdaasRepository>();

            var idaasId = Guid.NewGuid().ToString();

            IdaasInfo idaasInfo = new(
                new IdaasId(idaasId),
                new DisplayName("test"),
                new MailAddress("test@sample.com")
                );

            idaasRepository.Setup(_ => _.FindAsync(idaasInfo.IdaasId)).ReturnsAsync(idaasInfo);

            var userRepository = new Mock<IUserRepository>();

            userRepository.Setup(_ => _.FindAsync(idaasInfo.IdaasId)).ReturnsAsync(null as User);

            var idaasManagement = new Mock<IIdaasManagement>();

            var logger = new Mock<ILogger<UserUpdateUseCase>>();

            UserUpdateUseCase sut = new(idaasRepository.Object, userRepository.Object, idaasManagement.Object, logger.Object);

            var userId = Guid.NewGuid();

            User user = new(
                new UserId(userId),
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

            var userUpdateRequest = new UserUpdateRequest
            {
                IdaasId = idaasInfo.IdaasId.Value,
                DisplayName = idaasInfo.DisplayName.Value,
                MailAddress = "update@sample.com",
                FirstName = user.Name.FirstName,
                LastName = user.Name.LastName,
                DateOfBirth = user.DateOfBirth.Value,
                PhoneNumber = user.PhoneNumber.Value,
                Zip = user.Address.Zip.Value,
                State = user.Address.State.Value,
                City = user.Address.City.Value,
                Street1 = user.Address.Street1.Value,
                Street2 = user.Address.Street2.Value
            };

            await sut.ExecuteAsync(userUpdateRequest);

            IdaasInfo idaasInfoUpdate = new(
                new IdaasId(userUpdateRequest.IdaasId),
                new DisplayName(userUpdateRequest.DisplayName),
                new MailAddress(userUpdateRequest.MailAddress)
            );

            idaasManagement.Verify(_ => _.UserUpdateAsync(idaasInfoUpdate), Times.Once());
            idaasRepository.Verify(_ => _.UpdateAsync(idaasInfoUpdate), Times.Once());
            userRepository.Verify(_ => _.CreateAsync(It.IsAny<User>()), Times.Once());
            userRepository.Verify(_ => _.UpdateAsync(It.IsAny<User>()), Times.Never());

        }

        [Fact(DisplayName = "IdaasInfoもUserも正常に更新すること（DisplayName変更あり）")]
        public async Task ExecuteAsync03()
        {
            var idaasRepository = new Mock<IIdaasRepository>();

            var idaasId = Guid.NewGuid().ToString();

            IdaasInfo idaasInfo = new(
                new IdaasId(idaasId),
                new DisplayName("test"),
                new MailAddress("test@sample.com")
            );

            idaasRepository.Setup(_ => _.FindAsync(idaasInfo.IdaasId)).ReturnsAsync(idaasInfo);

            var userRepository = new Mock<IUserRepository>();

            var userId = Guid.NewGuid();

            User user = new(
                new UserId(userId),
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

            var idaasManagement = new Mock<IIdaasManagement>();

            var logger = new Mock<ILogger<UserUpdateUseCase>>();

            UserUpdateUseCase sut = new(idaasRepository.Object, userRepository.Object, idaasManagement.Object, logger.Object);

            var userUpdateRequest = new UserUpdateRequest
            {
                IdaasId = idaasInfo.IdaasId.Value,
                DisplayName = "update displayName",
                MailAddress = idaasInfo.MailAddress.Value,
                FirstName = user.Name.FirstName,
                LastName = user.Name.LastName,
                DateOfBirth = user.DateOfBirth.Value,
                PhoneNumber = user.PhoneNumber.Value,
                Zip = user.Address.Zip.Value,
                State = user.Address.State.Value,
                City = user.Address.City.Value,
                Street1 = user.Address.Street1.Value,
                Street2 = user.Address.Street2.Value
            };

            await sut.ExecuteAsync(userUpdateRequest);

            IdaasInfo idaasInfoUpdate = new(
                new IdaasId(userUpdateRequest.IdaasId),
                new DisplayName(userUpdateRequest.DisplayName),
                new MailAddress(userUpdateRequest.MailAddress)
            );

            idaasManagement.Verify(_ => _.UserUpdateAsync(idaasInfoUpdate), Times.Once());
            idaasRepository.Verify(_ => _.UpdateAsync(idaasInfoUpdate), Times.Once());
            userRepository.Verify(_ => _.CreateAsync(It.IsAny<User>()), Times.Never());
            userRepository.Verify(_ => _.UpdateAsync(It.IsAny<User>()), Times.Once());
        }

        [Fact(DisplayName = "IdaasInfoもUserも正常に更新すること（mailAddress変更あり）")]
        public async Task ExecuteAsync04()
        {
            var idaasRepository = new Mock<IIdaasRepository>();

            var idaasId = Guid.NewGuid().ToString();

            IdaasInfo idaasInfo = new(
                new IdaasId(idaasId),
                new DisplayName("test"),
                new MailAddress("test@sample.com")
            );

            idaasRepository.Setup(_ => _.FindAsync(idaasInfo.IdaasId)).ReturnsAsync(idaasInfo);

            var userRepository = new Mock<IUserRepository>();

            var userId = Guid.NewGuid();

            User user = new(
                new UserId(userId),
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

            var idaasManagement = new Mock<IIdaasManagement>();

            var logger = new Mock<ILogger<UserUpdateUseCase>>();

            UserUpdateUseCase sut = new(idaasRepository.Object, userRepository.Object, idaasManagement.Object, logger.Object);

            var userUpdateRequest = new UserUpdateRequest
            {
                IdaasId = idaasInfo.IdaasId.Value,
                DisplayName = idaasInfo.DisplayName.Value,
                MailAddress = "update@sample.com",
                FirstName = user.Name.FirstName,
                LastName = user.Name.LastName,
                DateOfBirth = user.DateOfBirth.Value,
                PhoneNumber = user.PhoneNumber.Value,
                Zip = user.Address.Zip.Value,
                State = user.Address.State.Value,
                City = user.Address.City.Value,
                Street1 = user.Address.Street1.Value,
                Street2 = user.Address.Street2.Value
            };

            await sut.ExecuteAsync(userUpdateRequest);

            IdaasInfo idaasInfoUpdate = new(
                new IdaasId(userUpdateRequest.IdaasId),
                new DisplayName(userUpdateRequest.DisplayName),
                new MailAddress(userUpdateRequest.MailAddress)
            );

            idaasManagement.Verify(_ => _.UserUpdateAsync(idaasInfoUpdate), Times.Once());
            idaasRepository.Verify(_ => _.UpdateAsync(idaasInfoUpdate), Times.Once());
            userRepository.Verify(_ => _.CreateAsync(It.IsAny<User>()), Times.Never());
            userRepository.Verify(_ => _.UpdateAsync(It.IsAny<User>()), Times.Once());
        }

        [Fact(DisplayName = "IdaasInfo存在しない、IdaasNotFoundExceptionになること")]
        public async Task ExecuteAsync06()
        {
            var idaasRepository = new Mock<IIdaasRepository>();

            var userRepository = new Mock<IUserRepository>();

            var idaasManagement = new Mock<IIdaasManagement>();

            var logger = new Mock<ILogger<UserUpdateUseCase>>();

            UserUpdateUseCase sut = new(idaasRepository.Object, userRepository.Object, idaasManagement.Object, logger.Object);

            var idaasId = Guid.NewGuid().ToString();

            var userUpdateRequest = new UserUpdateRequest
            {
                IdaasId = Guid.NewGuid().ToString()
            };

            await Assert.ThrowsAsync<IdaasNotFoundException>(() => sut.ExecuteAsync(userUpdateRequest));
        }
    }
}
