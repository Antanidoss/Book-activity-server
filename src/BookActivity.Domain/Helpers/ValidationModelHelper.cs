using BookActivity.Domain.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookActivity.Domain.Helpers
{
    public static class ValidationModelHelper
    {
        public static void ValidateObject<T>(T entity) where T : BaseEntity
        {
            var validationContext = new ValidationContext(entity);
            var results = new List<ValidationResult>();

            if (!Validator.TryValidateObject(entity, validationContext, results, true))
            {
                throw new ValidationException(GetValiationErrorStr(results));
            }
        }

        public static void ValidateProperty<T>(T entity, object propertyValue, string propertyName) where T : BaseEntity
        {
            var validationContext = new ValidationContext(entity) { MemberName = propertyName };
            var results = new List<ValidationResult>();

            if (!Validator.TryValidateProperty(propertyValue, validationContext, results))
            {
                throw new ValidationException(GetValiationErrorStr(results));
            }
        }

        private static string GetValiationErrorStr(List<ValidationResult> results)
        {
            var stringBuilder = new StringBuilder();

            foreach (var result in results)
            {
                stringBuilder.AppendLine($"{string.Join(",", result.MemberNames)}: {result.ErrorMessage}");
            }

            return stringBuilder.ToString();
        }
    }
}
