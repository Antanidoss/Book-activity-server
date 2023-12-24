using AutoMapper;
using BookActivity.Application.Interfaces.Services;
using BookActivity.Application.Models.Dto.Read;
using BookActivity.Domain.Commands.UserNotificationCommands.RemoveUserNotifications;
using BookActivity.Domain.Filters.Models;
using BookActivity.Domain.Interfaces;
using BookActivity.Domain.Interfaces.Repositories;
using BookActivity.Domain.Models;
using BookActivity.Domain.Specifications.UserNotificationSpecs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookActivity.Application.Implementation.Services
{
    internal sealed class UserNotificationService : IUserNotificationService
    {
        private readonly IUserNotificationRepository _userNotificationRepository;

        private readonly IMapper _mapper;

        private readonly IMediatorHandler _mediatorHandler;

        public UserNotificationService(IUserNotificationRepository userNotificationRepository, IMapper mapper, IMediatorHandler mediatorHandler)
        {
            _userNotificationRepository = userNotificationRepository;
            _mapper = mapper;
            _mediatorHandler = mediatorHandler;
        }

        public async Task<IEnumerable<UserNotificationDto>> GetUserNotificationsAsync(Guid userId)
        {
            UserNotificationByUserIdSpec specification = new(userId);
            DbMultipleResultFilterModel<UserNotification> filterModel = new(specification);
            var notifications = (await _userNotificationRepository.GetByFilterAsync(filterModel)).ToList();

            return _mapper.Map<IEnumerable<UserNotificationDto>>(notifications);
        }

        public async Task RemoveUserNotifications(Guid notificationId)
        {
            RemoveUserNotificationsCommand command = new(notificationId);

            await _mediatorHandler.SendCommandAsync(command);
        }
    }
}
