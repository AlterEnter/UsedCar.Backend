﻿using UsedCar.Backend.Domains.Users.AggregateRoots;
using UsedCar.Backend.Domains.Users.ValueObjects;

namespace UsedCar.Backend.Domains.Users
{
    public interface IUserRepository
    {
        Task CreateAsync(User user);
        Task<User> FindAsync(IDassId iDassID);
        Task UpdateAsync(User user);
        Task DeleteAsync(UserId userId);
    }
}
