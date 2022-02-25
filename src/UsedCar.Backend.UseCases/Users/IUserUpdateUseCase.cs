using UsedCar.Backend.UseCases.Users.Models;

namespace UsedCar.Backend.UseCases.Users
{
    public interface IUserUpdateUseCase
    {
        Task ExecuteAsync(UserUpdateRequest userUpdateRequest);
    }
}
