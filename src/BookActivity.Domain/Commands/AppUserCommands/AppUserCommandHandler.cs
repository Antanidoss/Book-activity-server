using Antanidoss.Specification.Filters.Implementation;
using BookActivity.Domain.Interfaces.Repositories;
using BookActivity.Domain.Models;
using BookActivity.Domain.Specifications.AppUserSpecs;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Identity;
using NetDevPack.Messaging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BookActivity.Domain.Commands.AppUserCommands
{
    internal sealed class AppUserCommandHandler : CommandHandler,
        IRequestHandler<AddAppUserCommand, ValidationResult>,
        IRequestHandler<SubscribeAppUserCommand, ValidationResult>,
        IRequestHandler<UpdateAppUserCommand, ValidationResult>
    {
        private readonly IAppUserRepository _appUserRepository;

        public AppUserCommandHandler(IAppUserRepository appUserRepository)
        {
            _appUserRepository = appUserRepository;
        }

        public async Task<ValidationResult> Handle(AddAppUserCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
                return request.ValidationResult;

            var createUserResult = await _appUserRepository.Addasync(new AppUser()
            {
                Email = request.Email,
                UserName = request.Name
            }, request.Password).ConfigureAwait(false);

            return ToValidationResult(createUserResult);
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
            var updateUserResult = await _appUserRepository.UpdateAsync(currentUser).ConfigureAwait(false);

            return ToValidationResult(updateUserResult);
        }

        public async Task<ValidationResult> Handle(UpdateAppUserCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
                return request.ValidationResult;

            AppUserByIdSpec specification = new(request.AppUserId);
            FirstOrDefault<AppUser> filter = new(specification);
            var user = _appUserRepository.GetByFilterAsync(filter);

            user.AvatarImage = request.AvatarImage;

            return ToValidationResult(await _appUserRepository.UpdateAsync(user));
        }

        private ValidationResult ToValidationResult(IdentityResult identityResult)
        {
            return identityResult.Succeeded
                ? new ValidationResult()
                : new ValidationResult(identityResult.Errors.Select(e => new ValidationFailure(string.Empty, e.Description)));
        }
    }
}
