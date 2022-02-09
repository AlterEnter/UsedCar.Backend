using System.Text.RegularExpressions;

namespace UsedCar.Backend.Domains.Users.ValueObjects
{
    /// <summary>
    /// メールアドレス
    /// </summary>
    public record MailAddress
    {
        /// <summary>
        /// メールアドレス文字数制限
        /// </summary>
        private static readonly int s_maxCharactersCount = 254;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="mailAddress"></param>
        /// <exception cref="ArgumentException"></exception>
        public MailAddress(string mailAddress)
        {
            if (
                !string.IsNullOrEmpty(mailAddress)
                && !Regex.IsMatch(mailAddress, @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250))
            )
            {
                throw new ArgumentException("Invalid format.");
            }
            if (mailAddress.Length > s_maxCharactersCount)
            {
                throw new ArgumentOutOfRangeException(nameof(mailAddress),
                    "The number of characters has exceeded upper limit.");
            }
            Value = mailAddress;
        }
        /// <summary>
        /// メールアドレスの値
        /// </summary>
        public string Value { get; set; }
    }
}
