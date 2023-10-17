using BookActivity.Api.Attributes;
using BookActivity.Api.Common.Constants;
using BookActivity.Api.Common.Extansions;
using BookActivity.Api.Common.Models;
using BookActivity.Application.Interfaces.Services;
using BookActivity.Domain.Queries.ActiveBookStatisticQueries.GetActiveBooksStatistic;
using BookActivity.Domain.Queries.ActiveBookStatisticQueries.GetActiveBooksStatisticByDay;
using BookActivity.Shared.Models;
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
        public async Task<ActiveBooksStatistic> GetActiveBooksStatisticsAsync(Guid? userId)
        {
            return await _activeBookStatisticService.GetActiveBookStatisticsAsync(userId ?? _currentUser.Id);
        }

        [HttpGet(ApiConstants.GetActiveBooksStatisticByDayMethod)]
        public async Task<ApiResult<IEnumerable<ActiveBookStatisticByDay>>> GetActiveBooksStatisticByDayAsync(DateTime day, Guid? userId)
        {
            return (await _activeBookStatisticService.GetActiveBookStatisticByDayAsync(day, userId ?? _currentUser.Id)).ToApiResult();
        }
    }
}
