using Moq;
using System;
using System.Threading.Tasks;
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
        [Fact(DisplayName = "IdaasInfoのみの場合、User正常にCreateすること")]
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


            UserUpdateUseCase sut = new(idaasRepository.Object, userRepository.Object);

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

            
            idaasRepository.Setup(_ => _.UpdateAsync(idaasInfo)).Verifiable();
            userRepository.Setup(_ => _.CreateAsync(user)).Verifiable();

            await sut.ExecuteAsync(userUpdateRequest);

            idaasRepository.Verify(_ => _.UpdateAsync(idaasInfo), Times.Once());
            userRepository.Verify(_ => _.CreateAsync(It.IsAny<User>()), Times.Once());
            userRepository.Verify(_ => _.UpdateAsync(It.IsAny<User>()), Times.Never());

        }

        [Fact(DisplayName = "IdaasInfoもUserも正常に更新すること")]
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

            UserUpdateUseCase sut = new(idaasRepository.Object, userRepository.Object);

            var userUpdateRequest = new UserUpdateRequest
            {
                IdaasId = idaasInfo.IdaasId.Value,
                DisplayName = idaasInfo.DisplayName.Value,
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

            idaasRepository.Setup(_ => _.UpdateAsync(idaasInfo)).Verifiable();
            userRepository.Setup(_ => _.UpdateAsync(user)).Verifiable();

            idaasRepository.Verify(_ => _.UpdateAsync(idaasInfo), Times.Once());
            userRepository.Verify(_ => _.CreateAsync(It.IsAny<User>()), Times.Never());
            userRepository.Verify(_ => _.UpdateAsync(It.IsAny<User>()), Times.Once());
        }

        [Fact(DisplayName = "IdaasInfo存在しない、IdaasNotFoundExceptionになること")]
        public async Task ExecuteAsync03()
        {
            var idaasRepository = new Mock<IIdaasRepository>();

            var userRepository = new Mock<IUserRepository>();

            UserUpdateUseCase sut = new(idaasRepository.Object, userRepository.Object);

            var idaasId = Guid.NewGuid().ToString();

            var userUpdateRequest = new UserUpdateRequest
            {
                IdaasId = Guid.NewGuid().ToString()
            };

            await Assert.ThrowsAsync<IdaasNotFoundException>(() => sut.ExecuteAsync(userUpdateRequest));
        }
    }
}
