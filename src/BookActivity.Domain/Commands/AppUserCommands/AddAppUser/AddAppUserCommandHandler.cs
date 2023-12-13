using BookActivity.Domain.Extensions;
using BookActivity.Domain.Interfaces.Repositories;
using BookActivity.Domain.Models;
using FluentValidation.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BookActivity.Domain.Commands.AppUserCommands.AddAppUser
{
    internal sealed class AddAppUserCommandHandler : CommandHandler,
        IRequestHandler<AddAppUserCommand, ValidationResult>
    {
        private readonly IAppUserRepository _appUserRepository;

        public AddAppUserCommandHandler(IAppUserRepository appUserRepository)
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
                UserName = request.Name,
                AvatarImage = request.AvatarImage
            }, request.Password, cancellationToken);

            return createUserResult.ToValidationResult();
        }
    }
}
