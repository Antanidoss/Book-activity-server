using Ardalis.Result;
using BookActivity.Shared.Models;

namespace BookActivity.Domain.Queries.AppUserQueries.AuthenticationUser
{
    public sealed class AuthenticationUserQuery : Query<Result<AuthenticationResult>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
