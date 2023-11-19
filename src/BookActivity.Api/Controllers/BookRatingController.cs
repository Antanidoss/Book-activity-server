using BookActivity.Api.Attributes;
using BookActivity.Api.Common.Constants;
using BookActivity.Application.Interfaces.Services;
using BookActivity.Application.Models.Dto.Update;
using BookActivity.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookActivity.Api.Controllers
{
    [Route(ApiConstants.BookRatingService)]
    [Authorize]
    public class BookRatingController : BaseController
    {
        private readonly IBookRatingService _bookRatingService;

        public BookRatingController(IBookRatingService bookRatingService, CurrentUser currentUser) : base(currentUser)
        {
            _bookRatingService = bookRatingService;
        }

        [HttpPut(ApiConstants.UpdateBookRatingMethod)]
        [DtoValidator]
        public async Task UpdateBookRatingAsync([FromBody] UpdateBookRatingDto updateBookRating)
        {
            updateBookRating.UserId = _currentUser.Id;

            await _bookRatingService.UpdateBookRatingAsync(updateBookRating);
        }
    }
}
