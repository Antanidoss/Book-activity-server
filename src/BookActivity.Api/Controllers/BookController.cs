using BookActivity.Api.Common.Constants;
using BookActivity.Application.Interfaces;
using BookActivity.Application.Models.DTO.Create;
using BookActivity.Application.Models.DTO.Filters;
using BookActivity.Application.Models.DTO.Read;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
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

        [HttpGet(ApiConstants.GetBooksMethod)]
        public async Task<BookDTO> GetBookSAsync(BookDTOFilterModel filterModel)
        {
            return (await _bookService.GetByFilterAsync(filterModel)).FirstOrDefault();
        }
    }
}
