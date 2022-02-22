using Microsoft.EntityFrameworkCore;
using UsedCar.Backend.Domains.Users;
using UsedCar.Backend.Domains.Users.ValueObjects;
using UsedCar.Backend.Infrastructures.EntityFrameworkCore.Models;

namespace UsedCar.Backend.Infrastructures.EntityFrameworkCore.Users
{
    public class IdaasRepository : IIdaasRepository
    {
        /// <summary>
        /// DBコンテキスト
        /// </summary>
        private readonly UsedCarDBContext _dBContext;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="dBContext"></param>
        public IdaasRepository(UsedCarDBContext dBContext)
        {
            _dBContext = dBContext;
        }

        /// <summary>
        /// 新規作成
        /// </summary>
        /// <param name="idaasInfo"></param>
        /// <returns></returns>
        public async Task CreateAsync(Domains.Users.AggregateRoots.IdaasInfo idaasInfo)
        {
            var idaasInfoEntity = idaasInfo.ToTable();

            await _dBContext.AddAsync(idaasInfoEntity);

            await _dBContext.SaveChangesAsync();
        }

        /// <summary>
        /// 削除
        /// </summary>
        /// <param name="idaasInfo"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task DeleteAsync(Domains.Users.AggregateRoots.IdaasInfo idaasInfo)
        {
            var idaasInfoEntity = await _dBContext.IdaasInfos
                .FirstOrDefaultAsync(i => i.IdpUserId == idaasInfo.IdaasId.Value) ?? throw new ArgumentNullException(nameof(idaasInfo), "idaasInfo can not be found");

            _dBContext.Remove(idaasInfoEntity);

            await _dBContext.SaveChangesAsync();
        }

        /// <summary>
        /// ユーザー検索
        /// </summary>
        /// <param name="idaasId"></param>
        /// <returns></returns>
        public async Task<Domains.Users.AggregateRoots.IdaasInfo?> FindAsync(IdaasId idaasId) => await _dBContext
            .IdaasInfos
            .Where(i => i.IdpUserId == idaasId.Value)
            .Select(i => new Domains.Users.AggregateRoots.IdaasInfo(
                new IdaasId(i.IdpUserId),
                new DisplayName(i.DisplayName),
                new MailAddress(i.MailAddress)))
            .FirstOrDefaultAsync();

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="idaasInfo"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task UpdateAsync(Domains.Users.AggregateRoots.IdaasInfo idaasInfo)
        {
            var idaasInfoEntity = await _dBContext.IdaasInfos
                .FirstOrDefaultAsync(i => i.IdpUserId == idaasInfo.IdaasId.Value) ?? throw new ArgumentNullException(nameof(idaasInfo), "idaasInfo can not be found");

            idaasInfoEntity.MailAddress = idaasInfo.MailAddress.Value;
            idaasInfoEntity.DisplayName = idaasInfo.DisplayName.Value;

            await _dBContext.SaveChangesAsync();
        }
    }
}
