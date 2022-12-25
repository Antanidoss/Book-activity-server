using BookActivity.Domain.Extensions;
using BookActivity.Domain.Interfaces.Repositories;
using BookActivity.Domain.Models;
using BookActivity.Domain.Specifications.AppUserSpecs;
using FluentValidation.Results;
using MediatR;
using NetDevPack.Messaging;
using System.Threading;
using System.Threading.Tasks;

namespace BookActivity.Domain.Commands.AppUserCommands.SubscribeAppUser
{
    internal sealed class SubscribeAppUserCommandHandler : CommandHandler,
        IRequestHandler<SubscribeAppUserCommand, ValidationResult>
    {
        private readonly IAppUserRepository _appUserRepository;

        public SubscribeAppUserCommandHandler(IAppUserRepository appUserRepository)
        {
            _appUserRepository = appUserRepository;
        }

        public async Task<ValidationResult> Handle(SubscribeAppUserCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
                return request.ValidationResult;

            AppUserByIdSpec specification = new(request.AppUserId);
            var currentUser = await _appUserRepository.GetBySpecAsync(specification, forAccountOperation: true, u => u.Subscriptions).ConfigureAwait(false);

            currentUser.Subscriptions.Add(new AppUser { Id = request.SubscribedUserId});

            return (await _appUserRepository.UpdateAsync(currentUser).ConfigureAwait(false)).ToValidationResult();
        }
    }
}
