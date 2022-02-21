using UsedCar.Backend.Domains.Users.AggregateRoots;
using UsedCar.Backend.Domains.Users.ValueObjects;

namespace UsedCar.Backend.Domains.Users
{
    public interface IIdaasRepository
    {
        Task CreateAsync(IdaasInfo idaasInfo);
        Task<IdaasInfo?> FindAsync(IdaasId idaasId);
        Task UpdateAsync(IdaasInfo idaasInfo);
        Task DeleteAsync(IdaasInfo idaasInfo);
    }
}
