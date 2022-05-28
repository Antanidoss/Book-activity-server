namespace BookActivity.Application.Models
{
    public sealed class AuthenticationModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
