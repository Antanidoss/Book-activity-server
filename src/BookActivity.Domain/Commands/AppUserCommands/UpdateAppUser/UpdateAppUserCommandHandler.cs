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
            FirstOrDefault<AppUser> filter = new(specification);
            var user = _appUserRepository.GetByFilterAsync(filter);

            user.AvatarImage = request.AvatarImage;

            return (await _appUserRepository.UpdateAsync(user)).ToValidationResult();
        }
    }
}
