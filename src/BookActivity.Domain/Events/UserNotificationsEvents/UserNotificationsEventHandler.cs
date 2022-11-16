using BookActivity.Domain.Events.ActiveBookEvent;
using BookActivity.Domain.Interfaces.Repositories;
using BookActivity.Domain.Models;
using BookActivity.Domain.Specifications.BookSpecs;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace BookActivity.Domain.Events.UserNotificationsEvents
{
    internal sealed class UserNotificationsEventHandler : INotificationHandler<AddActiveBookEvent>
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
            BookByIdSpec bookByIdSpec = new(notification.BookId);
            var book = await _bookRepository.GetBySpecAsync(bookByIdSpec);
            var user = await _userManager.FindByIdAsync(notification.UserId.ToString());

            foreach (var followedUser in user.FollowedUsers)
            {
                followedUser.UserNotifications.Add(new UserNotification($"Пользователь {followedUser.UserName} сделал книгу \"{book.Title}\" активной", followedUser.Id, true));

                await _userManager.UpdateAsync(followedUser);
            }
        }
    }
}
