using UsedCar.Backend.Domains.Users.AggregateRoots;
using UsedCar.Backend.UseCases.Users.Models;

namespace UsedCar.Backend.UseCases.Users
{
    public interface IUserCreateUseCase
    {
        Task ExecuteAsync(UserCreateRequest userCreateRequest);
    }
}
