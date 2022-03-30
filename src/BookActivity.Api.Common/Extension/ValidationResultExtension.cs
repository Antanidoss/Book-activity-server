using Microsoft.AspNetCore.Mvc;
using FluentValidation.Results;
using System.Collections.Generic;

namespace BookActivity.Api.Common.Extension
{
    public static class ValidationResultExtension
    {
        public static ActionResult ToActionResult(this ValidationResult validationResult)
        {
            return validationResult.IsValid ? new OkObjectResult(null) : new BadRequestObjectResult(ValidationErrorsToString(validationResult.Errors)); 
        }

        private static string ValidationErrorsToString(List<ValidationFailure> errors)
        {
            string result = string.Empty;

            foreach (var error in errors)
            {
                result += error.ErrorMessage + "/n";
            }
            
            return result;
        }
    }
}