using BookActivity.Api.Attributes;
using BookActivity.Api.Common.Constants;
using BookActivity.Api.Common.Extansions;
using BookActivity.Application.Interfaces.Services;
using BookActivity.Application.Models.Dto.Create;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookActivity.Api.Controllers
{
    [Route(ApiConstants.BookNoteService)]
    [Authorize]
    public sealed class BookNoteController : BaseController
    {
        private readonly IBookNoteService _bookNoteService;

        public BookNoteController(IBookNoteService bookNoteService)
        {
            _bookNoteService = bookNoteService;
        }

        [HttpPost(ApiConstants.AddBookNoteMethod)]
        public async Task<ActionResult> AddBookNote([FromBody] CreateBookNoteDto createBookNoteModel)
        {
            return (await _bookNoteService.AddBookNoteAsync(createBookNoteModel)
                .ConfigureAwait(false))
                .ToActionResult();
        }
    }
}
