namespace BookActivity.Domain.Constants
{
    public static class ValidationErrorConstants
    {
        public static string GetEnitityNotFoundMessage(string entityName) => $"{entityName} not found";
        public const string IncorrectPassword = "Wrong login or password";
        public const string IncorrectEmail = "Wrong login or password";
        public const string FailedSign = "Failed to login";
    }
}