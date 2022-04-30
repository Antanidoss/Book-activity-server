using BookActivity.Api.Common.Constants;
using BookActivity.Application.Interfaces;
using BookActivity.Application.Models.DTO.Read;
using BookActivity.Domain.Filters.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookActivity.Api.Controllers
{
    [Route(ApiConstants.ActiveBookService)]
    public sealed class ActiveBookController : Controller
    {
        private readonly IActiveBookService _activeBookService;

        public ActiveBookController(IActiveBookService activeBookService)
        {
            _activeBookService = activeBookService;
        }

        [HttpGet(ApiConstants.GetActiveBooksMethod)]
        public async Task<IEnumerable<ActiveBookDTO>> GetaActiveBooks(ActiveBookFilterModel activeBookFilterModel)
        {
            return await _activeBookService.GetByFilterAsync(activeBookFilterModel);
        }
    }
}