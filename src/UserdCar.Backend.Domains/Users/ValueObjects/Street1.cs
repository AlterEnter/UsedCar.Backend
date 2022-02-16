namespace UsedCar.Backend.Domains.Users.ValueObjects
{
    /// <summary>
    /// 詳細住所１
    /// </summary>
    public record Street1
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="street1"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public Street1(string street1)
        {
            if (string.IsNullOrEmpty(street1))
            {
                throw new ArgumentNullException(nameof(street1), "street1 can not be null or empty.");
            }
            Value = street1;
        }
        /// <summary>
        /// street1の値
        /// </summary>
        public string Value { get; }
    }
}
