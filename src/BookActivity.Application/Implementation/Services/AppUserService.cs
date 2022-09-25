using Antanidoss.Specification.Filters.Implementation;
using Ardalis.Result;
using AutoMapper;
using BookActivity.Application.Constants;
using BookActivity.Application.Interfaces.Services;
using BookActivity.Application.Models;
using BookActivity.Application.Models.DTO.Create;
using BookActivity.Application.Models.DTO.Read;
using BookActivity.Application.Models.DTO.Update;
using BookActivity.Domain.Commands.AppUserCommands.AddAppUser;
using BookActivity.Domain.Commands.AppUserCommands.SubscribeAppUser;
using BookActivity.Domain.Commands.AppUserCommands.UpdateAppUser;
using BookActivity.Domain.Interfaces.Repositories;
using BookActivity.Domain.Models;
using BookActivity.Domain.Specifications.AppUserSpecs;
using BookActivity.Domain.Vidations;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NetDevPack.Mediator;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BookActivity.Application.Implementation.Services
{
    internal class AppUserService : IAppUserService
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

        public async Task<ValidationResult> AddAsync(CreateAppUserDto appUserCreateDTO)
        {
            var addAppUserCommand = _mapper.Map<AddAppUserCommand>(appUserCreateDTO);

            return await _mediatorHandler.SendCommand(addAppUserCommand);
        }

        public async Task<ValidationResult> SubscribeAppUserAsync(Guid currentUserId, Guid subscribedUserId)
        {
            CommonValidator.ThrowExceptionIfEmpty(currentUserId, nameof(currentUserId));
            CommonValidator.ThrowExceptionIfEmpty(subscribedUserId, nameof(subscribedUserId));

            SubscribeAppUserCommand subscribeAppUserCommand = new(currentUserId, subscribedUserId);

            return await _mediatorHandler.SendCommand(subscribeAppUserCommand).ConfigureAwait(false);
        }

        public async Task<ValidationResult> UpdateAsync(UpdateAppUserDto updateAppUserModel)
        {
            CommonValidator.ThrowExceptionIfEmpty(updateAppUserModel.AppUserId, nameof(updateAppUserModel.AppUserId));

            return await _mediatorHandler.SendCommand(_mapper.Map<UpdateAppUserCommand>(updateAppUserModel));
        }

        public async Task<Result<AuthenticationResult>> PasswordSignInAsync(AuthenticationModel authenticationModel)
        {
            AppUserByEmailSpec specification = new(authenticationModel.Email);
            FirstOrDefault<AppUser> filter = new(specification);
            var appUser = _appUserRepository.GetByFilter(filter);

            if (appUser is null)
                return Result<AuthenticationResult>.Error(ValidationErrorConstants.IncorrectEmail);

            var isCorrectPassword = await _userManager.CheckPasswordAsync(appUser, authenticationModel.Password).ConfigureAwait(false);
            if (!isCorrectPassword)
                return Result<AuthenticationResult>.Error(ValidationErrorConstants.IncorrectPassword);

            var signResult = await _signInManager.PasswordSignInAsync(appUser, authenticationModel.Password, authenticationModel.RememberMe, lockoutOnFailure: false)
                .ConfigureAwait(false);

            if (!signResult.Succeeded)
                return Result<AuthenticationResult>.Error(ValidationErrorConstants.FailedSign);

            string token = GenerateJwtToken(appUser.Id.ToString());

            return new Result<AuthenticationResult>(new AuthenticationResult(appUser.Id, appUser.UserName, appUser.Email, token));
        }

        public async Task<Result<AppUserDto>> FindByIdAsync(Guid appUserId)
        {
            CommonValidator.ThrowExceptionIfEmpty(appUserId, nameof(appUserId));

            AppUserByIdSpec specification = new(appUserId);
            FirstOrDefault<AppUser> filter = new(specification);
            var appUser = _appUserRepository.GetByFilter(filter);

            return _mapper.Map<AppUserDto>(appUser);
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
