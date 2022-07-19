namespace BookActivity.Application.Models
{
    public sealed class AuthenticationResult
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }

        public AuthenticationResult(string userName, string email, string token)
        {
            UserName = userName;
            Email = email;
            Token = token;
        }
    }
}
