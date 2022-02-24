using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UsedCar.Backend.Presentations.Functions.Users.Validations;

/// <summary>
/// リクエスト本文
/// </summary>
/// <typeparam name="T">リクエスト本文の型</typeparam>
/// <param name="IsValid">リクエスト本文が妥当かどうか</param>
/// <param name="Value">リクエスト本文の値</param>
/// <param name="ValidationResults">バリデーション結果</param>
public record RequestBody<T>(
    bool IsValid,
    T Value,
    IReadOnlyList<ValidationResult> ValidationResults)
{
    /// <summary>
    ///     リクエスト本文が妥当かどうか
    /// </summary>
    public bool IsValid { get; } = IsValid;

    /// <summary>
    ///     リクエスト本文の値
    /// </summary>
    public T Value { get; } = Value;

    /// <summary>
    ///     バリデーション結果
    /// </summary>
    public IReadOnlyList<ValidationResult> ValidationResults { get; } = ValidationResults;
}