using BookActivity.Api.Attributes;
using BookActivity.Api.Common.Constants;
using BookActivity.Application.Interfaces.Services;
using BookActivity.Application.Models.Dto.Read;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookActivity.Api.Controllers
{
    [Route(ApiConstants.UserNotificationService)]
    [Authorize]
    public class UserNotificationController : Controller
    {
        private readonly IUserNotificationService _userNotificationService;

        public UserNotificationController(IUserNotificationService userNotificationService)
        {
            _userNotificationService = userNotificationService;
        }

        [HttpGet(ApiConstants.GetUserNotifications)]
        public async Task<IEnumerable<UserNotificationDto>> GetUserNotificationsAsync([FromServices] AppUserDto currentUser)
        {
            return await _userNotificationService.GetUserNotificationsAsync(currentUser.Id);
        }
    }
}
