namespace UsedCar.Backend.Domains.Users.ValueObjects
{
    /// <summary>
    /// 詳細住所２
    /// </summary>
    public record Street2Test
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="street2"></param>
        public Street2Test(string? street2)
        {
            Vaule = street2;
        }
        /// <summary>
        /// 詳細住所2の値
        /// </summary>
        public string? Vaule { get; }
    }
}
