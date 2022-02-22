using Microsoft.EntityFrameworkCore;
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

            await _dBContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Domains.Users.AggregateRoots.IdaasInfo idaasInfo)
        {
            var idaasInfoEntity = await _dBContext.IdaasInfos
                .FirstOrDefaultAsync(i => i.IdpUserId == idaasInfo.IdaasId.Value) ?? throw new ArgumentNullException(nameof(idaasInfo), "idaasInfo can not be found");

            _dBContext.Remove(idaasInfoEntity);

            await _dBContext.SaveChangesAsync();
        }

        public async Task<Domains.Users.AggregateRoots.IdaasInfo?> FindAsync(IdaasId idaasId) => await _dBContext
            .IdaasInfos
            .Where(i => i.IdpUserId == idaasId.Value)
            .Select(i => new Domains.Users.AggregateRoots.IdaasInfo(
                new IdaasId(i.IdpUserId),
                new DisplayName(i.DisplayName),
                new MailAddress(i.MailAddress)))
            .FirstOrDefaultAsync();

        public Task UpdateAsync(Domains.Users.AggregateRoots.IdaasInfo idaasInfo)
        {
            throw new NotImplementedException();
        }
    }
}
