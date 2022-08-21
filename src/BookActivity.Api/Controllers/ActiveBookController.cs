using BookActivity.Api.Attributes;
using BookActivity.Api.Common.Constants;
using BookActivity.Api.Common.Extansions;
using BookActivity.Api.Common.Models;
using BookActivity.Application.Interfaces.Services;
using BookActivity.Application.Models.DTO.Create;
using BookActivity.Application.Models.DTO.Read;
using BookActivity.Application.Models.DTO.Update;
using BookActivity.Application.Models.HistoryData;
using BookActivity.Domain.FilterModels;
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
        public async Task<ActionResult> AddActiveBookAsync(CreateActiveBookDTO createActiveBookModel)
        {
            createActiveBookModel.UserId = GetCurrentUser().Id;

            return (await _activeBookService.AddActiveBookAsync(createActiveBookModel).ConfigureAwait(false))
                .ToActionResult();
        }

        [HttpPut(ApiConstants.UpdateNumberPagesReadMethod)]
        public async Task<ActionResult> UpdateNumberPagesReadAsync(UpdateNumberPagesReadDTO updateActiveBookModel)
        {
            return (await _activeBookService.UpdateActiveBookAsync(updateActiveBookModel).ConfigureAwait(false))
                .ToActionResult();
        }

        [HttpDelete(ApiConstants.RemoveActiveBookMethod)]
        public async Task<ActionResult> RemoveActiveBookAsync(Guid activeBookId)
        {
            return (await _activeBookService.RemoveActiveBookAsync(activeBookId).ConfigureAwait(false))
                .ToActionResult();
        }

        [HttpGet(ApiConstants.GetActiveBooksByIdsMethod)]
        public async Task<ApiResult<IEnumerable<ActiveBookDTO>>> GetaActiveBooksByIdsAsync(Guid[] activeBookIds)
        {
            return (await _activeBookService.GetByActiveBookIdAsync(activeBookIds).ConfigureAwait(false))
                .ToApiResult();
        }

        [HttpGet(ApiConstants.GetActiveBooksByUserIdMethod)]
        public async Task<ApiResult<IEnumerable<ActiveBookDTO>>> GetaActiveBooksByUserIdsAsync(Guid userId, PaginationModel paginationModel)
        {
            return (await _activeBookService.GetByUserIdAsync(paginationModel, userId).ConfigureAwait(false))
                .ToApiResult();
        }

        [HttpGet(ApiConstants.GetActiveBooksByCurrentUserMethod)]
        public async Task<ApiResult<IEnumerable<ActiveBookDTO>>> GetActiveBooksByCurrentUserAsync(PaginationModel paginationModel)
        {
            var currentUserId = GetCurrentUser().Id;

            return (await _activeBookService.GetByUserIdAsync(paginationModel, currentUserId).ConfigureAwait(false))
                .ToApiResult();
        }

        [HttpGet(ApiConstants.GetActiveBookHistoryDataMethod)]
        public async Task<ApiResult<IEnumerable<ActiveBookHistoryData>>> GetActiveBookHistoryDataAsync(Guid activeBookId)
        {
            return (await _activeBookService.GetActiveBookHistoryDataAsync(activeBookId).ConfigureAwait(false))
                .ToApiResult();
        }
    }
}