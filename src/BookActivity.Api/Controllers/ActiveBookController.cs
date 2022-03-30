using BookActivity.Api.Common.Constants;
using BookActivity.Application.Interfaces;
using BookActivity.Application.Models.DTO.Read;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookActivity.Api.Controllers
{
    [Route(ApiConstants.ActiveBookService)]
    public class ActiveBookController : Controller
    {
        private readonly IActiveBookService _activeBookService;

        public ActiveBookController(IActiveBookService activeBookService)
        {
            _activeBookService = activeBookService;
        }

        [HttpGet(ApiConstants.GetaActiveBooksMethod)]
        public async Task<IEnumerable<ActiveBookDTO>> GetaActiveBooks(Guid userId)
        {
            return await _activeBookService.GetByFilterAsync(new Application.Models.Filters.ActiveBookFilterModel());
        }
    }
}