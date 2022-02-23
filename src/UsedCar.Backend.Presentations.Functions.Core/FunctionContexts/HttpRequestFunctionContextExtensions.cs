using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

namespace UsedCar.Backend.Presentations.Functions.Core.FunctionContexts;

/// <summary>
/// FunctionContextからHttpRequestDataを取得する拡張メソッド
/// </summary>
public static class HttpRequestFunctionContextExtensions
{
    /// <summary>
    /// FunctionContextからHttpRequestDataを取得する
    /// </summary>
    /// <param name="functionContext">FunctionContext</param>
    /// <returns>HttpRequestData</returns>
    public static HttpRequestData? GetHttpRequestData(this FunctionContext functionContext)
    {
        try
        {
            KeyValuePair<Type, object> keyValuePair = functionContext.Features
                .SingleOrDefault(f => f.Key.Name == "IFunctionBindingsFeature");
            object functionBindingsFeature = keyValuePair.Value;
            Type type = functionBindingsFeature.GetType();
            IReadOnlyDictionary<string, object>? inputData = type.GetProperties()
                .Single(p => p.Name == "InputData")
                .GetValue(functionBindingsFeature) as IReadOnlyDictionary<string, object>;
            return inputData?.Values.SingleOrDefault(o => o is HttpRequestData) as HttpRequestData;
        }
        catch
        {
            return null;
        }
    }
}
