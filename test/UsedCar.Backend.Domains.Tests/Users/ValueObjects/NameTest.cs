namespace UsedCar.Backend.Domains.Users.ValueObjects
{
    /// <summary>
    /// 名前
    /// </summary>
    public record NameTest
    {
        /// <summary>
        /// 電話番号の最長値
        /// </summary>
        private static readonly int _maxNumber = 15;
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public NameTest(string firstName, string lastName)
        {
            if (firstName.Length > _maxNumber || LastName.Length > _maxNumber)
            {
                throw new ArgumentOutOfRangeException("out of range");
            }
            FirstName = firstName;
            LastName = lastName;
        }
        /// <summary>
        /// 姓
        /// </summary>
        public string FirstName { get; }
        /// <summary>
        /// 名
        /// </summary>
        public string LastName { get; }
    }
}
