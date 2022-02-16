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
            var userRepository = new Mock<IUserRepository>();
            userRepository.Setup(_ => _.CreateAsync(It.IsAny<User>())).Verifiable();

            UserCreateRequest request = new()
            {
                City = "例：広島市",
                DateOfBirth = DateTime.Now.Date.AddYears(-25),
                DisplayName = "Red",
                IDassId = Guid.NewGuid().ToString(),
                MailAddress = "test@example.com",
                FirstName = "例：太郎",
                LastName = "例：中古",
                PhoneNumber = "09099999999",
                State = "例；広島県",
                Street1 = "例：国泰寺町",
                Street2 = "例：1丁目6-34 市役所",
                Zip = "例：730-8586"
            };
            UserCreateUseCase sut = new(userRepository.Object);
            await sut.ExecuteAsync(request);

            userRepository.Verify(_ => _.CreateAsync(It.IsAny<User>()), Times.Once());
        }

        [Fact(DisplayName = "ユーザーがすでに登録されており、DuplicatedUserExceptionになること")]
        public async Task ExecuteAsync02()
        {
            var userRepository = new Mock<IUserRepository>();

            UserCreateRequest request = new()
            {
                City = "例：広島市",
                DateOfBirth = DateTime.Now.Date.AddYears(-25),
                DisplayName = "Red",
                IDassId = Guid.NewGuid().ToString(),
                MailAddress = "test@example.com",
                FirstName = "例：太郎",
                LastName = "例：中古",
                PhoneNumber = "09099999999",
                State = "例；広島県",
                Street1 = "例：国泰寺町",
                Street2 = "例：1丁目6-34 市役所",
                Zip = "例：730-8586"
            };

            User expectedUser = new
            (
                UserId.Create(),
                new IDassId(request.IDassId),
                new Name(request.FirstName, request.LastName),
                new DisplayName(request.DisplayName),
                new DateOfBirth(request.DateOfBirth),
                new PhoneNumber(request.PhoneNumber),
                new MailAddress(request.MailAddress),
                new Address(
                    new Zip(request.Zip),
                    new State(request.State),
                    new City(request.City),
                    new Street1(request.Street1),
                    new Street2(request.Street2))
            );
            userRepository.Setup(_ => _.FindAsync(It.IsAny<IDassId>())).ReturnsAsync(expectedUser);

            UserCreateUseCase sut = new(userRepository.Object);

            _ = await Assert.ThrowsAsync<DuplicatedUserException>(() => sut.ExecuteAsync(request));

        }
    }
}
