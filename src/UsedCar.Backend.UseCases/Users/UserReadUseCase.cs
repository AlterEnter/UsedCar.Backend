using UsedCar.Backend.Domains.Users;
using UsedCar.Backend.Domains.Users.ValueObjects;
using UsedCar.Backend.UseCases.Exceptions;
using UsedCar.Backend.UseCases.Users.Models;

namespace UsedCar.Backend.UseCases.Users
{
    public class UserReadUseCase : IUserReadUseCase
    {

        private readonly IIdaasRepository _idaasRepository;

        private readonly IUserRepository _userRepository;

        public UserReadUseCase(
            IIdaasRepository idaasRepository, 
            IUserRepository userRepository)
        {
            _idaasRepository = idaasRepository;
            _userRepository = userRepository;
        }

        public async Task<UserReadResponse?> ExecuteAsync(string idaasId)
        {
            var idaasInfo = await _idaasRepository.FindAsync(new IdaasId(idaasId));

            if (idaasInfo == null)
            {
                throw new IdaasNotFoundException();
            }

            var userReadResponse = new UserReadResponse()
            {
                MailAddress = idaasInfo.MailAddress.Value,
                DisplayName = idaasInfo.DisplayName.Value
            };

            var user = await _userRepository.FindAsync(new IdaasId(idaasId));

            if (user == null)
            {
                return userReadResponse;
            }
            
            userReadResponse.UserId = user.UserId.Value.ToString();
            userReadResponse.FirstName = user.Name.FirstName;
            userReadResponse.LastName = user.Name.LastName;
            userReadResponse.DateOfBirth = user.DateOfBirth.Value;
            userReadResponse.PhoneNumber = user.PhoneNumber.Value;
            userReadResponse.Zip = user.Address.Zip.Value;
            userReadResponse.State = user.Address.State.Value;
            userReadResponse.City = user.Address.City.Value;
            userReadResponse.Street1 = user.Address.Street1.Value;
            userReadResponse.Street2 = user.Address.Street2.Value;
            return userReadResponse;
        }
    }
}
