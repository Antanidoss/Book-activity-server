﻿using BookActivity.Domain.Events.ActiveBookEvent;
using BookActivity.Domain.Interfaces.Repositories;
using BookActivity.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace BookActivity.Domain.Events.UserNotificationsEvents
{
    public class UserNotificationsEventHandler :
        INotificationHandler<AddActiveBookEvent>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IBookRepository _bookRepository;

        public UserNotificationsEventHandler(UserManager<AppUser> userManager, IBookRepository bookRepository)
        {
            _userManager = userManager;
            _bookRepository = bookRepository;
        }

        public async Task Handle(AddActiveBookEvent notification, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(notification.UserId.ToString());
            var book = await _bookRepository.GetByAsync(b => b.Id == notification.BookId);

            foreach (var followedUser in user.FollowedUsers)
            {
                followedUser.UserNotifications.Add(new UserNotification($"Пользователь {followedUser.UserName} сделал книгу \"{book.Title}\" активной", followedUser.Id, notification.IsPublic));

                await _userManager.UpdateAsync(followedUser);
            }
        }
    }
}