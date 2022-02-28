using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Graph;
using UsedCar.Backend.Domains.Users.AggregateRoots;
using UsedCar.Backend.LoggerExtensions;
using UsedCar.Backend.UseCases.Users;
using User = Microsoft.Graph.User;

namespace UsedCar.Backend.Infrastructures.Idaas
{
    public class IdaasManagement : IIdaasManagement
    {
        private readonly GraphServiceClient _client;

        private readonly IConfiguration _configuration;

        private readonly ILogger<IdaasManagement> _logger;

        public IdaasManagement(IConfiguration configuration, ILogger<IdaasManagement> logger)
        {
            var authProvider = new ClientCredentialAuthProvider(
                configuration["AzureAdB2CApi:ClientId"],
                configuration["AzureAdB2CApi:TenantId"],
                configuration["AzureAdB2CApi:Secret"]);
            _client = new GraphServiceClient(authProvider);
            _configuration = configuration;
            _logger = logger;
        }

        public async Task UserDeleteAsync(string idaasId)
        {
            try
            {
                await _client.Users[idaasId].Request().DeleteAsync();
            }
            catch (Exception e)
            {
                _logger.IdaasDeleteFailed(e.Message, e);
                throw new IdaasErrorException(e.Message, e);
            }
        }

        public async Task UserUpdateAsync(IdaasInfo idaasInfo)
        {
            // 基本情報
            var user = new User()
            {
                DisplayName = idaasInfo.DisplayName.Value,
                // ユーザ特定のキーとなるサインインユーザ名とメールアドレスを変更
                Identities = new List<ObjectIdentity>()
                {
                    new ObjectIdentity()
                    {
                        SignInType = "emailAddress",
                        Issuer = _configuration["AzureAdB2CApi:Domain"],
                        IssuerAssignedId = idaasInfo.MailAddress.Value
                    }
                },
                // サインインのブロック
                AccountEnabled = false,
                // 連絡用メールアドレスの変更サンプル
                OtherMails = new string[]{ 
                    idaasInfo.MailAddress.Value
                }
            };

            // ユーザアカウントの更新
            var result = await _client.Users[idaasInfo.IdaasId.Value]
                .Request()
                .UpdateAsync(user);
        }
    }
}
