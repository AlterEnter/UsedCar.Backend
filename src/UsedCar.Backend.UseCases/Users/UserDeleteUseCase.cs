using Microsoft.Extensions.Logging;
using System.Transactions;
using UsedCar.Backend.Domains.Users;
using UsedCar.Backend.Domains.Users.ValueObjects;
using UsedCar.Backend.LoggerExtensions;
using UsedCar.Backend.UseCases.Exceptions;

namespace UsedCar.Backend.UseCases.Users
{
    public class UserDeleteUseCase : IUserDeleteUseCase
    {
        private readonly IIdaasRepository _idaasRepository;
        private readonly IUserRepository _userRepository;
        private readonly IIdaasManagement _idaasManagement; 
        private readonly ILogger<UserDeleteUseCase> _logger;

        public UserDeleteUseCase(IIdaasRepository idaasRepository, IUserRepository userRepository, ILogger<UserDeleteUseCase> logger, IIdaasManagement idaasManagement)
        {
            _idaasRepository = idaasRepository;
            _userRepository = userRepository;
            _logger = logger;
            _idaasManagement = idaasManagement;
        }

        public async Task ExecuteAsync(string idaasId)
        {

            var idaasInfo = _idaasRepository.FindAsync(new IdaasId(idaasId)).Result;

            if (idaasInfo is null)
            {
                throw new UserForbiddenException(UserForbiddenException.ForbiddenVariation.NoIdaasInfo);
            }

            var user = _userRepository.FindAsync(idaasInfo.IdaasId).Result;

            if (user is null)
            {
                throw new UserForbiddenException(UserForbiddenException.ForbiddenVariation.NoUserInfo);
            }

            await _idaasManagement.UserDeleteAsync(idaasId);

            try
            {
                using var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
                await _userRepository.DeleteAsync(user.UserId);
                await _idaasRepository.DeleteAsync(idaasInfo);
                transaction.Complete();
            }
            catch (Exception e)
            {
                _logger.UserDeleteFailed(idaasInfo.IdaasId.Value, e);
                throw new DbException();
            }
        }
    }
}
