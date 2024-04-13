using BookActivity.Api.Common.Constants;
using BookActivity.Api.Common.Extansions;
using BookActivity.Api.Common.Models;
using BookActivity.Application.Interfaces.Services;
using BookActivity.Application.Models.Dto.Create;
using Microsoft.AspNetCore.Mvc;
using BookActivity.Api.Attributes;
using BookActivity.Shared.Constants;

namespace BookActivity.Api.Controllers
{
    [Route(ApiConstants.AuthorService)]
    [Authorize(RoleNamesConstants.Admin)]
    public sealed class AuthorController : BaseController
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpPost(ApiConstants.AddAuthorMethod)]
        [DtoValidator]
        public async Task<ApiResult<Guid>> AddAuthorAsync(CreateAuthorDto createAuthor)
        {
            return (await _authorService.AddAsync(createAuthor)).ToApiResult();
        }
    }
}
