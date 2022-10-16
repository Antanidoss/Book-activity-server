using System;

namespace BookActivity.Application.Models
{
    public sealed class AuthenticationResult
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public byte[] AvatarImage { get; set; }

        public AuthenticationResult(Guid userId, string userName, string email, string token, byte[] avatarImage)
        {
            UserId = userId;
            UserName = userName;
            Email = email;
            Token = token;
            AvatarImage = avatarImage;
        }
    }
}
