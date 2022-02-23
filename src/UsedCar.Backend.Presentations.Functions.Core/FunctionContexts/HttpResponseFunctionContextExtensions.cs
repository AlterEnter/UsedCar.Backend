using System.Reflection;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

namespace UsedCar.Backend.Presentations.Functions.Core.FunctionContexts;

/// <summary>
/// FunctionContextにHttpResponseDataを設定する拡張メソッド
/// </summary>
public static class HttpResponseFunctionContextExtensions
{
    /// <summary>
    /// FunctionContextにHttpResponseDataを設定する
    /// </summary>
    /// <param name="context"></param>
    /// <param name="response"></param>
    public static void InvokeResult(this FunctionContext context, HttpResponseData response)
    {
        KeyValuePair<Type, object> keyValuePair =
            context.Features.SingleOrDefault(f => f.Key.Name == "IFunctionBindingsFeature");
        object functionBindingsFeature = keyValuePair.Value;
        Type type = functionBindingsFeature.GetType();
        PropertyInfo result = type.GetProperties().Single(p => p.Name == "InvocationResult");
        result.SetValue(functionBindingsFeature, response);
    }
}
