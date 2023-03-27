using BookActivity.Api.Attributes;
using BookActivity.Api.Common.Constants;
using BookActivity.Application.Interfaces.Services;
using BookActivity.Application.Models.Dto.Read;
using BookActivity.Application.Models.Dto.Update;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookActivity.Api.Controllers
{
    [Route(ApiConstants.BookRatingService)]
    [Authorize]
    public class BookRatingController : Controller
    {
        private readonly IBookRatingService _bookRatingService;

        public BookRatingController(IBookRatingService bookRatingService)
        {
            _bookRatingService = bookRatingService;
        }

        [HttpPut(ApiConstants.UpdateBookRatingMethod)]
        public async Task UpdateBookRatingAsync([FromBody] UpdateBookRatingDto updateBookRating, [FromServices] AppUserDto currentUser)
        {
            updateBookRating.BookOpinion.UserId = currentUser.Id;

            await _bookRatingService.UpdateBookRatingAsync(updateBookRating).ConfigureAwait(false);
        }
    }
}
