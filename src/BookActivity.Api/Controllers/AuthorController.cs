using BookActivity.Api.Common.Constants;
using BookActivity.Api.Common.Extansions;
using BookActivity.Api.Common.Models;
using BookActivity.Application.Interfaces.Services;
using BookActivity.Application.Models.Dto.Read;
using BookActivity.Application.Models.DTO.Create;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookActivity.Api.Controllers
{
    [Route(ApiConstants.AuthorService)]
    public sealed class AuthorController : BaseController
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _authorService = authorService;
        }

        [HttpPost(ApiConstants.AddAuthorMethod)]
        public async Task<ApiResult<Guid>> AddAuthorAsync(CreateAuthorDto createAuthor)
        {
            return (await _authorService.AddAsync(createAuthor)
                .ConfigureAwait(false))
                .ToApiResult();
        }

        [HttpGet(ApiConstants.GetAuthorByNameMethod)]
        public async Task<ApiResult<IEnumerable<AuthorDto>>> GetAuthorByNameAsync(string name, int take)
        {
            return (await _authorService.GetAuthorsByNameAsync(name, take)
                .ConfigureAwait(false))
                .ToApiResult();
        }

        [HttpGet(ApiConstants.GetAllAuthorsMethod)]
        public async Task<ApiResult<IEnumerable<AuthorDto>>> GetAllAuthorsAsync()
        {
            return (await _authorService.GetAllAuthorsAsync()
                .ConfigureAwait(false))
                .ToApiResult();
        }
    }
}
