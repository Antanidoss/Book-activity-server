using BookActivity.Api.Attributes;
using BookActivity.Api.Common.Constants;
using BookActivity.Application.Interfaces.Services;
using BookActivity.Application.Models.Dto.Read;
using BookActivity.Domain.Queries.ActiveBookStatisticQueries;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookActivity.Api.Controllers
{
    [Route(ApiConstants.ActiveBookStatisticService)]
    [Authorize]
    public sealed class ActiveBookStatisticController : Controller
    {
        private readonly IActiveBookStatisticService _activeBookStatisticService;

        public ActiveBookStatisticController(IActiveBookStatisticService activeBookStatisticService)
        {
            _activeBookStatisticService = activeBookStatisticService;
        }

        [HttpGet(ApiConstants.GetActiveBooksStaticMethod)]
        public async Task<ActiveBooksStatistic> GetActiveBooksStatistics([FromServices] AppUserDto currentUser)
        {
            return await _activeBookStatisticService.GetActiveBookStatistics(currentUser.Id);
        }
    }
}
