using BookActivity.Api.Common.Constants;
using BookActivity.Api.Common.Extansions;
using BookActivity.Api.Common.Models;
using BookActivity.Application.Interfaces.Services;
using BookActivity.Application.Models.Dto.Create;
using BookActivity.Application.Models.Dto.Read;
using BookActivity.Application.Models.Dto.Update;
using BookActivity.Application.Models.HistoryData;
using BookActivity.Shared.Models;
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

        public BookController(IBookService bookService, [FromServices] AppUserDto currentUser = null) : base(currentUser)
        {
            _bookService = bookService;
        }

        [HttpPost(ApiConstants.AddActiveBookMethod)]
        public async Task AddBookAsync([FromForm] CreateBookDto createBookModel)
        {
            await _bookService.AddActiveBookAsync(createBookModel);
        }

        [HttpGet(ApiConstants.GetBooksByIdsMethod)]
        public async Task<ApiResult<IEnumerable<BookDto>>> GetBooksByIdsAsync(Guid[] bookIds)
        {
            return (await _bookService.GetByBookIdsAsync(bookIds)
                .ConfigureAwait(false))
                .ToApiResult();
        }

        [HttpGet(ApiConstants.GetBooksByFilterMethod)]
        public async Task<ApiResult<EntityListResult<BookDto>>> GetBooksByFilterAsync(GetBooksByFilterDto bookFilterModel)
        {
            bookFilterModel.UserId = _currentUser?.Id ?? Guid.Empty;

            return (await _bookService.GetByFilterAsync(bookFilterModel)
                .ConfigureAwait(false))
                .ToApiResult();
        }

        [HttpDelete(ApiConstants.RemoveBookMethod)]
        public async Task<ActionResult> RemoveBookAsync(Guid bookId)
        {
            return (await _bookService.RemoveActiveBookAsync(bookId, _currentUser.Id)
                .ConfigureAwait(false))
                .ToActionResult();
        }

        [HttpPut(ApiConstants.UpdateBookMethod)]
        public async Task<ActionResult> UpdateBookAsync(UpdateBookDto updateBookModel)
        {
            return (await _bookService.UpdateBookAsync(updateBookModel)
                .ConfigureAwait(false))
                .ToActionResult();
        }

        [HttpGet(ApiConstants.GetBookHistoryDataMethod)]
        public async Task<ApiResult<IEnumerable<BookHistoryData>>> GetBookHistoryDataAsync(Guid bookId)
        {
            return (await _bookService.GetBookHistoryDataAsync(bookId)
                .ConfigureAwait(false))
                .ToApiResult();
        }
    }
}
