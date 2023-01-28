using BookActivity.Domain.Interfaces.Repositories;
using BookActivity.Domain.Models;
using BookActivity.Domain.Specifications.AppUserSpecs;
using FluentValidation.Results;
using MediatR;
using NetDevPack.Messaging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BookActivity.Domain.Commands.AppUserCommands.UnsubscribeAppUser
{
    internal sealed class UnsubscribeAppUserCommandHandler : CommandHandler,
        IRequestHandler<UnsubscribeAppUserCommand, ValidationResult>
    {
        private readonly IAppUserRepository _appUserRepository;

        private readonly ISubscriberRepository _subscriberRepository;

        private readonly ISubscriptionRepository _subscriptionRepository;

        public UnsubscribeAppUserCommandHandler(IAppUserRepository appUserRepository, ISubscriberRepository subscriberRepository, ISubscriptionRepository subscriptionRepository)
        {
            _appUserRepository = appUserRepository;
            _subscriberRepository = subscriberRepository;
            _subscriptionRepository = subscriptionRepository;
        }

        public async Task<ValidationResult> Handle(UnsubscribeAppUserCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
                return request.ValidationResult;

            AppUserByIdSpec unsubscribedUserSpec = new(request.UnsubscribedUserId);
            if (!(await _appUserRepository.CheckExistBySpecAsync(unsubscribedUserSpec)))
                throw new Exception();

            AppUserByIdSpec userWhoUnsubscribedSpec = new(request.UserIdWhoUnsubscribed);
            if (!(await _appUserRepository.CheckExistBySpecAsync(userWhoUnsubscribedSpec)))
                throw new Exception();

            _subscriberRepository.Remove(new Subscriber { UserIdWhoSubscribed = request.UserIdWhoUnsubscribed, SubscribedUserId = request.UnsubscribedUserId });
            _subscriptionRepository.Remove(new Subscription { UserIdWhoSubscribed = request.UserIdWhoUnsubscribed, SubscribedUserId = request.UnsubscribedUserId });

            return await Commit(_appUserRepository.UnitOfWork);
        }
    }
}
