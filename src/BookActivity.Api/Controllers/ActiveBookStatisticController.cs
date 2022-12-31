using BookActivity.Api.Attributes;
using BookActivity.Api.Common.Constants;
using BookActivity.Application.Interfaces.Services;
using BookActivity.Domain.Queries.ActiveBookStatisticQueries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BookActivity.Api.Controllers
{
    [Route(ApiConstants.ActiveBookStatisticService)]
    [Authorize]
    public sealed class ActiveBookStatisticController : BaseController
    {
        private readonly IActiveBookStatisticService _activeBookStatisticService;

        public ActiveBookStatisticController(IActiveBookStatisticService activeBookStatisticService, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _activeBookStatisticService = activeBookStatisticService;
        }

        [HttpGet(ApiConstants.GetActiveBooksStaticMethod)]
        public async Task<ActiveBooksStatistic> GetActiveBooksStatistics()
        {
            return await _activeBookStatisticService.GetActiveBookStatistics(Guid.Parse("4B8796DD-321B-45E3-3155-08DAAF5BF715") /*GetCurrentUser().Id*/);
        }
    }
}
