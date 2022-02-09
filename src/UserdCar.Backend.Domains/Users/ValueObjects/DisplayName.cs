namespace UsedCar.Backend.Domains.Users.ValueObjects
{
    /// <summary>
    /// 表示名
    /// </summary>
    public record DisplayName
    {
        /// <summary>
        /// 表示名文字数長さ
        /// </summary>
        private static readonly int s_maxCharactersCount = 30;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="displayName"></param>
        public DisplayName(string displayName)
        {
            if (!string.IsNullOrEmpty(displayName))
            {
                throw new ArgumentException("can not be null or empty");
            }

            if (displayName.Length > s_maxCharactersCount)
            {
                throw new ArgumentOutOfRangeException(nameof(displayName),
                    "The number of characters has exceeded upper limit.");
            }

            Value = displayName;
        }

        /// <summary>
        /// 表示名の値
        /// </summary>
        public string Value { get; }
    }
}
