using BookActivity.Api.Attributes;
using BookActivity.Api.Common.Constants;
using BookActivity.Api.Common.Extansions;
using BookActivity.Api.Common.Models;
using BookActivity.Application.Interfaces.Services;
using BookActivity.Application.Models.Dto.Create;
using BookActivity.Application.Models.Dto.Read;
using BookActivity.Application.Models.Dto.Update;
using BookActivity.Application.Models.HistoryData;
using BookActivity.Domain.Filters.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookActivity.Api.Controllers
{
    [Route(ApiConstants.ActiveBookService)]
    [Authorize]
    public sealed class ActiveBookController : BaseController
    {
        private readonly IActiveBookService _activeBookService;

        public ActiveBookController(IActiveBookService activeBookService, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _activeBookService = activeBookService;
        }

        [HttpPost(ApiConstants.AddActiveBookMethod)]
        public async Task<ApiResult<Guid>> AddActiveBookAsync([FromBody] CreateActiveBookDto createActiveBookModel)
        {
            createActiveBookModel.UserId = GetCurrentUser().Id;

            return (await _activeBookService.AddActiveBookAsync(createActiveBookModel)
                .ConfigureAwait(false))
                .ToApiResult();
        }

        [HttpPut(ApiConstants.UpdateNumberPagesReadMethod)]
        public async Task<ActionResult> UpdateNumberPagesReadAsync([FromBody] UpdateNumberPagesReadDto updateActiveBookModel)
        {
            return (await _activeBookService.UpdateActiveBookAsync(updateActiveBookModel)
                .ConfigureAwait(false))
                .ToActionResult();
        }

        [HttpDelete(ApiConstants.RemoveActiveBookMethod)]
        public async Task<ActionResult> RemoveActiveBookAsync([FromQuery] Guid activeBookId)
        {
            return (await _activeBookService.RemoveActiveBookAsync(activeBookId)
                .ConfigureAwait(false))
                .ToActionResult();
        }

        [HttpGet(ApiConstants.GetActiveBooksByIdsMethod)]
        public async Task<ApiResult<IEnumerable<ActiveBookDto>>> GetaActiveBooksByIdsAsync(Guid[] activeBookIds)
        {
            return (await _activeBookService.GetByActiveBookIdAsync(activeBookIds)
                .ConfigureAwait(false))
                .ToApiResult();
        }

        [HttpGet(ApiConstants.GetActiveBooksByUserIdMethod)]
        public async Task<ApiResult<IEnumerable<ActiveBookDto>>> GetaActiveBooksByUserIdsAsync(Guid userId, PaginationModel paginationModel)
        {
            return (await _activeBookService.GetByUserIdAsync(paginationModel, userId)
                .ConfigureAwait(false))
                .ToApiResult();
        }

        [HttpGet(ApiConstants.GetActiveBooksByCurrentUserMethod + "/{skip}/{take}")]
        public async Task<ApiResult<IEnumerable<ActiveBookDto>>> GetActiveBooksByCurrentUserAsync(int skip, int take)
        {
            var currentUserId = GetCurrentUser().Id;

            return (await _activeBookService.GetByUserIdAsync(new PaginationModel(skip, take), currentUserId)
                .ConfigureAwait(false))
                .ToApiResult();
        }

        [HttpGet(ApiConstants.GetActiveBookHistoryDataMethod)]
        public async Task<ApiResult<IEnumerable<ActiveBookHistoryData>>> GetActiveBookHistoryDataAsync(Guid activeBookId)
        {
            return (await _activeBookService.GetActiveBookHistoryDataAsync(activeBookId)
                .ConfigureAwait(false))
                .ToApiResult();
        }
    }
}