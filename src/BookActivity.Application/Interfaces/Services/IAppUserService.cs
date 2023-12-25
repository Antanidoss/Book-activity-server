using Ardalis.Result;
using BookActivity.Application.Models;
using BookActivity.Application.Models.Dto.Create;
using BookActivity.Application.Models.Dto.Update;
using BookActivity.Shared.Models;
using FluentValidation.Results;
using System;
using System.Threading.Tasks;

namespace BookActivity.Application.Interfaces.Services
{
    public interface IAppUserService
    {
        Task<ValidationResult> AddAsync(CreateAppUserDto appUserCreateDto);
        Task<ValidationResult> SubscribeAsync(Guid currentUserId, Guid subscribedUserId);
        Task<Result<AuthenticationResult>> AuthenticationAsync(AuthenticationModel authenticationModel);
        Task<ValidationResult> UpdateAsync(UpdateAppUserDto updateAppUserModel);
        Task<ValidationResult> UnsubscribeAsync(Guid currentUserId, Guid unsubscribedUserId);
        Task<CurrentUser> GetCurrentUserByIdAsync(Guid userId);
    }
}
