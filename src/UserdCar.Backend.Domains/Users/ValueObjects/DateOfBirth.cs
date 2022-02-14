namespace UsedCar.Backend.Domains.Users.ValueObjects
{
    /// <summary>
    /// 誕生日
    /// </summary>
    public record DateOfBirth
    {
        /// <summary>
        /// 誕生日の最古日
        /// </summary>
        private static readonly DateTime MinDate = new(1910,01,01);
        /// <summary>
        /// 現在の日付
        /// </summary>
        private static readonly DateTime MaxDate = DateTime.Today;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="dateOfBirth"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public DateOfBirth(DateTime dateOfBirth)
        {
            if (dateOfBirth > MaxDate || dateOfBirth < MinDate)
            {
                throw new ArgumentOutOfRangeException(nameof(dateOfBirth), "dateOfBirth is out of range.");
            }

            Value = dateOfBirth.Date;
        }
        /// <summary>
        /// 誕生日
        /// </summary>
        public DateTime Value { get; }
    }
}
