using BookActivity.Domain.Interfaces.Repositories;
using BookActivity.Domain.Models;
using BookActivity.Domain.Specifications.AppUserSpecs;
using FluentValidation.Results;
using MediatR;
using NetDevPack.Messaging;
using System;
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

        public SubscribeAppUserCommandHandler(IAppUserRepository appUserRepository, ISubscriberRepository subscriberRepository, ISubscriptionRepository subscriptionRepository)
        {
            _appUserRepository = appUserRepository;
            _subscriberRepository = subscriberRepository;
            _subscriptionRepository = subscriptionRepository;
        }

        public async Task<ValidationResult> Handle(SubscribeAppUserCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
                return request.ValidationResult;

            AppUserByIdSpec specification = new(request.SubscribedUserId);
            if (!(await _appUserRepository.CheckExistBySpecAsync(specification)))
                throw new Exception();

            specification = new(request.UserIdWhoSubscribed);
            if (!(await _appUserRepository.CheckExistBySpecAsync(specification)))
                throw new Exception();

            _subscriptionRepository.Add(new Subscription { UserIdWhoSubscribed = request.UserIdWhoSubscribed, SubscribedUserId = request.SubscribedUserId });
            _subscriberRepository.Add(new Subscriber { UserIdWhoSubscribed = request.SubscribedUserId, SubscribedUserId = request.UserIdWhoSubscribed });

            return await Commit(_subscriberRepository.UnitOfWork);
        }
    }
}
