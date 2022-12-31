using BookActivity.Domain.Events.ActiveBookEvent;
using BookActivity.Domain.Interfaces.Hubs;
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

        private readonly IUserNotificationsHub _userNotificationsHub;

        public UserNotificationsEventHandler(UserManager<AppUser> userManager, IBookRepository bookRepository, IUserNotificationsHub userNotificationsHub)
        {
            _userManager = userManager;
            _bookRepository = bookRepository;
            _userNotificationsHub = userNotificationsHub;
        }

        public async Task Handle(AddActiveBookEvent notification, CancellationToken cancellationToken)
        {
            BookByIdSpec bookByIdSpec = new(notification.BookId);
            var book = await _bookRepository.GetBySpecAsync(bookByIdSpec);
            var user = await _userManager.FindByIdAsync(notification.UserId.ToString());

            string notificationMessage = $"{user.UserName} has made the book \"{book.Title}\" active";

            foreach (var followedUser in user.Subscriptions)
            {
                followedUser.UserNotifications.Add(new UserNotification(notificationMessage, followedUser.Id, true));

                await _userManager.UpdateAsync(followedUser);
                await _userNotificationsHub.Send(new UserNotificationModel(
                    notification.UserId,
                    followedUser.Id,
                    notificationMessage
                ));
            }
        }
    }
}
