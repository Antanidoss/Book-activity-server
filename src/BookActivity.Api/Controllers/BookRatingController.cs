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
    public class BookRatingController : BaseController
    {
        private readonly IBookRatingService _bookRatingService;

        public BookRatingController(IBookRatingService bookRatingService, [FromServices] AppUserDto currentUser) : base(currentUser)
        {
            _bookRatingService = bookRatingService;
        }

        [HttpPut(ApiConstants.UpdateBookRatingMethod)]
        public async Task UpdateBookRatingAsync([FromBody] UpdateBookRatingDto updateBookRating)
        {
            updateBookRating.BookOpinion.UserId = _currentUser.Id;

            await _bookRatingService.UpdateBookRatingAsync(updateBookRating);
        }
    }
}
