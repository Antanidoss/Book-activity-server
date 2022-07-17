using BookActivity.Api.Common.Constants;
using BookActivity.Api.Common.Extansions;
using BookActivity.Api.Common.Models;
using BookActivity.Application.Interfaces.Services;
using BookActivity.Application.Models;
using BookActivity.Application.Models.DTO.Create;
using BookActivity.Application.Models.DTO.Read;
using BookActivity.Application.Models.DTO.Update;
using BookActivity.Domain.FilterModels;
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
        public async Task<ApiResult<IEnumerable<BookDTO>>> GetBooksByIdsAsync(Guid[] bookIds)
        {
            return (await _bookService.GetByBookIdsAsync(bookIds)).ToApiResult();
        }

        [HttpGet(ApiConstants.GetBooksByTitleContainsMethod)]
        public async Task<ApiResult<IEnumerable<BookDTO>>> GetBooksByTitlesAsync(PaginationModel paginationModel, string title)
        {
            return (await _bookService.GetByTitleContainsAsync(paginationModel, title)).ToApiResult();
        }

        [HttpDelete(ApiConstants.RemoveBookMethod)]
        public async Task<ActionResult> RemoveBookAsync(Guid bookId)
        {
            return (await _bookService.RemoveActiveBookAsync(bookId)).ToActionResult();
        }

        [HttpPut(ApiConstants.UpdateBookMethod)]
        public async Task<ActionResult> UpdateBookAsync(UpdateBookDTO updateBookModel)
        {
            return (await _bookService.UpdateBookAsync(updateBookModel)).ToActionResult();
        }
    }
}
