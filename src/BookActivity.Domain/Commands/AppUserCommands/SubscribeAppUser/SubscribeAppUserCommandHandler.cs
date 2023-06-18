using BookActivity.Domain.Events.AppUserEvents;
using BookActivity.Domain.Interfaces.Repositories;
using BookActivity.Domain.Models;
using BookActivity.Domain.Specifications.AppUserSpecs;
using FluentValidation.Results;
using MediatR;
using NetDevPack.Messaging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BookActivity.Domain.Commands.AppUserCommands.SubscribeAppUser
{
    internal sealed class SubscribeAppUserCommandHandler : CommandHandler,
        IRequestHandler<SubscribeAppUserCommand, ValidationResult>
    {
        private readonly IAppUserRepository _appUserRepository;

        private readonly ISubscriberRepository _subscriberRepository;

        private readonly ISubscriptionRepository _subscriptionRepository;

        public SubscribeAppUserCommandHandler(
            IAppUserRepository appUserRepository,
            ISubscriberRepository subscriberRepository,
            ISubscriptionRepository subscriptionRepository)
        {
            _appUserRepository = appUserRepository;
            _subscriberRepository = subscriberRepository;
            _subscriptionRepository = subscriptionRepository;
        }

        public async Task<ValidationResult> Handle(SubscribeAppUserCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
                return request.ValidationResult;

            AppUserByIdSpec subscribedUserSpec = new(request.SubscribedUserId);
            if (!(await _appUserRepository.CheckExistBySpecAsync(subscribedUserSpec)))
                throw new Exception($"Failed to find user by id = {request.SubscribedUserId}");

            AppUserByIdSpec userWhoSubscribedSpec = new(request.UserIdWhoSubscribed);
            if (!(await _appUserRepository.CheckExistBySpecAsync(userWhoSubscribedSpec)))
                throw new Exception($"Failed to find user by id = {request.UserIdWhoSubscribed}");

            Subscriber subscriber = new()
            {
                UserIdWhoSubscribed = request.UserIdWhoSubscribed,
                SubscribedUserId = request.SubscribedUserId
            };

            subscriber.AddDomainEvent(new SubscribeAppUserEvent
            {
                SubscribedUserId = request.SubscribedUserId,
                UserNameWhoSubscribed = await _appUserRepository.GetByFilterAsync<string>(GetFilter(userWhoSubscribedSpec)),
            });

            _subscriberRepository.Add(subscriber);
            _subscriptionRepository.Add(new Subscription
            {
                UserIdWhoSubscribed = request.UserIdWhoSubscribed,
                SubscribedUserId = request.SubscribedUserId
            });

            return await Commit(_subscriberRepository.UnitOfWork);
        }

        private Func<IQueryable<AppUser>, string> GetFilter(AppUserByIdSpec userWhoSubscribedSpec)
        {
            return query => query.Where(userWhoSubscribedSpec).Select(u => u.UserName).First();
        }
    }
}
