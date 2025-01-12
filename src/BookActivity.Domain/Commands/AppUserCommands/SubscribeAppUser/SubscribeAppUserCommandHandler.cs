using BookActivity.Domain.Events.AppUserEvents;
using BookActivity.Domain.Interfaces;
using BookActivity.Domain.Models;
using BookActivity.Domain.Specifications.AppUserSpecs;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BookActivity.Domain.Commands.AppUserCommands.SubscribeAppUser
{
    internal sealed class SubscribeAppUserCommandHandler : CommandHandler,
        IRequestHandler<SubscribeAppUserCommand, ValidationResult>
    {
        private readonly IDbContext _efContext;

        public SubscribeAppUserCommandHandler(IDbContext efContext)
        {
            _efContext = efContext;
        }

        public async Task<ValidationResult> Handle(SubscribeAppUserCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
                return request.ValidationResult;

            Subscriber subscriber = new(request.UserIdWhoSubscribed, request.SubscribedUserId);

            AppUserByIdSpec specification = new(request.UserIdWhoSubscribed);
            var userName = await _efContext.Users.Where(specification).Select(u => u.UserName).FirstAsync();
            subscriber.AddDomainEvent(new SubscribeAppUserEvent(request.SubscribedUserId, request.UserIdWhoSubscribed, userName));

            await _efContext.Subscribers.AddAsync(subscriber, cancellationToken);
            await _efContext.Subscriptions.AddAsync(new Subscription(request.UserIdWhoSubscribed, request.SubscribedUserId), cancellationToken);

            return await Commit(_efContext);
        }
    }
}
