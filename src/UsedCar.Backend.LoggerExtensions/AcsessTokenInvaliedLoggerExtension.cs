using Microsoft.Extensions.Logging;

namespace UsedCar.Backend.LoggerExtensions;
/// <summary>
/// AccessTokenInvalidLoggerExtensionMiddlewareのロガーの拡張メソッド
/// </summary>

public static class AccessTokenInvalidLoggerExtension
{
    /// <summary>
    /// AccessTokenInvalidLoggerExtensionMiddleware失敗ログ
    /// </summary>
    private static readonly Action<ILogger, Exception> s_acsessTokenInvalid;

    /// <summary>
    /// 拡張メソッド
    /// </summary>
    static AccessTokenInvalidLoggerExtension()
    {
        var eventId = new EventId(0, nameof(AccessTokenInvalid));
        s_acsessTokenInvalid = LoggerMessage.Define(
            LogLevel.Warning,
            eventId,
            "Bearerトークンの取得に失敗しました。");
    }

    public static void AccessTokenInvalid(this ILogger logger, Exception ex) =>
        s_acsessTokenInvalid(logger, ex);
}
