namespace UsedCar.Backend.Domains.Users.ValueObjects
{
    /// <summary>
    /// 電話番号
    /// </summary>
    public record PhoneNumber
    {
        /// <summary>
        /// 電話番号の最長値
        /// </summary>
        private static readonly int  _maxNumber = 15;
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="mobilePhoneNumber"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public PhoneNumber(string mobilePhoneNumber)
        {
            if (mobilePhoneNumber.Length > _maxNumber)
            {
                throw new ArgumentOutOfRangeException(nameof(mobilePhoneNumber), "mobilePhoneNumber is out of range.");
            }

            Value = mobilePhoneNumber;
        }
        /// <summary>
        /// 電話番号の値
        /// </summary>
        public string Value { get; }
    }
}
