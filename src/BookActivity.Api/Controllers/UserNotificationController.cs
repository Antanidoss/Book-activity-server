using BookActivity.Api.Attributes;
using BookActivity.Api.Common.Constants;
using BookActivity.Application.Interfaces.Services;
using BookActivity.Application.Models.Dto.Read;
using BookActivity.Shared;
using Microsoft.AspNetCore.Mvc;

namespace BookActivity.Api.Controllers
{
    [Route(ApiConstants.UserNotificationService)]
    [Authorize]
    public class UserNotificationController : BaseController
    {
        private readonly IUserNotificationService _userNotificationService;

        public UserNotificationController(IUserNotificationService userNotificationService, CurrentUser currentUser) : base(currentUser)
        {
            _userNotificationService = userNotificationService;
        }

        [HttpGet(ApiConstants.GetUserNotificationsMethod)]
        public async Task<IEnumerable<UserNotificationDto>> GetUserNotificationsAsync()
        {
            return await _userNotificationService.GetUserNotificationsAsync(_currentUser.Id);
        }

        [HttpDelete(ApiConstants.RemoveUserNotificationsMethod)]
        public async Task RemoveUserNotifications(IEnumerable<Guid> userNotificationIds)
        {
            await _userNotificationService.RemoveUserNotifications(userNotificationIds);
        }
    }
}
