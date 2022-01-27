using UsedCar.Backend.Domains.Users.ValueObjects;

namespace UsedCar.Backend.Domains.Users.ValueObjects
{
    public record Address(
        Zip Zip,
        State State,
        City City,
        Street1 Street1,
        Street2 Street2)
    {
    }
}
