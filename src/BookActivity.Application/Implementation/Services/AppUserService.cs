using Ardalis.Result;
using AutoMapper;
using BookActivity.Application.Constants;
using BookActivity.Application.Interfaces;
using BookActivity.Application.Models;
using BookActivity.Application.Models.DTO.Create;
using BookActivity.Application.Models.DTO.Read;
using BookActivity.Domain.Commands.AppUserCommands;
using BookActivity.Domain.Filters.Models;
using BookActivity.Domain.Filters.Specifications.AppUserSpecs;
using BookActivity.Domain.Interfaces.Repositories;
using BookActivity.Domain.Models;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NetDevPack.Mediator;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BookActivity.Application.Implementation.Services
{
    public class AppUserService : IAppUserService
    {
        private readonly IMapper _mapper;

        private readonly IMediatorHandler _mediatorHandler;

        private readonly IAppUserRepository _appUserRepository;

        private readonly UserManager<AppUser> _userManager;

        private readonly SignInManager<AppUser> _signInManager;

        private readonly TokenInfo _tokenInfo;

        public AppUserService(
            IMapper mapper,
            IMediatorHandler mediatorHandler,
            IAppUserRepository appUserRepository,
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            IOptions<TokenInfo> tokenInfo)
        {
            _mapper = mapper;
            _mediatorHandler = mediatorHandler;
            _appUserRepository = appUserRepository;
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenInfo = tokenInfo.Value;
        }

        public async Task<ValidationResult> AddAsync(AppUserCreateDTO appUserCreateDTO)
        {
            var addAppUserCommand = _mapper.Map<AddAppUserCommand>(appUserCreateDTO);

            return await _mediatorHandler.SendCommand(addAppUserCommand);
        }

        public async Task<AppUserDTO> FindByIdAsync(Guid appUserId)
        {
            AppUserFilterModel filterModel = new() { AppUserId = new FilterModelProp<AppUser, Guid>(appUserId, new AppUserByIdSpec()) };
            var appUser = (await _appUserRepository.GetByFilterAsync(filterModel))?.FirstOrDefault();

            return _mapper.Map<AppUserDTO>(appUser);
        }

        public async Task<Result<AuthenticationResult>> PasswordSignInAsync(AuthenticationModel authenticationModel)
        {
            AppUserFilterModel filterModel = new() { Email = new FilterModelProp<AppUser, string>(authenticationModel.Email, new AppUserByEmailSpec()) };
            var appUser = (await _appUserRepository.GetByFilterAsync(filterModel))?.FirstOrDefault();

            if (appUser == null)
                return Result<AuthenticationResult>.Error(new string[] { ValidationErrorConstants.IncorrectEmail });

            var isCorrectPassword = await _userManager.CheckPasswordAsync(appUser, authenticationModel.Password);
            if (!isCorrectPassword)
                return Result<AuthenticationResult>.Error(new string[] { ValidationErrorConstants.IncorrectPassword });

            var signResult = await _signInManager.PasswordSignInAsync(appUser, authenticationModel.Password, authenticationModel.RememberMe, lockoutOnFailure: false);
            if (!signResult.Succeeded)
                return Result<AuthenticationResult>.Error(new string[] { ValidationErrorConstants.FailedSign });

            string token = GenerateJwtToken(appUser.Id.ToString());

            return new Result<AuthenticationResult>(new AuthenticationResult() { Email = appUser.Email, UserName = appUser.UserName, Token = token });
        }

        private string GenerateJwtToken(string userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_tokenInfo.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", userId.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
