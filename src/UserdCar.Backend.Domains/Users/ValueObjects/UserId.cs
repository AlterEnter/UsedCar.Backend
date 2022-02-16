namespace UsedCar.Backend.Domains.Users.ValueObjects
{
    /// <summary>
    /// アプリ内ユーザーの識別子
    /// </summary>
    public record UserId
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="value"></param>
        public UserId(Guid value)
        {
            Value = value;
        }

        /// <summary>
        /// アプリ内ユーザーの識別子の値
        /// </summary>
        public Guid Value { get;}

        /// <summary>
        /// UserIdを作成する
        /// </summary>
        /// <returns>UserIdオブジェクト</returns>
        public static UserId Create()
        {
            return new UserId(Guid.NewGuid());
        }
    }
}
