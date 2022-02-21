using UsedCar.Backend.Domains.Users;
using UsedCar.Backend.Domains.Users.ValueObjects;
using UsedCar.Backend.Infrastructures.EntityFrameworkCore.Models;

namespace UsedCar.Backend.Infrastructures.EntityFrameworkCore.Users
{
    public class IdaasRepository : IIdaasRepository
    {
        private readonly UsedCarDBContext _dBContext;

        public IdaasRepository(UsedCarDBContext dBContext)
        {
            _dBContext = dBContext;
        }

        public async Task CreateAsync(Domains.Users.AggregateRoots.IdaasInfo idaasInfo)
        {
            var idaasInfoEfCore = idaasInfo.ToTable();

            await _dBContext.AddAsync(idaasInfoEfCore);
        }

        public Task DeleteAsync(Domains.Users.AggregateRoots.IdaasInfo idaasInfo)
        {
            throw new NotImplementedException();
        }

        public Task<Domains.Users.AggregateRoots.IdaasInfo?> FindAsync(IdaasId idaasId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Domains.Users.AggregateRoots.IdaasInfo idaasInfo)
        {
            throw new NotImplementedException();
        }
    }
}
