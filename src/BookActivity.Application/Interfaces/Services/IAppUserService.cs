using Ardalis.Result;
using BookActivity.Application.Models;
using BookActivity.Application.Models.Dto.Create;
using BookActivity.Application.Models.Dto.Read;
using BookActivity.Application.Models.Dto.Update;
using FluentValidation.Results;
using System;
using System.Threading.Tasks;

namespace BookActivity.Application.Interfaces.Services
{
    public interface IAppUserService
    {
        Task<ValidationResult> AddAsync(CreateAppUserDto appUserCreateDTO);
        Task<ValidationResult> SubscribeAppUserAsync(Guid currentUserId, Guid subscribedUserId);
        Task<Result<AuthenticationResult>> PasswordSignInAsync(AuthenticationModel authenticationModel);
        Task<Result<AppUserDto>> FindByIdAsync(Guid appUserId);
        Task<ValidationResult> UpdateAsync(UpdateAppUserDto updateAppUserModel);
    }
}
