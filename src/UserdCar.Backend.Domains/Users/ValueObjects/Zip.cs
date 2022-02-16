namespace UsedCar.Backend.Domains.Users.ValueObjects
{
    /// <summary>
    /// 郵便番号
    /// </summary>
    public class Zip
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="zip"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public Zip(string zip)
        {
            if (string.IsNullOrEmpty(zip))
            {
                throw new ArgumentNullException(nameof(zip), "zip can not be null or empty.");
            }
            Value = zip;
        }
        /// <summary>
        /// 郵便番号の値
        /// </summary>
        public string Value { get; }
    }
}
