using Ardalis.Result;
using BookActivity.Api.Models;

namespace BookActivity.Api.Extansions
{
    public static class ResultExtansion
    {
        public static ApiResult<T> ToApiResult<T>(this Result<T> result)
        {
            return new ApiResult<T>(result, result.IsSuccess) { ErrorMessage = result.Errors == null ? null : string.Join(",", result.Errors) };
        }
    }
}
