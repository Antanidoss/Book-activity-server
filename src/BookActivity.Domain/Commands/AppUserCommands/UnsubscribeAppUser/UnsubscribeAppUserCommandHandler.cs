using BookActivity.Domain.Interfaces;
using BookActivity.Domain.Models;
using FluentValidation.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BookActivity.Domain.Commands.AppUserCommands.UnsubscribeAppUser
{
    internal sealed class UnsubscribeAppUserCommandHandler : CommandHandler,
        IRequestHandler<UnsubscribeAppUserCommand, ValidationResult>
    {
        private readonly IDbContext _efContext;

        public UnsubscribeAppUserCommandHandler(IDbContext efContext)
        {
            _efContext = efContext;
        }

        public async Task<ValidationResult> Handle(UnsubscribeAppUserCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
                return request.ValidationResult;

            _efContext.Subscribers.Remove(new Subscriber
            {
                UserIdWhoSubscribed = request.UserIdWhoUnsubscribed,
                SubscribedUserId = request.UnsubscribedUserId
            });

            _efContext.Subscriptions.Remove(new Subscription
            {
                UserIdWhoSubscribed = request.UserIdWhoUnsubscribed,
                SubscribedUserId = request.UnsubscribedUserId
            });

            return await Commit(_efContext);
        }
    }
}
