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
using BookActivity.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BookActivity.Api.Controllers
{
    [Route(ApiConstants.AppUserService)]
    public sealed class AppUserController : BaseController
    {
        private readonly IAppUserService _appUserService;

        public AppUserController(IAppUserService appUserService, [FromServices] AppUserDto currentUser = null) : base(currentUser)
        {
            _appUserService = appUserService;
        }

        [HttpPost(ApiConstants.AddUserMethod)]
        public async Task<IActionResult> AddAppUserAsync([FromForm] CreateAppUserDto appUserCreateDTO)
        {
            return (await _appUserService.AddAsync(appUserCreateDTO)).ToActionResult();
        }

        [HttpGet(ApiConstants.GetUserByIdMethod)]
        public async Task<ApiResult<AppUserDto>> GetAppUserByIdAsync(Guid appUserId)
        {
            return (await _appUserService.FindByIdAsync(appUserId)).ToApiResult();
        }

        [HttpPost(ApiConstants.AuthenticationMethod)]
        public async Task<ApiResult<AuthenticationResult>> AuthenticationAsync([FromBody] AuthenticationModel authenticationModel)
        {
            return (await _appUserService.AuthenticationAsync(authenticationModel)).ToApiResult();
        }

        [HttpPut(ApiConstants.SubscribeAppUserMethod)]
        public async Task<ActionResult> SubscribeAppUserAsync([FromQuery] Guid subscribedUserId)
        {
            return (await _appUserService.SubscribeAppUserAsync(_currentUser.Id, subscribedUserId)).ToActionResult();
        }

        [HttpGet(ApiConstants.GetCurrentUserMethod)]
        public ApiResult<AppUserDto> GetCurrentUserAsync()
        {
            return _currentUser.ToApiResult();
        }

        [HttpPut(ApiConstants.UpdateUserMethod)]
        public async Task<ActionResult> UpdateUserAsync([FromForm] UpdateAppUserDto updateAppUserModel)
        {
            return (await _appUserService.UpdateAsync(updateAppUserModel)).ToActionResult();
        }

        [HttpGet(ApiConstants.GetUserByFilterMethod)]
        public async Task<EntityListResult<SelectedAppUser>> GetUsersByFilterAsync(GetUsersByFilterDto filterModel)
        {
            return await _appUserService.GetAppUserByFilter(filterModel);
        }

        [HttpDelete(ApiConstants.UnsubscribeAppUserMethod)]
        public async Task<ActionResult> UnsubscribeUserAsync([FromQuery] Guid unsubscribedUserId)
        {
            return (await _appUserService.UnsubscribeAppUserAsync(_currentUser.Id, unsubscribedUserId)).ToActionResult();
        }

        [HttpGet(ApiConstants.GetUserProfileInfoMethod)]
        public async Task<AppUserProfileInfo> GetUserProfileInfoAsync([FromQuery] Guid userId)
        {
            return await _appUserService.GetUserProfileInfoAsync(userId);
        }
    }
}
