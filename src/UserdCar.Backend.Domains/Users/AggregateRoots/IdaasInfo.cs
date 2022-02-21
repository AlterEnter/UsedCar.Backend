using UsedCar.Backend.Domains.Users.ValueObjects;

namespace UsedCar.Backend.Domains.Users.AggregateRoots
{
    /// <summary>
    /// IdaasInfo
    /// </summary>
    /// <param name="IdaasId">IDaaSId</param>
    /// <param name="DisplayName">表示名</param>
    /// <param name="MailAddress">メールアドレス</param>
    public record IdaasInfo(
        IdaasId IdaasId,
        DisplayName DisplayName,
        MailAddress MailAddress)
    {
    }
}
