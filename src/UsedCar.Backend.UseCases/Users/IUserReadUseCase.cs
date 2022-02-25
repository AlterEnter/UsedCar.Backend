using UsedCar.Backend.UseCases.Users.Models;

namespace UsedCar.Backend.UseCases.Users
{
    public interface IUserReadUseCase
    {
        Task<UserReadResponse?> ExecuteAsync(string idaasId);
    }
}
