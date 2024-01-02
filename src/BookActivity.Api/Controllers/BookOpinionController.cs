using BookActivity.Api.Attributes;
using BookActivity.Api.Common.Constants;
using BookActivity.Api.Common.Extansions;
using BookActivity.Application.Interfaces.Services;
using BookActivity.Application.Models.Dto.Update;
using BookActivity.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookActivity.Api.Controllers
{
    [Route(ApiConstants.BookRatingService)]
    [Authorize]
    public class BookOpinionController : BaseController
    {
        private readonly IBookOpinionService _bookOpinionService;

        public BookOpinionController(IBookOpinionService bookOpinionService, CurrentUser currentUser) : base(currentUser)
        {
            _bookOpinionService = bookOpinionService;
        }

        [HttpPost(ApiConstants.AddBookOpinionMethod)]
        [DtoValidator]
        public async Task AddBookOpinionAsync([FromBody] AddBookOpinionDto addBookOpinionDto)
        {
            addBookOpinionDto.UserId = _currentUser.Id;

            await _bookOpinionService.AddBookOpinionAsync(addBookOpinionDto);
        }

        [HttpPost(ApiConstants.AddBookOpinionDislikeMethod)]
        public async Task<ActionResult> AddDislikeAsync(Guid bookId, Guid userIdOpinion, [FromServices] CurrentUser currentUser)
        {
            return (await _bookOpinionService.AddDislikeAsync(bookId, currentUser.Id, userIdOpinion)).ToActionResult();
        }

        [HttpPost(ApiConstants.AddBookOpinionLikeMethod)]
        public async Task<ActionResult> AddLikeAsync(Guid bookId, Guid userIdOpinion, [FromServices] CurrentUser currentUser)
        {
            return (await _bookOpinionService.AddLikeAsync(bookId, currentUser.Id, userIdOpinion)).ToActionResult();
        }

        [HttpDelete(ApiConstants.RemoveBookOpinionDislikeMethod)]
        public async Task<ActionResult> RemoveDislikeAsync(Guid bookId, Guid userIdOpinion, [FromServices] CurrentUser currentUser)
        {
            return (await _bookOpinionService.RemoveDislikeAsync(bookId, currentUser.Id, userIdOpinion)).ToActionResult();
        }

        [HttpDelete(ApiConstants.RemoveBookOpinionLikeMethod)]
        public async Task<ActionResult> RemoveLikeAsync(Guid bookId, Guid userIdOpinion, [FromServices] CurrentUser currentUser)
        {
            return (await _bookOpinionService.RemoveLikeAsync(bookId, currentUser.Id, userIdOpinion)).ToActionResult();
        }
    }
}
