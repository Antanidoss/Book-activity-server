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
    public class UserNotificationController : BaseController
    {
        private readonly IUserNotificationService _userNotificationService;

        public UserNotificationController(IUserNotificationService userNotificationService, [FromServices] AppUserDto currentUser) : base(currentUser)
        {
            _userNotificationService = userNotificationService;
        }

        [HttpGet(ApiConstants.GetUserNotifications)]
        public async Task<IEnumerable<UserNotificationDto>> GetUserNotificationsAsync()
        {
            return await _userNotificationService.GetUserNotificationsAsync(_currentUser.Id);
        }
    }
}
