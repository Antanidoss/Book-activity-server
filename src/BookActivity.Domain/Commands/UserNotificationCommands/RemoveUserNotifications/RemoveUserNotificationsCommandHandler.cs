using BookActivity.Domain.Interfaces.Repositories;
using BookActivity.Domain.Specifications.UserNotificationSpecs;
using FluentValidation.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BookActivity.Domain.Commands.UserNotificationCommands.RemoveUserNotifications
{
    internal class RemoveUserNotificationsCommandHandler : CommandHandler,
        IRequestHandler<RemoveUserNotificationsCommand, ValidationResult>
    {
        private readonly IUserNotificationRepository _userNotificationRepository;

        public RemoveUserNotificationsCommandHandler(IUserNotificationRepository userNotificationRepository)
        {
            _userNotificationRepository = userNotificationRepository;
        }

        public async Task<ValidationResult> Handle(RemoveUserNotificationsCommand request, CancellationToken cancellationToken)
        {
            UserNotificationByIdsSpec specification = new(request.UserNotificationIds);

            _userNotificationRepository.RemoveRange(specification);

            return await Commit(_userNotificationRepository.UnitOfWork);
        }
    }
}
