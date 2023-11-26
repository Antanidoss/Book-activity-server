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
    }
}
