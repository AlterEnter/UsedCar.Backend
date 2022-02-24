using Microsoft.Extensions.Logging;

namespace UsedCar.Backend.LoggerExtensions;

/// <summary>
/// UnhandledExceptionMiddlewareのロガーの拡張メソッド
/// </summary>
public static class UnhandledExceptionLoggerExtension
{
    /// <summary>
    /// UnhandledExceptionMiddleware失敗ログ
    /// </summary>
    private static readonly Action<ILogger, string, Exception> s_unhandledException;
    /// <summary>
    /// 拡張メソッド
    /// </summary>
    static UnhandledExceptionLoggerExtension()
    {
        var eventId = new EventId(0, nameof(UnhandledException));
        s_unhandledException = LoggerMessage.Define<string>(
            LogLevel.Error,
            eventId,
            "予期せぬエラー。{ErrorMessage}");
    }
    /// <summary>
    /// UnhandledExceptionMiddleware失敗ログ
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="errorMessage"></param>
    /// <param name="ex"></param>
    public static void UnhandledException(this ILogger logger, string errorMessage, Exception ex) =>
        s_unhandledException(logger, errorMessage, ex);
}
