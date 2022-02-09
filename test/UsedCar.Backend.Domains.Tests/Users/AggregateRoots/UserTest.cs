using UsedCar.Backend.Domains.Users.ValueObjects;

namespace UsedCar.Backend.Domains.Users.AggregateRoots
{
    /// <summary>
    /// ユーザー情報
    /// </summary>
    /// <param name="UserId">ユーザーID</param>
    /// <param name="Name">ユーザー名</param>
    /// <param name="DisplayName">表示名</param>
    /// <param name="DateOfBirth">生年月日</param>
    /// <param name="PhoneNumber">電話番号</param>
    /// <param name="MailAddress">メールアドレス</param>
    /// <param name="Address">住所</param>
    public record UserTest(
        UserIdTest UserId,
        NameTest Name,
        DisplayNameTest DisplayName,
        DateOfBirthTest DateOfBirth,
        PhoneNumberTest PhoneNumber,
        MailAddressTest MailAddress,
        AddressTest Address)
    {
    }
}
