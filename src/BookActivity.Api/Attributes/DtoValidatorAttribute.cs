using BookActivity.Api.Common.Models;
using BookActivity.Application.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BookActivity.Api.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class DtoValidatorAttribute : Attribute, IActionFilter
    {
        private Type _baseDtoType = typeof(BaseDto);

        public void OnActionExecuted(ActionExecutedContext context) { }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var errorMessage = string.Empty;

            foreach (var dto in context.ActionArguments.Where(a => a.Value.GetType().IsAssignableTo(_baseDtoType)))
            {
                errorMessage += (dto.Value as BaseDto).Validate();
            }

            if (!string.IsNullOrEmpty(errorMessage))
                context.Result = new ObjectResult(new ApiResult<object>(errorMessage));
        }
    }
}
