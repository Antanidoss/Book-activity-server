using BookActivity.Domain.Filters.Models;
using BookActivity.Domain.Filters.Specifications.AppUserSpecs;
using BookActivity.Domain.Interfaces.Repositories;
using BookActivity.Domain.Models;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Identity;
using NetDevPack.Messaging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BookActivity.Domain.Commands.AppUserCommands
{
    internal sealed class AppUserCommandHandler : CommandHandler,
        IRequestHandler<AddAppUserCommand, ValidationResult>,
        IRequestHandler<SubscribeAppUserCommand, ValidationResult>
    {
        private readonly IAppUserRepository _appUserRepository;

        public AppUserCommandHandler(IAppUserRepository appUserRepository)
        {
            _appUserRepository = appUserRepository;
        }

        public async Task<ValidationResult> Handle(AddAppUserCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return request.ValidationResult;

            var createUserResult = await _appUserRepository.Addasync(new AppUser()
            {
                Email = request.Email,
                UserName = request.Name 
            }, request.Password);

            return ToValidationResult(createUserResult);
        }

        public async Task<ValidationResult> Handle(SubscribeAppUserCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return request.ValidationResult;

            var filterModel = new AppUserFilterModel(new AppUserByIdSpec(request.AppUserId));
            var currentUser = (await _appUserRepository.GetByFilterAsync(filterModel)).FirstOrDefault();

            filterModel = new AppUserFilterModel(new AppUserByIdSpec(request.SubscribedUserId));
            var subscribedUser = (await _appUserRepository.GetByFilterAsync(filterModel)).FirstOrDefault();

            subscribedUser.FollowedUsers.Add(currentUser);
            var updateUserResult = await _appUserRepository.Updateasync(currentUser);

            return ToValidationResult(updateUserResult);
        }

        private ValidationResult ToValidationResult(IdentityResult identityResult)
        {
            return identityResult.Succeeded
                ? new ValidationResult()
                : new ValidationResult(identityResult.Errors.Select(e => new ValidationFailure(string.Empty, e.Description)));
        }
    }
}
