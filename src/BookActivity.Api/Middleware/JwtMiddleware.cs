using BookActivity.Application.Interfaces.Services;
using BookActivity.Shared.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace BookActivity.Api.Middleware
{
    internal sealed class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly TokenInfo _tokenInfo;

        public JwtMiddleware(RequestDelegate next, IOptions<TokenInfo> tokenInfo)
        {
            _next = next;
            _tokenInfo = tokenInfo.Value;
        }

        public async Task Invoke(HttpContext context, IAppUserService userService)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault();
            if (token != null)
                await AttachUserToContextAsync(context, userService, token);

            await _next(context);
        }

        private async Task AttachUserToContextAsync(HttpContext context, IAppUserService userService, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_tokenInfo.SecretKey);

                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = jwtToken.Claims.First(x => x.Type == "userId").Value.ToString();
                var user = await userService.GetCurrentUserByIdAsync(Guid.Parse(userId));

                context.Items["User"] = new CurrentUser
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    AvatarImage = user.AvatarImage,
                    Token = jwtToken.RawData
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, string.Empty);
            }
        }
    }
}
