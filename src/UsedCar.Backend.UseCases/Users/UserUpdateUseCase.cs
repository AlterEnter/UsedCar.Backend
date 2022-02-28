using System.Transactions;
using Microsoft.Extensions.Logging;
using UsedCar.Backend.Domains.Users;
using UsedCar.Backend.Domains.Users.AggregateRoots;
using UsedCar.Backend.Domains.Users.ValueObjects;
using UsedCar.Backend.LoggerExtensions;
using UsedCar.Backend.UseCases.Exceptions;
using UsedCar.Backend.UseCases.Users.Models;

namespace UsedCar.Backend.UseCases.Users
{
    public class UserUpdateUseCase : IUserUpdateUseCase
    {
        private readonly IIdaasRepository _idaasRepository;

        private readonly IUserRepository _userRepository;

        private readonly IIdaasManagement _idaasManagement;

        private readonly ILogger<UserUpdateUseCase> _logger;

        public UserUpdateUseCase(
            IIdaasRepository idaasRepository, 
            IUserRepository userRepository, 
            IIdaasManagement idaasManagement, 
            ILogger<UserUpdateUseCase> logger)
        {
            _idaasRepository = idaasRepository;
            _userRepository = userRepository;
            _idaasManagement = idaasManagement;
            _logger = logger;
        }

        public async Task ExecuteAsync(UserUpdateRequest userUpdateRequest)
        {
            var idaasInfo = await _idaasRepository.FindAsync(new IdaasId(userUpdateRequest.IdaasId));

            if (idaasInfo == null)
            {
                throw new IdaasNotFoundException();
            }

            IdaasInfo idaasInfoUpdate = new(
                new IdaasId(userUpdateRequest.IdaasId),
                new DisplayName(userUpdateRequest.DisplayName),
                new MailAddress(userUpdateRequest.MailAddress)
            );

            if (userUpdateRequest.MailAddress != idaasInfo.MailAddress.Value || userUpdateRequest.DisplayName != idaasInfo.DisplayName.Value)
            {
                try
                {
                    await _idaasManagement.UserUpdateAsync(idaasInfoUpdate);
                }
                catch (Exception e)
                {
                    _logger.IdaasUpdateFailed(e.Message, e);
                    throw new IdaasErrorException(e.Message, e);
                }
            }

            using var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            await _idaasRepository.UpdateAsync(idaasInfoUpdate);

            var user = await _userRepository.FindAsync(new IdaasId(userUpdateRequest.IdaasId));

            if (user == null)
            {
                var userCreate = new User(
                    UserId.Create(),
                    new IdaasId(userUpdateRequest.IdaasId),
                    new Name(userUpdateRequest.FirstName, userUpdateRequest.LastName),
                    new DateOfBirth(userUpdateRequest.DateOfBirth),
                    new PhoneNumber(userUpdateRequest.PhoneNumber),
                    new Address(
                        new Zip(userUpdateRequest.Zip),
                        new State(userUpdateRequest.State),
                        new City(userUpdateRequest.City),
                        new Street1(userUpdateRequest.Street1),
                        new Street2(userUpdateRequest.Street2)
                    ));
                await _userRepository.CreateAsync(userCreate);
            }
            else
            {
                var userUpdate = new User(
                    user.UserId,
                    new IdaasId(userUpdateRequest.IdaasId),
                    new Name(userUpdateRequest.FirstName, userUpdateRequest.LastName),
                    new DateOfBirth(userUpdateRequest.DateOfBirth),
                    new PhoneNumber(userUpdateRequest.PhoneNumber),
                    new Address(
                        new Zip(userUpdateRequest.Zip),
                        new State(userUpdateRequest.State),
                        new City(userUpdateRequest.City),
                        new Street1(userUpdateRequest.Street1),
                        new Street2(userUpdateRequest.Street2)
                    ));
                await _userRepository.UpdateAsync(userUpdate);
            }

            transaction.Complete();
        }
    }
}
