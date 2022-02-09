namespace UsedCar.Backend.Domains.Users.ValueObjects
{
    /// <summary>
    /// アプリ内ユーザーの識別子
    /// </summary>
    public record UserId
    {
        /// <summary>
        /// アプリ内ユーザーの識別子の値
        /// </summary>
        private readonly Guid _value;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="value"></param>
        [Obsolete("", false)]
        public UserId(Guid value)
        {
            _value = value;
        }

        /// <summary>
        /// アプリ内ユーザーの識別子の値
        /// </summary>
        public Guid Value => _value;

        /// <summary>
        /// UserIdを作成する
        /// </summary>
        /// <returns>UserIdオブジェクト</returns>
        public static UserId Create()
        {
#pragma warning disable CS0618 // 型またはメンバーが旧型式です
            return new UserId(Guid.NewGuid());
#pragma warning restore CS0618 // 型またはメンバーが旧型式です
        }
    }
}
