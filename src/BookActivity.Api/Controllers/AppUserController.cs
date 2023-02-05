using BookActivity.Api.Common.Constants;
using BookActivity.Api.Common.Extansions;
using BookActivity.Api.Common.Models;
using BookActivity.Application.Interfaces.Services;
using BookActivity.Application.Models;
using BookActivity.Application.Models.Dto.Create;
using BookActivity.Application.Models.Dto.Read;
using BookActivity.Application.Models.Dto.Update;
using BookActivity.Domain.Filters.Models;
using BookActivity.Domain.Queries.AppUserQueries.GetUserProfileInfo;
using BookActivity.Domain.Queries.AppUserQueries.GetUsersByFilter;
using BookActivity.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BookActivity.Api.Controllers
{
    [Route(ApiConstants.AppUserService)]
    public sealed class AppUserController : BaseController
    {
        private readonly IAppUserService _appUserService;

        public AppUserController(IAppUserService appUserService, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _appUserService = appUserService;
        }

        [HttpPost(ApiConstants.AddUserMethod)]
        public async Task<IActionResult> AddAppUserAsync([FromForm] CreateAppUserDto appUserCreateDTO)
        {
            return (await _appUserService.AddAsync(appUserCreateDTO)
                .ConfigureAwait(false))
                .ToActionResult();
        }

        [HttpGet(ApiConstants.GetUserByIdMethod)]
        public async Task<ApiResult<AppUserDto>> GetAppUserByIdAsync(Guid appUserId)
        {
            return (await _appUserService.FindByIdAsync(appUserId)
                .ConfigureAwait(false))
                .ToApiResult();
        }

        [HttpPost(ApiConstants.AuthenticationMethod)]
        public async Task<ApiResult<AuthenticationResult>> AuthenticationAsync([FromBody] AuthenticationModel authenticationModel)
        {
            return (await _appUserService.AuthenticationAsync(authenticationModel)
                .ConfigureAwait(false))
                .ToApiResult();
        }

        [HttpPut(ApiConstants.SubscribeAppUserMethod)]
        public async Task<ActionResult> SubscribeAppUserAsync([FromQuery] Guid subscribedUserId)
        {
            var currentUser = GetCurrentUser();

            return (await _appUserService.SubscribeAppUserAsync(currentUser.Id, subscribedUserId)
                .ConfigureAwait(false))
                .ToActionResult();
        }

        [HttpGet(ApiConstants.GetCurrentUserMethod)]
        public ApiResult<AppUserDto> GetCurrentUserAsync()
        {
            return base.GetCurrentUser().ToApiResult();
        }

        [HttpPut(ApiConstants.UpdateUserMethod)]
        public async Task<ActionResult> UpdateUserAsync([FromForm] UpdateAppUserDto updateAppUserModel)
        {
            return (await _appUserService.UpdateAsync(updateAppUserModel)
                .ConfigureAwait(false))
                .ToActionResult();
        }

        [HttpGet(ApiConstants.GetUserByFilterMethod)]
        public async Task<EntityListResult<SelectedAppUser>> GetUsersByFilterAsync(GetUsersByFilterQuery filterModel)
        {
            return await _appUserService.GetAppUserByFilter(filterModel);
        }

        [HttpDelete(ApiConstants.UnsubscribeAppUserMethod)]
        public async Task<ActionResult> UnsubscribeUserAsync([FromQuery] Guid unsubscribedUserId)
        {
            var currentUser = GetCurrentUser();

            return (await _appUserService.UnsubscribeAppUserAsync(currentUser.Id, unsubscribedUserId)
                .ConfigureAwait(false))
                .ToActionResult();
        }

        [HttpGet(ApiConstants.GetUserProfileInfoMethod)]
        public async Task<AppUserProfileInfo> GetUserProfileInfoAsync([FromQuery] Guid userId)
        {
            return await _appUserService.GetUserProfileInfoAsync(userId);
        }
    }
}
