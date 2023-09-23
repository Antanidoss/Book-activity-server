using BookActivity.Api.Common.Constants;
using BookActivity.Api.Common.Extansions;
using BookActivity.Api.Common.Models;
using BookActivity.Application.Interfaces.Services;
using BookActivity.Application.Models;
using BookActivity.Application.Models.Dto.Create;
using BookActivity.Application.Models.Dto.Read;
using BookActivity.Application.Models.Dto.Update;
using BookActivity.Domain.Filters.Models;
using BookActivity.Shared;
using BookActivity.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookActivity.Api.Controllers
{
    [Route(ApiConstants.AppUserService)]
    public sealed class AppUserController : BaseController
    {
        private readonly IAppUserService _appUserService;

        public AppUserController(IAppUserService appUserService, CurrentUser currentUser = null) : base(currentUser)
        {
            _appUserService = appUserService;
        }

        [HttpPost(ApiConstants.AddUserMethod)]
        public async Task<IActionResult> AddAsync([FromForm] CreateAppUserDto appUserCreateDTO)
        {
            return (await _appUserService.AddAsync(appUserCreateDTO)).ToActionResult();
        }

        [HttpPost(ApiConstants.AuthenticationMethod)]
        public async Task<ApiResult<AuthenticationResult>> AuthenticationAsync([FromBody] AuthenticationModel authenticationModel)
        {
            return (await _appUserService.AuthenticationAsync(authenticationModel)).ToApiResult();
        }

        [HttpPut(ApiConstants.SubscribeAppUserMethod)]
        public async Task<ActionResult> SubscribeAppUserAsync([FromQuery] Guid subscribedUserId)
        {
            return (await _appUserService.SubscribeAsync(_currentUser.Id, subscribedUserId)).ToActionResult();
        }

        [HttpGet(ApiConstants.GetCurrentUserMethod)]
        public ApiResult<CurrentUser> GetCurrentUserAsync()
        {
            return _currentUser.ToApiResult();
        }

        [HttpPut(ApiConstants.UpdateUserMethod)]
        public async Task<ActionResult> UpdateAsync([FromForm] UpdateAppUserDto updateAppUserModel)
        {
            return (await _appUserService.UpdateAsync(updateAppUserModel)).ToActionResult();
        }

        [HttpGet(ApiConstants.GetUserByFilterMethod)]
        public async Task<EntityListResult<SelectedAppUser>> GetByFilterAsync(GetUsersByFilterDto filterModel)
        {
            return await _appUserService.GetByFilterAsync(filterModel);
        }

        [HttpDelete(ApiConstants.UnsubscribeAppUserMethod)]
        public async Task<ActionResult> UnsubscribeAsync([FromQuery] Guid unsubscribedUserId)
        {
            return (await _appUserService.UnsubscribeAsync(_currentUser.Id, unsubscribedUserId)).ToActionResult();
        }
    }
}
