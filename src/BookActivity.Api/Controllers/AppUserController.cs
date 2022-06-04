using BookActivity.Api.Common.Constants;
using BookActivity.Api.Common.Extension;
using BookActivity.Api.Extansions;
using BookActivity.Api.Models;
using BookActivity.Application.Interfaces;
using BookActivity.Application.Models;
using BookActivity.Application.Models.DTO.Create;
using BookActivity.Application.Models.DTO.Read;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BookActivity.Api.Controllers
{
    [Route(ApiConstants.AppUserService)]
    public class AppUserController : BaseController
    {
        private readonly IAppUserService _appUserService;

        public AppUserController(IAppUserService appUserService, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _appUserService = appUserService;
        }

        [HttpPost(ApiConstants.AddUser)]
        public async Task<IActionResult> AddAppUserAsync([FromBody] AppUserCreateDTO appUserCreateDTO)
        {
            return (await _appUserService.AddAsync(appUserCreateDTO)).ToActionResult();
        }

        [HttpGet(ApiConstants.GetUserById)]
        public async Task<AppUserDTO> GetAppUserByIdAsync(Guid appUserId)
        {
            return await _appUserService.FindByIdAsync(appUserId);
        }

        [HttpPost(ApiConstants.Authentication)]
        public async Task<ApiResult<AuthenticationResult>> AuthenticationAsync([FromBody] AuthenticationModel authenticationModel)
        {
            var result =  (await _appUserService.PasswordSignInAsync(authenticationModel));

            return result.ToApiResult();
        }

        [HttpPut(ApiConstants.SubscribeAppUser)]
        public async Task<ActionResult> SubscribeAppUserAsync(Guid subscribedUserId)
        {
            var currentUser = GetCurrentUser();

            return (await _appUserService.SubscribeAppUserCommand(currentUser.Id, subscribedUserId)).ToActionResult();
        }
    }
}
