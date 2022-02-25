using Microsoft.Extensions.Logging;

namespace UsedCar.Backend.LoggerExtensions
{
    public static class UserDeleteLoggerExtension
    {
        private static readonly Action<ILogger, string, Exception> s_userDeletefailedOnDb;

        static UserDeleteLoggerExtension()
        {
            var eventId = new EventId(0, nameof(UserDeleteFailed));
            s_userDeletefailedOnDb = LoggerMessage.Define<string>(
                LogLevel.Error,
                eventId,
                "ユーザー削除に失敗しました。idaasId = {idaasId}"
            );
        }

        public static void UserDeleteFailed(this ILogger logger, string idaasId, Exception e)
        {
            s_userDeletefailedOnDb(logger, idaasId, e);
        }

    }
}
