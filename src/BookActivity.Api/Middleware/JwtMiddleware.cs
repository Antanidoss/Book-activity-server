using BookActivity.Application.Interfaces.Services;
using BookActivity.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookActivity.Api.Middleware
{
    internal sealed class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly TokenInfo _tokenInfo;

        private readonly ILogger<JwtMiddleware> _logger;

        public JwtMiddleware(RequestDelegate next, IOptions<TokenInfo> tokenInfo, ILogger<JwtMiddleware> logger)
        {
            _next = next;
            _tokenInfo = tokenInfo.Value;
            _logger = logger;
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

                var user = (await userService.FindByIdAsync(Guid.Parse(userId))).Value;
                user.Token = jwtToken.RawData;
                context.Items["User"] = user;
            }
            catch (Exception ex)
            {
                _logger.LogError(new EventId(), ex, string.Empty);
            }
        }
    }
}
