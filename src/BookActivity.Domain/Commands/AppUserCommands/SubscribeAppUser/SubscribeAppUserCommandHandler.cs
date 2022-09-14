using Antanidoss.Specification.Filters.Implementation;
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
            FirstOrDefault<AppUser> filter = new(specification);
            var currentUser = _appUserRepository.GetByFilterAsync(filter);

            specification = new(request.SubscribedUserId);
            filter = new(specification);
            var subscribedUser = _appUserRepository.GetByFilterAsync(filter);

            subscribedUser.FollowedUsers.Add(currentUser);
            await _appUserRepository.UpdateAsync(currentUser).ConfigureAwait(false);

            return new ValidationResult();
        }
    }
}
