using Ardalis.Result;
using FluentValidation.Results;
using System.Linq;

namespace BookActivity.Application.Extensions
{
    public static class ValidationResultExtension
    {
        public static Result<T> ToResult<T>(this ValidationResult validationResult, T value)
        {
            return validationResult.IsValid
                ? new Result<T>(value)
                : Result<T>.Error(validationResult.Errors.Select(e => e.ErrorMessage).ToArray());
        }
    }
}
