using UsedCar.Backend.Domains.Users;
using UsedCar.Backend.Domains.Users.AggregateRoots;
using UsedCar.Backend.Domains.Users.ValueObjects;
using UsedCar.Backend.UseCases.Exceptions;
using UsedCar.Backend.UseCases.Users.Models;

namespace UsedCar.Backend.UseCases.Users
{
    public class UserCreateUseCase : IUserCreateUseCase
    {
        private readonly IIdaasRepository _idaasRepository;

        public UserCreateUseCase(IIdaasRepository idaasRepository)
        {
            _idaasRepository = idaasRepository;
        }

        public async Task ExecuteAsync(UserCreateRequest userCreateRequest)
        {
            IdaasInfo? duplicatedIdaas = await _idaasRepository.FindAsync(new IdaasId(userCreateRequest.IdaasId));

            if (duplicatedIdaas is not null)
            {
                throw new DuplicatedUserException();
            }

            IdaasInfo idaasInfo = new(
                new IdaasId(userCreateRequest.IdaasId),
                new DisplayName(userCreateRequest.DisplayName),
                new MailAddress(userCreateRequest.MailAddress)
                );

            await _idaasRepository.CreateAsync(idaasInfo);
        }
    }
}
