using UsedCar.Backend.Domains.Users.AggregateRoots;

namespace UsedCar.Backend.UseCases.Users
{
    public interface IIdaasManagement
    {
        Task UserDeleteAsync(string idaasId);

        Task UserUpdateAsync(IdaasInfo idaasInfo);
    }
}
