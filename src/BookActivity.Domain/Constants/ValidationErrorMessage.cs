namespace BookActivity.Domain.Constants
{
    public static class ValidationErrorMessage
    {
        public static string GetEnitityNotFoundMessage(string entityName) => $"{entityName} not found";
    }
}