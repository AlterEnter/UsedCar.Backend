namespace UsedCar.Backend.Domains.Users.ValueObjects
{
    /// <summary>
    /// 名前
    /// </summary>
    public record Name
    {
        /// <summary>
        /// 名前最大length
        /// </summary>
        private static readonly int _maxNumber = 15;
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public Name(string firstName, string lastName)
        {
            if (string.IsNullOrEmpty(firstName))
            {
                throw new ArgumentNullException(nameof(firstName), "firstName can not be null or empty.");
            }
            if (string.IsNullOrEmpty(lastName))
            {
                throw new ArgumentNullException(nameof(lastName), "lastName can not be null or empty.");
            }
            if (firstName.Length > _maxNumber)
            {
                throw new ArgumentOutOfRangeException(nameof(firstName), "firstName is out of range.");
            }
            if (lastName.Length > _maxNumber)
            {
                throw new ArgumentOutOfRangeException(nameof(lastName), "lastName is out of range.");
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
