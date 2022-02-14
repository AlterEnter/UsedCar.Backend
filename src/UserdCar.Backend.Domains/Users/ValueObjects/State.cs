namespace UsedCar.Backend.Domains.Users.ValueObjects
{
    /// <summary>
    /// 都道府県
    /// </summary>
    public record State
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="state"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public State(string state)
        {
            if (string.IsNullOrEmpty(state))
            {
                throw new ArgumentNullException(nameof(state), "state can not be null or empty.");
            }
            Value = state;
        }
        /// <summary>
        /// stateの値
        /// </summary>
        public string Value { get; }
    }
}
