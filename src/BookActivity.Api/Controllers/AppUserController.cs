using BookActivity.Api.Attributes;
using BookActivity.Api.Common.Constants;
using BookActivity.Api.Common.Extansions;
using BookActivity.Api.Common.Models;
using BookActivity.Application.Interfaces.Services;
using BookActivity.Application.Models;
using BookActivity.Application.Models.Dto.Create;
using BookActivity.Application.Models.Dto.Update;
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
        [DtoValidator]
        public async Task<IActionResult> AddAsync([FromForm] CreateUserDto createUserModel)
        {
            return (await _appUserService.AddAsync(createUserModel)).ToActionResult();
        }

        [HttpPost(ApiConstants.AuthenticationMethod)]
        public async Task<ApiResult<AuthenticationResult>> AuthenticationAsync([FromBody] AuthenticationModel authenticationModel)
        {
            return (await _appUserService.AuthenticationAsync(authenticationModel)).ToApiResult();
        }

        [HttpPut(ApiConstants.SubscribeAppUserMethod)]
        [Authorize]
        public async Task<ActionResult> SubscribeAsync([FromQuery] Guid subscribedUserId)
        {
            return (await _appUserService.SubscribeAsync(_currentUser.Id, subscribedUserId)).ToActionResult();
        }

        [HttpGet(ApiConstants.GetCurrentUserMethod)]
        public ApiResult<CurrentUser> GetCurrentUserAsync()
        {
            return _currentUser.ToApiResult();
        }

        [HttpPut(ApiConstants.UpdateUserMethod)]
        [Authorize]
        [DtoValidator]
        public async Task<ActionResult> UpdateAsync([FromForm] UpdateUserDto updateUserModel)
        {
            updateUserModel.UserId = _currentUser.Id;

            return (await _appUserService.UpdateAsync(updateUserModel)).ToActionResult();
        }

        [HttpDelete(ApiConstants.UnsubscribeAppUserMethod)]
        [Authorize]
        public async Task<ActionResult> UnsubscribeAsync([FromQuery] Guid unsubscribedUserId)
        {
            return (await _appUserService.UnsubscribeAsync(_currentUser.Id, unsubscribedUserId)).ToActionResult();
        }
    }
}
