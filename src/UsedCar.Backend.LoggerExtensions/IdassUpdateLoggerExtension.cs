using Microsoft.Extensions.Logging;

namespace UsedCar.Backend.LoggerExtensions
{
    public static class IdaasUpdateLoggerExtension
    {
        private static readonly Action<ILogger, string, Exception> s_idaasUpdateFailed;

        static IdaasUpdateLoggerExtension()
        {
            var eventId = new EventId(0, nameof(IdaasUpdateFailed));
            s_idaasUpdateFailed = LoggerMessage.Define<string>(
                LogLevel.Error,
                eventId,
                "Idaas情報の削除に失敗しました。{Message}"
                );
        }

        public static void IdaasUpdateFailed(this ILogger logger, string message, Exception e)
        {
            s_idaasUpdateFailed(logger, message, e);
        }
    }
}
