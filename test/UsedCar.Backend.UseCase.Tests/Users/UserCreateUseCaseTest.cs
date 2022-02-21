using Xunit;
using Moq;
using System;
using UsedCar.Backend.Domains.Users;
using UsedCar.Backend.Domains.Users.AggregateRoots;
using UsedCar.Backend.UseCases.Users;
using UsedCar.Backend.UseCases.Users.Models;
using System.Threading.Tasks;
using UsedCar.Backend.Domains.Users.ValueObjects;
using UsedCar.Backend.UseCases.Exceptions;

namespace UsedCar.Backend.UseCase.Users
{
    public class UserCreateUseCaseTest
    {
        [Fact(DisplayName = "ユーザーの登録が正常に完了すること")]
        public async Task ExecuteAsync01()
        {
            var idaasRepository = new Mock<IIdaasRepository>();
            idaasRepository.Setup(_ => _.CreateAsync(It.IsAny<IdaasInfo>())).Verifiable();

            UserCreateRequest userCreateRequest = new()
            {
                IdaasId = Guid.NewGuid().ToString(),
                DisplayName = "test",
                MailAddress = "test@sample.com"
            };

            UserCreateUseCase sut = new(idaasRepository.Object);

            await sut.ExecuteAsync(userCreateRequest);

            idaasRepository.Verify(_ => _.CreateAsync(It.IsAny<IdaasInfo>()), Times.Once());
        }

        [Fact(DisplayName = "ユーザーがすでに登録されており、DuplicatedUserExceptionになること")]
        public async Task ExecuteAsync02()
        {
            var idaasRepository = new Mock<IIdaasRepository>();

            UserCreateRequest userCreateRequest = new()
            {
                IdaasId = Guid.NewGuid().ToString(),
                DisplayName = "test",
                MailAddress = "test@sample.com"
            };

            IdaasInfo idaasInfo = new(
                new IdaasId(Guid.NewGuid().ToString()),
                new DisplayName("test"),
                new MailAddress("test@sample.com")
            );

            idaasRepository.Setup(_ => _.FindAsync(It.IsAny<IdaasId>())).ReturnsAsync(idaasInfo);

            UserCreateUseCase sut = new(idaasRepository.Object);

            _ = await Assert.ThrowsAsync<DuplicatedUserException>(() => sut.ExecuteAsync(userCreateRequest));

        }
    }
}
