using BookActivity.Api.Common.Constants;
using BookActivity.Api.Common.Extansions;
using BookActivity.Api.Common.Models;
using BookActivity.Application.Interfaces.Services;
using BookActivity.Application.Models.Dto.Create;
using BookActivity.Application.Models.Dto.Update;
using BookActivity.Application.Models.HistoryData;
using BookActivity.Shared;
using Microsoft.AspNetCore.Mvc;

namespace BookActivity.Api.Controllers
{
    [Route(ApiConstants.BookService)]
    public sealed class BookController : BaseController
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService, [FromServices] CurrentUser currentUser = null) : base(currentUser)
        {
            _bookService = bookService;
        }

        [HttpPost(ApiConstants.AddActiveBookMethod)]
        public async Task AddBookAsync([FromForm] CreateBookDto createBookModel)
        {
            await _bookService.AddActiveBookAsync(createBookModel);
        }

        [HttpDelete(ApiConstants.RemoveBookMethod)]
        public async Task<ActionResult> RemoveBookAsync(Guid bookId)
        {
            return (await _bookService.RemoveActiveBookAsync(bookId, _currentUser.Id)).ToActionResult();
        }

        [HttpPut(ApiConstants.UpdateBookMethod)]
        public async Task<ActionResult> UpdateBookAsync(UpdateBookDto updateBookModel)
        {
            return (await _bookService.UpdateBookAsync(updateBookModel)).ToActionResult();
        }

        [HttpGet(ApiConstants.GetBookHistoryDataMethod)]
        public async Task<ApiResult<IEnumerable<BookHistoryData>>> GetBookHistoryDataAsync(Guid bookId)
        {
            return (await _bookService.GetBookHistoryDataAsync(bookId)).ToApiResult();
        }
    }
}
