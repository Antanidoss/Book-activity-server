using BookActivity.Api.Common.Constants;
using BookActivity.Application.Interfaces;
using BookActivity.Application.Models.DTO.Filters;
using BookActivity.Application.Models.DTO.Read;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookActivity.Api.Controllers
{
    [Route(ApiConstants.ActiveBookService)]
    public sealed class ActiveBookController : BaseController
    {
        private readonly IActiveBookService _activeBookService;

        public ActiveBookController(IActiveBookService activeBookService, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _activeBookService = activeBookService;
        }

        [HttpGet(ApiConstants.GetActiveBooksByIdsMethod)]
        public async Task<IEnumerable<ActiveBookDTO>> GetaActiveBooksByIdsAsync(Guid[] activeBookIds)
        {
            return await _activeBookService.GetByFilterAsync(new ActiveBookDTOFilterModel { ActiveBookIds = activeBookIds, Take = activeBookIds.Length });
        }

        [HttpGet(ApiConstants.GetActiveBooksByUserIdMethod)]
        public async Task<IEnumerable<ActiveBookDTO>> GetaActiveBooksByIdsAsync(Guid userId, int? skip = 0, int? take = 1)
        {
            return await _activeBookService.GetByFilterAsync(new ActiveBookDTOFilterModel { UserId = userId, Skip = skip, Take = take });
        }
    }
}