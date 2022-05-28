using Ardalis.Result;
using BookActivity.Api.Common.Constants;
using BookActivity.Api.Common.Extension;
using BookActivity.Api.Extansions;
using BookActivity.Api.Models;
using BookActivity.Application.Interfaces;
using BookActivity.Application.Models;
using BookActivity.Application.Models.DTO.Create;
using BookActivity.Application.Models.DTO.Read;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BookActivity.Api.Controllers
{
    [Route(ApiConstants.AppUserService)]
    public class AppUserController
    {
        private readonly IAppUserService _appUserService;

        public AppUserController(IAppUserService appUserService)
        {
            _appUserService = appUserService;
        }

        [HttpPost(ApiConstants.AddUser)]
        public async Task<IActionResult> AddAppUser([FromBody] AppUserCreateDTO appUserCreateDTO)
        {
            return (await _appUserService.AddAsync(appUserCreateDTO)).ToActionResult();
        }

        [HttpGet(ApiConstants.GetUserById)]
        public async Task<AppUserDTO> GetAppUserById(Guid appUserId)
        {
            return await _appUserService.FindByIdAsync(appUserId);
        }

        [HttpPost(ApiConstants.Authentication)]
        public async Task<ApiResult<AuthenticationResult>> Authentication([FromBody] AuthenticationModel authenticationModel)
        {
            return (await _appUserService.PasswordSignInAsync(authenticationModel)).ToApiResult();
        }
    }
}
