using BookActivity.Application.Models.Dto.Read;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookActivity.Application.Interfaces.Services
{
    public  interface IUserNotificationService
    {
        Task<IEnumerable<UserNotificationDto>> GetUserNotificationsAsync(Guid userId);
    }
}
