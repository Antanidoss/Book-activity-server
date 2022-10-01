using BookActivity.Api.Attributes;
using BookActivity.Api.Common.Constants;
using BookActivity.Application.Interfaces.Services;
using BookActivity.Application.Models.Dto.Update;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookActivity.Api.Controllers
{
    [Route(ApiConstants.BookRatingService)]
    [Authorize]
    public class BookRatingController : BaseController
    {
        private readonly IBookRatingService _bookRatingService;

        public BookRatingController(IBookRatingService bookRatingService, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _bookRatingService = bookRatingService;
        }

        [HttpPut(ApiConstants.UpdateBookRatingMethod)]
        public async Task UpdateBookRatingAsync(UpdateBookRatingDto updateBookRating)
        {
            updateBookRating.BookOpinion.UserId = GetCurrentUser().Id;

            await _bookRatingService.UpdateBookRatingAsync(updateBookRating).ConfigureAwait(false);
        }
    }
}
