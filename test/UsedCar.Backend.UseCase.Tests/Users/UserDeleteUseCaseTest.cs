using System;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using Castle.Core.Logging;
using Microsoft.Extensions.Logging;
using Moq;
using UsedCar.Backend.Domains.Users;
using UsedCar.Backend.Domains.Users.AggregateRoots;
using UsedCar.Backend.Domains.Users.ValueObjects;
using UsedCar.Backend.UseCases.Exceptions;
using UsedCar.Backend.UseCases.Users;
using Xunit;

namespace UsedCar.Backend.UseCase.Users
{
    public class UserDeleteUseCaseTest
    {
        [Fact(DisplayName = "IdaasInfoが存在していなく、UserForbiddenExceptionになること")]
        public async Task UserDeleteAsync01()
        {
            //arrange
            var idaasRepository = new Mock<IIdaasRepository>();
            var userRepository = new Mock<IUserRepository>();
            var idaasManagement = new Mock<IIdaasManagement>();
            var logger = new Mock<ILogger<UserDeleteUseCase>>();

            var idaasId = Guid.NewGuid().ToString();

            //act
            var sut = new UserDeleteUseCase(idaasRepository.Object, userRepository.Object, logger.Object,
                idaasManagement.Object);

            //assert
            await Assert.ThrowsAsync<UserForbiddenException>(() => sut.ExecuteAsync(idaasId));

        }

        [Fact(DisplayName = "IdaasInfoが存在しているが、userが存在しない場合、UserForbiddenExceptionになること")]
        public async Task UserDeleteAsync02()
        {
            //arrange
            var idaasRepository = new Mock<IIdaasRepository>();
            var userRepository = new Mock<IUserRepository>();
            var idaasManagement = new Mock<IIdaasManagement>();
            var logger = new Mock<ILogger<UserDeleteUseCase>>();

            IdaasInfo idaasInfo = new(
                new IdaasId("idaasId test"),
                new DisplayName("displayName test"),
                new MailAddress("test@sample.com")
            );

            idaasRepository.Setup(_ => _.FindAsync(idaasInfo.IdaasId)).ReturnsAsync(idaasInfo);

            //act・assert
            var sut = new UserDeleteUseCase(idaasRepository.Object, userRepository.Object, logger.Object,
                idaasManagement.Object);
            await Assert.ThrowsAsync<UserForbiddenException>(() => sut.ExecuteAsync(idaasInfo.IdaasId.Value));

        }

        [Fact(DisplayName = "正常にユーザーを削除できること")]
        public async Task UserDeleteAsync03()
        {
            //arrange
            var idaasRepository = new Mock<IIdaasRepository>();
            var userRepository = new Mock<IUserRepository>();
            var idaasManagement = new Mock<IIdaasManagement>();
            var logger = new Mock<ILogger<UserDeleteUseCase>>();

            IdaasInfo idaasInfo = new(
                new IdaasId("idaasId test"),
                new DisplayName("displayName test"),
                new MailAddress("test@sample.com")
            );

            User expectedUser = new(
                UserId.Create(),
                idaasInfo.IdaasId,
                new Name("Arnold", "Schwarzenegger"),
                new DateOfBirth(DateTime.Now.Date.AddYears(-20)),
                new PhoneNumber("09099999999"),
                new Address(new Zip("518-8888"), new State("California"), new City("Los Angels"), new Street1("Best Street"), new Street2("Best House")));

            idaasRepository.Setup(_ => _.FindAsync(idaasInfo.IdaasId)).ReturnsAsync(idaasInfo);
            userRepository.Setup(_ => _.FindAsync(idaasInfo.IdaasId)).ReturnsAsync(expectedUser);

            //act・assert
            var sut = new UserDeleteUseCase(idaasRepository.Object, userRepository.Object, logger.Object,
                idaasManagement.Object);

            await sut.ExecuteAsync(idaasInfo.IdaasId.Value);
            
            idaasRepository.Verify(_ => _.DeleteAsync(idaasInfo), Times.Once());
            userRepository.Verify(_ => _.DeleteAsync(expectedUser.UserId), Times.Once());
        }

    }
}
