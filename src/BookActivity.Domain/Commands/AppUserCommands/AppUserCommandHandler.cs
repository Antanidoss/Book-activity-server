using BookActivity.Domain.Interfaces.Repositories;
using BookActivity.Domain.Models;
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
        IRequestHandler<AddAppUserCommand, ValidationResult>
    {
        private readonly IAppUserRepository _appUserRepository;

        public AppUserCommandHandler(IAppUserRepository appUserRepository)
        {
            _appUserRepository = appUserRepository;
        }

        public async Task<ValidationResult> Handle(AddAppUserCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return request.ValidationResult;

            var createUserResult = await _appUserRepository.Addasync(new AppUser() { Email = request.Email, UserName = request.Name }, request.Password);

            return ToValidationResult(createUserResult);
        }

        private ValidationResult ToValidationResult(IdentityResult identityResult)
        {
            return identityResult.Succeeded
                ? new ValidationResult()
                : new ValidationResult(identityResult.Errors.Select(e => new ValidationFailure(string.Empty, e.Description)));
        }
    }
}
