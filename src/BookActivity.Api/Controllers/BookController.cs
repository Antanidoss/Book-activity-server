using BookActivity.Api.Common.Constants;
using BookActivity.Application.Interfaces;
using BookActivity.Application.Models.DTO.Create;
using BookActivity.Application.Models.DTO.Filters;
using BookActivity.Application.Models.DTO.Read;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookActivity.Api.Controllers
{
    [Route(ApiConstants.BookService)]
    public sealed class BookController : BaseController
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _bookService = bookService;
        }

        [HttpPost(ApiConstants.AddActiveBookMethod)]
        public async Task AddBookAsync(CreateBookDTO createBookModel)
        {
            await _bookService.AddActiveBookAsync(createBookModel);
        }

        [HttpGet(ApiConstants.GetBooksByIdsMethod)]
        public async Task<IEnumerable<BookDTO>> GetBooksByIdsAsync(Guid[] bookIds)
        {
            return await _bookService.GetByFilterAsync(new BookDTOFilterModel { BookIds = bookIds, Take = bookIds.Length});
        }

        [HttpGet(ApiConstants.GetBooksByTitleContainsMethod)]
        public async Task<IEnumerable<BookDTO>> GetBooksByIdsAsync(string title, int? skip = 0, int? take = 1)
        {
            return await _bookService.GetByFilterAsync(new BookDTOFilterModel { Title = title, Skip = skip, Take = take });
        }
    }
}
