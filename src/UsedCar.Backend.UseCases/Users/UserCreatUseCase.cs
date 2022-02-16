using UsedCar.Backend.Domains.Users;
using UsedCar.Backend.Domains.Users.AggregateRoots;
using UsedCar.Backend.Domains.Users.ValueObjects;
using UsedCar.Backend.UseCases.Users.Models;

namespace UsedCar.Backend.UseCases.Users
{
    public class UserCreateUseCase : IUserCreateUseCase
    {
        private readonly IUserRepository _userRepository;

        public UserCreateUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task ExecuteAsync(UserCreateRequest userCreateRequest)
        {
            User user = new
            (
                UserId.Create(), 
                new IDassId(userCreateRequest.IDassId),
                new Name(userCreateRequest.FirstName, userCreateRequest.LastName),
                new DisplayName(userCreateRequest.DisplayName),
                new DateOfBirth(userCreateRequest.DateOfBirth),
                new PhoneNumber(userCreateRequest.PhoneNumber),
                new MailAddress(userCreateRequest.MailAddress),
                new Address(
                        new Zip(userCreateRequest.Zip),
                        new State(userCreateRequest.State),
                        new City(userCreateRequest.City),
                        new Street1(userCreateRequest.Street1),
                        new Street2(userCreateRequest.Street2))
                );
            await _userRepository.CreateAsync(user);
        }
    }
}
