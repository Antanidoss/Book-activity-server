﻿using BookActivity.Domain.Interfaces;
using BookActivity.Domain.Models;
using FluentValidation.Results;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BookActivity.Domain.Commands.UserNotificationCommands.RemoveUserNotifications
{
    internal class RemoveUserNotificationsCommandHandler : CommandHandler,
        IRequestHandler<RemoveUserNotificationsCommand, ValidationResult>
    {
        private readonly IDbContext _efContext;

        public RemoveUserNotificationsCommandHandler(IDbContext efContext)
        {
            _efContext = efContext;
        }

        public async Task<ValidationResult> Handle(RemoveUserNotificationsCommand request, CancellationToken cancellationToken)
        {
            var userNotifications = request.UserNotificationIds.Select(n => new Notification(n));
            _efContext.Notifications.RemoveRange(userNotifications);

            return await Commit(_efContext);
        }
    }
}
