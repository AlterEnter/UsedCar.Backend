using Microsoft.EntityFrameworkCore;
using UsedCar.Backend.Domains.Users;
using UsedCar.Backend.Domains.Users.ValueObjects;
using UsedCar.Backend.Infrastructures.EntityFrameworkCore.Models;
using User = UsedCar.Backend.Domains.Users.AggregateRoots.User;

namespace UsedCar.Backend.Infrastructures.EntityFrameworkCore.Users
{
    public class UserRepository : IUserRepository
    {
        /// <summary>
        /// DBコンテキスト
        /// </summary>
        private readonly UsedCarDBContext _dbContext;
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="dbContext"></param>
        public UserRepository(UsedCarDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        /// <summary>
        /// ユーザー詳細情報新規作成
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task CreateAsync(User user)
        {
            var userEntity = user.ToTable();
            await _dbContext.AddAsync(userEntity);
            await _dbContext.SaveChangesAsync();
        }
        /// <summary>
        /// ユーザー検索
        /// </summary>
        /// <param name="idaasId"></param>
        /// <returns></returns>
        public async Task<User?> FindAsync(IdaasId idaasId)
        {
            var targetUser = await _dbContext.Users.Include(x => x.IdpUser)
                .FirstOrDefaultAsync(i => i.IdpUserId == idaasId.Value);
            return targetUser is not null
                ? new User(
                    new UserId(targetUser.UserId),
                    new IdaasId(targetUser.IdpUserId),
                    new Name(targetUser.FirstName, targetUser.LastName),
                    new DateOfBirth(targetUser.DateOfBirth),
                    new PhoneNumber(targetUser.PhoneNumber),
                    new Address(
                        new Zip(targetUser.Zip),
                        new State(targetUser.State),
                        new City(targetUser.City),
                        new Street1(targetUser.Street1),
                        new Street2(targetUser.Street2)))
                : null;
        }
        /// <summary>
        /// ユーザー詳細情報更新
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task UpdateAsync(User user)
        {

            var userEntity = _dbContext.Users.FirstOrDefault(id => id.UserId == user.UserId.Value) ??
                             throw new ArgumentNullException(nameof(user), "user can not be found");

            userEntity.FirstName = user.Name.FirstName;
            userEntity.LastName = user.Name.LastName;
            userEntity.DateOfBirth = user.DateOfBirth.Value;
            userEntity.PhoneNumber = user.PhoneNumber.Value;
            userEntity.Zip = user.Address.Zip.Value;
            userEntity.State = user.Address.State.Value;
            userEntity.City = user.Address.City.Value;
            userEntity.Street1 = user.Address.Street1.Value;
            userEntity.Street2 = user.Address.Street2.Value;
            await _dbContext.SaveChangesAsync();

        }
        /// <summary>
        /// 削除
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task DeleteAsync(UserId userId)
        {
            var userEntity = await _dbContext.Users.FirstOrDefaultAsync(i => i.UserId == userId.Value) ??
                             throw new ArgumentNullException(nameof(userId), "User does not exist"); 
            _dbContext.Remove(userEntity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
