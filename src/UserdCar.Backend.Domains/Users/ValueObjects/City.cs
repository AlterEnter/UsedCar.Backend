namespace UsedCar.Backend.Domains.Users.ValueObjects
{
    /// <summary>
    /// 市区町村
    /// </summary>
    public record City
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="city"></param>
        /// <exception cref="ArgumentException"></exception>
        public City(string city)
        {
            if (!string.IsNullOrEmpty(city))
            {
                throw new ArgumentException();
            }
            Vaule = city;
        }
        /// <summary>
        /// Cityの値
        /// </summary>
        public string Vaule { get; }
    }
}
