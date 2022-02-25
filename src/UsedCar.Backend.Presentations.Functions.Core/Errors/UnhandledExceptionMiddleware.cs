using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker.Middleware;
using Microsoft.Extensions.Logging;
using System.Net;
using UsedCar.Backend.LoggerExtensions;
using UsedCar.Backend.Presentations.Functions.Core.FunctionContexts;

namespace UsedCar.Backend.Presentations.Functions.Core.Errors;

/// <summary>
/// ハンドルされていない例外ミドルウェア
/// </summary>
public class UnhandledExceptionMiddleware : IFunctionsWorkerMiddleware
{
    private readonly ILogger _logger;

    public UnhandledExceptionMiddleware(ILoggerFactory loggerFactory) => _logger = loggerFactory.CreateLogger<UnhandledExceptionMiddleware>();

    public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            _logger.UnhandledException(exception.Message, exception);
            HttpRequestData? req = context.GetHttpRequestData();
            if (req is null)
            {
#pragma warning disable CA2201 // 予約された例外の種類を発生させません
                throw new NullReferenceException("Fail to get http request data.", exception);
#pragma warning restore CA2201 // 予約された例外の種類を発生させません
            }

            HttpResponseData responseData = req.CreateResponse();
            responseData.StatusCode = HttpStatusCode.InternalServerError;
            context.InvokeResult(responseData);
        }
    }
}
