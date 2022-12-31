using AutoMapper;
using BookActivity.Application.Interfaces.Services;
using BookActivity.Application.Models.Dto.Read;
using BookActivity.Domain.Interfaces.Repositories;
using BookActivity.Domain.Specifications.UserNotificationSpecs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookActivity.Application.Implementation.Services
{
    internal sealed class UserNotificationService : IUserNotificationService
    {
        private readonly IUserNotificationRepository _userNotificationRepository;

        private readonly IMapper _mapper;

        public UserNotificationService(IUserNotificationRepository userNotificationRepository, IMapper mapper)
        {
            _userNotificationRepository = userNotificationRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserNotificationDto>> GetUserNotificationsAsync(Guid userId)
        {
            UserNotificationByUserIdSpec specification = new(userId);
            var notifications = await _userNotificationRepository.GetBySpecAsync(specification);

            return _mapper.Map<IEnumerable<UserNotificationDto>>(notifications);
        }
    }
}
