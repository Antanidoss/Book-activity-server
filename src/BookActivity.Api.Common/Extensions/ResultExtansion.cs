using Ardalis.Result;
using BookActivity.Api.Common.Models;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text;

namespace BookActivity.Api.Common.Extansions
{
    public static class ResultExtansion
    {
        public static ApiResult<T> ToApiResult<T>(this Result<T> result)
        {
            return new ApiResult<T>(result, result.IsSuccess)
            {
                ErrorMessage = result.Errors == null ? null : string.Join(",", result.Errors)
            };
        }

        public static ApiResult<T> ToApiResult<T>(this T result)
        {
            return new ApiResult<T>(result, success: true);
        }

        public static ActionResult ToActionResult(this ValidationResult validationResult)
        {
            return validationResult.IsValid ? new OkResult() : new BadRequestObjectResult(ValidationErrorsToString(validationResult.Errors));
        }

        private static string ValidationErrorsToString(List<ValidationFailure> errors)
        {
            StringBuilder result = new();

            foreach (var error in errors)
                result.AppendLine(error.ErrorMessage);

            return result.ToString();
        }
    }
}
