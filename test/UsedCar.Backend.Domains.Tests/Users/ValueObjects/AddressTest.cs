using UsedCar.Backend.Domains.Users.ValueObjects;

namespace UsedCar.Backend.Domains.Users.ValueObjects
{
    /// <summary>
    /// 住所
    /// </summary>
    /// <param name="Zip">郵便番号</param>
    /// <param name="State">都道府県</param>
    /// <param name="City">市区町村</param>
    /// <param name="Street1">詳細住所１</param>
    /// <param name="Street2">詳細住所２</param>
    public record AddressTest(
        ZipTest Zip,
        StateTest State,
        CityTest City,
        Street1Test Street1,
        Street2Test Street2)
    {
    }
}
