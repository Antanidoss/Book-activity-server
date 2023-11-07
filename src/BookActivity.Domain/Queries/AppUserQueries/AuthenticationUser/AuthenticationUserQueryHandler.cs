using Ardalis.Result;
using BookActivity.Domain.Constants;
using BookActivity.Domain.Filters.Models;
using BookActivity.Domain.Interfaces.Repositories;
using BookActivity.Domain.Models;
using BookActivity.Domain.Specifications.AppUserSpecs;
using BookActivity.Shared.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BookActivity.Domain.Queries.AppUserQueries.AuthenticationUser
{
    internal sealed class AuthenticationUserQueryHandler : IRequestHandler<AuthenticationUserQuery, Result<AuthenticationResult>>
    {
        private readonly IAppUserRepository _userRepository;

        private readonly UserManager<AppUser> _userManager;

        private readonly SignInManager<AppUser> _signInManager;

        private readonly TokenInfo _tokenInfo;

        public AuthenticationUserQueryHandler(IAppUserRepository userRepository, UserManager<AppUser> userManger, IOptions<TokenInfo> tokenInfo, SignInManager<AppUser> signInManager)
        {
            _userRepository = userRepository;
            _userManager = userManger;
            _tokenInfo = tokenInfo.Value;
            _signInManager = signInManager;
        }

        public async Task<Result<AuthenticationResult>> Handle(AuthenticationUserQuery request, CancellationToken cancellationToken)
        {
            AppUserByEmailSpec specification = new(request.Email);
            DbSingleResultFilterModel<AppUser> filterModel = new(specification, forUpdate: true);
            var appUser = await _userRepository.GetByFilterAsync(filterModel);

            if (appUser is null)
                return Result<AuthenticationResult>.Error(ValidationErrorConstants.IncorrectEmail);

            var isCorrectPassword = await _userManager.CheckPasswordAsync(appUser, request.Password);
            if (!isCorrectPassword)
                return Result<AuthenticationResult>.Error(ValidationErrorConstants.IncorrectPassword);

            var signResult = await _signInManager.PasswordSignInAsync(appUser, request.Password, request.RememberMe, lockoutOnFailure: false);

            if (!signResult.Succeeded)
                return Result<AuthenticationResult>.Error(ValidationErrorConstants.FailedSign);

            string token = GenerateJwtToken(appUser.Id.ToString());

            return new Result<AuthenticationResult>(new AuthenticationResult(appUser.Id, appUser.UserName, appUser.Email, token, appUser.AvatarImage));
        }

        private string GenerateJwtToken(string userId)
        {
            JwtSecurityTokenHandler tokenHandler = new();
            var key = Encoding.ASCII.GetBytes(_tokenInfo.SecretKey);
            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Subject = new ClaimsIdentity(new[] { new Claim(nameof(userId), userId.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
