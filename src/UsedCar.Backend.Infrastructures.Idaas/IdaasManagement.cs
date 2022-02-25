using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Graph;
using UsedCar.Backend.LoggerExtensions;
using UsedCar.Backend.UseCases.Users;

namespace UsedCar.Backend.Infrastructures.Idaas
{
    public class IdaasManagement : IIdaasManagement
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<IdaasManagement> _logger;

        public IdaasManagement(IConfiguration configuration, ILogger<IdaasManagement> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task UserDeleteAsync(string idaasId)
        {
            var authProvider = new ClientCredentialAuthProvider(
                _configuration["AzureAdB2CApi:ClientId"],
                _configuration["AzureAdB2CApi:TenantId"],
                _configuration["AzureAdB2CApi:Secret"]);

            GraphServiceClient client = new (authProvider);

            try
            {
                await client.Users[idaasId].Request().DeleteAsync();
            }
            catch (Exception e)
            {
                _logger.IdaasDeleteFailed(e.Message, e);
                throw new IdaasErrorException(e.Message, e);
            }


        }
    }
}
