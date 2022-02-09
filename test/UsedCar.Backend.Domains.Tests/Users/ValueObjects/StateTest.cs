namespace UsedCar.Backend.Domains.Users.ValueObjects
{
    /// <summary>
    /// 都道府県
    /// </summary>
    public record StateTest
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="state"></param>
        /// <exception cref="ArgumentException"></exception>
        public StateTest(string state)
        {
            if (!string.IsNullOrEmpty(state))
            {
                throw new ArgumentException();
            }
            Vaule = state;
        }
        /// <summary>
        /// stateの値
        /// </summary>
        public string Vaule { get; }
    }
}
