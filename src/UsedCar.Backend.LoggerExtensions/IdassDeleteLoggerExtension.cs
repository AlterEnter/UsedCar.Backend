using Microsoft.Extensions.Logging;

namespace UsedCar.Backend.LoggerExtensions
{
    public static class IdaasDeleteLoggerExtension
    {
        private static readonly Action<ILogger, string, Exception> s_idaasDeleteFailed;

        static IdaasDeleteLoggerExtension()
        {
            var eventId = new EventId(0, nameof(IdaasDeleteFailed));
            s_idaasDeleteFailed = LoggerMessage.Define<string>(
                LogLevel.Error,
                eventId,
                "Idass情報の削除に失敗しました。{Message}"
                );
        }

        public static void IdaasDeleteFailed(this ILogger logger, string message, Exception e)
        {
            s_idaasDeleteFailed(logger, message, e);
        }
    }
}
