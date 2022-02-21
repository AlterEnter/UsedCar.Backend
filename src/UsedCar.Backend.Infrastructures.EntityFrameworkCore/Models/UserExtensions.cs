namespace UsedCar.Backend.Infrastructures.EntityFrameworkCore.Models
{
    public static class UserExtensions
    {
        public static IdaasInfo ToTable(this Domains.Users.AggregateRoots.IdaasInfo idaasInfo) => new()
        {
            IdpUserId = idaasInfo.IdaasId.Value,
            DisplayName = idaasInfo.DisplayName.Value,
            MailAddress = idaasInfo.MailAddress.Value
        };
    }
}
