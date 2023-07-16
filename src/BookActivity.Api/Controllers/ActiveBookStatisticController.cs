using BookActivity.Api.Attributes;
using BookActivity.Api.Common.Constants;
using BookActivity.Application.Interfaces.Services;
using BookActivity.Domain.Queries.ActiveBookStatisticQueries;
using BookActivity.Shared;
using Microsoft.AspNetCore.Mvc;

namespace BookActivity.Api.Controllers
{
    [Route(ApiConstants.ActiveBookStatisticService)]
    [Authorize]
    public sealed class ActiveBookStatisticController : BaseController
    {
        private readonly IActiveBookStatisticService _activeBookStatisticService;

        public ActiveBookStatisticController(IActiveBookStatisticService activeBookStatisticService, CurrentUser currentUser) : base(currentUser)
        {
            _activeBookStatisticService = activeBookStatisticService;
        }

        [HttpGet(ApiConstants.GetActiveBooksStaticMethod)]
        public async Task<ActiveBooksStatistic> GetActiveBooksStatistics()
        {
            return await _activeBookStatisticService.GetActiveBookStatistics(_currentUser.Id);
        }
    }
}
