using UsedCar.Backend.Domains.Users.ValueObjects;

namespace UsedCar.Backend.Domains.Users.AggregateRoots
{
    public record User(
        UserId UserId,
        Name Name,
        DisplayName DisplayName,
        DateOfBirth DateOfBirth,
        PhoneNumber PhoneNumber,
        MailAddress MailAddress,
        Address Address)
    {
    }
}
