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

        public static User ToTable(this Domains.Users.AggregateRoots.User user) => new()
        {
            UserId = user.UserId.Value,
            IdpUserId = user.IdaasId.Value,
            PhoneNumber = user.PhoneNumber.Value,
            FirstName = user.Name.FirstName,
            LastName = user.Name.LastName,
            DateOfBirth = user.DateOfBirth.Value,
            Zip = user.Address.Zip.Value,
            State = user.Address.State.Value,
            City = user.Address.City.Value,
            Street1 = user.Address.Street1.Value,
            Street2 = user.Address.Street2.Value
        };
    }
}
