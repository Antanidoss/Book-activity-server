using AutoMapper;
using BookActivity.Application.Interfaces.Services;
using BookActivity.Application.Models.Dto.Read;
using BookActivity.Domain.Commands.UserNotificationCommands.RemoveUserNotifications;
using BookActivity.Domain.Interfaces;
using BookActivity.Domain.Specifications.UserNotificationSpecs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookActivity.Application.Implementation.Services
{
    internal sealed class UserNotificationService : IUserNotificationService
    {
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IDbContext _efContext;
        private readonly IMapper _mapper;

        public UserNotificationService(IMediatorHandler mediatorHandler, IDbContext efContext, IMapper mapper)
        {
            _mediatorHandler = mediatorHandler;
            _efContext = efContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserNotificationDto>> GetUserNotificationsAsync(Guid userId)
        {
            UserNotificationByUserIdSpec specification = new(userId);
            var notifications = await _efContext.UserNotifications.Where(specification).ToListAsync();

            return _mapper.Map<IEnumerable<UserNotificationDto>>(notifications);
        }

        public async Task RemoveUserNotifications(Guid notificationId)
        {
            RemoveUserNotificationsCommand command = new(notificationId);

            await _mediatorHandler.SendCommandAsync(command);
        }
    }
}
