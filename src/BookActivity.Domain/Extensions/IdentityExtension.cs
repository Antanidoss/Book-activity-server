using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace BookActivity.Domain.Extensions
{
    internal static class IdentityExtension
    {
        public static ValidationResult ToValidationResult(this IdentityResult identityResult)
        {
            return identityResult.Succeeded
                ? new ValidationResult()
                : new ValidationResult(identityResult.Errors.Select(e => new ValidationFailure(string.Empty, e.Description)));
        }
    }
}
