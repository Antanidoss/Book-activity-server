using BookActivity.Api.Common.Constants;
using BookActivity.Application.Interfaces;
using BookActivity.Application.Models.DTO.Read;
using BookActivity.Application.Models.Filters;
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

        [HttpGet(ApiConstants.GetActiveBooksMethod)]
        public async Task<IEnumerable<ActiveBookDTO>> GetaActiveBooks(Guid userId, int skip, int take)
        {
            return await _activeBookService.GetByFilterAsync(new ActiveBookFilterModel(skip, take, a => a.UserId == userId));
        }
    }
}