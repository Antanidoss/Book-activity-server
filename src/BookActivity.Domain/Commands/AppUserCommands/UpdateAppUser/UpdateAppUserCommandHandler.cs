using BookActivity.Domain.Extensions;
using BookActivity.Domain.Models;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace BookActivity.Domain.Commands.AppUserCommands.UpdateAppUser
{
    internal sealed class UpdateAppUserCommandHandler : CommandHandler,
        IRequestHandler<UpdateAppUserCommand, ValidationResult>
    {
        private readonly UserManager<AppUser> _userManager;

        public UpdateAppUserCommandHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ValidationResult> Handle(UpdateAppUserCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
                return request.ValidationResult;

            var user = await _userManager.Users.FirstAsync(u => u.Id == request.AppUserId);

            user.UserName = request.Name;
            user.Description = request.Description;

            var updateUserResult = await _userManager.UpdateAsync(user);

            return updateUserResult.ToValidationResult();
        }
    }
}
