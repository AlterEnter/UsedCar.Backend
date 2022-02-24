using Microsoft.Azure.Functions.Worker.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading.Tasks;

namespace UsedCar.Backend.Presentations.Functions.Users.Validations;

public static class FunctionRequestBodyValidationExtension
{
    /// <summary>
    /// Azure Functionsのリクエスト本文を取得する
    /// </summary>
    /// <typeparam name="T">リクエスト本文の型</typeparam>
    /// <param name="requestData">リクエスト本文</param>
    /// <returns>リクエスト本文</returns>
    public static async Task<RequestBody<T>> GetRequestBodyAsync<T>(this HttpRequestData requestData) where T: IDoValidation
    {
        var requestBody = await new StreamReader(requestData.Body).ReadToEndAsync();
        T viewModel = JsonConvert.DeserializeObject<T>(requestBody);

        List<ValidationResult> validationResult = new();
        bool isValid = Validator.TryValidateObject(
            viewModel,
            new ValidationContext(viewModel, null, null),
            validationResult,
            true);

        if (viewModel.IsFilledRequired() is false)
        {
            return new RequestBody<T>(false, viewModel, validationResult);
        }

        if (viewModel.IsUtc() is false)
        {
            return new RequestBody<T>(false, viewModel, validationResult);
        }

        return new RequestBody<T>(isValid, viewModel, validationResult);
    }
}