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
        /// <exception cref="ArgumentException"></exception>
        public Street1(string street1)
        {
            if (!string.IsNullOrEmpty(street1))
            {
                throw new ArgumentException();
            }
            Vaule = street1;
        }
        /// <summary>
        /// street1の値
        /// </summary>
        public string Vaule { get; }
    }
}
