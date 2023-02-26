using Ardalis.Result;
using BookActivity.Application.Models;
using BookActivity.Application.Models.Dto.Create;
using BookActivity.Application.Models.Dto.Read;
using BookActivity.Application.Models.Dto.Update;
using BookActivity.Domain.Filters.Models;
using BookActivity.Domain.Queries.AppUserQueries.GetUserProfileInfo;
using BookActivity.Domain.Queries.AppUserQueries.GetUsersByFilter;
using BookActivity.Shared.Models;
using FluentValidation.Results;
using System;
using System.Threading.Tasks;

namespace BookActivity.Application.Interfaces.Services
{
    public interface IAppUserService
    {
        Task<ValidationResult> AddAsync(CreateAppUserDto appUserCreateDTO);
        Task<ValidationResult> SubscribeAppUserAsync(Guid currentUserId, Guid subscribedUserId);
        Task<Result<AuthenticationResult>> AuthenticationAsync(AuthenticationModel authenticationModel);
        Task<Result<AppUserDto>> FindByIdAsync(Guid appUserId);
        Task<EntityListResult<SelectedAppUser>> GetAppUserByFilter(GetUsersByFilterDto filterModel);
        Task<ValidationResult> UpdateAsync(UpdateAppUserDto updateAppUserModel);
        Task<ValidationResult> UnsubscribeAppUserAsync(Guid currentUserId, Guid unsubscribedUserId);
        Task<AppUserProfileInfo> GetUserProfileInfoAsync(Guid userId);
    }
}
