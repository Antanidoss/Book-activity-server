using BookActivity.Domain.Extensions;
using BookActivity.Domain.Models;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace BookActivity.Domain.Commands.AppUserCommands.AddAppUser
{
    internal sealed class AddAppUserCommandHandler : CommandHandler,
        IRequestHandler<AddAppUserCommand, ValidationResult>
    {
        private readonly UserManager<AppUser> _userManager;

        public AddAppUserCommandHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ValidationResult> Handle(AddAppUserCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
                return request.ValidationResult;

            var createUserResult = await _userManager.CreateAsync(new AppUser(request.Name, request.AvatarImage, request.Email), request.Password);

            return createUserResult.ToValidationResult();
        }
    }
}
