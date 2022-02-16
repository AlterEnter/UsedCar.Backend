using UsedCar.Backend.Domains.Users.AggregateRoots;

namespace UsedCar.Backend.Domains.Users
{
    public interface IUserRepository
    {
        Task CreateAsync(User user);
    }
}
