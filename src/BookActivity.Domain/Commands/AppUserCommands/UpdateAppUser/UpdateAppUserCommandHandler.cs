using BookActivity.Domain.Extensions;
using BookActivity.Domain.Interfaces.Repositories;
using BookActivity.Domain.Specifications.AppUserSpecs;
using FluentValidation.Results;
using MediatR;
using NetDevPack.Messaging;
using System.Threading;
using System.Threading.Tasks;

namespace BookActivity.Domain.Commands.AppUserCommands.UpdateAppUser
{
    internal sealed class UpdateAppUserCommandHandler : CommandHandler,
        IRequestHandler<UpdateAppUserCommand, ValidationResult>
    {
        private readonly IAppUserRepository _appUserRepository;

        public UpdateAppUserCommandHandler(IAppUserRepository appUserRepository)
        {
            _appUserRepository = appUserRepository;
        }

        public async Task<ValidationResult> Handle(UpdateAppUserCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
                return request.ValidationResult;

            AppUserByIdSpec specification = new(request.AppUserId);
            var user = await _appUserRepository.GetBySpecAsync(specification, forAccountOperation: true).ConfigureAwait(false);

            user.AvatarImage = request.AvatarImage;
            user.UserName = request.Name;

            return (await _appUserRepository.UpdateAsync(user)).ToValidationResult();
        }
    }
}
