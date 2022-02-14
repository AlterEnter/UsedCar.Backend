namespace UsedCar.Backend.Domains.Users.ValueObjects
{
    /// <summary>
    /// 詳細住所２
    /// </summary>
    public record Street2
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="street2"></param>
        public Street2(string? street2)
        {
            Value = street2;
        }
        /// <summary>
        /// 詳細住所2の値
        /// </summary>
        public string? Value { get; }
    }
}
