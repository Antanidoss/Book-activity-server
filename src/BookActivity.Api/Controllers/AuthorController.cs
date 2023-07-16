using BookActivity.Api.Common.Constants;
using BookActivity.Api.Common.Extansions;
using BookActivity.Api.Common.Models;
using BookActivity.Application.Interfaces.Services;
using BookActivity.Application.Models.Dto.Read;
using BookActivity.Application.Models.Dto.Create;
using Microsoft.AspNetCore.Mvc;

namespace BookActivity.Api.Controllers
{
    [Route(ApiConstants.AuthorService)]
    public sealed class AuthorController : BaseController
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpPost(ApiConstants.AddAuthorMethod)]
        public async Task<ApiResult<Guid>> AddAuthorAsync(CreateAuthorDto createAuthor)
        {
            return (await _authorService.AddAsync(createAuthor)).ToApiResult();
        }

        [HttpGet(ApiConstants.GetAuthorByNameMethod)]
        public async Task<ApiResult<IEnumerable<AuthorDto>>> GetAuthorByNameAsync(string name, int take)
        {
            return (await _authorService.GetAuthorsByNameAsync(name, take)).ToApiResult();
        }
    }
}
